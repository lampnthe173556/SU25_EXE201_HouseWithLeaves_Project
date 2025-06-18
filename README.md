🌿 House with Leaves - Online Plant Store
![image](https://github.com/user-attachments/assets/4ac8bca7-c137-4781-87e7-6262eac83318)
📜 Giới thiệu dự án
House with Leaves là ứng dụng web bán cây cảnh và vật tư làm vườn trực tuyến với giao diện thân thiện cho khách hàng và trang quản trị mạnh mẽ cho quản trị viên. Hệ thống cung cấp giải pháp mua sắm toàn diện từ duyệt sản phẩm, quản lý giỏ hàng đến thanh toán an toàn.

https://img.shields.io/badge/License-MIT-yellow.svg
https://img.shields.io/badge/.NET-6.0-blue.svg
https://img.shields.io/badge/SQL_Server-2019+-CC2927.svg?logo=microsoft-sql-server

✨ Tính năng nổi bật
🛒 Giao diện người dùng
Danh mục sản phẩm với tìm kiếm và bộ lọc nâng cao

Giỏ hàng thông minh và quy trình thanh toán tối ưu

Quản lý tài khoản cá nhân và lịch sử đơn hàng

Hệ thống đánh giá và phản hồi sản phẩm

👨‍💻 Trang quản trị
Quản lý sản phẩm toàn diện (thêm/sửa/xóa/cập nhật)

Phân loại danh mục khoa học theo loại cây trồng

Quản lý tài khoản người dùng và phân quyền chi tiết

Xử lý phản hồi và hỗ trợ khách hàng chuyên nghiệp

Thống kê doanh thu và báo cáo bán hàng

🛠️ Công nghệ sử dụng

Category	Technologies & Tools

Backend	C#, ASP.NET Core MVC, Entity Framework Core

Frontend	HTML5, CSS3, JavaScript, Bootstrap 5, Razor Views

Database	SQL Server

Storage	MinIO (Object Storage)

Email	RazorLight (Template Engine)

Validation	FluentValidation

Tools	Visual Studio, Git, Postman


🚀 Hướng dẫn cài đặt
Yêu cầu hệ thống
.NET 6 SDK

Visual Studio 2022 (hoặc VS Code)

SQL Server

MinIO Server (cho lưu trữ file)
Các bước triển khai
# 1. Clone repository
git clone https://github.com/lampnthe173556/SU25_EXE201_HouseWithLeaves_Project.git
cd SU25_EXE201_HouseWithLeaves_Project

# 2. Cấu hình kết nối (appsettings.json)
{
  "ConnectionStrings": {
    "DBDefault": "Server=(local);Database=MiniPlantStore;UID=sa;PWD=your_password;TrustServerCertificate=True"
  },
  "MinIO": {
    "Endpoint": "localhost:9000",
    "AccessKey": "your_access_key",
    "SecretKey": "your_secret_key",
    "BucketName": "plant-images"
  },
  "EmailSettings": {
    "Host": "smtp.example.com",
    "Port": 587,
    "Username": "your_email@example.com",
    "Password": "your_email_password"
  }
}

# 3. Cập nhật cơ sở dữ liệu (Package Manager Console)
dotnet ef dbcontext scaffold "Server=(local);Database=MiniPlantStore;UID=sa;PWD=lamlam276762;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models
# 4. Khởi chạy ứng dụng
dotnet run
Cấu hình MinIO
Tải và cài đặt MinIO Server

Tạo bucket tên "plant-images"

Cấu hình Access Policy cho bucket

🧑‍💻 Đóng góp của tôi
🎨 Giao diện người dùng & Chức năng Client-Side
Thiết kế và triển khai giao diện người dùng với HTML/CSS/JavaScript và Razor Views

Phát triển các trang chính: Trang chủ, Cửa hàng, Hồ sơ, Lịch sử đơn hàng

Tối ưu trải nghiệm người dùng trên nhiều thiết bị

💳 Hệ thống thanh toán
Triển khai cơ chế thanh toán COD (Thanh toán khi nhận hàng)

Tích hợp thanh toán QR code với bên thứ ba

Xử lý luồng thanh toán an toàn, bảo mật

🔐 Bảo mật & Phân quyền
Xây dựng hệ thống phân quyền chi tiết (Admin, User, Guest)

Triển khai xác thực JWT và cookie-based authentication

Kiểm soát truy cập dựa trên vai trò (Role-based Access Control)

📧 Email tự động & Quản lý mật khẩu
Phát triển hệ thống gửi email tự động với RazorLight

Triển khai chức năng quên mật khẩu với OTP qua email

Xây dựng template email chuyên nghiệp cho các tình huống:

Xác nhận đơn hàng

Thông báo phản hồi mới

Khôi phục mật khẩu

📂 Quản lý file với MinIO
Tích hợp MinIO cho lưu trữ hình ảnh đám mây

Triển khai API upload/download file an toàn

Tối ưu hiệu năng truy xuất hình ảnh sản phẩm

📄 Giấy phép
Dự án được cấp phép theo Giấy phép MIT.

📫 Liên hệ
Lâm Phạm
📧 pntlam12g03@gmail.com
🌐 GitHub Repository

https://img.shields.io/badge/GitHub-Repository-181717?style=for-the-badge&logo=github
