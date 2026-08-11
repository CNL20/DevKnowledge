# Core Authentication (Phase 1)

## Overview
Tính năng Authentication cung cấp nền tảng bảo mật cho DevKnowledge, đảm bảo rằng mọi dữ liệu tri thức đều được quản lý, định danh và truy cập hợp lệ. Phase này tập trung vào việc cấp phát, quản lý vòng đời Token (Access Token & Refresh Token) và tích hợp hệ thống Identity mạnh mẽ.

## Các chức năng chính
1. **Đăng ký (Register):** Tạo tài khoản mới thông qua email và mật khẩu (yêu cầu mật khẩu mạnh).
2. **Đăng nhập (Login):** Trả về chuỗi JWT Access Token (hạn 15 phút) và Refresh Token (hạn 7 ngày).
3. **Cấp lại Token (Refresh Token):** Sử dụng Refresh Token còn hiệu lực để xin lại Access Token mới mà không cần đăng nhập lại.
4. **Đăng xuất (Logout):** Thu hồi (Revoke) Refresh Token trong Database, đảm bảo an toàn tuyệt đối.

## Kiến trúc Kỹ thuật
- **Identity Framework:** `ASP.NET Core Identity` quản lý User (`ApplicationUser`), Hash mật khẩu và xử lý các thao tác cơ bản.
- **Bảo mật Token:** 
  - JWT Bearer Token để xác thực các API endpoints.
  - Refresh Token được băm (hash SHA-256) trước khi lưu vào bảng `refresh_tokens` của PostgreSQL, tăng cường bảo mật trong trường hợp DB bị rò rỉ.
- **CQRS Pattern:** Mọi luồng API đều được chia thành các Commands (`RegisterCommand`, `LoginCommand`, v.v.) qua MediatR tại Application Layer.
- **Validation:** Sử dụng FluentValidation để kiểm tra tự động định dạng Email, độ dài Password trước khi chạm đến logic chính.

## Endpoints (API v1)
| Phương thức | Đường dẫn                 | Chức năng                               |
|-------------|---------------------------|-----------------------------------------|
| POST        | `/api/v1/auth/register`   | Đăng ký tài khoản                       |
| POST        | `/api/v1/auth/login`      | Đăng nhập (trả về Access & Refresh JWT) |
| POST        | `/api/v1/auth/refresh`    | Cấp lại Access Token mới                |
| POST        | `/api/v1/auth/logout`     | Đăng xuất & vô hiệu hoá Refresh Token   |

## Trạng thái hoàn thành
- [x] Cơ sở dữ liệu: `ApplicationUser` và `RefreshToken`
- [x] Logic xử lý: `IdentityService` và `JwtTokenGenerator`
- [x] API Controller: `AuthController`
- [x] Swagger/OpenAPI: Cấu hình Scalar UI 
- [x] Unit Tests: Đã cover 100% các logic cốt lõi.

> *Tài liệu này được tự động cập nhật ngay khi hoàn thành Phase 1 trong Roadmap.*
