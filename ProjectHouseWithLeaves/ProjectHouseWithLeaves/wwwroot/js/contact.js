document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('contactForm');
    const messageDiv = document.getElementById('formMessage');

    form.addEventListener('submit', function (e) {
        e.preventDefault();
        messageDiv.style.color = '#e74c3c';
        const fullname = form.fullname.value.trim();
        const email = form.email.value.trim();
        const phone = form.phone.value.trim();
        const message = form.message.value.trim();

        if (!fullname || !email || !message) {
            messageDiv.textContent = 'Vui lòng điền đầy đủ các trường bắt buộc.';
            return;
        }
        if (!validateEmail(email)) {
            messageDiv.textContent = 'Email không hợp lệ.';
            return;
        }
        // Nếu mọi thứ hợp lệ
        messageDiv.style.color = '#27ae60';
        messageDiv.textContent = 'Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất.';
        form.reset();
    });

    function validateEmail(email) {
        // Đơn giản, đủ dùng
        return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
    }
});
