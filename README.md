<h1> Reski AI </h1>

<p> O Reski AI é uma plataforma voltada para requalificação profissional, ajudando pessoas a se prepararem para as novas demandas do mercado de trabalho. </br>
O sistema permite que o usuário cadastre seu perfil, habilidades e objetivos de carreira, e converse diretamente com uma Inteligência Artificial para poder solicitar trilhas de aprendizado alinhadas ao cargo desejado. </p>

---

## 👥 Integrantes
 
- Iris Tavares Alves 557728 </br>
- Taís Tavares Alves 557553 </br>

---

## 🎬 Vídeo

> <a href="">Vídeo </a>

---

## ⚙️ Tecnologias

- .NET 9 + .NET 8 WebAPI
- Entity Framework Core
- Oracle
- JWT Bearer
- Swagger / OpenAPI 
- xUnit
- ML.NET
  
---

### ⚠️ Checar versões SDK (garanta que tenha ambas as versões 8 e 9 do SDK instaladas)
> <a href="https://dotnet.microsoft.com/pt-br/download/dotnet/9.0">SDK 9 Download</a>

> <a href="https://dotnet.microsoft.com/en-us/download/dotnet/8.0">SDK 8 Download</a>

### ⚠️ Para testar o Swagger estar na seguinte URL
```text
http://localhost:5179/swagger/index.html
```
### ⚠️ Comando para gerar sua JWT Key no PowerShell
```text
[Convert]::ToBase64String((1..32 | ForEach-Object { Get-Random -Minimum 0 -Maximum 256 }))
```

---

### 1. Clone o repositório
```text
git clone https://github.com/Irissuu/Reski_Ai_sharp.git
```

### 2. Restaure dependências
```text
dotnet restore
```

### 3. Coloque suas credenciais e a JWT Key
```text
"ConnectionStrings": {
    "Oracle": "User Id=;Password=;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));"
  },
  "Jwt": {
    "Key": "",
```

### 4. Gere o banco de dados com EF Core
```text
dotnet ef migrations add Inicial
dotnet ef database update
```

### 5. Execute o projeto
```text
dotnet run
```

---
## 🔁 Rotas Disponíveis (via Swagger)

### AuthController

| Método | Rota                   | Descrição               |
|--------|------------------------|--------------------------|
| POST   | `/api/v1/auth/login`  | Faz login e gera JWT    |


### UsuarioController

| Método | Rota                        | Descrição               |
|--------|-----------------------------|--------------------------|
| GET    | `/api/v1/usuarios/{id}`     | Busca um usuário por ID |
| POST   | `/api/v1/usuarios`          | Cria um usuário         |
| PUT    | `/api/v1/usuarios/{id}`     | Atualiza um usuário     |
| DELETE | `/api/v1/usuarios/{id}`     | Remove um usuário       |


### ObjetivoController 

| Método | Rota                                   | Descrição                             |
|--------|----------------------------------------|----------------------------------------|
| GET    | `/api/v1/objetivos`                    | Lista todos os objetivos               |
| GET    | `/api/v1/objetivos/{id}`               | Busca objetivo por ID                  |
| POST   | `/api/v1/objetivos`                    | Cria um objetivo                       |
| PUT    | `/api/v1/objetivos/{id}`               | Atualiza um objetivo                   |
| DELETE | `/api/v1/objetivos/{id}`               | Remove um objetivo                     |
| POST   | `/api/v1/objetivos/sugerir-nivel`      | Sugere nível baseado no ML.NET         |


### OracleIntegrationController

| Método | Rota                                              | Descrição                                           |
|--------|---------------------------------------------------|------------------------------------------------------|
| POST   | `/api/v1/oracle/usuarios/from-procedure`          | Insere usuário via procedure Oracle                  |
| GET    | `/api/v1/oracle/usuarios/{id}/perfil-json`        | Gera perfil JSON via função Oracle                   |
| POST   | `/api/v1/oracle/compatibilidade`                  | Calcula compatibilidade via procedure Oracle         |
| GET    | `/api/v1/oracle/dataset-json`                     | Exporta dataset JSON gerado pelo Oracle              |

### TrilhaController

| Método | Rota                              | Descrição                    |
|--------|-----------------------------------|-------------------------------|
| GET    | /api/v1/trilhas                   | Lista todas as trilhas       |
| GET    | /api/v1/trilhas/{id}              | Busca trilha por ID          |
| POST   | /api/v1/trilhas                   | Cadastra uma nova trilha     |
| PUT    | /api/v1/trilhas/{id}              | Atualiza uma trilha          |
| DELETE | /api/v1/trilhas/{id}              | Remove uma trilha            |

---

### ⚠️ Instruções para testar o swagger
Ao realizar o POST de usuário em seguida realize o AUTH utilizando o email e a senha cadastradas, copie o Token sem aspas e outras escritas além dele. 

---

## 📧 Testes Swagger 
## ⋆˚꩜｡ USUARIO
### POST /api/v1/usuarios
```text
{
  "nome": "Julia Almeida",
  "email": "jujualmei@gmail.com",
  "senha": "RmcTraiblazer",
  "cpf": "04617849556"
}
```


### POST /api/v1/auth/login
```text
{
  "email": "jujualmei@gmail.com",
  "senha": "rmctraiblazer"
}
```

### PUT /api/v1/usuarios/{id}
```text
{
  "nome": "Julia Almeida",
  "email": "jujualmei@gmail.com",
  "senha": "TheDeliverer"
}
```

## ⋆˚꩜｡ TRILHA
### POST /api/v1/trilhas  
```text
{
  "status": "estudando",
  "conteudo": "Direito Penal e Processual Penal",
  "competencia": "Direito Penal"
}
```

### PUT /api/v1/trilhas/{id} 
```text
{
  "status": "finalizado",
  "conteudo": "Direito Penal e Processual Penal",
  "competencia": "Direito Penal"
}
```

## ⋆˚꩜｡ OBJETIVO
### POST /api/v1/objetivos
```text
{
  "cargo": "Perito forense digital",
  "area": "Cibersegurança",
  "descricao": "Investiga incidentes de segurança, recupera e analisa dados digitais para reconstruir eventos e apresentar evidências. ",
  "demanda": "Alta e crescente"
}
```

### PUT /api/v1/objetivos/{id}
```text
{
  "cargo": "Red Team",
  "area": "Cibersegurança",
  "descricao": "Profissionais que atuam de forma ofensiva, simulando adversários reais para testar a defesa da empresa. ",
  "demanda": "Alta e crescente"
}
```

### POST /api/v1/objetivos/sugerir-nivel
```text
{
  "cargo": "Perito forense digital",
  "area": "Cibersegurança",
  "descricao": "Investiga incidentes de segurança, recupera e analisa dados digitais para reconstruir eventos e apresentar evidências. ",
  "demanda": "Alta e crescente"
}
```

## ⋆˚꩜｡ ORACLE INTEGRATION
### POST /api/v1/oracle/usuarios/from-procedure
```text
{
  "nome": "Gustavo Ferreira",
  "email": "gusferrei@gmail.com",
  "senha": "HatsuneMiku",
  "cpf": "47401130408"
}
```

### POST /api/v1/oracle/compatibilidade
```text
{
  "email": "gusferrei@gmail.com",
  "cpf": "47401130408",
  "pontuacaoUsuario": 80,
  "pontuacaoVaga": 100
}
```

---

## 📧 Testes xUnit

### 1. Entre na pasta de testes
<img width="1835" height="979" alt="image" src="https://github.com/user-attachments/assets/eaaa5673-c262-420e-8353-080bf5aa2ce3" />


### 2. Restaure dependências
```text
dotnet restore
```

### 3. Rodar TODOS os testes
```text
dotnet test
```

---

## 🧾 Consulta no banco Oracle

Para visualizar os dados diretamente no Oracle SQL Developer, use **aspas nos nomes das tabelas**:

```sql
select * from "UsuarioCsharp";
select * from "TrilhaCsharp";
select * from "ObjetivoCsharp";
```
