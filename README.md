# SportHub 2.0

**Nền tảng cộng đồng thể thao và đặt sân thông minh**  
Đồ án môn CSW 306 – Back-end Development, sử dụng ASP.NET Core 8 Web API và Entity Framework Core.

## Mô tả bài toán

Người chơi thể thao phong trào thường phải tìm sân qua nhiều kênh, gọi điện để hỏi lịch trống và tự so sánh giá, vị trí, chất lượng mặt sân cũng như tiện ích. Việc tìm đối thủ hoặc người chơi bổ sung cho một trận đấu lại phân tán trên các nhóm mạng xã hội, gây khó khăn khi ghép trình độ và thống nhất lịch. Chủ sân thiếu công cụ tập trung để quản lý lịch, hạn chế đặt trùng và khai thác các khung giờ thấp điểm.

SportHub 2.0 hướng tới một nền tảng chung để người chơi khám phá và đặt sân, kết nối cộng đồng theo môn thể thao và khu vực, đồng thời giúp chủ sân quản lý thông tin sân và lịch đặt.

### Mục tiêu

- Tìm kiếm sân theo môn thể thao, vị trí/khoảng cách, thời gian còn trống, giá và tiện ích.
- Cung cấp luồng đặt sân rõ ràng, hạn chế hai người đặt cùng một khung giờ.
- Hỗ trợ tạo trận giao lưu/thách đấu và tìm người chơi phù hợp.
- Tạo nền tảng để theo dõi kết quả, bảng xếp hạng khu vực và các hoạt động cộng đồng.
- Tách biệt API, nghiệp vụ, truy cập dữ liệu và giao diện để hệ thống có thể mở rộng.

### Đối tượng sử dụng

| Đối tượng | Nhu cầu chính |
|---|---|
| Người chơi | Tìm sân, đặt sân, tạo/tham gia trận đấu, tìm người chơi và xem đánh giá |
| Chủ sân | Cập nhật thông tin sân, giá, tiện ích và quản lý khung giờ/lịch đặt |
| Quản trị viên | Quản lý tài khoản, nội dung và hoạt động nền tảng |

### Phạm vi tính năng

**Tính năng cốt lõi**

- Tìm kiếm sân với bộ lọc vị trí, giờ trống, giá, loại sân và tiện ích.
- Quản lý lịch đặt và trạng thái khung giờ; giữ chỗ tạm thời trong quy trình đặt để tránh trùng lịch.
- Tạo trận giao lưu/thách đấu với môn thể thao, trình độ, địa điểm và thời gian.
- Ghi nhận kết quả và tính điểm xếp hạng Elo cho bảng xếp hạng khu vực.
- Đánh giá sân đã sử dụng; chỉ người có lượt đặt hoàn tất mới đủ điều kiện đánh giá.

**Ý tưởng mở rộng**

- Chia tiền sân cho thành viên và nhắc thanh toán.
- Đăng chỗ còn thiếu trong trận để tìm người chơi lấp chỗ trống.
- Gợi ý sân, khung giờ và đối thủ dựa trên hoạt động trước đó.
- Tạo giải đấu nhỏ, bốc thăm và cập nhật bảng đấu.

Các ý tưởng mở rộng là định hướng, không mặc định thuộc phạm vi phiên bản đầu tiên. Tích hợp cổng thanh toán và xác định vị trí cần cấu hình dịch vụ/nhà cung cấp phù hợp trước khi triển khai.

## Kiến trúc và cấu trúc dự án

Áp dụng layered architecture cho backend: **Controller → Service → Repository/EF Core → SQL Server**. DTO là ranh giới dữ liệu của API; entity ánh xạ dữ liệu lưu trữ. ASP.NET Core DI đăng ký các thành phần tại điểm khởi chạy.

```text
SportHub2/
├── README.md
├── QUICKSTART.md
├── Documentation/
│   └── Sprint1/
│       ├── Sprint1_Review_Notes.md
│       ├── Daily_Standup_Notes.md
│       ├── C4_Context_Container_Diagram.md
│       └── ADR_Backend_Technology_Decision.md
├── Database/
│   ├── README.md
│   └── schema.sql
├── Backend/
│   └── SportHub.API/
│       ├── Controllers/
│       │   ├── AuthController.cs
│       │   ├── FieldController.cs
│       │   ├── BookingController.cs
│       │   └── MatchController.cs
│       ├── Models/
│       │   ├── User.cs
│       │   ├── Field.cs
│       │   ├── Booking.cs
│       │   └── Match.cs
│       ├── Data/
│       │   └── SportHubDbContext.cs
│       ├── DTOs/
│       │   ├── FieldDto.cs
│       │   ├── BookingCreateDto.cs
│       │   └── MatchCreateDto.cs
│       ├── Repositories/
│       │   ├── IFieldRepository.cs
│       │   └── FieldRepository.cs
│       ├── Services/
│       │   ├── IFieldService.cs
│       │   └── FieldService.cs
│       ├── wwwroot/
│       │   └── uploads/fields/
│       ├── appsettings.json
│       ├── appsettings.Development.json
│       ├── Program.cs
│       └── SportHub.API.csproj
└── Frontend/
    ├── README.md
    ├── index.html
    ├── css/
    │   └── style.css
    └── js/
        └── main.js
```

### Trách nhiệm các tầng

- **Controllers:** nhận HTTP request, kiểm tra đầu vào ở mức API, gọi service và trả HTTP response; không đặt nghiệp vụ phức tạp trong controller.
- **Services:** thực hiện quy tắc nghiệp vụ, phối hợp các thao tác dữ liệu và xử lý các luồng đặt sân/trận đấu.
- **Repositories / EF Core:** truy vấn và cập nhật dữ liệu. Có thể dùng Repository Pattern cho truy cập cần trừu tượng hóa; không tạo lớp repository chỉ để bọc lại mọi lời gọi `DbContext` một cách máy móc.
- **Data:** `SportHubDbContext`, cấu hình entity và quan hệ.
- **Models:** entity ánh xạ đến bảng dữ liệu; không dùng trực tiếp làm hợp đồng request/response.
- **DTOs:** dữ liệu vào/ra API và validation tương ứng.
- **Database:** SQL Server; `schema.sql` là nguồn định nghĩa schema theo hướng Database-First, sau đó scaffold/cập nhật entity và DbContext bằng EF Core.
- **Frontend:** giao diện HTML/CSS/JavaScript giao tiếp với Web API.

### Quy ước triển khai

- Xác thực bằng JWT; phân quyền endpoint theo vai trò.
- Đặt sân phải kiểm tra tính khả dụng phía server trong cùng thao tác dữ liệu có kiểm soát cạnh tranh; không dựa riêng vào trạng thái giao diện.
- Giữ chỗ tạm thời cần có thời hạn hết hiệu lực và quy tắc giải phóng khi đặt/thanh toán không hoàn tất.
- Không commit mật khẩu, JWT signing key, connection string có thông tin xác thực hoặc khóa cổng thanh toán. Dùng User Secrets/biến môi trường cho giá trị nhạy cảm.
- Lỗi validation và nghiệp vụ cần trả phản hồi API có ý nghĩa; không trả thành công giả khi lưu dữ liệu thất bại.

## Tài liệu

- [Hướng dẫn khởi động](QUICKSTART.md)
- [Sprint 1 Review](Documentation/Sprint1/Sprint1_Review_Notes.md)
- [Mẫu Daily Stand-up](Documentation/Sprint1/Daily_Standup_Notes.md)
- [Sơ đồ C4 Context & Container](Documentation/Sprint1/C4_Context_Container_Diagram.md)
- [Quyết định công nghệ backend (ADR)](Documentation/Sprint1/ADR_Backend_Technology_Decision.md)
- [Ghi chú Database](Database/README.md)
- [Ghi chú Frontend](Frontend/README.md)

> Repository hiện mô tả kiến trúc và bài toán. Các tệp mã nguồn trong cây cấu trúc là những thành phần dự kiến, chưa được tạo bởi bộ tài liệu này.
