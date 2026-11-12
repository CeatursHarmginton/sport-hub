# SportHub.API

Dự án Web API mục tiêu: ASP.NET Core 8. Các thư mục và thành phần dự kiến được mô tả trong [README dự án](../../README.md#kiến-trúc-và-cấu-trúc-dự-án).

Luồng phụ thuộc:

```text
HTTP request → Controller → Service → Repository/DbContext → SQL Server
                     ↕
                 DTOs ↔ Models
```

Controller chỉ điều phối HTTP; quy tắc đặt sân, kiểm tra tính khả dụng và xử lý kết quả trận đấu nằm ở service. DTO dùng cho request/response, entity dùng cho lưu trữ. `Program.cs` đăng ký DI, middleware, authentication/authorization và CORS theo cấu hình môi trường.

Chưa có project `.csproj` hoặc mã API trong bộ tài liệu hiện tại.
