# SportHub 2.0 – C4 Context & Container

Sơ đồ thể hiện các thành phần dự kiến theo kiến trúc trong tài liệu đề bài. Tích hợp cổng thanh toán là hệ thống ngoài tùy chọn, chưa phải cam kết triển khai.

## Context

```mermaid
flowchart LR
    Player[Người chơi]
    Owner[Chủ sân]
    Admin[Quản trị viên]
    SportHub((SportHub 2.0\nNền tảng cộng đồng thể thao\nvà đặt sân))
    Payment[Nhà cung cấp thanh toán\nTùy chọn]

    Player -->|Tìm sân, đặt sân,\ntạo và tham gia trận| SportHub
    Owner -->|Quản lý thông tin sân,\ngiá và lịch| SportHub
    Admin -->|Quản lý nền tảng| SportHub
    SportHub -.->|Tạo/kiểm tra giao dịch\nkhi tích hợp| Payment
```

## Container

```mermaid
flowchart LR
    Player[Người chơi]
    Owner[Chủ sân]
    Admin[Quản trị viên]
    Browser[Trình duyệt]
    Web[Frontend\nHTML / CSS / JavaScript]
    API[SportHub.API\nASP.NET Core 8 Web API]
    SQL[(SQL Server)]
    Files[(Ảnh sân và avatar\nwwwroot/uploads)]
    Payment[Nhà cung cấp thanh toán\nTùy chọn]

    Player --> Browser
    Owner --> Browser
    Admin --> Browser
    Browser --> Web
    Web -->|HTTPS / JSON| API
    API -->|EF Core| SQL
    API --> Files
    API -.->|API tích hợp nếu được triển khai| Payment
```

## Ranh giới trách nhiệm API

- `AuthController`: đăng ký/đăng nhập; xác thực JWT và phân quyền được cấu hình trong ASP.NET Core.
- `FieldController`: thông tin sân và tìm kiếm/lọc.
- `BookingController`: tạo, truy vấn và cập nhật booking; xử lý xung đột/giữ chỗ ở tầng nghiệp vụ.
- `MatchController`: trận giao lưu/thách đấu, tham gia trận và kết quả/xếp hạng theo phạm vi đã chọn.
- `Services`: nghiệp vụ; `Repositories`/EF Core và `SportHubDbContext`: truy cập SQL Server.

Sơ đồ là kiến trúc mục tiêu, không khẳng định các container đã được triển khai hoặc tích hợp ngoài đã hoạt động.
