// --- CART LOGIC CHUNG ---
let cartItems = [];

async function getCartFromStorage() {
    const cartData = await cartService.getCart();
    // Lấy mảng cartItems từ response
    const items = cartData?.result?.cartItems || [];
    return items.map(item => ({
        productId: item.productId,
        name: item.productName,
        price: item.unitPrice ?? 0,
        img: item.imgUrl,
        qty: item.quantity
    }));
}

async function updateCartBadge() {
    cartItems = await getCartFromStorage();
    const badge = document.getElementById('cartCount');
    if (badge) {
        badge.textContent = cartItems.length;
        badge.style.display = cartItems.length > 0 ? 'inline-block' : 'none';
    }
}

async function addToCart(product) {
    if (!window.isAuthenticated) {
        alert('Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng!');
        window.location.href = '/Authen/Auth';
        return;
    }
    
    const result = await cartService.addItem(product.productId, 1);
    if (result) {
        await updateCartBadge();
        if (typeof showNotification === 'function') showNotification('Đã thêm sản phẩm vào giỏ hàng!');
        if (document.getElementById('cartPopup')) await renderCartPopup();
    }
}

async function removeFromCart(productId) {
    const result = await cartService.removeItem(productId);
    if (result) {
        await updateCartBadge();
        if (document.getElementById('cartPopup')) await renderCartPopup();
    }
}

async function removeAllFromCart() {
    const result = await cartService.clearCart();
    if (result) {
        await updateCartBadge();
        await renderCartPopup();
    }
}

async function renderCartPopup() {
    cartItems = await getCartFromStorage();
    let popup = document.getElementById('cartPopup');
    if (popup) popup.remove();
    popup = document.createElement('div');
    popup.id = 'cartPopup';
    document.body.appendChild(popup);
    
    popup.innerHTML = `<div class="cart-popup-content">
        <h3>Giỏ hàng</h3>
        <button class="close-cart-popup">×</button>
        ${
            cartItems.length > 0
            ? `
            <button class="remove-all-cart" style="position:absolute;top:18px;right:60px;background:#e74c3c;color:#fff;border:none;border-radius:6px;padding:6px 18px;font-size:1rem;cursor:pointer;">Xóa tất cả</button>
            <div class="cart-list">
                ${cartItems.map(item => `
                    <div class="cart-item">
                        <input type="checkbox" class="cart-item-checkbox" data-id="${item.productId}">
                        <div class="cart-item-img-wrap"><img src="${item.img || ''}" alt="${item.name}" class="cart-item-img"></div>
                        <div class="cart-item-info">
                            <div class="cart-item-name">${item.name}</div>
                            <div class="cart-item-meta">${item.meta || ''}</div>
                        </div>
                        <div class="cart-item-qty">
                            <button class="qty-btn" data-id="${item.productId}" data-action="decrease">-</button>
                            <input type="number" min="1" value="${item.qty}" class="qty-input" data-id="${item.productId}">
                            <button class="qty-btn" data-id="${item.productId}" data-action="increase">+</button>
                        </div>
                        <div class="cart-item-price">${item.price.toLocaleString()}₫</div>
                        <button class="remove-cart-item" data-id="${item.productId}">Remove</button>
                    </div>
                `).join('')}
            </div>
            <div class="cart-total">Tổng: <b>${cartItems.reduce((sum, i) => sum + i.price * i.qty, 0).toLocaleString()}₫</b></div>
            <div style="margin: 10px 0;">
                <label for="payment-method" style="font-weight: 500;">Phương thức thanh toán:</label>
                <select id="payment-method" style="width: 100%; padding: 6px; border: 1px solid #ccc; border-radius: 4px; margin-top: 4px; font-family: 'Quicksand', sans-serif;">
                    <option value="cash">Tiền mặt</option>
                    <option value="bank">Thẻ tín dụng</option>
                    <option value="vnpay">VNPay</option>
                </select>
            </div>
            <button class="checkout-btn">Thanh toán</button>
            `
            : `
            <div class="empty-cart-message" style="text-align: center; padding: 20px;">
                <i class="fa fa-shopping-cart" style="font-size: 50px; color: #ccc; margin-bottom: 10px;"></i>
                <p style="color: #666; font-size: 16px;">Chưa có sản phẩm nào.</p>
                <a href="/Shop" style="display: inline-block; margin-top: 10px; padding: 8px 15px; background: #4CAF50; color: white; text-decoration: none; border-radius: 4px;">Tiếp tục mua sắm</a>
            </div>
            `
        }
    </div>`;

    popup.style.display = 'flex';

    // Chỉ thêm event listeners nếu có sản phẩm trong giỏ hàng
    if (cartItems.length > 0) {
        document.querySelector('.remove-all-cart').onclick = async function () {
            await removeAllFromCart();
        };
        document.querySelectorAll('.remove-cart-item').forEach(btn => {
            btn.onclick = () => removeFromCart(btn.dataset.id);
        });
        document.querySelectorAll('.qty-btn').forEach(btn => {
            btn.onclick = async (e) => {
                const productId = btn.dataset.id;
                const action = btn.dataset.action;
                const item = cartItems.find(item => item.productId === productId);
                if (item) {
                    if (action === 'increase') {
                        const result = await cartService.addItem(productId, 1);
                        if (result) {
                            await renderCartPopup();
                        }
                    } else if (action === 'decrease' && item.qty > 1) {
                        const result = await cartService.decreaseQuantity(productId, 1);
                        if (result) {
                            await renderCartPopup();
                        }
                    }
                }
            };
        });

        // Checkbox logic
        const checkoutBtn = document.querySelector('.checkout-btn');
        checkoutBtn.disabled = true;
        checkoutBtn.style.opacity = 0.6;
        const checkboxes = document.querySelectorAll('.cart-item-checkbox');
        
        updateCheckoutButtonState();
        checkboxes.forEach(cb => {
            cb.onchange = updateCheckoutButtonState;
        });
        
        function updateCheckoutButtonState() {
            const anyChecked = Array.from(document.querySelectorAll('.cart-item-checkbox')).some(cb => cb.checked);
            checkoutBtn.disabled = !anyChecked;
            checkoutBtn.style.opacity = anyChecked ? 1 : 0.6;
        }

        // Handle checkout
        checkoutBtn.onclick = function () {
            const checkedIds = Array.from(document.querySelectorAll('.cart-item-checkbox'))
                .filter(cb => cb.checked)
                .map(cb => cb.dataset.id);
            const selectedCart = cartItems.filter(item => checkedIds.includes(item.productId));
            if (selectedCart.length === 0) return;
            const paymentMethod = document.getElementById('payment-method').value;
            if (paymentMethod === 'bank') {
                window.location.href = '/Payment/Paypal';
            } else {
                window.location.href = '/Payment/PaymentCommon';
            }
        };
    }

    // Luôn thêm nút đóng popup
    document.querySelector('.close-cart-popup').onclick = () => {
        document.getElementById('cartPopup').remove();
    };
}

function setupCartIconToggle() {
    const cartIcon = document.querySelector('.fa-shopping-cart, #cartIcon');
    if (!cartIcon) {
        console.error('Cart icon not found');
        return;
    }

    cartIcon.style.cursor = 'pointer';
    cartIcon.onclick = async function (e) {
        e.preventDefault();
        e.stopPropagation();
        console.log('Cart icon clicked');
        await renderCartPopup();
    };
}

// Initialize cart when DOM is ready
document.addEventListener('DOMContentLoaded', async function() {
    console.log('DOM loaded, initializing cart...');
    await updateCartBadge();
    setupCartIconToggle();
});

// --- Hiệu ứng hover và icon cart giống shop ---
document.querySelectorAll('.product-card').forEach(card => {
    // Bọc ảnh trong .product-img-wrap nếu chưa có
    let img = card.querySelector('img');
    let imgWrap = card.querySelector('.product-img-wrap');
    if (!imgWrap && img) {
        imgWrap = document.createElement('div');
        imgWrap.className = 'product-img-wrap';
        img.parentNode.insertBefore(imgWrap, img);
        imgWrap.appendChild(img);
    }
    // Thêm .product-hover nếu chưa có
    let hoverIcon = card.querySelector('.product-hover');
    if (!hoverIcon && imgWrap) {
        hoverIcon = document.createElement('div');
        hoverIcon.className = 'product-hover';
        hoverIcon.innerHTML = '<i class="fa fa-shopping-cart"></i>';
        imgWrap.appendChild(hoverIcon);
    }
    // Hover hiệu ứng
    card.onmouseenter = () => {
        if (img) {
            img.style.filter = 'brightness(0.7) grayscale(0.1)';
            img.style.opacity = '0.85';
        }
        if (hoverIcon) {
            hoverIcon.style.opacity = 1;
            hoverIcon.style.pointerEvents = 'auto';
        }
    };
    card.onmouseleave = () => {
        if (img) {
            img.style.filter = '';
            img.style.opacity = '1';
        }
        if (hoverIcon) {
            hoverIcon.style.opacity = 0;
            hoverIcon.style.pointerEvents = 'none';
        }
    };
    // Click icon cart để add
    if (hoverIcon) {
        hoverIcon.onclick = (e) => {
            e.stopPropagation();
            const name = card.querySelector('.name').textContent.trim();
            const priceText = card.querySelector('.price').textContent.trim().replace(/[^\d]/g, '');
            const price = parseInt(priceText, 10) || 0;
            const img = card.querySelector('img') ? card.querySelector('img').getAttribute('src') : '';
            // You can add more meta info if needed
            addToCart({ name, price, img });
        };
    }
});

document.addEventListener('DOMContentLoaded', function () {
    // ... các đoạn khác ...
    const filterButtons = document.querySelectorAll('.filter button');
    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            // ... như cũ ...
            console.log('Filter click:', category);
            filterProducts(category);
        });
    });
});
