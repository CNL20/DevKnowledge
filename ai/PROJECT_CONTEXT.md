# PROJECT_CONTEXT.md

## Sản phẩm
**DevKnowledge** — AI-Powered Developer Knowledge and Technical English Learning Platform.
Tagline: *Learn. Understand. Build.*

## Vấn đề giải quyết
Developer (đặc biệt sinh viên / Junior) phải mở nhiều nguồn phân tán (docs, blog, YouTube, Stack Overflow, AI...)
để hiểu trọn vẹn một khái niệm kỹ thuật. DevKnowledge gom các nguồn đáng tin cậy, dùng AI tổng hợp/giải thích,
và luôn giữ traceability về nguồn gốc.

## Nguyên tắc cốt lõi (không được vi phạm khi implement bất kỳ feature nào)
1. **AI explains, sources validate** — AI không được bịa technical facts hay tự tạo ra "sự thật" không có nguồn.
2. **Source Traceability** — mọi Knowledge item phải gắn ít nhất 1 nguồn (source URL) kiểm chứng được.
3. **Accuracy > Coverage** — không cố nhồi nhiều kiến thức/nhiều version nếu không mang lại giá trị thực tế.
4. **Understand before copy** — code example luôn đi kèm giải thích + hướng dẫn áp dụng, không phải copy-paste.
5. **Version Awareness** — chỉ lưu version có giá trị thực tế (widely used / LTS / breaking changes), không lưu đủ số.

## Phạm vi MVP
C#/.NET Backend Development knowledge domain: Programming & C#, DSA, Database, ORM, API, Software Architecture,
Distributed Systems, Backend Security, Testing, DevOps, Observability, Background Processing, Backend Performance,
Software Engineering Practices, System Design, Technical English (lớp xuyên suốt).

## Đối tượng dùng
Sinh viên Software Engineering, Junior Developer, Developer chuyển hướng công nghệ (vd. Frontend→Backend, Java→C#).

## KHÔNG phải là gì
Không phải: website dịch tài liệu, ChatGPT clone, blog aggregator, kho code copy-paste, Udemy clone,
AI code generator thuần túy, website chỉ dạy grammar.

## Tài liệu liên quan (Ưu tiên đọc Context)
AI context system giúp agent: Hiểu project → Hiểu architecture → Biết current state → Biết coding rules → Biết feature scope → Biết task hiện tại.
AI **không cần đọc lại toàn bộ repository một cách mù quáng**. Phải ưu tiên đọc:
1. `/ai/PROJECT_CONTEXT.md` (File này - luôn đọc TRƯỚC TIÊN)
2. `/ai/ARCHITECTURE.md`
3. `/ai/CURRENT_STATE.md`
4. `/ai/FUNCTIONAL_SCOPE.md` (Để đảm bảo code không vượt quá phạm vi chức năng)
5. Relevant rules (`/ai/CODING_RULES.md`, v.v.) & feature documentation.
Sau đó chỉ đọc code cần thiết cho task.

- Product spec đầy đủ: `/docs/product/PART1_PRODUCT_SPEC.md`
- Architecture đầy đủ: `/docs/architecture/PART2_ARCHITECTURE.md`
