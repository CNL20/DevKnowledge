# CODING_RULES.md

## Backend (C#/.NET)
- Tuân thủ SOLID, đặc biệt Dependency Inversion: Application chỉ phụ thuộc interface, không phụ thuộc EF Core/thư viện cụ thể.
- Naming: PascalCase cho class/method/property, camelCase cho local variable, `I` prefix cho interface.
- Mỗi entity kế thừa `BaseEntity` (trừ khi có lý do rõ ràng, phải ghi chú tại sao).
- Không viết business logic trong Controller — Controller chỉ gọi Application layer (Mediator/Service).
- Exception dùng custom exception trong `Application.Common.Exceptions`, xử lý tập trung ở `ExceptionHandlingMiddleware`.
- Không hardcode secret/connection string trong code — luôn qua configuration/environment variable.

## Frontend (React/TypeScript)
- Tổ chức theo feature folder, không đặt logic nghiệp vụ trong component dùng chung (`shared/`).
- Gọi API luôn qua `services/api`, không fetch trực tiếp trong component.
- State toàn cục chỉ dùng cho auth/session/theme; state cục bộ ưu tiên nằm trong feature.

## Chung
- Không tạo file/class/interface/service/abstraction "để dùng sau" nếu chưa có feature cụ thể cần (tránh dead code). Ưu tiên: **Build only what the current feature requires**.
- Mọi thay đổi ảnh hưởng kiến trúc phải cập nhật `/ai/ARCHITECTURE.md` và tạo ADR.
- Mọi PR/feature lớn phải cập nhật `/ai/CURRENT_STATE.md`.
