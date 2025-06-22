let orders = [];

function mapStatus(apiStatus) {
    switch (apiStatus) {
        case 'PENDING':
            return 'Đang xử lý';
        case 'SUCCESS':
        case 'DELIVERED':
            return 'Đã giao';
        case 'CANCELLED':
            return 'Đã hủy';
        default:
            return apiStatus;
    }
}

function transformOrderData(apiOrders) {
    if (!Array.isArray(apiOrders)) {
        console.error("Expected an array of orders, but received:", apiOrders);
        return [];
    }
    return apiOrders.map(order => ({
        id: `DH${String(order.orderId).padStart(3, '0')}`,
        date: new Date(order.orderDate).toISOString().split('T')[0],
        total: order.totalAmount,
        payment: order.payment,
        shipping: order.shipping,
        status: mapStatus(order.status),
        statusClass: order.statusClass,
        products: order.details.map(detail => ({
            name: detail.productName,
            qty: detail.quantity,
            price: detail.unitPrice
        }))
    }));
}

async function fetchAndRenderOrders() {
    try {
        const response = await fetch(`${window.BASE_API_URL}/OrderHistory/OrderHistoryById`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        const apiOrders = await response.json();
        orders = transformOrderData(apiOrders);
        rerender();
    } catch (error) {
        console.error("Could not fetch order history:", error);
        const tbody = document.getElementById('orderHistoryBody');
        const empty = document.getElementById('orderHistoryEmpty');
        if (tbody) tbody.innerHTML = '';
        if (empty) {
            empty.style.display = '';
            empty.innerHTML = '<i class="fa fa-exclamation-triangle"></i> Không thể tải lịch sử đơn hàng.';
        }
    }
}

let currentSort = { key: '', asc: true };
let currentFilter = '';

// Thêm biến phân trang
const ITEMS_PER_PAGE = 5;
let currentPage = 1;
let totalPages = 1;

function renderOrderHistory(data) {
    const tbody = document.getElementById('orderHistoryBody');
    const empty = document.getElementById('orderHistoryEmpty');
    tbody.innerHTML = '';
    if (!data || data.length === 0) {
        empty.style.display = '';
        return;
    }
    empty.style.display = 'none';

    // Tính toán phân trang
    totalPages = Math.ceil(data.length / ITEMS_PER_PAGE);
    const startIndex = (currentPage - 1) * ITEMS_PER_PAGE;
    const endIndex = startIndex + ITEMS_PER_PAGE;
    const paginatedData = data.slice(startIndex, endIndex);

    paginatedData.forEach((order, idx) => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
      <td>${order.id}</td>
      <td>${order.date}</td>
      <td>${order.total.toLocaleString()}₫</td>
      <td>${order.payment}</td>
      <td>${order.shipping}</td>
      <td class="status ${order.statusClass || ''}">${order.status}</td>
      <td><button class="detail-btn" data-idx="${startIndex + idx}"><i class="fa fa-eye"></i> Xem</button></td>
    `;
        tbody.appendChild(tr);
    });

    // Gán sự kiện xem chi tiết
    document.querySelectorAll('.detail-btn').forEach(btn => {
        btn.onclick = function () {
            const idx = this.getAttribute('data-idx');
            showOrderDetailModal(filteredAndSortedOrders()[idx]);
        };
    });

    // Render phân trang
    renderPagination();
}

function renderPagination() {
    const pageNumbers = document.getElementById('pageNumbers');
    const prevBtn = document.getElementById('prevPage');
    const nextBtn = document.getElementById('nextPage');

    prevBtn.disabled = currentPage === 1;
    nextBtn.disabled = currentPage === totalPages;

    pageNumbers.innerHTML = '';

    // Hiển thị phân trang dạng chấm chấm
    if (totalPages <= 7) {
        for (let i = 1; i <= totalPages; i++) {
            addPageBtn(i);
        }
    } else {
        if (currentPage <= 4) {
            for (let i = 1; i <= 5; i++) addPageBtn(i);
            addEllipsis();
            addPageBtn(totalPages);
        } else if (currentPage >= totalPages - 3) {
            addPageBtn(1);
            addEllipsis();
            for (let i = totalPages - 4; i <= totalPages; i++) addPageBtn(i);
        } else {
            addPageBtn(1);
            addEllipsis();
            for (let i = currentPage - 1; i <= currentPage + 1; i++) addPageBtn(i);
            addEllipsis();
            addPageBtn(totalPages);
        }
    }

    function addPageBtn(i) {
        const pageBtn = document.createElement('div');
        pageBtn.className = `page-number ${i === currentPage ? 'active' : ''}`;
        pageBtn.textContent = i;
        pageBtn.onclick = () => {
            currentPage = i;
            rerender();
        };
        pageNumbers.appendChild(pageBtn);
    }

    function addEllipsis() {
        const ellipsis = document.createElement('div');
        ellipsis.className = 'page-number';
        ellipsis.textContent = '..';
        ellipsis.style.cursor = 'default';
        ellipsis.style.pointerEvents = 'none';
        pageNumbers.appendChild(ellipsis);
    }
}

function filteredAndSortedOrders() {
    let filtered = orders;
    if (currentFilter) filtered = filtered.filter(o => o.status === currentFilter);
    if (currentSort.key) {
        filtered = filtered.slice().sort((a, b) => {
            let v1 = a[currentSort.key], v2 = b[currentSort.key];
            if (currentSort.key === 'total') {
                v1 = +v1; v2 = +v2;
            }
            if (currentSort.key === 'date') {
                v1 = v1.replace(/-/g, ''); v2 = v2.replace(/-/g, '');
            }
            if (v1 < v2) return currentSort.asc ? -1 : 1;
            if (v1 > v2) return currentSort.asc ? 1 : -1;
            return 0;
        });
    }
    return filtered;
}

function rerender() {
    renderOrderHistory(filteredAndSortedOrders());
}

function showOrderDetailModal(order) {
    document.getElementById('modalOrderId').textContent = order.id;
    document.getElementById('modalOrderInfo').innerHTML = `
    <b>Ngày đặt:</b> ${order.date} <br>
    <b>Trạng thái:</b> <span class="status ${order.statusClass || ''}">${order.status}</span><br>
    <b>Phương thức thanh toán:</b> ${order.payment}<br>
    <b>Phương thức giao hàng:</b> ${order.shipping}
    ${(order.payment === 'Chuyển khoản' || order.payment === 'QRCode') ? `
      <div class="order-qr-wrap">
        <div class="order-qr-title"><i class="fa fa-qrcode"></i> Quét mã QR để chuyển khoản</div>
        <img src="/img/QR.png" alt="QR chuyển khoản" class="order-qr-img">
        <div class="order-qr-note">Nội dung chuyển khoản: <b>${order.id}</b></div>
      </div>
    ` : ''}
  `;
    // Render sản phẩm
    const tbody = document.getElementById('modalOrderProducts');
    tbody.innerHTML = '';
    order.products.forEach(p => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
      <td>${p.name}</td>
      <td>${p.qty}</td>
      <td>${p.price.toLocaleString()}₫</td>
      <td>${(p.qty * p.price).toLocaleString()}₫</td>
    `;
        tbody.appendChild(tr);
    });
    document.getElementById('modalOrderTotal').textContent = order.total.toLocaleString() + '₫';
    document.getElementById('orderDetailModal').style.display = 'flex';
}

// Đóng modal
document.addEventListener('DOMContentLoaded', function () {
    fetchAndRenderOrders();

    document.getElementById('orderModalClose').onclick = function () {
        document.getElementById('orderDetailModal').style.display = 'none';
    };
    document.getElementById('orderDetailModal').onclick = function (e) {
        if (e.target === this) this.style.display = 'none';
    };

    // Thêm sự kiện cho nút prev/next
    document.getElementById('prevPage').onclick = function () {
        if (currentPage > 1) {
            currentPage--;
            rerender();
        }
    };

    document.getElementById('nextPage').onclick = function () {
        if (currentPage < totalPages) {
            currentPage++;
            rerender();
        }
    };

    // Sort
    document.getElementById('sortDate').onclick = function () {
        if (currentSort.key === 'date') currentSort.asc = !currentSort.asc;
        else { currentSort.key = 'date'; currentSort.asc = true; }
        updateSortIcons();
        rerender();
    };
    document.getElementById('sortTotal').onclick = function () {
        if (currentSort.key === 'total') currentSort.asc = !currentSort.asc;
        else { currentSort.key = 'total'; currentSort.asc = true; }
        updateSortIcons();
        rerender();
    };

    // Filter
    document.getElementById('orderStatusFilter').onchange = function () {
        currentFilter = this.value;
        currentPage = 1; // Reset về trang 1 khi lọc
        rerender();
    };
});

function updateSortIcons() {
    const dateTh = document.getElementById('sortDate');
    const totalTh = document.getElementById('sortTotal');
    dateTh.classList.remove('sorted-asc', 'sorted-desc');
    totalTh.classList.remove('sorted-asc', 'sorted-desc');
    dateTh.querySelector('i').className = 'fa fa-sort';
    totalTh.querySelector('i').className = 'fa fa-sort';
    if (currentSort.key === 'date') {
        dateTh.classList.add(currentSort.asc ? 'sorted-asc' : 'sorted-desc');
        dateTh.querySelector('i').className = currentSort.asc ? 'fa fa-sort-up' : 'fa fa-sort-down';
    }
    if (currentSort.key === 'total') {
        totalTh.classList.add(currentSort.asc ? 'sorted-asc' : 'sorted-desc');
        totalTh.querySelector('i').className = currentSort.asc ? 'fa fa-sort-up' : 'fa fa-sort-down';
    }
}
