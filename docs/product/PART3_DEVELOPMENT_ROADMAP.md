# PART 3 — DEVELOPMENT ROADMAP

Tài liệu này định nghĩa lộ trình phát triển chi tiết cho dự án DevKnowledge. Lộ trình tuân thủ nghiêm ngặt **Functional Scope** (tập trung vào nền tảng đọc - hiểu, không làm LMS) và nguyên tắc cốt lõi **"AI explains, sources validate"**.

---

## Tóm tắt Lộ Trình (Phases)

| Phase | Tên Phase | Trọng tâm |
|---|---|---|
| **Phase 0** | **Foundation Setup** | Kiến trúc, Bộ luật AI, Folder Skeleton (Đã hoàn thành) |
| **Phase 1** | **Core Authentication** | Khởi tạo Project, Database, Identity, JWT, Login/Register |
| **Phase 2** | **Knowledge Core** | CRUD Domain, Topic, Knowledge Article (cơ bản) |
| **Phase 3** | **Content Structuring** | Cấu trúc chuyên sâu: Code Example, Execution Flow, Comparison |
| **Phase 4** | **Source Traceability & Versioning** | Quản lý Source, Gắn Source vào Knowledge, Versioning công nghệ |
| **Phase 5** | **Technical English** | Quản lý thuật ngữ, Highlight & liên kết trong bài viết |
| **Phase 6** | **Search & Discovery** | Full-text search, Filter, Related Topics |
| **Phase 7** | **AI Knowledge Pipeline** | AI tự động tổng hợp Draft từ Source -> Admin Review Workflow |
| **Phase 8** | **AI Tutor** | Q&A trên Knowledge Base nội bộ (sử dụng pgvector) |
| **Phase 9** | **Knowledge Quiz** | Đánh giá mức độ hiểu bài qua bộ câu hỏi trắc nghiệm |

---

## Chi tiết từng Phase

### Phase 1: Core Authentication
- Tạo file `.sln` và các project 4 layer `.API`, `.Application`, `.Domain`, `.Infrastructure`, `.UnitTests`.
- Cấu hình PostgreSQL + EF Core.
- Cấu hình ASP.NET Core Identity.
- Implement luồng Register, Login, Logout (JWT + Refresh Token).

### Phase 2: Knowledge Management Core
- Khởi tạo khung xương hệ thống kiến thức.
- CRUD `Domain` (VD: Programming, Database, DevOps).
- CRUD `Topic` (VD: MVC, Clean Architecture, CI/CD).
- CRUD cơ bản `Knowledge Article` (Chỉ Admin mới có quyền thao tác).

### Phase 3: Content Structuring
- Cấu trúc lưu trữ và giao diện hiển thị cho `Code Example` (yêu cầu giải thích code, file, config).
- Xây dựng format mô tả `Execution Flow` (mental model).
- Xây dựng format `Comparison` (So sánh ưu/nhược, trade-offs).

### Phase 4: Source Traceability & Versioning (Cực kỳ quan trọng)
- CRUD `Source` (URL, Type: Official/Community).
- Bắt buộc liên kết Source vào Knowledge (`KnowledgeSource`).
- Quản lý `KnowledgeVersion` (What's new, Breaking changes).
- Phase này là **tiền đề bắt buộc** trước khi cho phép AI sinh nội dung.

### Phase 5: Technical English Layer
- CRUD `TechTerm` (Từ vựng, từ loại, ngữ nghĩa, ví dụ).
- Chức năng tự động highlight hoặc liên kết thuật ngữ bên trong nội dung `Knowledge Article`.

### Phase 6: Search & Discovery
- Full-text search bằng PostgreSQL.
- Tra cứu theo Keyword, Topic, Domain, Technology.
- Đề xuất bài viết liên quan.

### Phase 7: AI Knowledge Synthesis Pipeline
- Pipeline tổng hợp tri thức tự động dựa trên nguyên tắc **AI explains, sources validate**.
- Flow: `Trusted Source → URL → AI Synthesis (via LLM) → Nháp (Draft) → Admin Review → Verified → Publish`.
- Đảm bảo AI không tự biên tự diễn (hallucinate).

### Phase 8: AI Tutor (Source-based Q&A)
- Tích hợp `pgvector` vào PostgreSQL.
- RAG Pipeline: Người dùng đặt câu hỏi, AI chỉ được phép trả lời dựa trên các Knowledge Article và Source đã được Verified.

### Phase 9: Knowledge Quiz
- Đóng vai trò hệ thống kiểm tra nhanh (Test yourself).
- Multiple choice questions map trực tiếp với Knowledge Article.
- Trả về đáp án đúng kèm giải thích.

---

> Lộ trình trên đảm bảo xây dựng nền móng dữ liệu (Authentication, Knowledge Core) và tính minh bạch (Source Traceability) một cách vững chắc nhất TRƯỚC KHI kết nối và lạm dụng sức mạnh của AI, đúng theo tầm nhìn của hệ thống.
