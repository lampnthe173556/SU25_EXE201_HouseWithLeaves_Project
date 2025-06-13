document.addEventListener('DOMContentLoaded', function () {
    document.getElementById('changePasswordForm').onsubmit = function (e) {
        e.preventDefault();
        let error = '';
        const current = document.getElementById('currentPassword').value.trim();
        const newpass = document.getElementById('newPassword').value.trim();
        const confirm = document.getElementById('confirmPassword').value.trim();

        if (!current) {
            error = 'Vui lòng nhập mật khẩu hiện tại!';
        } else if (newpass.length < 8) {
            error = 'Mật khẩu mới phải có ít nhất 8 ký tự!';
        } else if (newpass === current) {
            error = 'Mật khẩu mới phải khác mật khẩu hiện tại!';
        } else if (newpass !== confirm) {
            error = 'Xác nhận mật khẩu không khớp!';
        }

        if (error) {
            document.getElementById('changepassError').textContent = error;
            showToast(error, 'error');
            return false;
        } else {
            document.getElementById('changepassError').textContent = '';
            showToast('Đổi mật khẩu thành công!', 'success');
            // Nếu muốn submit thật sự, bỏ comment dòng dưới:
            this.submit();
            return true;
        }
    };

    // Toggle eye for password fields
    document.querySelectorAll('.toggle-eye').forEach(function (eye) {
        eye.addEventListener('click', function () {
            const targetId = this.getAttribute('data-target');
            const input = document.getElementById(targetId);
            if (input.type === 'password') {
                input.type = 'text';
                this.innerHTML = '<i class="fa fa-eye-slash"></i>';
            } else {
                input.type = 'password';
                this.innerHTML = '<i class="fa fa-eye"></i>';
            }
        });
    });
});

// Toast notification
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
