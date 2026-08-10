# ARCHITECTURE.md (AI-facing summary)

> Bản đầy đủ: `/docs/architecture/PART2_ARCHITECTURE.md`. File này chỉ tóm tắt để AI agent tra cứu nhanh.

## Trạng thái Technology Stack
Các technology sau đã được **Final / Confirmed** (người dùng đã duyệt):
- .NET 10 & ASP.NET Core 10
- PostgreSQL
- EF Core
- ASP.NET Core Identity
- JWT Bearer Authentication + Refresh Token
- Clean Architecture & Modular Monolith
- React + TypeScript
- Docker + Compose
- FluentValidation & Serilog
- Unit + Integration Test
- GitHub Actions
- AI/LLM & Source Traceability (Product-wise)

## Authentication Clarification
**ASP.NET Core Identity và JWT không phải hai lựa chọn loại trừ nhau.** Có thể sử dụng kết hợp cả hai.
- **Identity**: Quản lý user, password, roles, claims, email confirmation, password reset, lockout.
- **JWT**: Cơ chế authentication của API thông qua Bearer Token.
Lưu ý: Giải pháp Identity + JWT đã được **Confirmed**.

## Kiến trúc tổng thể
**Modular Monolith**, tổ chức nội bộ theo **Clean Architecture**. Không dùng microservices ở giai đoạn này.

## Backend layers (dependency direction: API → Application → Domain ← Infrastructure)
```text
DevKnowledge.Domain          (không phụ thuộc project nào khác)
DevKnowledge.Application     (phụ thuộc Domain)
DevKnowledge.Infrastructure  (phụ thuộc Application, implement các interface của Application)
DevKnowledge.API             (phụ thuộc Application + Infrastructure, chỉ để composition root)
```
Quy tắc bắt buộc: **Domain và Application không được reference Infrastructure hay API.**
Application định nghĩa interface, Infrastructure implement.

## Module theo feature
Auth, Knowledge, Source, AI (Knowledge Synthesis), Learning, TechnicalEnglish, Admin.
Mỗi module tổ chức theo vertical slice bên trong layer tương ứng, KHÔNG tách microservice riêng.

## Frontend
SPA (React + TypeScript), tổ chức theo feature-folder: `features/<feature>/{components,hooks,api}`.
`shared/` chứa component/hook dùng chung. `services/api` chứa HTTP client + auth interceptor.

## Database
PostgreSQL + EF Core (Code First). Naming: PascalCase entity, snake_case ở DB level qua convention.
Soft delete qua `IsDeleted`. Audit fields chuẩn: CreatedAtUtc/UpdatedAtUtc/CreatedBy/UpdatedBy trong `BaseEntity`.

## AI Pipeline (không implement ở Part 2, chỉ có interface khung)
`Trusted Source → Source Collection → Content Processing → AI Processing (IAIKnowledgeService) → Knowledge Generation → Validation (Admin review) → Source Citation → Versioning → Publish`

## Quy tắc thay đổi kiến trúc / Technology
AI **không được tự ý thay đổi** .NET version, Database, Architecture, Auth mechanism, ORM, Message broker, Cache tech, Testing framework, Deployment strategy.
Nếu thấy technology hiện tại có vấn đề, AI phải làm theo quy trình đề xuất:
`Current approach → Problem → Evidence → Alternative → Trade-offs → Recommendation → Ask for approval`
Bất kỳ thay đổi kiến trúc nào phải được ghi lại thành ADR trong `/docs/adr`.
