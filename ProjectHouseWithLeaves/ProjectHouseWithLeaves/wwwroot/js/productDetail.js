// Dữ liệu mẫu cho demo
const demoProduct = {
    name: "Terrarium – Tropical Forest 002",
    price: 299000,
    images: [
        "https://9xgarden.com/wp-content/uploads/2022/10/ami-bang-sing.jpg",
        "https://9xgarden.com/wp-content/uploads/2022/10/akira-mon-hong.jpg",
        "https://9xgarden.com/wp-content/uploads/2022/10/akira-trau-ba-brasil.jpg"
    ],
    meta: [
        "<b>Đường kính:</b> Size S (10 cm); Size M (13.5 cm); Size L (16 cm)",
        "<b>Mức độ dễ sống:</b> 9/10",
        "<b>Bảo hành:</b> 90 ngày (3 tháng)",
        "Sản phẩm sẽ được chụp ảnh <b>xác nhận</b> trước khi giao"
    ],
    sizes: ["size S", "size M", "size L"],
    desc: `<p>Terrarium là hệ sinh thái thu nhỏ trong bình thủy tinh, dễ chăm sóc, phù hợp trang trí bàn làm việc, phòng khách, quà tặng ý nghĩa.</p>`,
    faq: `<ul><li>Bảo hành cây 90 ngày</li><li>Hỗ trợ chăm sóc trọn đời</li></ul>`
};

function renderProductDetail(product) {
    document.getElementById('productTitle').textContent = product.name;
    document.getElementById('productPrice').innerHTML = product.price.toLocaleString() + '₫';
    // Meta
    const meta = document.getElementById('productMeta');
    meta.innerHTML = product.meta.map(m => `<li>${m}</li>`).join('');
    // Ảnh chính và thumbnails
    const mainImg = document.getElementById('mainProductImage');
    mainImg.src = product.images[0];
    mainImg.alt = product.name;
    const thumbs = document.getElementById('productThumbnails');
    thumbs.innerHTML = product.images.map((img, i) => `<img src="${img}" class="${i === 0 ? 'active' : ''}" data-idx="${i}">`).join('');
    thumbs.querySelectorAll('img').forEach(img => {
        img.onclick = function () {
            mainImg.src = this.src;
            thumbs.querySelectorAll('img').forEach(t => t.classList.remove('active'));
            this.classList.add('active');
        };
    });
    // Tab content
    document.getElementById('tabDesc').innerHTML = product.desc;
    document.getElementById('tabFaq').innerHTML = product.faq;
}

// Tab switching
document.addEventListener('DOMContentLoaded', function () {
    renderProductDetail(demoProduct);
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
    document.getElementById('addToCartBtn').onclick = function () {
        const qty = parseInt(document.getElementById('qtyInput').value, 10) || 1;
        const product = {
            name: demoProduct.name,
            price: demoProduct.price,
            img: demoProduct.images[0]
        };
        for (let i = 0; i < qty; i++) {
            addToCart(product);
        }
    };
});
