## Projeto: Plataforma Escolar - Microserviços com .NET 9

### Visão Geral
Este projeto simula uma plataforma de gestão escolar para ensino médio, implementada com uma arquitetura moderna baseada em microserviços, seguindo os padrões CQRS, DDD, Clean Architecture, SOLID e separação por camadas.

---

## ✅ Tecnologias Utilizadas
- **.NET 9**
- **PostgreSQL**
- **MassTransit + RabbitMQ** (mensageria)
- **YARP (Gateway)**
- **Serilog + Seq** (logs centralizados)
- **Docker Compose** (orquestração)

---

## 🎓 Microserviços Criados

### 1. `Professores.API`
- CRUD completo de professores
- Publica eventos: `ProfessorCriadoEvent`, `ProfessorAtualizadoEvent`
- Consumido por: `Grade.API` (projeção local)

### 2. `Alunos.API`
- CRUD de alunos
- Publica eventos: `AlunoCriadoEvent`, `AlunoTransferidoEvent`
- Consumido por: `Grade.API`, `Alunos.API` (projeção de turmas)

### 3. `Grade.API`
- CRUD da grade horária semanal
- Consome eventos de Professores, Alunos, Turmas, Matérias
- Publica eventos como: `GradeHorarioAtualizadaEvent`

### 4. `Turmas.API`
- CRUD de turmas
- Publica: `TurmaCriadaEvent`, `TurmaAtualizadaEvent`
- Consumido por: `Grade.API`, `Alunos.API`

### 5. `Materias.API`
- CRUD de matérias
- Publica: `MateriaCriadaEvent`, `MateriaAtualizadaEvent`, `MateriaRemovidaEvent`
- Consumido por: `Grade.API`

### 6. `Gateway.API`
- Gateway reverso com **YARP**
- Roteia rotas como `/api/professores` para `Professores.API`

---

## 📧 Mensageria com RabbitMQ
- Todos os microserviços estão integrados via **RabbitMQ** usando **MassTransit**
- Cada evento publicado vai para uma **exchange** do tipo `fanout`
- Consumers estão conectados a filas dedicadas por serviço
- Projeções locais criadas a partir dos eventos recebidos

---

## 🌐 Gateway com YARP
- Porta de entrada única da aplicação
- Repassa chamadas para os microserviços corretos

---

## 📊 Observabilidade com Seq + Serilog
- Todos os MS possuem **Serilog** configurado
- Os logs são enviados para o **Seq** via `http://seq:80`
- Cada log inclui informações de contexto (serviço, thread, etc)

---

## 📁 Banco de Dados
- Usamos **PostgreSQL** com `docker-compose`
- Cada MS possui seu próprio schema e `DbContext`
- Migrations criadas com `dotnet ef migrations` por serviço

---

## ✅ Testes
- Testes **unitários** para todos os comandos, handlers, services
- Testes **de integração** com `MassTransit.Testing` para todos os consumers

---

## 📊 Diagrama de Arquitetura

```
+-------------+     RabbitMQ      +------------+
| Professores |------------------>|   Grade    |
+-------------+                   +------------+
                                        |
+-------------+     RabbitMQ      +------------+
|   Alunos    |------------------>|   Grade    |
+-------------+                   +------------+
                                        |
+-------------+                   +------------+
|   Turmas    |------------------>|   Grade    |
+-------------+                   +------------+
                                        |
+-------------+                   +------------+
|  Materias   |------------------>|   Grade    |
+-------------+                   +------------+

     \                                    /
      \                                  /
       +---------------+----------------+
                    Gateway
```

---

## 💡 Manual de Uso com Front-End

### 🚀 Como consumir a API
O front-end (ex: Angular, React) pode consumir as APIs através do gateway:

| Microserviço  | Endpoint via Gateway                   |
|---------------|-----------------------------------------|
| Professores   | `GET /api/professores`                 |
| Alunos        | `GET /api/alunos`                      |
| Grade         | `GET /api/grade`                       |
| Turmas        | `GET /api/turmas`                      |
| Matérias      | `GET /api/materias`                    |

### ⚖ Exemplo de chamada com `fetch`
```js
fetch("http://localhost:5000/api/professores")
  .then(res => res.json())
  .then(console.log)
```

---

## 📚 Considerações Finais
Este projeto cobre:
- Arquitetura limpa com microserviços independentes
- Alta coesão e baixo acoplamento via eventos
- Observabilidade completa com Seq
- Gateway centralizado com expansão futura para autenticação, cache, rate-limit e versionamento