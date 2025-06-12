// Dữ liệu mẫu
let products = [];

async function fetchProducts() {
    try {
        const response = await fetch('https://localhost:7115/Shop/GetAllProductJson');
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const data = await response.json();
        products.length = 0;
        products.push(...data);
        console.log('Đã lấy dữ liệu thành công:', products);

        // Tìm min và max price từ data
        let minPriceValue = Math.min(...products.map(p => p.price));
        let maxPriceValue = Math.max(...products.map(p => p.price));

        // Gán lại cho input range
        const priceMin = document.getElementById('priceMin');
        const priceMax = document.getElementById('priceMax');
        priceMin.min = minPriceValue;
        priceMin.max = maxPriceValue;
        priceMin.value = minPriceValue;
        priceMax.min = minPriceValue;
        priceMax.max = maxPriceValue;
        priceMax.value = maxPriceValue;

        // Cập nhật giao diện slider nếu có hàm
        if (typeof updatePriceSliderUI === 'function') updatePriceSliderUI();

        filterProducts();
    } catch (error) {
        console.error('Lỗi khi lấy dữ liệu:', error);
    }
}

// Gọi API khi trang được tải
document.addEventListener('DOMContentLoaded', fetchProducts);

const PRODUCTS_PER_PAGE = 6;
let currentPage = 1;
let filteredProducts = products.slice();

// --- ADD BUTTON TO PRODUCT CARD ---
function renderProducts(list) {
    const productList = document.getElementById('productList');
    productList.innerHTML = '';
    if (list.length === 0) {
        productList.innerHTML = '<div>Không tìm thấy sản phẩm phù hợp.</div>';
    }
    list.forEach(product => {
        const imageUrl = product.imageUrls && product.imageUrls.length > 0 ? product.imageUrls[0] : 'link_anh_mac_dinh.png';
        const card = document.createElement('div');
        card.className = 'product-card';
        card.innerHTML = `
      <div class="product-img-wrap">
        <img src="${imageUrl}" alt="${product.productName}">
        <div class="product-hover"><i class="fa fa-shopping-cart"></i></div>
      </div>
      <div class="product-name">
        <a href="/ProductDetail/Index/${product.productId}">${product.productName}</a>
      </div>
      <div class="product-price">${product.price.toLocaleString()}₫</div>
    `;

        // Thêm sự kiện click cho icon giỏ hàng
        const cartIcon = card.querySelector('.product-hover');
        cartIcon.addEventListener('click', function (e) {
            e.preventDefault();
            addToCart(product);
        });

        productList.appendChild(card);
    });
    document.getElementById('productCount').textContent = `Hiển thị ${list.length} / ${filteredProducts.length} sản phẩm trên trang ${currentPage}`;
}

function renderPagination(totalProducts, currentPage) {
    const totalPages = Math.ceil(totalProducts / PRODUCTS_PER_PAGE);
    const pagination = document.getElementById('pagination');
    pagination.innerHTML = '';
    if (totalPages <= 1) return;

    // Previous
    if (currentPage > 1) {
        const prev = document.createElement('li');
        prev.innerHTML = '&lt;';
        prev.onclick = () => gotoPage(currentPage - 1);
        pagination.appendChild(prev);
    }

    // Hiển thị trang đầu, các trang lân cận, trang cuối và dấu ...
    let pageList = [];
    if (totalPages <= 7) {
        // Hiển thị tất cả nếu ít trang
        for (let i = 1; i <= totalPages; i++) pageList.push(i);
    } else {
        // Luôn hiển thị trang 1
        pageList.push(1);
        // Hiển thị ... nếu cần
        if (currentPage > 4) pageList.push('...');
        // Hiển thị các trang lân cận
        for (let i = Math.max(2, currentPage - 1); i <= Math.min(totalPages - 1, currentPage + 1); i++) {
            pageList.push(i);
        }
        // Hiển thị ... nếu cần
        if (currentPage < totalPages - 3) pageList.push('...');
        // Luôn hiển thị trang cuối
        pageList.push(totalPages);
    }

    pageList.forEach(p => {
        const li = document.createElement('li');
        if (p === '...') {
            li.textContent = '...';
            li.style.cursor = 'default';
            li.style.background = 'transparent';
            li.style.border = 'none';
            li.style.fontWeight = 'bold';
        } else {
            li.textContent = p;
            if (p === currentPage) li.className = 'active';
            li.onclick = () => gotoPage(p);
        }
        pagination.appendChild(li);
    });

    // Next
    if (currentPage < totalPages) {
        const next = document.createElement('li');
        next.innerHTML = '&rarr;';
        next.onclick = () => gotoPage(currentPage + 1);
        pagination.appendChild(next);
    }
}

function gotoPage(page) {
    currentPage = page;
    showCurrentPage();
}

function showCurrentPage() {
    const start = (currentPage - 1) * PRODUCTS_PER_PAGE;
    const end = start + PRODUCTS_PER_PAGE;
    renderProducts(filteredProducts.slice(start, end));
    renderPagination(filteredProducts.length, currentPage);
}

// --- LỌC THEO DANH MỤC ---
let currentCategory = null;

function filterProducts() {
    const minPrice = parseInt(document.getElementById('priceMin').value, 10);
    const maxPrice = parseInt(document.getElementById('priceMax').value, 10);
    const search = document.getElementById('searchInput').value.trim().toLowerCase();
    filteredProducts = products.filter(p => {
        let matchCategory = true;
        if (currentCategory) {
            matchCategory = (p.categoryId == currentCategory);
        }
        return (
            p.price >= minPrice &&
            p.price <= maxPrice &&
            p.productName.toLowerCase().includes(search) &&
            matchCategory
        );
    });
    currentPage = 1;
    showCurrentPage();
}

// Gắn sự kiện click cho các link danh mục
const categoryLinks = document.querySelectorAll('.category-link');
categoryLinks.forEach(link => {
    link.addEventListener('click', function (e) {
        e.preventDefault();
        currentCategory = this.dataset.category;
        filterProducts();
        // Highlight danh mục đang chọn
        categoryLinks.forEach(l => l.classList.remove('active'));
        this.classList.add('active');
    });
});

function updatePriceSliderUI() {
    const min = parseInt(priceMin.value, 10);
    const max = parseInt(priceMax.value, 10);
    const minLimit = parseInt(priceMin.min, 10);
    const maxLimit = parseInt(priceMax.max, 10);
    // Tính phần trăm vị trí thumb
    const percentMin = ((min - minLimit) / (maxLimit - minLimit)) * 100;
    const percentMax = ((max - minLimit) / (maxLimit - minLimit)) * 100;
    // Track màu động
    const track = document.querySelector('.slider-track');
    if (track) {
        track.style.background = `linear-gradient(90deg, #e0e0e0 ${percentMin}%, #6b8a4c ${percentMin}%, #2196f3 ${percentMax}%, #e0e0e0 ${percentMax}%)`;
    }
    // Giá trị min/max sát thumb
    const minValue = document.getElementById('priceMinValue');
    const maxValue = document.getElementById('priceMaxValue');
    if (minValue && maxValue) {
        minValue.textContent = min.toLocaleString() + '₫';
        maxValue.textContent = max.toLocaleString() + '₫';
        minValue.style.left = `calc(${percentMin}% - 8px)`;
        maxValue.style.left = `calc(${percentMax}% - 8px)`;
    }
}

const priceMin = document.getElementById('priceMin');
const priceMax = document.getElementById('priceMax');
if (priceMin && priceMax) {
    priceMin.addEventListener('input', function () {
        if (parseInt(priceMin.value) > parseInt(priceMax.value)) {
            priceMin.value = priceMax.value;
        }
        updatePriceSliderUI();
        filterProducts();
    });
    priceMax.addEventListener('input', function () {
        if (parseInt(priceMax.value) < parseInt(priceMin.value)) {
            priceMax.value = priceMin.value;
        }
        updatePriceSliderUI();
        filterProducts();
    });
    updatePriceSliderUI();
}

document.getElementById('searchInput').addEventListener('input', filterProducts);

// Khởi tạo
filterProducts();

function addToCart(product) {
    try {
        // Lấy giỏ hàng hiện tại từ localStorage
        let cart = JSON.parse(localStorage.getItem('cart')) || [];

        // Kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
        const existingProduct = cart.find(item => item.productId === product.productId);

        if (existingProduct) {
            // Nếu đã có thì tăng số lượng
            existingProduct.quantity += 1;
        } else {
            // Nếu chưa có thì thêm mới với số lượng là 1
            cart.push({
                productId: product.productId,
                productName: product.productName,
                price: product.price,
                image: product.imageUrls && product.imageUrls.length > 0 ? product.imageUrls[0] : '',
                quantity: 1
            });
        }

        // Lưu giỏ hàng mới vào localStorage
        localStorage.setItem('cart', JSON.stringify(cart));

        // Thông báo cho người dùng
        alert('Đã thêm sản phẩm vào giỏ hàng!');

        // Log để debug
        console.log('Cart after adding:', cart);
    } catch (error) {
        console.error('Error adding to cart:', error);
        alert('Có lỗi xảy ra khi thêm vào giỏ hàng!');
    }
}
