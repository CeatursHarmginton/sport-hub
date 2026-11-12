# ADR: Công nghệ và kiến trúc Backend

- Trạng thái: Đề xuất theo architecture blueprint
- Ngày: 2026-11-12
- Bối cảnh môn học: CSW 306 – Back-end Development

## Bối cảnh

SportHub cần REST API cho tài khoản, sân, đặt sân và trận đấu cộng đồng; cần truy cập SQL Server và cần ranh giới dễ trình bày giữa API, nghiệp vụ và dữ liệu trong phạm vi đồ án.

## Quyết định

- Dùng ASP.NET Core 8 Web API và C# cho backend.
- Dùng Entity Framework Core với SQL Server.
- Tổ chức theo layered architecture: Controllers, Services, Repositories/Data, Models và DTOs.
- Theo blueprint, quản lý schema bằng SQL (`Database/schema.sql`) và dùng quy trình Database-First để scaffold/cập nhật entity cùng `DbContext`.
- Dùng JWT cho xác thực API và authorization cho các endpoint cần bảo vệ.
- Dùng frontend HTML/CSS/JavaScript độc lập, gọi API qua HTTPS/JSON.

## Lý do

- Phù hợp yêu cầu kỹ thuật đã nêu trong đề bài và môn học.
- Tách HTTP concerns khỏi nghiệp vụ và truy cập dữ liệu.
- EF Core hỗ trợ ánh xạ dữ liệu quan hệ, LINQ và SQL Server.
- DTO giúp giữ hợp đồng API tách biệt với entity lưu trữ.

## Hệ quả và lưu ý

- Cần quản lý tương thích phiên bản .NET 8, EF Core và SQL Server provider.
- Thay đổi schema cần có quy trình cập nhật script và đồng bộ entity/`DbContext`.
- Việc giữ chỗ và xác nhận booking phải được bảo vệ khỏi request đồng thời; kiểm tra ở giao diện là không đủ.
- Secret/connection string nhạy cảm phải cấu hình ngoài source control.
- Cần cân nhắc dùng ASP.NET Core Identity hay tự quản lý JWT issuance; blueprint chỉ yêu cầu JWT, chưa quyết định chi tiết identity store.
- Thanh toán, gợi ý AI và giải đấu tự động là các ý tưởng mở rộng, chưa nằm trong quyết định bắt buộc của backend ban đầu.

## Phương án chưa chọn

- Spring Boot/Maven: không chọn vì dự án định hướng .NET theo blueprint.
- Lưu dữ liệu trực tiếp trong controller: không chọn vì trộn lẫn trách nhiệm và làm khó kiểm thử nghiệp vụ.
- Code-first migrations làm nguồn schema duy nhất: chưa chọn để thống nhất với blueprint Database-First; có thể xem xét lại nếu nhóm và giảng viên thống nhất thay đổi quy trình.
