# 📚 Plataforma Escolar - Microserviços com .NET 9

Este projeto simula uma aplicação escolar estruturada com .NET 9 em arquitetura de microserviços, mensageria com RabbitMQ, logs centralizados com Seq e gateway reverso com YARP.

---

## 🐳 Inicialização com Docker

> Pré-requisitos: Docker e Docker Compose instalados.

### ✅ Passos

1. Clone o repositório
2. Acesse a raiz do projeto
3. Execute:

```bash
docker-compose build
docker-compose up -d
```

Se desejar limpar e subir de novo

```bash
docker-compose down --volumes
docker-compose build --no-cache
docker-compose up -d
```

> Isso irá subir todos os serviços: PostgreSQL, RabbitMQ, Seq, Gateway e os microserviços (`Professores`, `Alunos`, `Grade`, `Turmas`, `Matérias`).

---

## 🔗 Acessos importantes

| Serviço        | URL                    | Observações                          |
| -------------- | ---------------------- | ------------------------------------ |
| Gateway (YARP) | http://localhost:5000  | Porta de entrada para o sistema      |
| RabbitMQ       | http://localhost:15672 | Login: `guest`, Senha: `guest`       |
| Seq (Logs)     | http://localhost:5341  | Login: `admin`, Senha: `admin`       |
| PostgreSQL     | localhost:5432         | Login: `postgres`, Senha: `postgres` |

---

## 🧪 Acesso ao Scalar (por microserviço)

Cada microserviço possui o Scalar ativado por padrão.

Se quiser acessar diretamente (sem gateway), use:

| Microserviço    | Scalar URL                      |
| --------------- | ------------------------------- |
| Professores.API | http://localhost:5001/scalar/v1 |
| Alunos.API      | http://localhost:5003/scalar/v1 |
| Grade.API       | http://localhost:5005/scalar/v1 |
| Turmas.API      | http://localhost:5007/scalar/v1 |
| Materias.API    | http://localhost:5009/scalar/v1 |

---

## 🌐 Roteamento via Gateway

Exemplos de requisições ao gateway:

```bash
GET http://localhost:5000/api/professores
GET http://localhost:5000/api/alunos
GET http://localhost:5000/api/grade
GET http://localhost:5000/api/turmas
GET http://localhost:5000/api/materiais
```

> O gateway redireciona automaticamente as chamadas para o microserviço correto via YARP.

---

## 🗂 Banco de Dados

Todos os microserviços compartilham o mesmo PostgreSQL, mas cada um possui seu próprio schema. As migrations são aplicadas automaticamente ao iniciar cada serviço.

---

## ✅ Conclusão

Com esse setup, você tem:

- Microserviços isolados com CQRS, DDD e SOLID
- Mensageria assíncrona via RabbitMQ
- Observabilidade com Serilog e Seq
- Gateway reverso com roteamento entre APIs
- Totalmente orquestrado com Docker Compose

---

🚀 Para dúvidas ou melhorias, sinta-se à vontade para abrir uma issue ou contribuir.
