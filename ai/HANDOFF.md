# HANDOFF.md

> Mỗi session làm việc (người hoặc AI agent) thêm 1 mục mới vào ĐẦU file này theo format bên dưới.
> Mục đích là giúp AI tiếp theo biết chính xác: đã làm gì, thay đổi file nào, đang ở trạng thái nào, quyết định nào đã chốt hoặc pending, blocker, và việc tiếp theo. KHÔNG ghi lịch sử dài dòng không cần thiết.

## Format
```text
### [YYYY-MM-DD] <Tên task/feature>
- Đã làm / File đã thay đổi: ...
- Trạng thái hiện tại: ...
- Quyết định đã chốt: ...
- Quyết định pending: ...
- Issue / Blocker: ...
- Việc cần làm tiếp theo: ...
```

---

### [2026-08-10] Hoàn thành Part 3 Development Roadmap
- Đã làm / File đã thay đổi: `docs/product/PART3_DEVELOPMENT_ROADMAP.md`, `ai/CURRENT_STATE.md`, `docs/architecture/PART2_ARCHITECTURE.md`.
- Trạng thái hiện tại: Đã chốt xong toàn bộ Roadmap gồm 9 Phase.
- Quyết định đã chốt: Đưa Source Traceability lên làm tiền đề bắt buộc trước khi chạy AI Synthesis, đúng triết lý "AI explains, sources validate".
- Quyết định pending: Không có.
- Issue / Blocker: Không có.
- Việc cần làm tiếp theo: Bắt đầu Code Phase 1! Chạy script `dotnet new` để tạo các project `.NET 10`.

---

### [2026-08-10] Cập nhật Technology Stack, Architecture Approval & Functional Scope
- Đã làm / File đã thay đổi: `ai/ARCHITECTURE.md`, `ai/CURRENT_STATE.md`, `docs/architecture/PART2_ARCHITECTURE.md`, `ai/FUNCTIONAL_SCOPE.md`, `ai/PROJECT_CONTEXT.md`.
- Trạng thái hiện tại: Hoàn thành Phase Foundation (Part 2). Đã xác định rõ ràng ranh giới các tính năng trong FUNCTIONAL_SCOPE. Chuẩn bị bước sang Part 3.
- Quyết định đã chốt: Người dùng đã DUYỆT toàn bộ bộ tech stack và kiến trúc (đặc biệt là .NET 10, ASP.NET Core 10, Identity + JWT, Clean Architecture, Modular Monolith). Đã chốt naming convention: PascalCase (C#) & snake_case (PostgreSQL). Đã chốt Functional Scope chặt chẽ cho toàn bộ hệ thống.
- Quyết định pending: Không còn quyết định block nào ở mức Foundation.
- Issue / Blocker: Không có.
- Việc cần làm tiếp theo: Bắt đầu Part 3 - Lên kế hoạch và thực hiện feature Authentication. Chạy script tạo `.sln` và các project.

---

### [Part 2] Architecture & Foundation Setup
- Đã làm: Đề xuất Technology Stack, System/Backend/Frontend/DB/AI Architecture, tạo project skeleton,
  tạo `/ai` context system, tạo `/docs` structure.
- Chưa xong: Chưa implement bất kỳ business feature nào (theo đúng yêu cầu Part 2). Chờ xác nhận stack/architecture.
- Quyết định quan trọng: Chọn Modular Monolith + Clean Architecture nội bộ thay vì Microservices (xem lý do trong
  PART2_ARCHITECTURE.md mục B).
- Việc tiếp theo: Review với người thứ hai → xác nhận → chuyển sang Part 3 (Feature-by-feature plan), bắt đầu từ Authentication.
