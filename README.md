# 💰 Finanças

**Finanças** é um sistema de gerenciamento financeiro que permite o controle de receitas e despesas, fornecendo uma visão clara do fluxo de caixa por meio de gráficos e tabelas. Com autenticação segura e controle de acesso, ele garante que cada usuário visualize apenas seus próprios dados.

## 🛠️ Tecnologias Utilizadas
- **Frontend:** Vue.js 2 + Vuetify 2
- **Backend:** .NET Core (C#) com Entity Framework
- **Banco de Dados:** PostgreSQL
- **Autenticação:** JWT 
- **Documentação da API:** Swagger
- **Testes Automatizados:** xUnit (backend) 

## 🏗️ Como Executar o Projeto

### 📌 Pré-requisitos
Antes de começar, certifique-se de ter instalado:
- **.NET 8** (versão compatível com o projeto)
- **Node.js** (para o frontend)
- **PostgreSQL** (para o banco de dados)

### 🔧 Passos para Rodar o Backend (.NET Core)
1. Clone este repositório:  
   ```bash
   git clone https://github.com/becamello/financas.git
   cd financas
2. Navegue até a pasta do backend: 
   ```bash
   cd backend/src/Financas.Api
3. Compile e execute a API:
   ```bash
   dotnet build
   dotnet run
### 🔧 Passos para Rodar o Frontend (Vue.js)
1. Instale as dependências: 
   ```bash
   cd frontend/financas
   npm install
2. Inicie a aplicação:
   ```bash
   npm run serve
## 📝 Documentação da API
A API está documentada utilizando Swagger. Após iniciar o backend, acesse:
📌 http://localhost:5047/
