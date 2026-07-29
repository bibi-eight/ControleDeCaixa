# 🛒 Bibi's Market - Controle de Caixa

Sistema desktop desenvolvido em **C#**, **.NET 8** e **WPF**, com foco no gerenciamento de caixas e movimentações financeiras de um mercado.

O projeto foi desenvolvido utilizando boas práticas de arquitetura em camadas, MVVM, Entity Framework Core e validações com FluentValidation.

---

## ✨ Funcionalidades

### Caixas

- ✅ Cadastro de caixas
- ✅ Edição de caixas
- ✅ Exclusão lógica (Soft Delete)
- ✅ Listagem de caixas
- ✅ Exibição do saldo atual
- ✅ Indicador visual de saldo mínimo

### Movimentações

- ✅ Cadastro de entradas
- ✅ Cadastro de saídas
- ✅ Edição de movimentações
- ✅ Exclusão lógica (Soft Delete)
- ✅ Atualização automática do saldo

### Validações

- Nome obrigatório
- Saldo mínimo maior ou igual a zero
- Descrição obrigatória
- Categoria obrigatória
- Valor maior que zero

---

## 🏗 Arquitetura

O projeto foi organizado em camadas:

```text
ControleCaixa
│
├── ControleCaixa.UI
│   ├── Views
│   ├── ViewModels
│   └── Components
│
├── ControleCaixa.Business
│   ├── Services
│   ├── DTOs
│   ├── Validators
│   └── Interfaces
│
├── ControleCaixa.Data
│   ├── Context
│   ├── Mappings
│   ├── Repositories
│   └── Interfaces
│
├── ControleCaixa.Model
│   ├── Entities
│   ├── Enums
│   └── ValueObjects
│
└── ControleCaixa.Tests
```

---

## 🛠 Tecnologias utilizadas

- .NET 8
- C#
- WPF
- MVVM
- Entity Framework Core
- SQL Server
- FluentValidation
- CommunityToolkit.Mvvm
- xUnit
- Moq

---

## 📦 Padrões utilizados

- Repository Pattern
- Unit of Work
- MVVM
- Injeção de Dependência
- SOLID
- Clean Code

---

## 🗄 Banco de Dados

Banco utilizado:

- SQL Server

O projeto utiliza Entity Framework Core com Migrations.

### Criando o banco

Execute:

```bash
dotnet ef database update
```

ou, pelo Package Manager:

```powershell
Update-Database
```

---

## ⚙ Configuração

Edite o arquivo:

```text
appsettings.json
```

Altere a Connection String:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=SEU_SERVIDOR;Database=ControleCaixa;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

---

## 🚀 Como executar

Clone o repositório:

```bash
git clone https://github.com/seu-usuario/ControleCaixa.git
```

Entre na pasta:

```bash
cd ControleDeCaixa
```

Restaure os pacotes:

```bash
dotnet restore
```

Atualize o banco:

```bash
dotnet ef database update --project ControleCaixa.Data --startup-project ControleCaixa.UI
```
Execute:

```bash
dotnet run --project ControleCaixa.UI
```


---

## 🧪 Testes

Para executar os testes:

```bash
dotnet test
```

---

## 📂 Migrations

As migrations estão disponíveis no projeto **ControleCaixa.Data**.

Caso seja necessário recriar o banco:

```bash
dotnet ef database update
```

---

## 🚀 Futuras melhorias

Algumas funcionalidades que podem ser implementadas em versões futuras do projeto:

- 📅 Filtro de movimentações por período.
- 📊 Dashboard com gráficos e indicadores financeiros.
- 🔎 Busca por descrição e categoria.
- 🏷️ Cadastro de categorias personalizadas.
- 💰 Transferência de saldo entre caixas.
- 🔔 Notificações quando o saldo estiver abaixo do mínimo.
- 🧪 Maior cobertura de testes unitários e de integração.

## 👩‍💻 Desenvolvido por

**Brenda Regina Ribeiro de Brito**

Backend Developer .NET