document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('contactForm');
    const messageDiv = document.getElementById('formMessage');

    form.addEventListener('submit', function (e) {
        e.preventDefault(); // Luôn chặn submit mặc định

        messageDiv.style.color = '#e74c3c';
        const email = form.EmailContact.value.trim();
        const description = form.DescriptionContact.value.trim();

        if (!email || !description) {
            messageDiv.textContent = 'Vui lòng điền đầy đủ các trường bắt buộc.';
            return;
        }
        if (!validateEmail(email)) {
            messageDiv.textContent = 'Email không hợp lệ.';
            return;
        }

        // Gửi dữ liệu lên server bằng fetch
        fetch('/Contact/Contact', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/x-www-form-urlencoded'
            },
            body: `EmailContact=${encodeURIComponent(email)}&DescriptionContact=${encodeURIComponent(description)}`
        })
        .then(response => {
            if (response.ok) {
                messageDiv.style.color = '#27ae60';
                messageDiv.textContent = 'Cảm ơn bạn đã liên hệ! Chúng tôi sẽ phản hồi sớm nhất.';
                form.reset();
            } else {
                messageDiv.textContent = 'Có lỗi xảy ra, vui lòng thử lại sau.';
            }
        })
        .catch(() => {
            messageDiv.textContent = 'Có lỗi xảy ra, vui lòng thử lại sau.';
        });
    });

    function validateEmail(email) {
        // Đơn giản, đủ dùng
        return /^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email);
    }
});
