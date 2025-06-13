// Admin.js - Xử lý các chức năng cho trang Admin

// Khởi tạo các biến toàn cục
let currentPage = 1;
let itemsPerPage = 10;
let currentSort = { column: 'ProductId', direction: 'asc' };
let products = [];
let categories = [];
let filteredItems = [];

// Hàm khởi tạo khi trang được load
$(document).ready(function() {
    // Toggle sidebar
    $("#sidebarToggle").click(function(e) {
        e.preventDefault();
        $("#wrapper").toggleClass("toggled");
    });

    // Khởi tạo các event handlers cho Products
    if ($("#productsTable").length) {
        initializeProductsPage();
    }

    // Khởi tạo các event handlers cho Categories
    if ($("#categoryTableBody").length) {
        initializeCategoriesPage();
    }

    // Initialize tooltips
    $('[data-bs-toggle="tooltip"]').tooltip();

    // Initialize toasts
    $('.toast').toast();

    // Active sidebar item based on current page
    var currentUrl = window.location.pathname;
    $('.list-group-item').each(function() {
        var linkUrl = $(this).attr('href');
        if (currentUrl.indexOf(linkUrl) !== -1) {
            $(this).addClass('active');
        }
    });
});

// Các hàm xử lý cho trang Products
function initializeProductsPage() {
    // Load products từ server
    loadProducts();

    // Xử lý tìm kiếm real-time
    $("#searchInput").on('input', function() {
        const searchTerm = $(this).val().toLowerCase();
        filterProducts(searchTerm);
    });

    // Xử lý lọc theo danh mục
    $("#categoryFilter").change(function() {
        filterProducts($("#searchInput").val().toLowerCase());
    });

    // Xử lý sắp xếp khi click vào header của bảng
    $(".sortable").click(function() {
        const column = $(this).data('column');
        sortProducts(column);
    });

    // Xử lý form thêm sản phẩm
    $("#createProductForm").submit(function(e) {
        e.preventDefault();
        if (!validateForm('createProductForm')) return;

        submitFormAjax('createProductForm', '/Admin/Product/Create', function(response) {
            if (response.success) {
                $("#createForm").collapse('hide');
                loadProducts();
            }
        });
    });

    // Xử lý nút sửa sản phẩm
    $(document).on('click', '.edit-product', function() {
        const productId = $(this).data('id');
        const product = products.find(p => p.productId === productId);
        if (product) {
            populateEditForm(product);
            $("#editProductModal").modal('show');
        }
    });

    // Xử lý nút xóa sản phẩm
    $(document).on('click', '.delete-product', function() {
        const productId = $(this).data('id');
        if (confirmDelete('Bạn có chắc chắn muốn xóa sản phẩm này?')) {
            deleteProduct(productId);
        }
    });
}

// Hàm load products từ server
function loadProducts() {
    $.ajax({
        url: '/Admin/Product/GetAll',
        type: 'GET',
        success: function(response) {
            if (response.success) {
                products = response.data;
                filteredItems = [...products];
                displayProducts();
                showToast('Đã tải danh sách sản phẩm');
            } else {
                showToast('Không thể tải danh sách sản phẩm', 'error');
            }
        },
        error: function() {
            showToast('Lỗi kết nối server', 'error');
        }
    });
}

// Hàm lọc sản phẩm
function filterProducts(searchTerm) {
    const categoryId = $("#categoryFilter").val();
    
    filteredItems = products.filter(product => {
        const matchesSearch = !searchTerm || 
            product.productName.toLowerCase().includes(searchTerm) ||
            product.description?.toLowerCase().includes(searchTerm) ||
            product.size?.toLowerCase().includes(searchTerm);
            
        const matchesCategory = !categoryId || product.categoryId.toString() === categoryId;
        
        return matchesSearch && matchesCategory;
    });
    
    currentPage = 1;
    displayProducts();
}

// Hàm sắp xếp sản phẩm
function sortProducts(column) {
    if (currentSort.column === column) {
        currentSort.direction = currentSort.direction === 'asc' ? 'desc' : 'asc';
    } else {
        currentSort.column = column;
        currentSort.direction = 'asc';
    }

    filteredItems.sort((a, b) => {
        let valueA = a[column];
        let valueB = b[column];

        if (typeof valueA === 'string') {
            valueA = valueA.toLowerCase();
            valueB = valueB.toLowerCase();
        }

        if (valueA === null) return 1;
        if (valueB === null) return -1;

        if (currentSort.direction === 'asc') {
            return valueA > valueB ? 1 : -1;
        } else {
            return valueA < valueB ? 1 : -1;
        }
    });

    updateSortIcons();
    displayProducts();
}

// Hàm hiển thị sản phẩm với phân trang
function displayProducts() {
    const start = (currentPage - 1) * itemsPerPage;
    const end = start + itemsPerPage;
    const pageProducts = filteredItems.slice(start, end);

    // Cập nhật bảng sản phẩm
    const tbody = $("#productsTableBody");
    tbody.empty();

    if (pageProducts.length === 0) {
        tbody.append(`
            <tr>
                <td colspan="7" class="text-center">Không có sản phẩm nào</td>
            </tr>
        `);
        $("#pagination").empty();
        return;
    }

    pageProducts.forEach(product => {
        tbody.append(`
            <tr data-id="${product.productId}" data-category="${product.categoryId}">
                <td>${product.productId}</td>
                <td>
                    <div class="d-flex align-items-center">
                        ${product.imageUrl ? 
                            `<img src="${product.imageUrl}" alt="${product.productName}" 
                                class="product-thumbnail me-2" style="width: 50px; height: 50px; object-fit: cover;">` 
                            : ''}
                        <div>
                            <div class="fw-bold">${product.productName}</div>
                            <small class="text-muted">${product.size || ''}</small>
                        </div>
                    </div>
                </td>
                <td class="text-end">${formatCurrency(product.price)}</td>
                <td class="text-center">
                    <span class="badge ${product.quantityInStock > 0 ? 'bg-success' : 'bg-danger'}">
                        ${product.quantityInStock}
                    </span>
                </td>
                <td>${product.categoryName}</td>
                <td>
                    <button class="btn btn-sm btn-primary edit-product" data-id="${product.productId}">
                        <i class="fas fa-edit"></i>
                    </button>
                    <button class="btn btn-sm btn-danger delete-product" data-id="${product.productId}">
                        <i class="fas fa-trash-alt"></i>
                    </button>
                </td>
            </tr>
        `);
    });

    updatePagination();
}

// Hàm cập nhật phân trang
function updatePagination() {
    const totalPages = Math.ceil(filteredItems.length / itemsPerPage);
    const pagination = $("#pagination");
    pagination.empty();

    if (totalPages <= 1) return;

    let html = '<ul class="pagination justify-content-center">';
    
    // Nút Previous
    html += `
        <li class="page-item ${currentPage === 1 ? 'disabled' : ''}">
            <a class="page-link" href="#" data-page="${currentPage - 1}">
                <i class="fas fa-chevron-left"></i>
            </a>
        </li>
    `;

    // Các nút số trang
    for (let i = 1; i <= totalPages; i++) {
        if (
            i === 1 || 
            i === totalPages || 
            (i >= currentPage - 1 && i <= currentPage + 1)
        ) {
            html += `
                <li class="page-item ${i === currentPage ? 'active' : ''}">
                    <a class="page-link" href="#" data-page="${i}">${i}</a>
                </li>
            `;
        } else if (
            i === currentPage - 2 || 
            i === currentPage + 2
        ) {
            html += '<li class="page-item disabled"><span class="page-link">...</span></li>';
        }
    }

    // Nút Next
    html += `
        <li class="page-item ${currentPage === totalPages ? 'disabled' : ''}">
            <a class="page-link" href="#" data-page="${currentPage + 1}">
                <i class="fas fa-chevron-right"></i>
            </a>
        </li>
    `;

    html += '</ul>';
    pagination.html(html);

    // Xử lý sự kiện click phân trang
    $('.page-link').click(function(e) {
        e.preventDefault();
        const page = $(this).data('page');
        if (page && page !== currentPage) {
            currentPage = page;
            displayProducts();
        }
    });
}

// Hàm cập nhật biểu tượng sắp xếp
function updateSortIcons() {
    $('.sortable i').attr('class', 'fas fa-sort text-muted ms-1');
    const currentHeader = $(`.sortable[data-column="${currentSort.column}"]`);
    currentHeader.find('i').attr('class',
        `fas fa-sort-${currentSort.direction === 'asc' ? 'up' : 'down'} ms-1`
    );
}

// Hàm xóa sản phẩm
function deleteProduct(productId) {
    $.ajax({
        url: '/Admin/Product/Delete',
        type: 'POST',
        data: { id: productId },
        success: function(response) {
            if (response.success) {
                showToast('Đã xóa sản phẩm thành công');
                loadProducts();
            } else {
                showToast(response.message || 'Không thể xóa sản phẩm', 'error');
            }
        },
        error: function() {
            showToast('Lỗi kết nối server', 'error');
        }
    });
}

// Hàm điền thông tin vào form sửa
function populateEditForm(product) {
    $("#editProductId").val(product.productId);
    $("#editProductName").val(product.productName);
    $("#editPrice").val(product.price);
    $("#editQuantity").val(product.quantityInStock);
    $("#editCategoryId").val(product.categoryId);
    $("#editSize").val(product.size);
    $("#editDescription").val(product.description);
}

// Các hàm xử lý cho trang Categories
function initializeCategoriesPage() {
    // Xử lý tìm kiếm danh mục
    $("#searchCategory").on('input', function() {
        const searchTerm = $(this).val().toLowerCase();
        filterCategories(searchTerm);
    });

    // Xử lý lọc theo trạng thái
    $("#statusFilter").change(function() {
        const status = $(this).val();
        filterCategories($("#searchCategory").val().toLowerCase(), status);
    });

    // Xử lý sắp xếp
    $(".sortable").click(function() {
        const column = $(this).data('column');
        sortCategories(column);
    });

    // Xử lý thêm danh mục
    $("#btnAddCategory").click(function() {
        resetCategoryForm();
        $("#categoryModalLabel").text("Thêm danh mục mới");
        $("#categoryModal").modal("show");
    });

    // Xử lý sửa danh mục
    $(document).on('click', '.btnEditCategory', function() {
        const id = $(this).data('id');
        const name = $(this).data('name');
        const description = $(this).data('description');
        const status = $(this).data('status');

        $("#CategoryId").val(id);
        $("#CategoryName").val(name);
        $("#Description").val(description);
        $("#Status").val(status);

        $("#categoryModalLabel").text("Sửa danh mục");
        $("#categoryModal").modal("show");
    });

    // Xử lý lưu danh mục
    $("#btnSaveCategory").click(function() {
        const formData = {
            CategoryId: $("#CategoryId").val(),
            CategoryName: $("#CategoryName").val(),
            Description: $("#Description").val(),
            Status: $("#Status").val()
        };

        const isNew = !formData.CategoryId;
        const url = isNew ? '/Category/Create' : '/Category/Update';

        $.ajax({
            url: url,
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function(response) {
                if (response.success) {
                    $("#categoryModal").modal("hide");
                    showToast('Thành công', isNew ? 'Đã thêm danh mục mới' : 'Đã cập nhật danh mục');
                    setTimeout(() => location.reload(), 1000);
                } else {
                    showToast('Lỗi', response.message, 'error');
                }
            },
            error: function() {
                showToast('Lỗi', 'Không thể lưu danh mục', 'error');
            }
        });
    });

    // Xử lý xóa danh mục
    $(document).on('click', '.btnDeleteCategory', function() {
        const id = $(this).data('id');
        if (confirm('Bạn có chắc muốn xóa danh mục này?')) {
            $.ajax({
                url: '/Category/Delete',
                type: 'POST',
                data: { id: id },
                success: function(response) {
                    if (response.success) {
                        showToast('Thành công', 'Đã xóa danh mục');
                        $(`#categoryRow_${id}`).fadeOut();
                    } else {
                        showToast('Lỗi', response.message, 'error');
                    }
                },
                error: function() {
                    showToast('Lỗi', 'Không thể xóa danh mục', 'error');
                }
            });
        }
    });
}

// Hàm lọc danh mục
function filterCategories(searchTerm, status = '') {
    const rows = $("#categoryTableBody tr");
    rows.each(function() {
        const name = $(this).find('td:eq(1)').text().toLowerCase();
        const description = $(this).find('td:eq(2)').text().toLowerCase();
        const categoryStatus = $(this).find('td:eq(3) .badge').text().toLowerCase();
        
        const matchesSearch = name.includes(searchTerm) || description.includes(searchTerm);
        const matchesStatus = !status || categoryStatus === (status === '1' ? 'hoạt động' : 'không hoạt động');

        $(this).toggle(matchesSearch && matchesStatus);
    });
}

// Hàm sắp xếp danh mục
function sortCategories(column) {
    const table = $("#categoryTableBody");
    const rows = table.find("tr").toArray();
    const isAsc = $(`th[data-column="${column}"] i`).hasClass('fa-sort-up');

    rows.sort(function(a, b) {
        const aValue = $(a).find(`td:eq(${getColumnIndex(column)})`).text();
        const bValue = $(b).find(`td:eq(${getColumnIndex(column)})`).text();
        return isAsc ? aValue.localeCompare(bValue) : bValue.localeCompare(aValue);
    });

    // Cập nhật icon sắp xếp
    $(".sortable i").attr('class', 'fas fa-sort');
    $(`th[data-column="${column}"] i`).attr('class', `fas fa-sort-${isAsc ? 'down' : 'up'}`);

    // Cập nhật bảng
    table.empty().append(rows);
}

// Hàm lấy index của cột
function getColumnIndex(column) {
    switch(column) {
        case 'CategoryId': return 0;
        case 'CategoryName': return 1;
        case 'Description': return 2;
        case 'Status': return 3;
        default: return 0;
    }
}

// Hàm reset form danh mục
function resetCategoryForm() {
    $("#CategoryId").val('');
    $("#CategoryName").val('');
    $("#Description").val('');
    $("#Status").val('1');
}

// Hàm hiển thị thông báo
function showToast(title, message, type = 'success') {
    Toastify({
        text: `${title}: ${message}`,
        duration: 3000,
        gravity: "top",
        position: 'right',
        backgroundColor: type === 'success' ? "#28a745" : "#dc3545",
        stopOnFocus: true
    }).showToast();
}

// Toast notification helper
function showToast(message, type = 'success') {
    Toastify({
        text: message,
        duration: 3000,
        gravity: "top",
        position: 'right',
        backgroundColor: type === 'success' ? '#28a745' : '#dc3545',
        stopOnFocus: true
    }).showToast();
}

// Format currency helper
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

// Format date helper
function formatDate(dateString) {
    return new Date(dateString).toLocaleDateString('vi-VN', {
        year: 'numeric',
        month: 'long',
        day: 'numeric',
        hour: '2-digit',
        minute: '2-digit'
    });
}

// Confirm delete helper
function confirmDelete(message = 'Bạn có chắc chắn muốn xóa?') {
    return confirm(message);
}

// Table sorting helper
function initTableSorting() {
    $('.sortable').click(function() {
        const column = $(this).data('column');
        const currentOrder = $(this).hasClass('asc') ? 'desc' : 'asc';
        
        // Remove sorting classes from all headers
        $('.sortable').removeClass('asc desc');
        
        // Add sorting class to current header
        $(this).addClass(currentOrder);
        
        // Sort the table
        sortTable(column, currentOrder);
    });
}

// Search filter helper
function initSearchFilter() {
    $('#searchInput').on('keyup', function() {
        const value = $(this).val().toLowerCase();
        const table = $(this).data('table');
        
        $(`#${table} tbody tr`).filter(function() {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1);
        });
    });
}

// Category filter helper
function initCategoryFilter() {
    $('#categoryFilter').change(function() {
        const categoryId = $(this).val();
        const table = $(this).data('table');
        
        if (categoryId) {
            $(`#${table} tbody tr`).filter(function() {
                $(this).toggle($(this).data('category') == categoryId);
            });
        } else {
            $(`#${table} tbody tr`).show();
        }
    });
}

// Form validation helper
function validateForm(formId) {
    const form = document.getElementById(formId);
    if (!form.checkValidity()) {
        form.reportValidity();
        return false;
    }
    return true;
}

// AJAX form submission helper
function submitFormAjax(formId, url, successCallback) {
    if (!validateForm(formId)) return;

    const form = $(`#${formId}`);
    const formData = new FormData(form[0]);

    $.ajax({
        url: url,
        type: 'POST',
        data: formData,
        processData: false,
        contentType: false,
        success: function(response) {
            if (response.success) {
                showToast(response.message || 'Thao tác thành công!');
                if (successCallback) successCallback(response);
            } else {
                showToast(response.message || 'Có lỗi xảy ra!', 'error');
            }
        },
        error: function() {
            showToast('Có lỗi xảy ra!', 'error');
        }
    });
} 