# Online Shop Backend

## Skills

66 specialist skills are installed in `.agents/skills/`. **Automatically load the relevant skill(s) before starting any task.** Read the full `SKILL.md` and any relevant `references/` files.

### Available Skills

| Skill | Invoke | Use For |
|-------|--------|---------|
| `csharp-developer` | `/csharp-developer` | C#, .NET 8+, ASP.NET Core, EF Core, Blazor |
| `dotnet-core-expert` | `/dotnet-core-expert` | Clean architecture, CQRS, .NET patterns |
| `api-designer` | `/api-designer` | REST API design, OpenAPI, versioning, pagination |
| `database-optimizer` | `/database-optimizer` | EF Core queries, indexes, PostgreSQL tuning |
| `postgres-pro` | `/postgres-pro` | PostgreSQL, advanced queries, performance |
| `sql-pro` | `/sql-pro` | SQL queries, schema design |
| `architecture-designer` | `/architecture-designer` | System design, ADRs, architecture patterns |
| `microservices-architect` | `/microservices-architect` | Microservices, event-driven, distributed systems |
| `security-reviewer` | `/security-reviewer` | Security review, OWASP, vulnerabilities |
| `secure-code-guardian` | `/secure-code-guardian` | Secure coding patterns |
| `devops-engineer` | `/devops-engineer` | Docker, CI/CD, GitHub Actions, Kubernetes |
| `kubernetes-specialist` | `/kubernetes-specialist` | K8s deployments, Helm, scaling |
| `terraform-engineer` | `/terraform-engineer` | IaC, Terraform |
| `cloud-architect` | `/cloud-architect` | AWS, Azure, GCP, multi-cloud |
| `sre-engineer` | `/sre-engineer` | SLOs, incident response, reliability |
| `monitoring-expert` | `/monitoring-expert` | Observability, metrics, alerting |
| `test-master` | `/test-master` | Testing strategy, xUnit, integration tests |
| `code-reviewer` | `/code-reviewer` | Code review, feedback, checklists |
| `debugging-wizard` | `/debugging-wizard` | Debugging, root cause analysis |
| `code-documenter` | `/code-documenter` | API docs, XML comments, user guides |
| `typescript-pro` | `/typescript-pro` | TypeScript, type safety |
| `javascript-pro` | `/javascript-pro` | JavaScript, modern ES features |
| `react-expert` | `/react-expert` | React, hooks, state management |
| `nextjs-developer` | `/nextjs-developer` | Next.js, SSR, App Router |
| `vue-expert` | `/vue-expert` | Vue 3, Composition API |
| `vue-expert-js` | `/vue-expert-js` | Vue 3 with JavaScript |
| `angular-architect` | `/angular-architect` | Angular, NgRx, RxJS |
| `nestjs-expert` | `/nestjs-expert` | NestJS, decorators, modules |
| `fastapi-expert` | `/fastapi-expert` | FastAPI, Pydantic, async Python |
| `django-expert` | `/django-expert` | Django, DRF, ORM |
| `python-pro` | `/python-pro` | Python, modern patterns |
| `pandas-pro` | `/pandas-pro` | Pandas, data manipulation |
| `golang-pro` | `/golang-pro` | Go, concurrency, performance |
| `rust-engineer` | `/rust-engineer` | Rust, memory safety, performance |
| `java-architect` | `/java-architect` | Java, Spring patterns |
| `spring-boot-engineer` | `/spring-boot-engineer` | Spring Boot, JPA, REST |
| `kotlin-specialist` | `/kotlin-specialist` | Kotlin, coroutines, Android/JVM |
| `swift-expert` | `/swift-expert` | Swift, iOS, SwiftUI |
| `flutter-expert` | `/flutter-expert` | Flutter, Dart, mobile |
| `react-native-expert` | `/react-native-expert` | React Native, mobile |
| `php-pro` | `/php-pro` | PHP, modern patterns |
| `laravel-specialist` | `/laravel-specialist` | Laravel, Eloquent, Artisan |
| `rails-expert` | `/rails-expert` | Ruby on Rails, ActiveRecord |
| `wordpress-pro` | `/wordpress-pro` | WordPress, plugins, themes |
| `shopify-expert` | `/shopify-expert` | Shopify, Liquid, apps |
| `salesforce-developer` | `/salesforce-developer` | Salesforce, Apex, LWC |
| `graphql-architect` | `/graphql-architect` | GraphQL, schemas, resolvers |
| `websocket-engineer` | `/websocket-engineer` | WebSockets, real-time, SignalR |
| `ml-pipeline` | `/ml-pipeline` | ML pipelines, training, deployment |
| `rag-architect` | `/rag-architect` | RAG, vector DBs, embeddings |
| `fine-tuning-expert` | `/fine-tuning-expert` | LLM fine-tuning |
| `prompt-engineer` | `/prompt-engineer` | Prompt design, LLM interactions |
| `mcp-developer` | `/mcp-developer` | MCP servers, Claude tools |
| `game-developer` | `/game-developer` | Game dev, Unity, Unreal |
| `embedded-systems` | `/embedded-systems` | Embedded C, RTOS, hardware |
| `cpp-pro` | `/cpp-pro` | C++, modern features, performance |
| `spark-engineer` | `/spark-engineer` | Apache Spark, big data |
| `cli-developer` | `/cli-developer` | CLI tools, argument parsing |
| `playwright-expert` | `/playwright-expert` | Playwright, E2E testing |
| `legacy-modernizer` | `/legacy-modernizer` | Refactoring, modernization |
| `feature-forge` | `/feature-forge` | Feature development workflow |
| `spec-miner` | `/spec-miner` | Requirements, spec extraction |
| `fullstack-guardian` | `/fullstack-guardian` | Full-stack oversight |
| `chaos-engineer` | `/chaos-engineer` | Chaos engineering, resilience |
| `atlassian-mcp` | `/atlassian-mcp` | Jira, Confluence via MCP |
| `the-fool` | `/the-fool` | Creative, unconventional approaches |

## Auto-Load Rules

- C# / .NET work → load `csharp-developer` + `dotnet-core-expert`
- API design → load `api-designer`
- Database queries/migrations → load `database-optimizer` + `postgres-pro`
- Security concerns → load `secure-code-guardian` + `security-reviewer`
- Docker/CI/CD → load `devops-engineer`
- Code review → load `code-reviewer`
- Writing tests → load `test-master`
- Debugging → load `debugging-wizard`
