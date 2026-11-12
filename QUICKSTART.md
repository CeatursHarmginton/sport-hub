# Quickstart

Tài liệu này mô tả cách chuẩn bị môi trường cho SportHub 2.0. Repository hiện tại mới có tài liệu kiến trúc; chưa có project `.csproj`, schema hoặc giao diện chạy được.

## Yêu cầu dự kiến

- .NET 8 SDK
- SQL Server (LocalDB hoặc một instance SQL Server có thể truy cập)
- Công cụ quản lý SQL Server, ví dụ SQL Server Management Studio
- Git và trình soạn thảo mã nguồn

## Khi mã nguồn được khởi tạo

1. Tạo ASP.NET Core 8 Web API tại `Backend/SportHub.API`.
2. Tạo database SportHub trên SQL Server và áp dụng `Database/schema.sql`.
3. Cấu hình connection string trong User Secrets hoặc biến môi trường; không đưa thông tin xác thực vào Git.
4. Cài các package EF Core tương thích .NET 8 và SQL Server provider.
5. Scaffold entity và `DbContext` từ database theo quy trình Database-First, hoặc cập nhật entity/schema theo quyết định của nhóm.
6. Đăng ký `DbContext`, repositories, services, authentication và authorization trong DI tại `Program.cs`.
7. Chạy API từ thư mục backend:

   ```powershell
   dotnet restore
   dotnet run
   ```

8. Phục vụ `Frontend/index.html` qua một web server tĩnh và cấu hình URL API tương ứng. Không nên dùng đường dẫn `file://` nếu trình duyệt chặn request cross-origin.

## Cấu hình cần có trước khi chạy

- Connection string tới SQL Server.
- JWT issuer, audience và signing key đủ độ dài, được giữ ngoài mã nguồn.
- URL API cho frontend; cấu hình CORS chỉ cho các origin cần thiết.
- Nếu triển khai thanh toán, cần tài khoản sandbox và cấu hình callback/webhook riêng; hiện đây là tính năng mở rộng.

## Kiểm tra sau khi khởi tạo

- API khởi động thành công và kết nối được SQL Server.
- Endpoint health/status (nếu được thêm) trả trạng thái sẵn sàng.
- Endpoint có bảo vệ yêu cầu JWT hợp lệ; endpoint công khai không bị buộc đăng nhập ngoài ý muốn.
- Đặt cùng một sân/khung giờ đồng thời không tạo hai booking xác nhận.
- Không có secret trong source control hoặc log.
