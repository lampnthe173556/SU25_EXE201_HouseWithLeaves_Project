// Fetch product data from API
async function fetchProductData() {
    try {
        const response = await fetch(`https://localhost:7115/ProductDetail/GetJson/${window.productId}`);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const product = await response.json();
        console.log(product);
        
        return product;
    } catch (error) {
        console.error('Error fetching product data:', error);
        return null;
    }
}

function renderProductDetail(product) {
    document.getElementById('productTitle').textContent = product.productName;
    document.getElementById('productPrice').innerHTML = product.price.toLocaleString() + '₫';

    // Meta (nếu không có thì để trống)
    const meta = document.getElementById('productMeta');
    meta.innerHTML = [
        "<b>Mức độ dễ sống:</b> 9/10",
        "<b>Bảo hành:</b> 7 ngày ",
        "Sản phẩm sẽ được chụp ảnh <b>xác nhận</b> trước khi giao"
    ].map(m => `<li>${m}</li>`).join('');

    // Lấy mảng link ảnh
    const images = Array.isArray(product.productImages)
        ? product.productImages.map(img => img.imageUrl)
        : [];

    // Ảnh chính và thumbnails
    const mainImg = document.getElementById('mainProductImage');
    if (images.length > 0) {
        mainImg.src = images[0];
        mainImg.alt = product.productName;
    } else {
        mainImg.src = 'link_anh_mac_dinh.jpg'; // hoặc để trống ''
        mainImg.alt = 'No image';
    }

    const thumbs = document.getElementById('productThumbnails');
    if (images.length >= 3) {
        thumbs.innerHTML = images.map((img, i) => `<img src="${img}" class="${i === 0 ? 'active' : ''}" data-idx="${i}">`).join('');
        thumbs.querySelectorAll('img').forEach(img => {
            img.onclick = function () {
                mainImg.src = this.src;
                thumbs.querySelectorAll('img').forEach(t => t.classList.remove('active'));
                this.classList.add('active');
            };
        });
    } else {
        thumbs.style.display = 'none';
    }

    // Tab content
    document.getElementById('tabDesc').innerHTML = product.description || '';
    document.getElementById('tabFaq').innerHTML = ''; // Nếu không có FAQ thì để trống
}

// Tab switching
document.addEventListener('DOMContentLoaded', async function () {
    const product = await fetchProductData();
    if (product) {
        renderProductDetail(product);
    } else {
        console.error('Failed to load product data');
    }
    
    document.querySelectorAll('.tab-btn').forEach(btn => {
        btn.onclick = function () {
            document.querySelectorAll('.tab-btn').forEach(b => b.classList.remove('active'));
            this.classList.add('active');
            document.getElementById('tabDesc').style.display = this.dataset.tab === 'desc' ? '' : 'none';
            document.getElementById('tabFaq').style.display = this.dataset.tab === 'faq' ? '' : 'none';
        };
    });
    // Zoom (demo)
    document.querySelector('.zoom-btn').onclick = function () {
        window.open(document.getElementById('mainProductImage').src, '_blank');
    };

    // Thêm vào giỏ hàng khi bấm nút MUA HÀNG
    document.getElementById('addToCartBtn').onclick = async function () {
        if (!window.isAuthenticated) {
            alert('Bạn cần đăng nhập để thêm sản phẩm vào giỏ hàng!');
            window.location.href = '/Authen/Auth';
            return;
        }
        
        const qty = parseInt(document.getElementById('qtyInput').value, 10) || 1;
        const result = await cartService.addItem(window.productId, qty);
        if (result) {
            if (typeof updateCartBadge === 'function') await updateCartBadge();
            if (typeof showNotification === 'function') showNotification('Đã thêm sản phẩm vào giỏ hàng!');
        }
    };
});
