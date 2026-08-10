# PART 2 — TECHNICAL ARCHITECTURE & PROJECT FOUNDATION
### DevKnowledge — AI-Powered Developer Knowledge and Technical English Learning Platform

> Tài liệu này là kết quả Part 2, dùng để review trước khi implement. Không chứa business logic — chỉ
> Architecture + Technology Selection + Project Structure + Foundation + Documentation + AI Context System.

---

## A. Technology Stack

### Backend
| Technology | Purpose | Priority |
|---|---|---|
| .NET 10 (C#) | Backend runtime/language | Required |
| ASP.NET Core Web API 10 | API layer | Required |
| Entity Framework Core | ORM | Required |
| MediatR | CQRS mediator trong Application layer | Recommended |
| FluentValidation | Validation ở Application layer | Recommended |
| AutoMapper hoặc mapping thủ công | Map Entity ↔ DTO | Optional |

### Frontend
| Technology | Purpose | Priority |
|---|---|---|
| React + TypeScript | SPA framework | Required |
| Vite | Build tool | Required |
| TanStack Query (React Query) | Server state / data fetching | Recommended |
| Zustand hoặc Redux Toolkit | Client state (auth/session/theme) | Recommended |
| Tailwind CSS | Styling | Recommended |
| React Router | Routing | Required |

### Database
| Technology | Purpose | Priority |
|---|---|---|
| PostgreSQL | Relational database chính | Required |
| Redis | Cache, session, rate limiting | Recommended |

### ORM
| Technology | Purpose | Priority |
|---|---|---|
| EF Core (Code First + Migrations) | Data access, migration | Required |
| Npgsql | PostgreSQL provider cho EF Core | Required |

### Authentication / Authorization
| Technology | Purpose | Priority |
|---|---|---|
| ASP.NET Core Identity (hoặc custom User store nhẹ) | Quản lý user/password hash | Required |
| JWT (access token) + Refresh Token | Xác thực API | Required |
| Role-based Authorization (User/Admin) | Phân quyền cơ bản | Required |
| OAuth 2.0 / OpenID Connect (Google login...) | Social login | Future |

### AI / LLM
| Technology | Purpose | Priority |
|---|---|---|
| Anthropic API (Claude) hoặc OpenAI API | Knowledge synthesis, AI Tutor | Required |
| Prompt template có cấu trúc (system prompt ràng buộc "AI explains, sources validate") | Kiểm soát output AI | Required |

### RAG / Vector Database
| Technology | Purpose | Priority |
|---|---|---|
| pgvector (extension của PostgreSQL) | Vector search cho AI Tutor tra cứu knowledge base nội bộ | Recommended (không phải Required ở MVP core, nhưng cần cho AI Tutor hoạt động tốt) |
| Vector DB độc lập (Qdrant/Pinecone) | Thay thế pgvector nếu scale lớn | Future |

> Lý do chọn pgvector thay vì vector DB riêng: tránh thêm 1 hệ hạ tầng mới khi PostgreSQL đã là DB chính,
> phù hợp quy mô MVP/portfolio.

### Search Engine
| Technology | Purpose | Priority |
|---|---|---|
| PostgreSQL Full-text Search | Search Topic/Knowledge cơ bản | Recommended |
| Elasticsearch/Meilisearch | Search nâng cao (typo-tolerance, ranking) | Future |

### Message Queue
| Technology | Purpose | Priority |
|---|---|---|
| — | Không cần ở MVP (chưa có nhu cầu xử lý bất đồng bộ liên service) | — |
| RabbitMQ | Khi có nhu cầu xử lý event bất đồng bộ (vd. content pipeline nặng) | Future |

### Background Job
| Technology | Purpose | Priority |
|---|---|---|
| Hangfire | Scheduled/recurring job (vd. detect source update) | Recommended |

### File / Object Storage
| Technology | Purpose | Priority |
|---|---|---|
| Local disk (dev) / S3-compatible storage (vd. Cloudflare R2, MinIO) | Lưu ảnh minh họa, tài nguyên tĩnh nếu cần | Optional |

### Logging
| Technology | Purpose | Priority |
|---|---|---|
| Serilog | Structured logging | Required |
| Seq (local/dev) | Xem log tập trung khi dev | Recommended |

### Monitoring / Observability
| Technology | Purpose | Priority |
|---|---|---|
| ASP.NET Core Health Checks | Health endpoint | Required |
| OpenTelemetry | Tracing/metrics | Future |
| Prometheus + Grafana | Metrics + dashboard | Future |

### Testing
| Technology | Purpose | Priority |
|---|---|---|
| xUnit | Unit test | Required |
| FluentAssertions | Assertion dễ đọc | Recommended |
| Moq | Mocking | Recommended |
| Testcontainers | Integration test với Postgres thật | Recommended |
| Vitest + React Testing Library | Frontend test | Recommended |

### Docker
| Technology | Purpose | Priority |
|---|---|---|
| Docker + Docker Compose | Local dev environment (Postgres, Redis) | Required |

### CI/CD
| Technology | Purpose | Priority |
|---|---|---|
| GitHub Actions | Build, test, lint tự động | Recommended |
| Deploy pipeline (build → test → deploy) | Tự động hóa release | Future |

### Deployment
| Technology | Purpose | Priority |
|---|---|---|
| Railway / Render / Fly.io (cho portfolio-scale) | Hosting đơn giản, chi phí thấp | Recommended |
| Azure/AWS | Khi sản phẩm mở rộng thật sự | Future |

---

## B. System Architecture

### So sánh
| Kiến trúc | Phù hợp khi | Đánh giá cho DevKnowledge |
|---|---|---|
| Layered Architecture (thuần) | Project nhỏ, ít ràng buộc | Dễ làm nhưng dễ coupling Domain↔Persistence về sau |
| Clean/Onion/Hexagonal | Cần tách biệt Domain khỏi framework, dễ test, dễ thay Infrastructure | Phù hợp — đúng với mục tiêu portfolio (thể hiện kỹ năng kiến trúc) |
| Modular Monolith | 1 team/1 dev, nhiều module logic nhưng chưa cần scale độc lập | Phù hợp cho quy mô hiện tại |
| Microservices | Nhiều team, cần scale độc lập từng service, hạ tầng phức tạp | Không phù hợp ở MVP — overkill, tăng chi phí vận hành |

### Kiến trúc đề xuất cuối cùng
> **Modular Monolith, tổ chức nội bộ theo Clean Architecture** (Domain – Application – Infrastructure – API),
> chia module theo feature (Auth, Knowledge, Source, AI, Learning, Technical English, Admin).

Lý do:
- Quy mô hiện tại (1 developer, MVP, portfolio) không cần chi phí vận hành của microservices.
- Clean Architecture giữ Domain độc lập với framework/DB → dễ test, dễ thay đổi công nghệ Infrastructure sau này.
- Module hóa theo feature giúp dễ tách thành microservice trong tương lai nếu sản phẩm mở rộng thật (Future Expansion ở Part 1).
- Thể hiện tốt kỹ năng kiến trúc cho mục tiêu Portfolio.

Chi tiết quyết định và phương án thay thế: xem `docs/adr/0001-modular-monolith-clean-architecture.md`.

---

## C. Backend Architecture

```
Backend/
└── src/
    ├── DevKnowledge.Domain          (không phụ thuộc project nào khác)
    ├── DevKnowledge.Application     (phụ thuộc Domain)
    ├── DevKnowledge.Infrastructure  (phụ thuộc Application; implement interface của Application)
    └── DevKnowledge.API             (phụ thuộc Application + Infrastructure; composition root)
```

**Dependency direction:** `API → Application → Domain ← Infrastructure` (Domain là trung tâm, không phụ thuộc ai).

### Domain
Entities (khung, field chi tiết xác nhận ở Part 3): `User`, `Domain` (Knowledge Domain), `Topic`, `Knowledge`,
`Source`, `KnowledgeSource` (junction), `KnowledgeVersion`, `CodeExample`, `TechTerm`, `LearningProgress`, `Bookmark`.
Common: `BaseEntity` (Id, audit fields, soft delete).

### Application
Tổ chức theo feature (vertical slice), mỗi feature có Commands/Queries riêng (CQRS pattern qua MediatR):
`Features/Auth`, `Features/Knowledge`, `Features/Source`, `Features/AI`, `Features/Learning`.
`Common/Interfaces`: các abstraction mà Infrastructure phải implement (`IApplicationDbContext`,
`IAIKnowledgeService`, `ICurrentUserService`, `IDateTimeProvider`).

### Infrastructure
`Persistence` (EF Core DbContext + Configurations + Migrations), `Identity` (JWT, password hashing, current user),
`AI` (implementation gọi LLM provider thật), `Caching` (Redis), `BackgroundJobs` (Hangfire), `Logging` (Serilog).

### API
Chỉ đóng vai trò composition root + HTTP boundary: `Controllers`, `Middleware` (exception handling, v.v.),
`Filters`, `Configuration` (DI registration theo layer).

### Test projects
`DevKnowledge.UnitTests`, `DevKnowledge.IntegrationTests` (Testcontainers + Postgres thật),
`DevKnowledge.ArchitectureTests` (kiểm tra dependency direction bằng NetArchTest — tránh vi phạm Clean Architecture).

---

## D. Frontend Architecture

```
Frontend/
└── src/
    ├── app/            (routing, layout gốc, providers)
    ├── features/
    │   ├── auth/
    │   ├── knowledge/
    │   ├── learning/
    │   └── ai-tutor/
    ├── shared/
    │   ├── components/ (UI dùng chung: Button, Card, Modal...)
    │   └── hooks/       (hook dùng chung: useDebounce, useAuth...)
    ├── services/
    │   └── api/         (HTTP client, auth interceptor, API theo resource)
    ├── store/            (auth/session state)
    └── types/            (type dùng chung, vd DTO response)
```

- **State management:** server state qua TanStack Query (cache, refetch, loading state); client state (auth/theme)
  qua store nhẹ (Zustand). Không đặt server data vào global store để tránh cache lỗi thời.
- **API communication:** mọi gọi API đi qua `services/api`, không fetch trực tiếp trong component.
- **Authentication handling:** access token lưu in-memory, refresh token qua httpOnly cookie (bảo mật hơn localStorage);
  interceptor tự refresh khi access token hết hạn (chi tiết implement ở Part 3).
- Mỗi `features/<x>` có cấu trúc con: `components/`, `hooks/`, `api/` — không chia sẻ logic chéo giữa các feature.

---

## E. Database Architecture

### Nhóm entity chính (MVP)
| Entity | Mô tả |
|---|---|
| `User` | Tài khoản người dùng |
| `Domain` (Knowledge Domain) | Nhóm kiến thức lớn, vd "Backend Security" |
| `Topic` | Chủ đề con thuộc 1 Domain |
| `Knowledge` | Bài học/nội dung cụ thể thuộc Topic |
| `Source` | Nguồn tài liệu (URL, loại: Official/Authoritative/Community) |
| `KnowledgeSource` | Bảng nối Knowledge ↔ Source (1 Knowledge có thể nhiều Source) |
| `KnowledgeVersion` | Nội dung gắn với version công nghệ cụ thể (what's new/changed) |
| `CodeExample` | Code example gắn với Knowledge |
| `TechTerm` | Thuật ngữ Technical English (term, nghĩa, ví dụ câu) |
| `LearningProgress` | Trạng thái học của User với 1 Knowledge (learned/learning/need review) |
| `Bookmark` | User đánh dấu Knowledge để xem lại |

### Quyết định kỹ thuật
- **Database:** PostgreSQL.
- **ORM:** EF Core, Code First, migration theo tên mô tả rõ nghĩa.
- **Naming convention:** PascalCase ở code C#, ánh xạ tự động sang **snake_case** ở DB PostgreSQL (qua EF Core naming convention package/interceptor). Đã chốt.
- **Audit fields:** `CreatedAtUtc`, `UpdatedAtUtc`, `CreatedBy`, `UpdatedBy`, `IsDeleted` trong `BaseEntity`.
- **Versioning strategy:** không duplicate toàn bộ Knowledge cho mỗi version — `KnowledgeVersion` chỉ lưu phần
  thay đổi, tham chiếu `Knowledge` gốc (đúng nguyên tắc Part 1 mục 11).
- **Index strategy:** index mọi FK; index cột dùng để search/filter (`Slug`, `Status`); composite index cho
  query lọc nhiều cột đồng thời (vd. `DomainId + Status`).
- Chi tiết migration cụ thể **chưa** viết ở Part 2 (đúng yêu cầu — chỉ ở mức architecture).

---

## F. AI & Knowledge Architecture

```
Trusted Source
      ↓
Source Collection        (Admin nhập/duyệt Source URL)
      ↓
Content Processing        (extract nội dung liên quan từ Source)
      ↓
AI Processing              (IAIKnowledgeService: summarize, explain, structure theo template ở Part 1 mục 12)
      ↓
Knowledge Generation        (tạo Knowledge item nháp, gắn Source)
      ↓
Validation                  (Admin review nội dung + kiểm tra source)
      ↓
Source Citation              (đảm bảo mọi claim quan trọng có nguồn)
      ↓
Versioning                    (gắn KnowledgeVersion nếu liên quan version cụ thể)
      ↓
Publish
```

### Thành phần cần thiết
- `IAIKnowledgeService` (Application) — abstraction, không phụ thuộc LLM provider cụ thể.
- `Infrastructure.AI` — implementation thật (gọi Claude/OpenAI API), áp dụng system prompt ràng buộc nguyên tắc
  "AI explains, sources validate" (Part 1 mục 9).
- Admin Review workflow (Draft → Verified → Published) — bảng `Knowledge.Status`.
- (Future) `pgvector` cho AI Tutor tra cứu knowledge base nội bộ trước khi trả lời, giảm hallucination.
- Prompt template lưu dạng versioned file (không hardcode rải rác trong code) — vị trí đề xuất:
  `DevKnowledge.Infrastructure/AI/Prompts/`.

Chưa implement AI feature thật ở Part 2 — chỉ có interface khung (`IAIKnowledgeService`,
`Infrastructure.AI.AIKnowledgeService` throw `NotImplementedException`).

---

## G. Authentication & Security Foundation

| Thành phần | Quyết định |
|---|---|
| Register/Login | Email + Password, hash bằng BCrypt/Argon2 |
| JWT | Access token ngắn hạn (15 phút) |
| Refresh Token | Lưu server-side (DB) hoặc httpOnly cookie, hạn 7 ngày, rotate khi dùng |
| OAuth | Google login — **Future**, không bắt buộc MVP |
| Role | `User`, `Admin` (đủ cho MVP) |
| Permission | Role-based đơn giản ở MVP; Policy-based khi cần phân quyền chi tiết hơn (Future) |
| Password security | BCrypt/Argon2, không lưu plaintext, không tự chế thuật toán hash |
| Email verification | **Future** (không bắt buộc MVP vì là project cá nhân/portfolio ban đầu) |
| Password reset | **Recommended** ở MVP nếu có user thật ngoài bản thân dev |

Cấu hình JWT đã có khung trong `appsettings.json` (Issuer, Audience, thời hạn token) — chưa implement middleware xác thực thật.

---

## H. Infrastructure

```
Docker            → docker-compose cho Postgres + Redis (local dev)
PostgreSQL         → database chính
Redis               → cache, rate limiting, session phụ trợ
Background Jobs      → Hangfire (dùng chính PostgreSQL làm storage, không cần thêm hạ tầng riêng)
Logging                → Serilog, ghi ra console (dev) + file/Seq (tùy chọn)
Monitoring              → Health check endpoint MVP; OpenTelemetry/Prometheus/Grafana để Future
CI/CD                    → GitHub Actions (build + test khi push/PR)
```

Chỉ đưa vào Foundation những gì thực sự cần ngay: Docker Compose (Postgres, Redis), Dockerfile khung cho API,
CI workflow khung. Message Queue, Kubernetes, Elasticsearch — để Future, tránh over-engineering ở MVP.

---

## I. Project Folder Structure

```
DevKnowledge/
├── Backend/
│   ├── src/
│   │   ├── DevKnowledge.API/
│   │   ├── DevKnowledge.Application/
│   │   ├── DevKnowledge.Domain/
│   │   └── DevKnowledge.Infrastructure/
│   └── tests/
│       ├── DevKnowledge.UnitTests/
│       ├── DevKnowledge.IntegrationTests/
│       └── DevKnowledge.ArchitectureTests/
├── Frontend/
│   └── src/
│       ├── app/
│       ├── features/{auth,knowledge,learning,ai-tutor}/
│       ├── shared/{components,hooks}/
│       ├── services/api/
│       ├── store/
│       └── types/
├── Infrastructure/
│   ├── docker-compose.yml
│   ├── docker/Backend.Dockerfile
│   └── scripts/
├── docs/
│   ├── product/       (Part 1 spec)
│   ├── architecture/  (tài liệu này)
│   ├── features/      (mô tả từng feature — tạo ở Part 3)
│   ├── api/
│   ├── database/
│   └── adr/            (Architecture Decision Records)
├── ai/                   (AI Context System — xem mục J)
├── .github/workflows/
├── .gitignore
└── README.md
```

(Cấu trúc này đã được tạo thật trong repository — xem phần Foundation Setup, mục M.)

---

## J. AI Context System (`/ai`)

| File | Vai trò |
|---|---|
| `PROJECT_CONTEXT.md` | Tóm tắt sản phẩm, vấn đề, nguyên tắc cốt lõi, phạm vi MVP — đọc đầu tiên |
| `ARCHITECTURE.md` | Tóm tắt kiến trúc, dependency direction, module — bản đầy đủ nằm ở `/docs/architecture` |
| `CODING_RULES.md` | Quy tắc code Backend/Frontend |
| `DATABASE_RULES.md` | Naming convention, audit fields, index, versioning strategy |
| `API_RULES.md` | Quy tắc thiết kế API, response format, error handling |
| `FEATURE_RULES.md` | Quy tắc khi thêm/implement feature mới |
| `AI_WORKFLOW.md` | Hướng dẫn thứ tự đọc + nguyên tắc làm việc dành riêng cho AI coding agent |
| `CURRENT_STATE.md` | Phase hiện tại, feature đã/đang/chưa làm — **phải cập nhật liên tục** |
| `GLOSSARY.md` | Thuật ngữ nội bộ project (tránh nhầm lẫn, vd "Domain" nghĩa là gì trong từng ngữ cảnh) |
| `HANDOFF.md` | Log bàn giao giữa các phiên làm việc (người hoặc AI agent) |

Toàn bộ các file trên đã được tạo với nội dung khởi tạo thật trong repo (không phải file rỗng).

---

## K. Documentation Structure (`/docs`)

```
docs/
├── product/        → PART1_PRODUCT_SPEC.md (lưu trữ Product Spec)
├── architecture/    → PART2_ARCHITECTURE.md (tài liệu này)
├── features/         → 1 file/feature, tạo dần ở Part 3 trước khi implement từng feature
├── api/                → API reference (tạo dần khi endpoint được xác nhận)
├── database/            → ERD, mô tả entity chi tiết (tạo dần)
└── adr/                   → Architecture Decision Records, đánh số tăng dần (0001, 0002...)
```

`/docs` là nguồn tham khảo chính cho **cả developer lẫn AI coding agent**; `/ai` là bản tóm tắt tối ưu cho AI đọc nhanh.

---

## L. Development Roadmap
Lộ trình chi tiết đã được chốt và tách ra thành tài liệu riêng. Xem tại `/docs/product/PART3_DEVELOPMENT_ROADMAP.md`.

Nguyên tắc cốt lõi của lộ trình: Đảm bảo **Source Traceability (Phase 4)** được xây dựng xong hoàn toàn TRƯỚC KHI cho phép **AI Synthesis (Phase 7)** can thiệp vào quá trình tạo nội dung, tuân thủ đúng nguyên lý *AI explains, sources validate*.

---

## M. Foundation Setup — đã thực hiện ở Part 2

Đã tạo trong repository (skeleton, không có business logic):
- Solution structure 4 layer Backend (Domain/Application/Infrastructure/API) + 3 test project.
- Entity skeleton (kế thừa `BaseEntity`) cho toàn bộ nhóm dữ liệu MVP.
- Interface khung cho Application (`IApplicationDbContext`, `IAIKnowledgeService`, `ICurrentUserService`, `IDateTimeProvider`).
- `ApplicationDbContext` khung + `AIKnowledgeService`/`CurrentUserService` khung ở Infrastructure.
- API skeleton: `HealthController`, `ExceptionHandlingMiddleware`, `appsettings.json` (connection string, JWT config, Redis).
- Frontend skeleton: cấu trúc thư mục theo feature, `httpClient.ts` khung, `package.json`.
- `docker-compose.yml` (Postgres + Redis), `Backend.Dockerfile` khung.
- Toàn bộ `/ai` context system (10 file, nội dung thật).
- `/docs` structure + ADR đầu tiên (lựa chọn kiến trúc).
- `README.md`, `.gitignore` gốc.

Chưa tạo (đúng phạm vi Part 2, sẽ làm ở Part 3 khi bắt đầu feature đầu tiên):
- File `.csproj`/solution `.sln` thật (cần xác nhận .NET SDK version cài trên máy dev trước khi generate).
- `package.json` đầy đủ dependency + `vite.config.ts`, `tsconfig.json` thật.
- Global exception handling chi tiết theo từng loại lỗi nghiệp vụ.
- Bất kỳ migration EF Core nào (chưa có gì để migrate — entity còn là khung rỗng).

---

## N. Các điểm cần xác nhận trước khi implementation

1. **Database naming convention**: Đã chốt dùng PascalCase trong C# và tự động convert sang snake_case ở PostgreSQL.
2. **LLM provider**: dùng Anthropic API, OpenAI API, hay để linh hoạt qua abstraction (khuyến nghị: abstraction
   sẵn có qua `IAIKnowledgeService`, nhưng cần chọn provider đầu tiên để implement Part 3/4).
3. **pgvector cho AI Tutor**: triển khai ngay ở Phase 4 (cùng AI Pipeline) hay để tới Phase 7 mới thêm?
4. **Email verification / Password reset**: có cần ngay ở Phase 1 hay có thể để sau (vì ban đầu chỉ 1 user)?
5. **Đơn vị host/deploy**: Railway/Render/Fly.io hay tự VPS — ảnh hưởng cách viết CI/CD pipeline.
6. **MediatR/CQRS**: xác nhận dùng MediatR (thư viện ngoài, có license mới cần lưu ý ở bản mới) hay tự viết
   Service pattern đơn giản không qua Mediator.
7. **.NET SDK version** cài đặt thực tế trên máy dev (Đã chốt: .NET 10) để generate `.sln`/`.csproj` thật ở Part 3.

Sau khi các điểm trên được xác nhận, chuyển sang **PART 3 — FEATURE-BY-FEATURE DEVELOPMENT PLAN**, bắt đầu từ
feature **Authentication**.
