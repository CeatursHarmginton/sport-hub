# Database

Thư mục này chứa schema SQL Server của SportHub 2.0. Theo architecture blueprint, `schema.sql` là nguồn schema theo hướng Database-First; entity và `SportHubDbContext` trong `Backend/SportHub.API` cần được scaffold/cập nhật nhất quán từ schema.

## Quy tắc

- Tạo thay đổi schema có thể lặp lại và review được; ghi rõ thứ tự chạy nếu tách nhiều script.
- Định nghĩa khóa chính, khóa ngoại, ràng buộc và chỉ mục phù hợp với truy vấn.
- Đặt ràng buộc phía database cho dữ liệu cần duy nhất/toàn vẹn; kiểm tra xung đột booking không chỉ dựa vào UI.
- Không đưa dữ liệu cá nhân thật, thông tin thanh toán, mật khẩu hoặc secret vào script mẫu.
- Ghi chú cách cập nhật EF Core models/`DbContext` sau mỗi thay đổi schema.

`schema.sql` sẽ được bổ sung khi mô hình dữ liệu được thống nhất.
