// 20 đơn hàng mẫu
const orders = [
    {
        id: 'DH001', date: '2024-06-01', total: 350000, payment: 'COD', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Kim Ngân', qty: 1, price: 200000 }, { name: 'Chậu Sứ Trắng', qty: 2, price: 75000 }]
    },
    {
        id: 'DH002', date: '2024-06-05', total: 120000, payment: 'Chuyển khoản', status: 'Đang xử lý', statusClass: 'pending',
        products: [{ name: 'Sen Đá', qty: 3, price: 40000 }, { name: 'Sen Đá', qty: 3, price: 40000 }, { name: 'Sen Đá', qty: 3, price: 40000 }, { name: 'Sen Đá', qty: 3, price: 40000 }, { name: 'Sen Đá', qty: 3, price: 40000 }]
    },
    {
        id: 'DH003', date: '2024-06-10', total: 500000, payment: 'COD', status: 'Đã hủy', statusClass: 'cancelled',
        products: [{ name: 'Cây Lưỡi Hổ', qty: 2, price: 250000 }]
    },
    {
        id: 'DH004', date: '2024-06-12', total: 250000, payment: 'Chuyển khoản', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Trầu Bà', qty: 1, price: 250000 }]
    },
    {
        id: 'DH005', date: '2024-06-13', total: 180000, payment: 'COD', status: 'Đang xử lý', statusClass: 'pending',
        products: [{ name: 'Cây Phát Lộc', qty: 2, price: 90000 }]
    },
    {
        id: 'DH006', date: '2024-06-14', total: 320000, payment: 'Chuyển khoản', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Ngọc Ngân', qty: 2, price: 160000 }]
    },
    {
        id: 'DH007', date: '2024-06-15', total: 210000, payment: 'COD', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Vạn Lộc', qty: 1, price: 210000 }]
    },
    {
        id: 'DH008', date: '2024-06-16', total: 150000, payment: 'Chuyển khoản', status: 'Đã hủy', statusClass: 'cancelled',
        products: [{ name: 'Cây Kim Tiền', qty: 1, price: 150000 }]
    },
    {
        id: 'DH009', date: '2024-06-17', total: 400000, payment: 'COD', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Bàng Singapore', qty: 1, price: 400000 }]
    },
    {
        id: 'DH010', date: '2024-06-18', total: 220000, payment: 'Chuyển khoản', status: 'Đang xử lý', statusClass: 'pending',
        products: [{ name: 'Cây Trầu Bà Đế Vương', qty: 2, price: 110000 }]
    },
    {
        id: 'DH011', date: '2024-06-19', total: 170000, payment: 'COD', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Lưỡi Hổ Mini', qty: 1, price: 170000 }]
    },
    {
        id: 'DH012', date: '2024-06-20', total: 300000, payment: 'Chuyển khoản', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Đa Búp Đỏ', qty: 1, price: 300000 }]
    },
    {
        id: 'DH013', date: '2024-06-21', total: 260000, payment: 'COD', status: 'Đã hủy', statusClass: 'cancelled',
        products: [{ name: 'Cây Cau Tiểu Trâm', qty: 2, price: 130000 }]
    },
    {
        id: 'DH014', date: '2024-06-22', total: 190000, payment: 'Chuyển khoản', status: 'Đang xử lý', statusClass: 'pending',
        products: [{ name: 'Cây Trầu Bà Sọc', qty: 1, price: 190000 }]
    },
    {
        id: 'DH015', date: '2024-06-23', total: 350000, payment: 'COD', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Kim Ngân', qty: 1, price: 350000 }]
    },
    {
        id: 'DH016', date: '2024-06-24', total: 120000, payment: 'Chuyển khoản', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Sen Đá', qty: 3, price: 40000 }]
    },
    {
        id: 'DH017', date: '2024-06-25', total: 500000, payment: 'COD', status: 'Đã hủy', statusClass: 'cancelled',
        products: [{ name: 'Cây Lưỡi Hổ', qty: 2, price: 250000 }]
    },
    {
        id: 'DH018', date: '2024-06-26', total: 250000, payment: 'Chuyển khoản', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Trầu Bà', qty: 1, price: 250000 }]
    },
    {
        id: 'DH019', date: '2024-06-27', total: 180000, payment: 'COD', status: 'Đang xử lý', statusClass: 'pending',
        products: [{ name: 'Cây Phát Lộc', qty: 2, price: 90000 }]
    },
    {
        id: 'DH020', date: '2024-06-28', total: 320000, payment: 'Chuyển khoản', status: 'Đã giao', statusClass: 'success',
        products: [{ name: 'Cây Ngọc Ngân', qty: 2, price: 160000 }]
    }
];

// Thêm các đơn hàng mẫu để đủ 200 đơn
for (let i = 21; i <= 200; i++) {
    orders.push({
        id: `DH${i.toString().padStart(3, '0')}`,
        date: `2024-07-${(i % 30 + 1).toString().padStart(2, '0')}`,
        total: 100000 + (i * 10000),
        payment: i % 2 === 0 ? 'COD' : 'Chuyển khoản',
        status: i % 3 === 0 ? 'Đã giao' : (i % 3 === 1 ? 'Đang xử lý' : 'Đã hủy'),
        statusClass: i % 3 === 0 ? 'success' : (i % 3 === 1 ? 'pending' : 'cancelled'),
        products: [
            { name: 'Cây Demo ' + i, qty: 1 + (i % 3), price: 50000 + (i * 1000) },
            { name: 'Chậu Demo ' + i, qty: 1, price: 20000 + (i * 500) }
        ]
    });
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
    <b>Phương thức thanh toán:</b> ${order.payment}
    ${order.payment === 'Chuyển khoản' ? `
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
    rerender();

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
