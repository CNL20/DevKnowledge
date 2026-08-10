# DEVKNOWLEDGE — FUNCTIONAL SCOPE

Đây là **phạm vi chức năng hiện tại của sản phẩm DevKnowledge**.
Đây là scope đã được người dùng xác định. Khi implement bất kỳ feature nào, **không được tự ý thêm chức năng ngoài scope**. Nếu phát hiện một chức năng cần thiết nhưng chưa có trong scope, phải đề xuất và chờ người dùng xác nhận trước.

---

## 1. Authentication
Mục tiêu: cung cấp xác thực cơ bản cho người dùng.
### Chức năng
* Register
* Login
* Logout
### Chưa làm
* Email Verification
* Google Login / OAuth
* Password Reset
* 2FA
Các chức năng trên có thể xem xét trong tương lai nhưng **không thuộc scope hiện tại**.

---

## 2. Knowledge
Đây là **core feature quan trọng nhất của DevKnowledge**.
Người dùng vào hệ thống để đọc và hiểu kiến thức Backend Development.

### 2.1 Domain
Nhóm kiến thức lớn. Ví dụ:
* Programming & C#
* Database
* ORM & Data Access
* API Design
* Software Architecture
* Distributed Systems
* Backend Security
* Testing
* DevOps
* Observability
* Backend Performance
* System Design

### 2.2 Topic
Chủ đề con thuộc Domain. Ví dụ: Software Architecture -> MVC, Clean Architecture, Onion Architecture, v.v.

### 2.3 Knowledge Article
Mỗi Topic có thể có nhiều Knowledge Article. Không sử dụng một template cứng cho mọi Topic. Tùy bản chất kiến thức, article có thể bao gồm:
Definition, Core Concepts, Principles, How it works, Advantages, Disadvantages, When to use, When NOT to use, Comparison, Implementation Guide, Code Example, Request / Execution Flow, Common Mistakes, Best Practices, Version-specific changes, Technical English.
Không bắt buộc mọi article phải có tất cả các phần trên.

### 2.4 Code Example
Các kiến thức có code phải có code example khi phù hợp. Code example phải:
* Có thể tham khảo để áp dụng vào project thực tế.
* Giải thích code, file đặt code, dependency, config, cách thay đổi, flow hoạt động.
Mục tiêu: > Understand before copy. Không biến DevKnowledge thành kho copy-paste code.

### 2.5 Code Flow
Giúp người dùng xây dựng **mental model**. Ví dụ:
`Request ↓ Controller ↓ Application ↓ Logic ↓ Infrastructure ↓ Database ↓ Response`

### 2.6 Comparison
So sánh công nghệ/concept (VD: MVC vs Clean Architecture). Tập trung vào: Khác nhau ở đâu, Ưu/Nhược, Khi nào dùng/không dùng, Trade-off thực tế.

---

## 3. Source Management & Traceability
Nguyên tắc cốt lõi của DevKnowledge.

### 3.1 Source
Mỗi Knowledge quan trọng phải có source URL có thể kiểm chứng (Official, Authoritative, Trusted Community). Ưu tiên official.

### 3.2 Source Traceability
AI không được tự tạo technical facts. Nguyên tắc: > AI explains, sources validate.
Knowledge phải có khả năng truy ngược về source gốc.

### 3.3 Source Verification
Cần có trạng thái phân biệt: Draft, Verified, Published, Archived. Admin review và quyết định publish.

---

## 4. Technology Version
Chỉ tập trung vào version đang được sử dụng rộng rãi, LTS, stable, hoặc có breaking changes (vd: .NET 8, 9, 10). Không lưu đủ mọi version.
Mỗi version tập trung vào: What's New, What's Changed, Breaking Changes, Migration. Không copy toàn bộ documentation.

---

## 5. AI
AI là thành phần hỗ trợ Knowledge, không phải mục tiêu duy nhất.

### 5.1 AI Knowledge Synthesis
AI tổng hợp knowledge từ trusted sources. KHÔNG bịa technical facts, KHÔNG thay đổi ý nghĩa quan trọng, KHÔNG biến assumption thành fact, GIỮ source traceability, GIẢI THÍCH dễ hiểu hơn.

### 5.2 AI Explanation
Giải thích lại concept, code, architecture, flow, comparison dựa trên knowledge/source đã có.

### 5.3 AI Tutor / Source-based Q&A
AI trả lời dựa trên Knowledge Base và source. Không xây dựng chatbot tự do không có knowledge grounding.

---

## 6. Quiz
Quiz dùng để **kiểm tra mức độ hiểu kiến thức**, không phải hệ thống quản lý học tập. (Read → Understand → Test yourself).

---

## 7. Search
Tìm nhanh kiến thức (Knowledge, Topic, Tech, Filter, Related).

---

## 8. Admin
Quản lý Knowledge (CRUD, Publish, Archive), Quản lý Source, AI Draft Review, Version Management.

---

## 9. Những chức năng KHÔNG thuộc scope hiện tại
KHÔNG TỰ IMPLEMENT CÁC CHỨC NĂNG SAU:
- **Learning Management**: Progress, Mark as Completed, Streak, Analytics, Adaptive Learning, History.
- **Personalization**: Bookmark, Personal Notes, Collections.
- **Technical English**: Pronunciation, Speech Recognition (chỉ tập trung vào từ vựng, ngữ cảnh, ví dụ trong bài).
- **Authentication nâng cao**: Google OAuth, Email Verification, Password Reset, 2FA.

---

## 10. Nguyên tắc quan trọng nhất
DevKnowledge không phải: Udemy clone, Duolingo, LMS, Blog aggregator, ChatGPT clone, AI Code Generator, Copy-paste repo.
DevKnowledge là: > **Technical Knowledge & Developer Reference Platform**
Mục tiêu: > **Learn → Understand → Build**

---

## 11. Quy tắc dành cho AI Coding Agent
Khi nhận task:
1. Đọc `/ai/PROJECT_CONTEXT.md`.
2. Đọc `/ai/ARCHITECTURE.md`.
3. Đọc `/ai/CURRENT_STATE.md`.
4. Đọc các Rules liên quan.
5. Xác định task có thuộc Functional Scope hay không.
6. Nếu thuộc scope → tiếp tục phân tích.
7. Nếu không thuộc scope → **không tự implement**.
8. Nếu cần mở rộng scope → đề xuất trước và chờ User approval.

Không được lấy lý do: "Best practice", "Future-proof", "Tiện thể", "Nên làm luôn" để tự ý mở rộng sản phẩm.
