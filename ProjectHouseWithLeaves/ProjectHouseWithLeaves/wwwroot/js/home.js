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
// (Chỉ hiệu ứng, không thao tác dữ liệu)
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
    // Gắn sự kiện addToCart cho icon
    if (hoverIcon) {
        hoverIcon.onclick = (e) => {
            e.stopPropagation();
            const name = card.querySelector('.name').textContent.trim();
            const priceText = card.querySelector('.price').textContent.trim().replace(/[^\d]/g, '');
            const price = parseInt(priceText, 10) || 0;
            const img = card.querySelector('img') ? card.querySelector('img').getAttribute('src') : '';
            const productId = card.querySelector('.name').getAttribute('href')?.split('/').pop();
            if (typeof addToCart === 'function') {
                addToCart({ productId, name, price, img });
            } else if (window.addToCart) {
                window.addToCart({ productId, name, price, img });
            } else {
                alert('Không tìm thấy chức năng thêm vào giỏ hàng!');
            }
        };
    }
});
