# GCDesk

<img src="Login-gcdesk.jpeg" alt="GCDesk login screen" width="600">

Sistema de gestão de chamados para colaboradores e analistas de TI.  
Permite abertura, acompanhamento, resolução e avaliação de chamados técnicos.

---

## Architecture

### Layered design

The application follows a strict 3-layer architecture plus a utility layer, all written in C# with ASP.NET Web Forms (.NET Framework 4.8).

```mermaid
graph TB
  UI["Presentation Layer<br/>ASPX Pages + MasterPage"]
  BL["Business Layer<br/>Services"]
  DAL["Data Access Layer<br/>Persist (BD classes)"]
  UTIL["Infrastructure<br/>DataHelper / Mapped / AuditService / BasePage"]

  UI --> BL
  BL --> DAL
  DAL --> UTIL
  UTIL --> DB[(MySQL)]
```

| Layer              | Directory            | Responsibility                                                                                                                              |
| ------------------ | -------------------- | ------------------------------------------------------------------------------------------------------------------------------------------- |
| **Presentation**   | `Pages/`             | ASPX markup, code-behind, MasterPage layout, toast notifications                                                                            |
| **Business**       | `App_Code/Services/` | Orchestrates business rules, calls Persist, emits audit/history/notifications                                                               |
| **Data Access**    | `App_Code/Persist/`  | `IRepository<T>` implementations, raw SQL via `DataHelper`                                                                                  |
| **Infrastructure** | `App_Code/Infra/`    | `DataHelper`, `Mapped` (ADO.NET factory), `AuditService`, `ToastHelper`, `ReportHelper`, `BasePage`, `Function` (hashing), `IRepository<T>` |
| **Domain**         | `App_Code/Class/`    | Plain entity classes (User, Ticket, Notification, Department, Equipment, Service)                                                           |

---

## Component diagram

```mermaid
graph LR
  subgraph "Presentation"
    Login["Default.aspx<br/>Login"]
    Master["MasterPage.master<br/>Nav + FirstLogin modal"]
    AnaDash["Analista/Index/<br/>Dashboard + Claim"]
    AnaMyTix["Analista/MeusChamados/<br/>My tickets"]
    AnaAllTix["Analista/TodosChamados/<br/>All tickets + search"]
    AnaDetail["Analista/DetalheChamado/<br/>Detail + comments + history"]
    AnaCollab["Analista/ListaColaboradores/<br/>Manage users"]
    AnaUpdate["Analista/EditarUsuario/<br/>Edit user"]
    AnaReport["Analista/Relatorios/<br/>Filter + export Excel"]
    AnaAudit["Analista/LogsAuditoria/<br/>Read-only log viewer"]
    CollabDash["Colaborador/Index/<br/>Dashboard + create ticket"]
    CollabRate["Colaborador/AvaliarChamado/<br/>Rate finished ticket"]
    Profile["Perfil/<br/>Edit profile"]
    Forgot["RecuperarSenha/<br/>Reset password"]
  end

  subgraph "Business Services (App_Code/Services)"
    US["UserService"]
    TS["TicketService"]
    NS["NotificationService"]
    ES["EmailService"]
  end

  subgraph "Persistence (App_Code/Persist)"
    UBD["UserBD"]
    TBD["TicketBD"]
    NBD["NotificationBD"]
    DBD["DepartmentBD"]
    SBD["ServiceBD"]
    EBD["EquipmentBD"]
    HBD["HistoryBD"]
    CBD["CommentBD"]
    ABD["AuditBD<br/>(read-only)"]
  end

  subgraph "Infrastructure (App_Code/Infra)"
    DH["DataHelper"]
    MP["Mapped<br/>Connection/Command/Adapter"]
    BP["BasePage<br/>CSRF + Access Control"]
    AS["AuditService"]
    TH["ToastHelper"]
    RH["ReportHelper"]
    FN["Function<br/>SHA-512"]
    IR["IRepository<T>"]
  end

  Login --> US
  AnaDash --> TS
  AnaMyTix --> TS
  AnaAllTix --> TS
  AnaDetail --> TS
  AnaDetail --> CBD
  AnaDetail --> HBD
  AnaCollab --> US
  AnaUpdate --> US
  AnaReport --> DH
  AnaAudit --> ABD
  CollabDash --> TS
  CollabDash --> NS
  CollabRate --> TS
  Profile --> US
  Forgot --> US

  US --> UBD
  TS --> TBD
  TS --> HBD
  NS --> NBD
  NS --> TBD

  UBD --> DH
  TBD --> DH
  NBD --> DH
  DBD --> DH
  SBD --> DH
  EBD --> DH
  HBD --> DH
  CBD --> DH

  DH --> MP
  MP --> DB[(MySQL)]
```

---

## Database schema (ER)

```mermaid
erDiagram
  USER ||--o{ TICKET : "requests"
  USER ||--o{ TICKET : "assigned as analyst"
  USER ||--o{ TICKET_HISTORY : "performs"
  USER ||--o{ TICKET_COMMENTS : "writes"
  USER ||--o{ PASSWORD_RESETS : "requests"
  USER }o--|| DEPARTMENT : "belongs to"
  TICKET ||--o{ NOTIFICATION : "triggers"
  TICKET ||--o{ TICKET_HISTORY : "has"
  TICKET ||--o{ TICKET_COMMENTS : "has"
  DEPARTMENT ||--o{ SERVICE : "offers"
  EQUIPMENT ||--o{ SERVICE : "uses"

  USER {
    int user_id PK
    string user_name
    string user_position
    int user_status "1=active"
    string user_typeAnalyst
    string user_email
    string user_password "SHA-512 hash"
    int user_typeAccess "0=analyst,1=collab"
    int user_firstLogin "0=must change"
    int dep_id FK
    string user_image
  }

  TICKET {
    int tic_id PK
    string tic_description
    string tic_localization
    string tic_type
    int tic_status "0=open,1=progress,2=done"
    string tic_openTime
    string tic_closeTime
    int grade
    int rating "1-5"
    string rating_comment
    string rating_at
    int user_id FK
    int ana_analisty_id FK
  }

  NOTIFICATION {
    int not_id PK
    string not_description
    string not_title
    int tic_id FK
    string not_timeMensage
    int not_status
  }

  AUDIT_LOG {
    int log_id PK
    string user_id
    string action
    string detail
    string ip_address
    datetime created_at
  }

  TICKET_HISTORY {
    int history_id PK
    int tic_id FK
    int user_id FK
    string action "CRIADO|ACEITO|FINALIZADO|COMENTARIO|AVALIACAO"
    string description
    string created_at
  }

  TICKET_COMMENTS {
    int comment_id PK
    int tic_id FK
    int user_id FK
    string comment_text
    string created_at
  }

  PASSWORD_RESETS {
    int reset_id PK
    int user_id FK
    string token
    string expires_at
    int used
    string created_at
  }

  DEPARTMENT {
    int dep_id PK
    string dep_sector
  }

  EQUIPMENT {
    int equip_id PK
    string equip_description
    int equip_number
  }

  SERVICE {
    int service_id PK
    string service_solution
    int dep_id FK
    int equip_id FK
  }
```

---

## User flows

### 1. Ticket lifecycle

```mermaid
sequenceDiagram
  actor C as Collaborator
  actor A as Analyst
  participant System

  C->>System: Login (email + password)
  System->>System: Authenticate via UserBD
  System-->>C: Redirect to Dashboard

  C->>System: Create ticket (description, type, location)
  System->>System: Insert ticket, notification, history
  System-->>C: Toast "Chamado criado com sucesso"

  A->>System: Login as analyst
  System-->>A: Dashboard shows open tickets

  A->>System: Click "Accept" on open ticket
  System->>System: Set analyst, status=1, insert notification+history
  System-->>A: Toast success, table refreshes (UpdatePanel)

  A->>System: Click detail on ticket
  System-->>A: Show DetalheChamado page (description, timeline, comments)

  A->>System: Add comment
  System->>System: Insert ticket_comments, update timeline (UpdatePanel)

  A->>System: Finish ticket on My Tickets page
  System->>System: Set status=2, insert notification+history
  System-->>A: Toast "Chamado Finalizado"

  C->>System: Dashboard shows finished ticket
  C->>System: Click "Rate" -> AvaliarChamado page
  C->>System: Submit rating (1-5 stars) + optional comment
  System->>System: Update ticket rating, insert history
  System-->>C: Toast "Obrigado pela sua avaliação"
```

### 2. Account recovery

```mermaid
sequenceDiagram
  actor U as User

  U->>System: Access login page
  U->>System: Click "Esqueci minha senha"
  System-->>U: RecuperarSenha page

  U->>System: Enter email
  System->>System: Find user by email, generate token, store in password_resets
  System-->>U: Redirect with token

  U->>System: RedefinirSenha page with token
  U->>System: Enter new password + confirmation
  System->>System: Validate token (not expired, not used), update password
  System-->>U: Redirect to login with success message
```

### 3. First login (password change)

```mermaid
sequenceDiagram
  actor U as User

  U->>System: Login with default password
  System-->>U: MasterPage detects FirstLogin=0, shows modal
  U->>System: Enter new password + confirmation
  System->>System: Hash password, update user, set FirstLogin=1
  System-->>U: Toast "Senha Alterada com Sucesso"
```

---

## Page routing

| Page              | URL                                                           | Access    | Purpose                        |
| ----------------- | ------------------------------------------------------------- | --------- | ------------------------------ |
| Login             | `/Default.aspx`                                               | Public    | Authenticate                   |
| Analyst Dashboard | `/Pages/Sistema/Analista/Index/Default.aspx`                  | Analyst 0 | Charts + accept tickets        |
| My Tickets        | `/Pages/Sistema/Analista/MeusChamados/Default.aspx`           | Analyst 0 | Progress/finish                |
| All Tickets       | `/Pages/Sistema/Analista/TodosChamados/Default.aspx`          | Analyst 0 | Search + browse                |
| Ticket Detail     | `/Pages/Sistema/Analista/DetalheChamado/Default.aspx?id=N`    | Analyst 0 | View + comment + history       |
| Manage Users      | `/Pages/Sistema/Analista/ListaColaboradores/Default.aspx`     | Analyst 0 | Create/delete/activate users   |
| Edit User         | `/Pages/Sistema/Analista/EditarUsuario/Default.aspx`          | Analyst 0 | Edit user fields               |
| Reports           | `/Pages/Sistema/Analista/Relatorios/Default.aspx`             | Analyst 0 | Filter + export XLS            |
| Audit Log         | `/Pages/Sistema/Analista/LogsAuditoria/Default.aspx`          | Analyst 0 | Read-only log viewer + filters |
| Collab Dashboard  | `/Pages/Sistema/Colaborador/Index/Default.aspx`               | Collab 1  | Create ticket + summary        |
| Rate Ticket       | `/Pages/Sistema/Colaborador/AvaliarChamado/Default.aspx?id=N` | Collab 1  | Rate finished                  |
| Profile           | `/Pages/Sistema/Perfil/Default.aspx`                          | Any       | Edit name/email/pass           |
| Forgot Password   | `/Pages/RecuperarSenha/Default.aspx`                          | Public    | Request reset                  |
| Reset Password    | `/Pages/RedefinirSenha/Default.aspx?token=T`                  | Public    | Execute reset                  |
| Error 400         | `/Pages/PageError/Error400.aspx`                              | Public    | Bad request                    |
| Error 403         | `/Pages/PageError/Error403.aspx`                              | Public    | Forbidden / access denied      |
| Error 404         | `/Pages/PageError/Error404.aspx`                              | Public    | Page not found                 |
| Error 500         | `/Pages/PageError/Error.aspx`                                 | Public    | Internal server error          |

---

## How to run

### Docker (recommended)

```bash
docker compose up -d --build
```

- Web: http://localhost:8080
- MySQL: localhost:3306 (user `root`, password `root`, database `base`)

### Manual (requires Mono + MySQL)

```bash
# 1. Restore NuGet packages
nuget restore packages.config -PackagesDirectory packages
mkdir -p bin
cp packages/MySql.Data.8.0.29/lib/net48/MySql.Data.dll bin/
cp packages/Ubiety.Dns.Core.2.2.1/lib/netstandard2.0/Ubiety.Dns.Core.dll bin/

# 2. Configure Web.config (replace ${DB_HOST}, ${DB_USER}, ${DB_PASS})
# 3. Run with xsp4
xsp4 --port 80 --nonstop
```

### Default users

| Email               | Password   | Role            |
| ------------------- | ---------- | --------------- |
| `admin@gcdesk.com`  | `admin123` | Analyst (admin) |
| `ana@gcdesk.com`    | `123456`   | Analyst         |
| `carlos@gcdesk.com` | `123456`   | Analyst         |
| `joao@gcdesk.com`   | `123456`   | Collaborator    |
| `maria@gcdesk.com`  | `123456`   | Collaborator    |
| `pedro@gcdesk.com`  | `123456`   | Collaborator    |
| `julia@gcdesk.com`  | `123456`   | Collaborator    |
| `lucas@gcdesk.com`  | `123456`   | Collaborator    |

> On first login with any `123456` user, the system forces a password change.

---

## Key design decisions

| Decision                  | Rationale                                                                                                                                      |
| ------------------------- | ---------------------------------------------------------------------------------------------------------------------------------------------- |
| **`IRepository<T>`**      | Single CRUD contract for all entities; Persist classes also expose static methods for DataSet-based queries                                    |
| **`DataHelper`**          | Eliminates repeated connection/command/dispose boilerplate; filters null params automatically                                                  |
| **`BasePage`**            | Every page inherits CSRF protection (`ViewStateUserKey`), access control, and audit logging                                                    |
| **`UpdatePanel`**         | Async postbacks for table actions (delete/activate/claim/comment) without full page reload                                                     |
| **SHA-512 hashing**       | Passwords stored as Base64 SHA-512 hashes; `Function.HashText()` is the single hash entry point                                                |
| **ToastHelper**           | Centralized Bootstrap toast markup for success/warning/error messages                                                                          |
| **Static Map methods**    | Each Persist class maps `IDataReader` rows to entities; `HasColumn()` handles optional rating columns                                          |
| **Mixed static/instance** | Static methods used for simple reads/writes where no instance state is needed; instance methods used for `IRepository<T>` interface compliance |

---

## Security controls

- **CSRF**: Anti-forgery token stored in session + `ViewStateUserKey`, validated on every postback (`BasePage.cs:29`)
- **Access control**: `RequiredAccessType` abstract property enforces role-based access per page (`BasePage.cs:57`)
- **CSP**: Content-Security-Policy header set in `Global.asax` with whitelisted CDN origins
- **X-Frame-Options**: `DENY` to prevent clickjacking
- **X-Content-Type-Options**: `nosniff` to prevent MIME sniffing
- **SQL injection**: All queries use parameterized statements via `Mapped.Parameter()`
- **Audit trail**: All user actions (login, logout, ticket create/accept/finish, comment, rate, user create/deactivate/activate/update, profile update, report export, access denials, CSRF failures) are logged to `audit_log` via `AuditService` + `BasePage.LogAction`

- **Audit log viewer**: `/Pages/Sistema/Analista/LogsAuditoria/Default.aspx` is **read-only** (no mutations allowed) and supports filtering by action, user id, and date range
- **Session timeout**: 30 minutes (`InProc` mode)

---

## Project structure

```
├── App_Code/
│   ├── Class/           # Domain entities
│   ├── Infra/           # Cross-cutting infrastructure
│   │   ├── DataHelper.cs    # ADO.NET wrapper
│   │   ├── Mapped.cs        # MySQL connection/command/parameter factory
│   │   ├── BasePage.cs      # CSRF + access control base class
│   │   ├── AuditService.cs  # Audit trail logging
│   │   ├── ToastHelper.cs   # Bootstrap toast builder
│   │   ├── ReportHelper.cs  # HTML/Excel export
│   │   ├── Function.cs      # SHA-512 hashing
│   │   └── IRepository.cs   # Generic CRUD interface
│   ├── Persist/         # Data access (IRepository<T> + static helpers)
│   │   └── AuditBD.cs       # Read-only audit_log queries
│   └── Services/        # Business logic
├── css/                 # Custom styles
├── db/init.sql          # Schema + seed data
├── image/               # Static assets (logos, backgrounds, error page SVGs)
├── js/                  # Custom scripts
├── uploads/             # User uploaded files (profile images — UUID filenames)
├── Pages/
│   ├── Master/          # MasterPage (layout + nav)
│   ├── PageError/       # Error pages (400, 403, 404, 500)
│   └── Sistema/
    │       ├── Analista/    # Dashboard, MeusChamados, TodosChamados, DetalheChamado, ListaColaboradores, EditarUsuario, Relatorios, LogsAuditoria
    │       ├── Colaborador/ # Index, AvaliarChamado
    │       └── Perfil/
    │   ├── RecuperarSenha/
    │   └── RedefinirSenha/
├── Default.aspx         # Login page
├── Global.asax          # CSP headers + app events
├── Web.config           # App settings, security, compilation
├── packages.config      # NuGet dependencies
├── Dockerfile           # Mono + xsp4 image
└── docker-compose.yml   # Web + MySQL orchestration
```
