// --- CART LOGIC CHUNG ---
let cart = JSON.parse(localStorage.getItem('cart')) || [];

function updateCartBadge() {
    const badge = document.getElementById('cartCount');
    if (badge) {
        badge.textContent = cart.length;
        badge.style.display = cart.length > 0 ? 'inline-block' : 'none';
    }
}
function saveCart() {
    localStorage.setItem('cart', JSON.stringify(cart));
}
function addToCart(product) {
    const idx = cart.findIndex(item => item.name === product.name);
    if (idx > -1) {
        cart[idx].qty++;
    } else {
        cart.push({ ...product, qty: 1, img: product.img || product.image });
    }
    saveCart();
    updateCartBadge();
    if (typeof showNotification === 'function') showNotification('Đã thêm sản phẩm vào giỏ hàng!');
    if (document.getElementById('cartPopup')) renderCartPopup();
}
function removeFromCart(name) {
    cart = cart.filter(item => item.name !== name);
    saveCart();
    updateCartBadge();
    if (document.getElementById('cartPopup')) renderCartPopup();
}
function removeAllFromCart() {
    cart = [];
    saveCart();
    updateCartBadge();
    renderCartPopup();
}
function renderCartPopup() {
    let popup = document.getElementById('cartPopup');
    if (popup) popup.remove();
    popup = document.createElement('div');
    popup.id = 'cartPopup';
    document.body.appendChild(popup);
    popup.innerHTML = `<div class="cart-popup-content">
    <h3>Giỏ hàng</h3>
    <button class="close-cart-popup">×</button>
    <button class="remove-all-cart" style="position:absolute;top:18px;right:60px;background:#e74c3c;color:#fff;border:none;border-radius:6px;padding:6px 18px;font-size:1rem;cursor:pointer;">Xóa tất cả</button>
    <div class="cart-list">
      ${cart.length === 0 ? '<div>Chưa có sản phẩm nào.</div>' : cart.map(item => `
        <div class="cart-item">
          <input type="checkbox" class="cart-item-checkbox" data-name="${item.name}">
          <div class="cart-item-img-wrap"><img src="${item.img || ''}" alt="${item.name}" class="cart-item-img"></div>
          <div class="cart-item-info">
            <div class="cart-item-name">${item.name}</div>
            <div class="cart-item-meta">${item.meta || ''}</div>
          </div>
          <div class="cart-item-qty">
            <button class="qty-btn" data-name="${item.name}" data-action="decrease">-</button>
            <input type="number" min="1" value="${item.qty}" class="qty-input" data-name="${item.name}">
            <button class="qty-btn" data-name="${item.name}" data-action="increase">+</button>
          </div>
          <div class="cart-item-price">${item.price.toLocaleString()}₫</div>
          <button class="remove-cart-item" data-name="${item.name}">Remove</button>
        </div>
      `).join('')}
    </div>
    <div class="cart-total">Tổng: <b>${cart.reduce((sum, i) => sum + i.price * i.qty, 0).toLocaleString()}₫</b></div>
    <div style="margin: 10px 0;">
      <label for="payment-method" style="font-weight: 500;">Phương thức thanh toán:</label>
      <select id="payment-method" style="width: 100%; padding: 6px; border: 1px solid #ccc; border-radius: 4px; margin-top: 4px; font-family: 'Quicksand', sans-serif;">
        <option value="cash">Tiền mặt</option>
        <option value="bank">Thẻ tín dụng</option>
        <option value="vnpay">VNPay</option>
      </select>
    </div>
    <button class="checkout-btn">Thanh toán</button>
  </div>`;
    popup.style.display = 'flex';
    document.querySelector('.close-cart-popup').onclick = () => {
        document.getElementById('cartPopup').remove();
    };
    document.querySelector('.remove-all-cart').onclick = function () {
        cart = [];
        saveCart();
        updateCartBadge();
        renderCartPopup();
    };
    document.querySelectorAll('.remove-cart-item').forEach(btn => {
        btn.onclick = () => removeFromCart(btn.dataset.name);
    });
    document.querySelectorAll('.qty-btn').forEach(btn => {
        btn.onclick = (e) => {
            const name = btn.dataset.name;
            const action = btn.dataset.action;
            const idx = cart.findIndex(item => item.name === name);
            if (idx > -1) {
                if (action === 'increase') cart[idx].qty++;
                if (action === 'decrease' && cart[idx].qty > 1) cart[idx].qty--;
                saveCart();
                renderCartPopup();
            }
        };
    });

    // Checkbox logic: enable checkout only if at least one is checked
    const checkoutBtn = document.querySelector('.checkout-btn');
    checkoutBtn.disabled = true;
    checkoutBtn.style.opacity = 0.6;
    const checkboxes = document.querySelectorAll('.cart-item-checkbox');
    // Đọc trạng thái đã lưu và set lại cho checkbox
    const checkedNames = JSON.parse(localStorage.getItem('cart_checked') || '[]');
    checkboxes.forEach(cb => {
        if (checkedNames.includes(cb.dataset.name)) cb.checked = true;
    });
    updateCheckoutButtonState(); // Đảm bảo nút Thanh toán đúng trạng thái khi mở lại popup
    checkboxes.forEach(cb => {
        cb.onchange = updateCheckoutButtonState;
    });
    function updateCheckoutButtonState() {
        const anyChecked = Array.from(document.querySelectorAll('.cart-item-checkbox')).some(cb => cb.checked);
        checkoutBtn.disabled = !anyChecked;
        checkoutBtn.style.opacity = anyChecked ? 1 : 0.6;
        // Lưu trạng thái checkbox vào localStorage
        const checkedNames = Array.from(document.querySelectorAll('.cart-item-checkbox'))
            .filter(cb => cb.checked)
            .map(cb => cb.dataset.name);
        localStorage.setItem('cart_checked', JSON.stringify(checkedNames));
    }

    // Khi bấm thanh toán chỉ lấy sản phẩm đã tích
    checkoutBtn.onclick = function () {
        const checkedNames = Array.from(document.querySelectorAll('.cart-item-checkbox'))
            .filter(cb => cb.checked)
            .map(cb => cb.dataset.name);
        const selectedCart = cart.filter(item => checkedNames.includes(item.name));
        if (selectedCart.length === 0) return;
        const paymentMethod = document.getElementById('payment-method').value;
        localStorage.setItem('checkout_cart', JSON.stringify(selectedCart));
        localStorage.setItem('checkout_method', paymentMethod);
        if (paymentMethod === 'bank') {
            window.location.href = '/Payment/Paypal';
        } else {
            window.location.href = '/Payment/PaymentCommon';
        }
    };
}
function setupCartIconToggle() {
    const cartIcon = document.getElementById('cartIcon');
    if (cartIcon) {
        cartIcon.onclick = function (e) {
            e.preventDefault();
            let popup = document.getElementById('cartPopup');
            if (popup) {
                popup.remove();
            } else {
                renderCartPopup();
            }
        };
    }
}
// Gọi hàm này sau khi DOM ready
updateCartBadge();
setupCartIconToggle();

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
