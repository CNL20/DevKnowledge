# DevKnowledge

AI-Powered Developer Knowledge and Technical English Learning Platform.
*Learn. Understand. Build.*

## Trạng thái hiện tại
Foundation phase — xem `/ai/CURRENT_STATE.md`.

## Cấu trúc repo
```
Backend/          - .NET solution (Clean Architecture: API/Application/Domain/Infrastructure)
Frontend/         - React + TypeScript SPA
Infrastructure/   - Docker, docker-compose, deployment scripts
docs/             - Product, Architecture, Feature, API, Database docs, ADR
ai/               - Context system cho AI coding agent (đọc trước khi làm bất kỳ task nào)
```

## Bắt đầu
1. Đọc `/ai/PROJECT_CONTEXT.md` và `/ai/ARCHITECTURE.md` trước khi code.
2. `docker compose -f Infrastructure/docker-compose.yml up -d` để chạy Postgres + Redis local.
3. Xem `/ai/CURRENT_STATE.md` để biết phase/feature hiện tại.
