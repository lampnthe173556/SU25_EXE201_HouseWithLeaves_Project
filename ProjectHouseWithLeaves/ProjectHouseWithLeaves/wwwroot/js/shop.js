// Dữ liệu mẫu
const products = [
    // 20 sản phẩm gốc
    {
        name: "Ami x Bàng Sing",
        price: 189000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-bang-sing.jpg"
    },
    {
        name: "Ami x Lan Ý",
        price: 189000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-lan-y.jpg"
    },
    {
        name: "Ami x Lưỡi Hổ",
        price: 189000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-luoi-ho.jpg"
    },
    {
        name: "Ami x Ngũ Gia Bì",
        price: 189000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-ngu-gia-bi.jpg"
    },
    {
        name: "Ami x Vạn Lộc",
        price: 189000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-van-loc.jpg"
    },
    {
        name: "Ami x Phú Quý",
        price: 189000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-phu-quy.jpg"
    },
    {
        name: "Ami x Trường Sinh Xanh",
        price: 145000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-truong-sinh-xanh.jpg"
    },
    {
        name: "Akira x Môn Hồng",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-mon-hong.jpg"
    },
    {
        name: "Akira x Lưỡi Hổ",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-luoi-ho.jpg"
    },
    {
        name: "Akira x Kim Tiền",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-kim-tien.jpg"
    },
    {
        name: "Akira x Trầu Bà Brasil",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-trau-ba-brasil.jpg"
    },
    {
        name: "Akira x Trầu Bà Neon",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-trau-ba-neon.jpg"
    },
    {
        name: "Akira x Trầu Bà Cẩm Thạch",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-trau-ba-cam-thach.jpg"
    },
    {
        name: "Akira x Phát Tài Úc",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-phat-tai-uc.jpg"
    },
    {
        name: "Akira x Môn Trắng",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-mon-trang.jpg"
    },
    {
        name: "Akira x Ngọc Ngân",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-ngoc-ngan.jpg"
    },
    {
        name: "Akira x Vạn Lộc",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-van-loc.jpg"
    },
    {
        name: "Akira x Phú Quý",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-phu-quy.jpg"
    },
    {
        name: "Akira x Trường Sinh Xanh",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-truong-sinh-xanh.jpg"
    },
    {
        name: "Akira x Bàng Sing",
        price: 350000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/akira-bang-sing.jpg"
    }
];

// Thêm sản phẩm mẫu cho đủ 100 sản phẩm
for (let i = 21; i <= 100; i++) {
    products.push({
        name: `Sản phẩm mẫu ${i}`,
        price: 100000 + (i % 10) * 10000,
        image: "https://9xgarden.com/wp-content/uploads/2022/10/ami-bang-sing.jpg"
    });
}

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
        const card = document.createElement('div');
        card.className = 'product-card';
        card.innerHTML = `
      <div class="product-img-wrap">
        <img src="${product.image}" alt="${product.name}">
        <div class="product-hover"><i class="fa fa-shopping-cart"></i></div>
      </div>
      <div class="product-name">
        <a href="../html/productDetail.html">${product.name}</a>
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

function filterProducts() {
    const minPrice = parseInt(document.getElementById('priceMin').value, 10);
    const maxPrice = parseInt(document.getElementById('priceMax').value, 10);
    const search = document.getElementById('searchInput').value.trim().toLowerCase();
    filteredProducts = products.filter(p =>
        p.price >= minPrice &&
        p.price <= maxPrice &&
        p.name.toLowerCase().includes(search)
    );
    currentPage = 1;
    showCurrentPage();
}

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
        const existingProduct = cart.find(item => item.name === product.name);

        if (existingProduct) {
            // Nếu đã có thì tăng số lượng
            existingProduct.quantity += 1;
        } else {
            // Nếu chưa có thì thêm mới với số lượng là 1
            cart.push({
                name: product.name,
                price: product.price,
                image: product.image,
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
