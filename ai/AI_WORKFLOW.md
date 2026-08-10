# AI_WORKFLOW.md — Hướng dẫn cho AI coding agent làm việc trên repo này

**MỤC TIÊU: AI không phải autonomous coder. AI là coding assistant có kiểm soát.**

## 1. Workflow Chính: Understand → Explain → Ask → Modify → Verify
Đây là workflow bắt buộc khi AI thay đổi code:
- **Understand**: Hiểu vấn đề và context trước.
- **Explain**: Giải thích nguyên nhân và phương án.
- **Ask**: Xin phép user.
- **Modify**: Chỉ sửa đúng phạm vi được duyệt.
- **Verify**: Chạy test/build/check và báo kết quả.

## 2. Change Approval Protocol (Explain Before Modify)
AI coding agent **không được tự ý sửa code ngay khi người dùng đưa lỗi hoặc yêu cầu thay đổi**.
Trước khi sửa code, AI phải trình bày:
1. Vấn đề đang xảy ra.
2. Nguyên nhân gốc hoặc nguyên nhân có khả năng xảy ra (phân biệt rõ ràng "Likely Cause" và "Confirmed Cause" - KHÔNG coi AI đoán là sự thật).
3. Bằng chứng từ code, log, exception hoặc hành vi quan sát được (Nếu chưa đủ bằng chứng, AI phải nói rõ: "Chưa đủ bằng chứng để kết luận").
4. Các nguyên nhân thay thế nếu chưa đủ bằng chứng.
5. Phương án sửa được đề xuất.
6. File/module/layer bị ảnh hưởng.
7. Phạm vi thay đổi chính xác.
8. Rủi ro hoặc tác động có thể có.
Sau đó **phải hỏi người dùng có đồng ý thực hiện hay không**. Chỉ thực hiện khi User đồng ý.

## 3. Scope Control & "Do Not Expand the Task"
- Ưu tiên **Smallest Appropriate Change**. Không sửa toàn bộ hệ thống chỉ vì một lỗi nhỏ.
- Khi user đã đồng ý sửa một phạm vi cụ thể, AI **chỉ được sửa trong phạm vi đó**.
- Không được tự động làm thêm feature hoặc refactor ngoài task được giao. Không được dùng lý do "tiện thể", "best practice" hoặc "để đồng bộ" để tự mở rộng phạm vi.
- Nếu phát hiện cần thay đổi thêm file/module khác: AI phải dừng lại → giải thích tại sao cần → xác định phạm vi mới → hỏi user có cho phép mở rộng không.

## 4. Debugging Workflow
Khi user đưa lỗi, AI KHÔNG được lập tức sửa code:
1. Đọc error / log / stack trace.
2. Xác định nơi lỗi xảy ra.
3. Đọc các file liên quan trực tiếp.
4. Phân tích nguyên nhân & đưa ra bằng chứng.
5. Xác định nguyên nhân có khả năng nhất & phạm vi nhỏ nhất cần kiểm tra/sửa.
6. Đề xuất solution & Hỏi user approval.
7. Sau khi được phép mới sửa, test lại, và báo cáo.

## 5. Change Summary
Sau khi implementation xong, AI phải báo cáo theo format:
```text
## Change Summary
### Đã thay đổi
- File:
- Nội dung:
### Không thay đổi
- File/module ngoài scope:
### Nguyên nhân
...
### Cách sửa
...
### Verification
- Build:
- Test:
- Manual verification:
### Còn tồn tại
...
```

## 6. Thay Đổi Luôn Yêu Cầu User Approval
AI luôn phải hỏi trước khi thực hiện:
- Thay đổi Software Architecture / dependency direction.
- Thêm/xóa/thay thế technology, package, library, version quan trọng.
- Thay đổi database schema, tạo migration.
- Xóa dữ liệu hoặc file.
- Thay đổi API contract, Authentication/Authorization mechanism, security configuration quan trọng.
- Sửa code ở layer/module ngoài phạm vi task, refactor lớn, thay đổi environment behavior.

## 7. Các Nguyên Tắc Khác
- KHÔNG tự ý đổi kiến trúc, tech stack (chi tiết xem `/ai/ARCHITECTURE.md`).
- KHÔNG bịa nội dung Knowledge (sản phẩm cốt lõi).
- Sau khi hoàn thành task: cập nhật `/ai/CURRENT_STATE.md`.
- Khi kết thúc phiên làm việc: ghi chú vào `/ai/HANDOFF.md`.
