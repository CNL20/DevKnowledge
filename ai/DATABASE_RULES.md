# DATABASE_RULES.md

## Nguyên tắc
- Mọi entity chính kế thừa `BaseEntity` (Id: Guid, audit fields, IsDeleted).
- Soft delete mặc định cho dữ liệu người dùng tạo ra (LearningProgress, Bookmark...); Knowledge/Source có thể
  dùng trạng thái `Status` (Draft/Verified/Published/Archived) thay vì xóa cứng.
- Migration: mỗi thay đổi schema = 1 migration có tên mô tả rõ (`AddKnowledgeVersioning`, không dùng `update1`).
- Không migration trực tiếp trên production mà không qua review.

## Naming convention
- Table/Entity: PascalCase số ít ở mức C# (vd. `Knowledge`, `UserProfile`). Ở mức PostgreSQL, EF Core sẽ tự động ánh xạ sang **snake_case** (vd. `knowledge`, `user_profile`).
- Foreign key: `<Entity>Id` (C#) -> `<entity>_id` (Postgres).
- Junction table: `<A><B>` (C#) -> `<a>_<b>` (Postgres).

## Index strategy
- Index cho mọi FK.
- Index cho cột dùng để search/filter thường xuyên (Knowledge.Slug, Knowledge.Status, Topic.Slug).
- Composite index khi query luôn lọc theo nhiều cột cùng lúc (vd. DomainId + Status).

## Versioning dữ liệu Knowledge
- Không tạo bản duplicate toàn bộ nội dung cho mỗi version công nghệ.
- Dùng bảng `KnowledgeVersion` để lưu phần "what changed" gắn với version cụ thể, tham chiếu Knowledge gốc.
