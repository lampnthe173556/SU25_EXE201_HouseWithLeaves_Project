async function renderPaypalCart() {
    const cart = JSON.parse(localStorage.getItem('checkout_cart') || '[]');
    const cartCol = document.querySelector('.col-lg-7');
    if (!cartCol) return;
    if (cart.length === 0) {
        cartCol.innerHTML = `<h5 class="mb-3"><a href="../html/shop.html" class="text-body"><i class="fas fa-long-arrow-alt-left me-2"></i>Tiếp tục mua hàng</a></h5><hr><div>Chưa có sản phẩm nào trong giỏ hàng.</div>`;
        updatePaypalTotal();
        return;
    }

    let shippingOptionsHtml = '';
    try {
        const shippingMethods = await fetchShippingMethods();
        const storedShipping = localStorage.getItem('paypal_shipping');
        let isShippingSelected = false;

        if (shippingMethods && shippingMethods.length > 0) {
            shippingMethods.forEach(method => {

                const isSelected = method.shippingMethodId === 1;
                if (isSelected) isShippingSelected = true;
                shippingOptionsHtml += `<option id="${method.shippingMethodId}" value="${method.shippingCost}" ${isSelected ? 'selected' : ''}>${method.methodName}</option>`;
            });
        }

        if (!isShippingSelected && shippingMethods && shippingMethods.length > 0) {
            localStorage.setItem('paypal_shipping', shippingMethods[0].price);
        }

    } catch (e) {
        console.error("Failed to load shipping methods:", e);
        shippingOptionsHtml = `<option value="">Lỗi khi tải PTVC</option>`;
    }

    let html = `<h5 class="mb-3"><a href="../html/shop.html" class="text-body"><i class="fas fa-long-arrow-alt-left me-2"></i>Tiếp tục mua hàng</a></h5><hr>`;
    html += `<div style="margin: 10px 0 18px 0;">
    <label for="shipping-method" style="font-weight: 500;">Phương thức vận chuyển:</label>
    <select id="shipping-method" style="width: 220px; padding: 6px; border: 1px solid #ccc; border-radius: 4px; margin-left: 8px; font-family: 'Quicksand', sans-serif;">
      ${shippingOptionsHtml}
    </select>
  </div>`;
    html += `<div class="d-flex justify-content-between align-items-center mb-4">
    <div>
      <p class="mb-0">Bạn có ${cart.reduce((sum, i) => sum + i.qty, 0)} sản phẩm trong giỏ hàng</p>
    </div>
    <button id="removeAllPaypalCart" style="background:#e74c3c;color:#fff;border:none;border-radius:6px;padding:6px 18px;font-size:1rem;cursor:pointer;">Xóa tất cả</button>
  </div>`;
    cart.forEach(item => {
        html += `<div class="card mb-3"><div class="card-body"><div class="d-flex justify-content-between">
      <div class="d-flex flex-row align-items-center">
        <div><img src="${item.img || ''}" class="img-fluid rounded-3" alt="${item.name}" style="width: 65px;"></div>
        <div class="ms-3"><h5>${item.name}</h5><p class="small mb-0">${item.meta || ''}</p></div>
      </div>
      <div class="d-flex flex-row align-items-center">
        <input type="text" disabled min="1" value="${item.qty}" class="qty-input-paypal" data-name="${item.name}" style="width:38px;text-align:center;border:1px solid #e0e0e0;border-radius:4px;font-size:1rem;padding:2px 0;margin:0 4px;">
        <div style="width: 80px;text-align:right;font-weight:700;color:#222;font-size:1.05rem;margin-left:10px;">${item.price.toLocaleString()}₫</div>
        <button class="remove-paypal-cart-item" data-name="${item.name}" style="background:#e74c3c;color:#fff;border:none;border-radius:6px;padding:4px 10px;font-size:0.95rem;cursor:pointer;margin-left:8px;">Xóa</button>
      </div>
    </div></div></div>`;
    });
    cartCol.innerHTML = html;

    // Gán sự kiện
    document.querySelectorAll('.qty-btn-paypal').forEach(btn => {
        btn.onclick = function () {
            const name = btn.dataset.name;
            const action = btn.dataset.action;
            let cart = JSON.parse(localStorage.getItem('checkout_cart') || '[]');
            const idx = cart.findIndex(i => i.name === name);
            if (idx > -1) {
                if (action === 'increase') cart[idx].qty++;
                if (action === 'decrease' && cart[idx].qty > 1) cart[idx].qty--;
                localStorage.setItem('checkout_cart', JSON.stringify(cart));
                renderPaypalCart();
            }
        };
    });
    document.querySelectorAll('.qty-input-paypal').forEach(input => {
        input.onchange = function () {
            const name = input.dataset.name;
            let cart = JSON.parse(localStorage.getItem('checkout_cart') || '[]');
            const idx = cart.findIndex(i => i.name === name);
            let val = parseInt(input.value, 10);
            if (idx > -1 && val > 0) {
                cart[idx].qty = val;
                localStorage.setItem('checkout_cart', JSON.stringify(cart));
                renderPaypalCart();
            }
        };
    });
    document.querySelectorAll('.remove-paypal-cart-item').forEach(btn => {
        btn.onclick = function () {
            let cart = JSON.parse(localStorage.getItem('checkout_cart') || '[]');
            cart = cart.filter(i => i.name !== btn.dataset.name);
            localStorage.setItem('checkout_cart', JSON.stringify(cart));
            renderPaypalCart();
        };
    });
    const removeAllBtn = document.getElementById('removeAllPaypalCart');
    if (removeAllBtn) {
        removeAllBtn.onclick = function () {
            localStorage.setItem('checkout_cart', '[]');
            renderPaypalCart();
        };
    }
    const shippingSelect = document.getElementById('shipping-method');
    if (shippingSelect) {
        shippingSelect.onchange = function () {
            localStorage.setItem('paypal_shipping', shippingSelect.value);
            updatePaypalTotal();
        };
    }
    updatePaypalTotal();
}

function updatePaypalTotal() {
    const cart = JSON.parse(localStorage.getItem('checkout_cart') || '[]');
    const total = cart.reduce((sum, i) => sum + i.price * i.qty, 0);
    const subtotalEl = document.getElementById('paypal-subtotal');
    const shippingEl = document.getElementById('paypal-shipping');
    const totalEl = document.getElementById('paypal-total');
    if (subtotalEl) subtotalEl.textContent = total.toLocaleString() + '₫';
    const shippingValue = parseInt(localStorage.getItem('paypal_shipping') || '20000', 10);
    if (shippingEl) shippingEl.textContent = shippingValue.toLocaleString() + '₫';
    if (totalEl) totalEl.textContent = (total + shippingValue).toLocaleString() + '₫';
    // Cập nhật nút checkout
    const checkoutBtn = document.querySelector('.btn-info.btn-block.btn-lg span');
    if (checkoutBtn) {
        checkoutBtn.textContent = (total + shippingValue).toLocaleString() + '₫';
    }
}

// --- Địa chỉ: Dropdown tỉnh, huyện, xã ---
// API 


async function setupAddressDropdowns() {
    const provinceSelect = document.getElementById('province');
    const districtSelect = document.getElementById('district');
    const wardSelect = document.getElementById('ward');
    if (!provinceSelect || !districtSelect || !wardSelect) return;

    // Load provinces
    provinceSelect.innerHTML = '<option value="">Chọn tỉnh/thành phố</option>';
    try {
        const provinces = await fetchProvinces();
        provinces.forEach(p => {
            const option = document.createElement('option');
            option.value = p.code;
            option.textContent = p.name;
            provinceSelect.appendChild(option);
        });
    } catch (e) {
        alert(e.message);
    }

    provinceSelect.addEventListener('change', async function () {
        districtSelect.innerHTML = '<option value="">Chọn quận/huyện</option>';
        wardSelect.innerHTML = '<option value="">Chọn phường/xã</option>';
        if (this.value) {
            try {
                const districts = await fetchDistricts(this.value);
                districts.forEach(d => {
                    const option = document.createElement('option');
                    option.value = d.code;
                    option.textContent = d.name;
                    districtSelect.appendChild(option);
                });
            } catch (e) {
                alert(e.message);
            }
        }
    });

    districtSelect.addEventListener('change', async function () {
        wardSelect.innerHTML = '<option value="">Chọn phường/xã</option>';
        if (this.value) {
            try {
                const wards = await fetchWards(this.value);
                wards.forEach(w => {
                    const option = document.createElement('option');
                    option.value = w.code;
                    option.textContent = w.name;
                    wardSelect.appendChild(option);
                });
            } catch (e) {
                alert(e.message);
            }
        }
    });
}

window.addEventListener('DOMContentLoaded', function () {
    renderPaypalCart();
    setupAddressDropdowns(); // Khởi tạo dropdown địa chỉ khi DOM ready

    const paymentForm = document.getElementById('payment-form');
    if (paymentForm) {
        paymentForm.addEventListener('submit', async function (e) {
            // Ngăn form submit mặc định
            e.preventDefault();

            // Lấy các trường
            const province = document.getElementById('province');
            const district = document.getElementById('district');
            const ward = document.getElementById('ward');
            const addressDetail = document.getElementById('address-detail');

            let valid = true;
            let msg = '';
            if (!province.value) { valid = false; msg = 'Vui lòng chọn Tỉnh/Thành phố.'; province.focus(); }
            else if (!district.value) { valid = false; msg = 'Vui lòng chọn Quận/Huyện.'; district.focus(); }
            else if (!ward.value) { valid = false; msg = 'Vui lòng chọn Phường/Xã.'; ward.focus(); }
            else if (!addressDetail.value.trim()) { valid = false; msg = 'Vui lòng nhập địa chỉ chi tiết.'; addressDetail.focus(); }

            const cart = JSON.parse(localStorage.getItem('checkout_cart') || '[]');
            if (cart.length === 0) {
                valid = false;
                msg = 'Giỏ hàng của bạn đang trống.';
            }

            if (!valid) {
                showDangerNotification(msg);
                return false;
            }

            // Populate hidden fields before submission
            const shippingSelect = document.getElementById('shipping-method');
            const shippingValue = parseInt(shippingSelect.value, 10);
            const shippingId = shippingSelect.options[shippingSelect.selectedIndex].id;
            const shippingName = shippingSelect.options[shippingSelect.selectedIndex].text;

            const subtotal = cart.reduce((sum, i) => sum + i.price * i.qty, 0);
            const total = subtotal + shippingValue;

            document.getElementById('cartData').value = JSON.stringify(cart);
            document.getElementById('shippingMethodInfo').value = JSON.stringify({
                shippingMethodId: shippingId,
                methodName: shippingName,
                shippingCost: shippingValue
            });
            document.getElementById('totalAmount').value = total;


            const provinceSelect = document.getElementById('province');
            const districtSelect = document.getElementById('district');
            const wardSelect = document.getElementById('ward');


            const addressText = {
                province: provinceSelect.options[provinceSelect.selectedIndex].text,
                district: districtSelect.options[districtSelect.selectedIndex].text,
                ward: wardSelect.options[wardSelect.selectedIndex].text,
                addressDetail: document.getElementById('address-detail').value
            };
            const productList = JSON.parse(document.getElementById('cartData').value);
            const paymentMethodId = localStorage.getItem('payment_method_id');

            const orderData = {
                totalAmount: document.getElementById('totalAmount').value,
                shipping: JSON.parse(document.getElementById('shippingMethodInfo').value),
                addressDetail: addressText,
                paymentMethodId: paymentMethodId,
                items: productList.map(p => ({
                    productId: p.productId,
                    quantity: p.qty,
                    unitPrice: p.price,
                }))
            }

            console.log('OrderData:', orderData);

            // Disable the button to prevent multiple clicks
            const submitBtn = document.getElementById('btn-pay');
            const originalBtnText = submitBtn.innerHTML;
            submitBtn.disabled = true;
            submitBtn.innerHTML = `<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Đang xử lý...`;

            // Gửi dữ liệu đơn hàng đến API
            try {
                const response = await fetch(window.BASE_API_URL + '/api/orders/create', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json',
                    },
                    body: JSON.stringify(orderData)
                });

                console.log(response);

                if (!response.ok) {
                    throw new Error('Lỗi khi tạo đơn hàng');
                }

                const result = await response.json();
                console.log('PaymentMethodId:', paymentMethodId);
                console.log('API Response:', result);

                // Xử lý kết quả thành công
                if (result.success) {
                    if (paymentMethodId == 1) {
                        showSuccessNotification(
                            'Đặt hàng thành công, quý khách vui lòng theo dõi đơn hàng tại lịch sử đơn hàng ở phần tài khoản!',
                            10, // Thời gian đếm ngược (giây)
                            '/Home/home' // URL để chuyển hướng
                        );
                    } else {
                        // Chuyển hướng hoặc hiển thị thông báo thành công
                        showSuccessNotification(
                            'Đặt hàng thành công, quý khách vui lòng thanh toán mã QR ở lịch sử đơn hàng ở phần tài khoản để hoàn tất đơn hàng!',
                            10, // Thời gian đếm ngược (giây)
                            '/Home/home' // URL để chuyển hướng
                        );
                    }

                    // Xóa giỏ hàng sau khi đặt hàng thành công
                    localStorage.setItem('checkout_cart', '[]');
                    localStorage.removeItem('paypal_shipping');

                } else {
                    showDangerNotification(result.message || 'Có lỗi xảy ra khi đặt hàng');
                    submitBtn.disabled = false;
                    submitBtn.innerHTML = originalBtnText;
                }
            } catch (error) {
                console.error('Lỗi khi gửi đơn hàng:', error);
                showDangerNotification('Lỗi kết nối. Vui lòng thử lại sau.');
                submitBtn.disabled = false;
                submitBtn.innerHTML = originalBtnText;
            }
        });
    }
});

async function fetchProvinces() {
    const res = await fetch('https://provinces.open-api.vn/api/p/');
    if (!res.ok) throw new Error('Lỗi lấy danh sách tỉnh');
    return await res.json(); // [{name: "...", code: ...}, ...]
}

async function fetchDistricts(provinceCode) {
    const res = await fetch(`https://provinces.open-api.vn/api/p/${provinceCode}?depth=2`);
    if (!res.ok) throw new Error('Lỗi lấy danh sách huyện');
    const data = await res.json();
    return data.districts || []; // [{name: "...", code: ...}, ...]
}

async function fetchWards(districtCode) {
    const res = await fetch(`https://provinces.open-api.vn/api/d/${districtCode}?depth=2`);
    if (!res.ok) throw new Error('Lỗi lấy danh sách xã');
    const data = await res.json();
    return data.wards || []; // [{name: "...", code: ...}, ...]
}

function showDangerNotification(message) {
    const container = document.getElementById('notification-container');
    if (!container) return;
    const toast = document.createElement('div');
    toast.className = 'toast-notification toast-danger';
    toast.innerHTML = `
    <span>${message}</span>
    <span class="toast-close" onclick="this.parentElement.remove()">&times;</span>
  `;
    container.appendChild(toast);
    setTimeout(() => {
        toast.remove();
    }, 3500);
}

function showSuccessNotification(message, countdownSeconds = 0, redirectUrl = '') {
    const container = document.getElementById('notification-container');
    if (!container) return;
    const toast = document.createElement('div');
    toast.className = 'toast-notification toast-success';

    let countdownHtml = '';
    if (countdownSeconds > 0 && redirectUrl) {
        countdownHtml = ` <span class="countdown-timer">(Chuyển hướng sau ${countdownSeconds}s)</span>`;
    }

    toast.innerHTML = `
        <span>${message}</span>
        ${countdownHtml}
        <span class="toast-close" onclick="this.parentElement.remove()">&times;</span>
    `;
    container.appendChild(toast);

    let intervalId;

    if (countdownSeconds > 0 && redirectUrl) {
        let remaining = countdownSeconds;
        const timerSpan = toast.querySelector('.countdown-timer');

        intervalId = setInterval(() => {
            remaining--;
            if (timerSpan) {
                timerSpan.textContent = ` (Chuyển hướng sau ${remaining}s)`;
            }
            if (remaining <= 0) {
                clearInterval(intervalId);
                window.location.href = redirectUrl;
            }
        }, 1000);

        // Fallback redirection in case interval fails
        setTimeout(() => {
            clearInterval(intervalId); // Clear interval before redirecting
            window.location.href = redirectUrl;
        }, countdownSeconds * 1000);
    } else {
        // Auto-remove toast if not redirecting
        setTimeout(() => {
            toast.remove();
        }, 3500);
    }

    // Allow manual closing
    toast.querySelector('.toast-close').addEventListener('click', () => {
        clearInterval(intervalId);
        toast.remove();
    });
}

async function fetchShippingMethods() {
    const res = await fetch(window.BASE_API_URL + '/Payment/GetShippingMethodJson');
    if (!res.ok) throw new Error('Lỗi lấy danh sách phương thức vận chuyển');
    return await res.json();
}
