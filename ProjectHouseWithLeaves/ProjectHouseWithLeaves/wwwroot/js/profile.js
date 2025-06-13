document.addEventListener('DOMContentLoaded', function () {
    // Set default date to today (hoặc lấy từ dữ liệu user nếu có)
    const dobInput = document.getElementById('profileDob');
    if (dobInput && !dobInput.value) {
        dobInput.valueAsDate = new Date();
    }

    function toDateInputValue(date) {
        // date là object Date
        const d = new Date(date);
        return d.toISOString().slice(0, 10);
    }
    // Gán value
    if (window.profileDob) {
        document.getElementById('profileDob').value = window.profileDob;
    }

    document.getElementById('profileForm').onsubmit = function (e) {
        e.preventDefault();
        let error = '';
        const email = document.getElementById('profileEmail').value.trim();
        const fullname = document.getElementById('profileFullname').value.trim();
        const phone = document.getElementById('profilePhone').value.trim();
        const dob = document.getElementById('profileDob').value;
        const gender = document.getElementById('profileGender').value;

        if (fullname.length < 2) {
            error = 'Họ tên phải có ít nhất 2 ký tự!';
        }
        // Phone validation
        else if (phone.length !== 10 || !/^\d+$/.test(phone)) {
            error = 'Số điện thoại phải gồm đúng 10 chữ số!';
        }
        // Date validation
        else if (!dob) {
            error = 'Vui lòng chọn ngày sinh!';
        }
        // Gender validation
        else if (!gender) {
            error = 'Vui lòng chọn giới tính!';
        }

        if (error) {
            document.getElementById('profileError').textContent = error;
            return false;
        } else {
            
            this.submit();
            return true;
        }
    };
});

function showToast(message, type = "success") {
    const container = document.getElementById('toast-container');
    const toast = document.createElement('div');
    toast.className = 'toast ' + type;
    toast.innerHTML = `
        <span>${type === "success" ? "✅" : "❌"}</span>
        <span>${message}</span>
        <span class="toast-close">&times;</span>
    `;
    container.appendChild(toast);

    // Tự động ẩn sau 3s
    setTimeout(() => {
        toast.remove();
    }, 3000);

    // Đóng khi click vào nút close
    toast.querySelector('.toast-close').onclick = () => toast.remove();
}
