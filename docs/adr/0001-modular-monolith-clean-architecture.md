# ADR 0001: Sử dụng Modular Monolith với Clean Architecture nội bộ

**Status:** Accepted (chờ xác nhận cuối cùng ở Part 2 review)
**Date:** 2026-08-10

## Context
DevKnowledge khởi đầu là project cá nhân/portfolio, 1 developer, MVP tập trung C#/.NET Backend.
Cần kiến trúc dễ maintain, dễ mở rộng dần theo feature, không cần vận hành hạ tầng phức tạp.

## Decision
Chọn **Modular Monolith**, tổ chức code nội bộ theo **Clean Architecture** (Domain/Application/Infrastructure/API).
Không chọn Microservices ở giai đoạn này.

## Alternatives considered
- **Microservices**: phù hợp khi có nhiều team/nhiều domain phức tạp cần scale độc lập — hiện tại overkill,
  tăng chi phí vận hành (nhiều service, network, deployment) không tương xứng với giá trị ở MVP.
- **Layered Architecture thuần (không có Domain-centric dependency rule)**: dễ dẫn tới coupling giữa
  business logic và persistence, khó test, khó tách feature sau này.
- **Hexagonal/Onion**: về bản chất tương đồng Clean Architecture ở điểm cốt lõi (dependency hướng vào Domain);
  Clean Architecture được chọn vì phổ biến trong hệ sinh thái .NET, dễ tìm tài liệu/pattern chuẩn.

## Consequences
- Dễ tách thành microservice sau này nếu cần (module đã tách biệt theo feature).
- Cần kỷ luật giữ đúng dependency direction (đã ghi trong `/ai/CODING_RULES.md`).
