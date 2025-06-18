# 🌿 House with Leaves - Online Plant Store

![image](https://github.com/user-attachments/assets/4ac8bca7-c137-4781-87e7-6262eac83318)

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET 8.0](https://img.shields.io/badge/.NET-8.0-blue.svg)]()
[![SQL Server 2022+](https://img.shields.io/badge/SQL_Server-2022+-CC2927.svg?logo=microsoft-sql-server)]()

---

## 📜 Giới thiệu dự án

**House with Leaves** là ứng dụng web bán cây cảnh và vật tư làm vườn trực tuyến với giao diện thân thiện cho khách hàng và trang quản trị mạnh mẽ cho quản trị viên.

---

## ✨ Tính năng nổi bật

### 🛒 Giao diện người dùng
- Danh mục sản phẩm với tìm kiếm và bộ lọc nâng cao
- Giỏ hàng thông minh và quy trình thanh toán tối ưu
- Quản lý tài khoản cá nhân và lịch sử đơn hàng
- Hệ thống đánh giá và phản hồi sản phẩm

### 👨‍💻 Trang quản trị
- Quản lý sản phẩm toàn diện (thêm/sửa/xóa/cập nhật)
- Phân loại danh mục theo loại cây trồng
- Quản lý tài khoản người dùng & phân quyền chi tiết
- Xử lý phản hồi và hỗ trợ khách hàng
- Thống kê doanh thu và báo cáo bán hàng

---

## 🛠️ Công nghệ sử dụng

| Backend            | Frontend                           | Database   | Storage | Email         | Validation       | Tools                        |
|--------------------|------------------------------------|------------|---------|---------------|------------------|------------------------------|
| C#, ASP.NET Core MVC, Entity Framework Core | HTML5, CSS3, JavaScript, Bootstrap 5, Razor Views | SQL Server | MinIO   | RazorLight     | FluentValidation | Visual Studio, Git, Postman  |

---

## 🚀 Hướng dẫn cài đặt

### **Yêu cầu hệ thống**
- .NET 8 SDK
- Visual Studio 2022 (hoặc VS Code)
- SQL Server
- MinIO Server (cho lưu trữ file)

### **Các bước triển khai**

1. **Clone repository**
    ```bash
    git clone https://github.com/lampnthe173556/SU25_EXE201_HouseWithLeaves_Project.git
    cd SU25_EXE201_HouseWithLeaves_Project
    ```

2. **Cấu hình kết nối (`appsettings.json`)**
    ```json
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
    ```

3. **Cập nhật cơ sở dữ liệu**
    ```bash
    dotnet ef dbcontext scaffold "Server=(local);Database=MiniPlantStore;UID=sa;PWD=lamlam276762;TrustServerCertificate=True" Microsoft.EntityFrameworkCore.SqlServer --output-dir Models
    ```

4. **Khởi chạy ứng dụng**
    ```bash
    dotnet run
    ```

#### **Cấu hình MinIO**
- Tải và cài đặt MinIO Server
- Tạo bucket tên `plant-images`
- Cấu hình Access Policy cho bucket

---

## 🧑‍💻 Đóng góp của tôi

- **🎨 Giao diện người dùng & Chức năng Client-Side**
    - Thiết kế và triển khai giao diện với HTML/CSS/JS & Razor Views
    - Phát triển các trang: Trang chủ, Cửa hàng, Hồ sơ, Lịch sử đơn hàng
    - Tối ưu trải nghiệm trên nhiều thiết bị

- **💳 Hệ thống thanh toán**
    - Triển khai thanh toán COD
    - Tích hợp thanh toán QR code với bên thứ ba
    - Xử lý luồng thanh toán an toàn

- **🔐 Bảo mật & Phân quyền**
    - Xây dựng hệ thống phân quyền (Admin, User, Guest) theo ROLE

- **📧 Email tự động & Quản lý mật khẩu**
    - Gửi email tự động với RazorLight
    - Quên mật khẩu với OTP qua email
    - Template email cho feedback, khôi phục mật khẩu

- **📂 Quản lý file với MinIO**
    - Lưu trữ hình ảnh đám mây với MinIO
    - Upload file an toàn
    - Tối ưu hiệu năng truy xuất ảnh sản phẩm

---

## 📄 Giấy phép

Dự án được cấp phép theo [Giấy phép MIT](LICENSE).

---

## 📫 Liên hệ

- **Lâm Phạm**
- 📧 pntlam12g03@gmail.com
- 🌐 [GitHub Repository](https://github.com/lampnthe173556/SU25_EXE201_HouseWithLeaves_Project)

[![GitHub](https://img.shields.io/badge/GitHub-Repository-181717?style=for-the-badge&logo=github)](https://github.com/lampnthe173556/SU25_EXE201_HouseWithLeaves_Project)
