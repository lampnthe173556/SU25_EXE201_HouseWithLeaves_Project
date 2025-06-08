// Mobile Navigation Toggle
document.addEventListener('DOMContentLoaded', function () {


    const nav = document.querySelector('.main-nav');
    nav.insertBefore(mobileMenuButton, nav.firstChild);

    mobileMenuButton.addEventListener('click', function () {
        const navLinks = document.querySelector('.nav-links');
        navLinks.classList.toggle('show');
    });
});

// Product Image Zoom
document.querySelectorAll('.product-card img').forEach(img => {
    img.addEventListener('mouseover', function () {
        this.style.transform = 'scale(1.1)';
        this.style.transition = 'transform 0.3s ease';
    });

    img.addEventListener('mouseout', function () {
        this.style.transform = 'scale(1)';
    });
});

// Smooth Scroll
document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        document.querySelector(this.getAttribute('href')).scrollIntoView({
            behavior: 'smooth'
        });
    });
});

// Add to Cart Animation
document.querySelectorAll('.buy-button').forEach(button => {
    button.addEventListener('click', function () {
        const cart = document.querySelector('.fa-shopping-cart');
        cart.classList.add('bounce');
        setTimeout(() => {
            cart.classList.remove('bounce');
        }, 1000);
    });
});

// Sticky Header
window.addEventListener('scroll', function () {
    const header = document.querySelector('header');
    if (window.scrollY > 100) {
        header.classList.add('sticky');
    } else {
        header.classList.remove('sticky');
    }
});

// Product Filter
function filterProducts(category) {
    const products = document.querySelectorAll('.product-card');
    products.forEach(product => {
        if (category === 'all' || product.dataset.category === category) {
            product.style.display = '';
        } else {
            product.style.display = 'none';
        }
    });
}

// Add filter button click handlers
document.addEventListener('DOMContentLoaded', function () {
    const filterButtons = document.querySelectorAll('.filter button');
    filterButtons.forEach(button => {
        button.addEventListener('click', function () {
            // Remove active class from all buttons
            filterButtons.forEach(btn => btn.classList.remove('active'));
            // Add active class to clicked button
            this.classList.add('active');
            // Get category from button text
            let category = 'all';
            if (this.textContent === 'Cây để bàn') {
                category = 'desk';
            } else if (this.textContent === 'Terrarium') {
                category = 'terrarium';
            } else if (this.textContent === 'Cây để sàn') {
                category = 'floor';
            }
            console.log('Filter click:', category); // DEBUG LOG
            // Filter products
            filterProducts(category);
        });
    });
});

// --- CART LOGIC (dùng chung với shop) ---
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
        cart.push({ ...product, qty: 1 });
    }
    saveCart();
    updateCartBadge();
    showNotification('Đã thêm sản phẩm vào giỏ hàng!');
}
function removeFromCart(name) {
    cart = cart.filter(item => item.name !== name);
    saveCart();
    updateCartBadge();
    renderCartPopup();
}
function renderCartPopup() {
    let popup = document.getElementById('cartPopup');
    if (popup) popup.remove(); // Xóa popup cũ nếu có
    popup = document.createElement('div');
    popup.id = 'cartPopup';
    document.body.appendChild(popup);

    popup.innerHTML = `<div class="cart-popup-content">
    <h3>Giỏ hàng</h3>
    <button class="close-cart-popup">×</button>
    <div class="cart-list">
      ${cart.length === 0 ? '<div>Chưa có sản phẩm nào.</div>' : cart.map(item => `
        <div class="cart-item">
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
    <button class="checkout-btn">Thanh toán</button>
  </div>`;
    popup.style.display = 'flex';

    document.querySelector('.close-cart-popup').onclick = () => {
        document.getElementById('cartPopup').remove();
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
    document.querySelectorAll('.qty-input').forEach(input => {
        input.onchange = (e) => {
            const name = input.dataset.name;
            const idx = cart.findIndex(item => item.name === name);
            let val = parseInt(input.value, 10);
            if (idx > -1 && val > 0) {
                cart[idx].qty = val;
                saveCart();
                renderCartPopup();
            }
        };
    });
}
const cartIcon = document.getElementById('cartIcon');
cartIcon.onclick = function (e) {
    e.preventDefault();
    let popup = document.getElementById('cartPopup');
    if (popup) {
        popup.remove(); // Nếu đang mở thì tắt
    } else {
        renderCartPopup(); // Nếu đang tắt thì mở
    }
};
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
updateCartBadge();

function showNotification(message) {
    const notification = document.createElement('div');
    notification.classList.add('notification');
    notification.textContent = message;
    document.body.appendChild(notification);

    setTimeout(() => {
        notification.remove();
    }, 3000);
}
