# API_RULES.md

- REST, versioning qua URL prefix: `/api/v1/...` (xác nhận ở Part 3 khi bắt đầu implement feature đầu tiên).
- Response format thống nhất: `{ data, error, meta }` — chi tiết schema xác nhận ở Part 3.
- Toàn bộ error đi qua `ExceptionHandlingMiddleware`, không trả raw exception ra ngoài.
- Endpoint yêu cầu auth dùng `[Authorize]`, phân quyền theo Role/Policy (xác nhận ở Security Foundation).
- Validation dùng FluentValidation ở Application layer, không validate trong Controller.
