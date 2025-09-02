# Projeto de Avaliação para Desenvolvedor da Elogica
Uma aplicação .NET 8.0 demonstrando padrões de arquitetura de software modernos e melhores práticas. Este projeto apresenta Domain-Driven Design (DDD), padrão CQRS/Mediator, Entity Framework Core com PostgreSQL e testes unitários abrangentes.

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download/dotnet/8.0)
<img width="1512" height="1268" alt="swagger" src="https://github.com/user-attachments/assets/5ba7b45d-1603-41b2-877c-899fd006dfb7" />
<img width="1485" height="664" alt="unit-tests" src="https://github.com/user-attachments/assets/357c1f0e-b2eb-4cbb-93b9-4f08837a079e" />

## 🚀 Funcionalidades

- **Domain-Driven Design** com clara separação de responsabilidades
- **Padrão CQRS** usando MediatR para manipulação de requisições/respostas
- **Entity Framework Core** com banco de dados PostgreSQL
- **Autenticação JWT** com autorização baseada em funções
- **Testes Unitários** com xUnit e NSubstitute
- **Documentação da API** com Swagger/OpenAPI
- **Logging Estruturado** com Serilog
- **Health Checks** para monitoramento

## 📋 Índice

- [Pré-requisitos](#pré-requisitos)
- [Primeiros Passos](#primeiros-passos)
- [Configuração](#configuração)
- [Configuração do Banco de Dados](#configuração-do-banco-de-dados)
- [Executando a Aplicação](#executando-a-aplicação)
- [Testes](#testes)
- [Estrutura do Projeto](#estrutura-do-projeto)
- [Stack Tecnológico](#stack-tecnológico)
- [Documentação da API](#documentação-da-api)
- [Regras de Negócio](#regras-de-negócio)
- [Contribuindo](#contribuindo)

## 🛠 Pré-requisitos

Certifique-se de ter o seguinte instalado:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL 14+]
- [Git](https://git-scm.com/)
- IDE: [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [JetBrains Rider](https://www.jetbrains.com/rider/)

## 🚀 Primeiros Passos

### 1. Clonar o Repositório

```bash
git clone https://github.com/your-username/ambev-developer-evaluation.git
cd ambev-developer-evaluation
```

### 2. Mudar para a Branch de Desenvolvimento

```bash
git checkout develop
```

### 3. Compilar a Solução

```bash
cd src
dotnet build
```

## ⚙️ Configuração

### Configurações da Aplicação

Atualize `src/TaskManager.DeveloperEvaluation.WebApi/appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=AmbevDevEval;Username=<SEU_USUARIO_DB>;Password=<SUA_SENHA_DB>"
  },
  "Jwt": {
    "Key": "SuaChaveJwtForteAqui",
    "Issuer": "TaskManager.Dev.Eval",
    "Audience": "TaskManager.Dev.Eval"
  },
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning"
      }
    },
    "WriteTo": [
      { "Name": "Console" }
    ]
  }
}
```

**Notas Importantes de Configuração:**
- Substitua `<SEU_USUARIO_DB>` e `<SUA_SENHA_DB>` pelas suas credenciais do PostgreSQL
- Use uma chave JWT segura (mínimo de 32 caracteres)
- Ajuste os níveis de logging conforme necessário

## 🗄️ Configuração do Banco de Dados

### 1. Criar Banco de Dados PostgreSQL

```bash
# Conectar ao SQL Server
\q
```

### 2. Aplicar Migrações do Entity Framework

```bash
cd src/TaskManager.DeveloperEvaluation.ORM
dotnet ef database update --context DefaultContext --startup-project ../TaskManager.DeveloperEvaluation.WebApi
```

### 3. Criar Nova Migração (se necessário)

```bash
dotnet ef migrations add <NomeDaMigracao> \
  --project ../TaskManager.DeveloperEvaluation.ORM \
  --startup-project . \
  --context DefaultContext
```

## 🏃‍♂️ Executando a Aplicação

### Iniciar a API Web

```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet run
```

A aplicação estará disponível em:
- **HTTPS**: `https://localhost:7181`
- **Swagger UI**: `https://localhost:7181/swagger`

### Autenticação no Swagger

1. Clique no ícone de cadeado no Swagger UI
2. Digite: `Bearer <Seu_Token_JWT>`
3. Obtenha o token JWT através do endpoint `/api/auth/login`

## 🧪 Testes

### Executar Testes Unitários

```bash
cd tests/TaskManager.DeveloperEvaluation.Unit
dotnet test
```

**Framework de Testes:**
- **xUnit** para framework de testes
- **FluentAssertions** para asserções legíveis
- **NSubstitute** para mocks

## 📁 Estrutura do Projeto

```
root/
├── src/
│   ├── TaskManager.DeveloperEvaluation.Application/   # Handlers CQRS, DTOs, Perfis
│   ├── TaskManager.DeveloperEvaluation.Common/        # Utilitários compartilhados
│   ├── TaskManager.DeveloperEvaluation.Domain/        # Entidades e lógica do domínio
│   ├── TaskManager.DeveloperEvaluation.ORM/           # Infraestrutura EF Core
│   ├── TaskManager.DeveloperEvaluation.IoC/           # Injeção de dependência
│   └── TaskManager.DeveloperEvaluation.WebApi/        # Controllers da API e startup
└── tests/
    └── TaskManager.DeveloperEvaluation.Unit/          # Testes unitários
```

### Responsabilidades das Camadas

| Camada | Propósito |
|--------|-----------|
| **Application** | Comandos/Consultas CQRS, perfis AutoMapper, FluentValidation |
| **Domain** | Entidades de negócio, objetos de valor, lógica do domínio, interfaces de repositório |
| **ORM** | Contexto EF Core, implementações de repositório, migrações do banco |
| **IoC** | Configuração de injeção de dependência e registro de módulos |
| **WebApi** | API HTTP, controllers, middleware, autenticação |

## 🛠 Stack Tecnológico

### Tecnologias Principais
- **.NET 8.0** - Framework mais recente com recursos do C# 12
- **SQL Server** - Banco de dados relacional robusto
- **Entity Framework Core 8.0** - ORM com provedor SQL Server

### Padrões de Arquitetura
- **Domain-Driven Design (DDD)** - Arquitetura centrada no domínio
- **CQRS** - Segregação de Responsabilidade de Comando e Consulta
- **Padrão Mediator** - Manipulação de requisições desacoplada
- **Padrão Repository** - Abstração de acesso a dados

### Bibliotecas e Ferramentas
- **MediatR** - Messaging em processo
- **AutoMapper** - Mapeamento objeto-para-objeto
- **FluentValidation** - Validação de entrada
- **Serilog** - Logging estruturado
- **JWT Bearer** - Autenticação e autorização
- **Swagger/OpenAPI** - Documentação da API

## 📚 Documentação da API

### Autenticação

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| POST | `/api/auth/login` | Autenticar usuário e receber token JWT |

### Gerenciamento de Usuários

| Método | Endpoint | Descrição | Autorização |
|--------|----------|-----------|-------------|
| GET | `/api/users` | Listar todos os usuários (paginado) | Obrigatória |
| POST | `/api/users` | Criar novo usuário | Obrigatória |
| GET | `/api/users/{id}` | Obter usuário por ID | Obrigatória |
| PUT | `/api/users/{id}` | Atualizar usuário | Obrigatória |
| DELETE | `/api/users/{id}` | Excluir usuário | Obrigatória |

## 🤝 Contribuindo

Seguimos o fluxo de trabalho **GitFlow** para desenvolvimento:

### Estratégia de Branches

| Branch | Propósito |
|--------|-----------|
| `main` | Código pronto para produção |
| `develop` | Branch de integração |
| `feature/*` | Novas funcionalidades (ex: `feature/sales-crud`) |

### Fluxo de Trabalho de Desenvolvimento

1. **Criar Branch de Feature**
   ```bash
   git checkout develop
   git checkout -b feature/<descricao-curta>
   ```

2. **Fazer Commit das Alterações**
   ```bash
   git add .
   git commit -m "feat: adicionar lógica de cancelamento do carrinho"
   ```

3. **Fazer Push e Criar PR**
   ```bash
   git push origin feature/<descricao-curta>
   ```

4. **Abrir Pull Request** contra a branch `develop`

### Padrões de Qualidade de Código

- ✅ Seguir convenções de nomenclatura C# (PascalCase para público, camelCase para local)
- ✅ Escrever testes unitários para todos os novos handlers/lógica
- ✅ Garantir que todos os testes passem antes do merge
- ✅ Usar mensagens de commit semânticas (`feat:`, `fix:`, `docs:`, etc.)
- ✅ Manter métodos focados e aplicar o Princípio da Responsabilidade Única

### Checklist Pré-merge

- [ ] Todos os testes unitários passam
- [ ] Migrações do EF Core estão atualizadas
- [ ] Swagger UI reflete novos endpoints
- [ ] Código segue convenções do projeto
- [ ] PR foi revisado e aprovado

## 📄 Licença

Este projeto está licenciado sob a Licença MIT - veja o arquivo [LICENSE](LICENSE) para detalhes.

## 🆘 Suporte

Se você encontrar algum problema ou tiver dúvidas:

1. Verifique as [issues existentes](https://github.com/your-username/ambev-developer-evaluation/issues)
2. Crie uma nova issue com descrição detalhada
3. Inclua passos para reproduzir o problema

---

**Bom Código! 🚀**
