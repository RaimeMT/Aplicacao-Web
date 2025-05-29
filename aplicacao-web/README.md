# Loja do Seu Manoel - Sistema de Embalagem de Pedidos

## 📦 Sobre o Projeto

API REST desenvolvida em .NET 8 para automatizar o processo de embalagem de pedidos da Loja do Seu Manoel, com integração ao SQL Server via Docker.

---

## 🚀 Pré-requisitos

- Docker
- Docker Compose

---

## ⚡ Como rodar a aplicação

1. **Suba os containers com Docker Compose:**
   ```bash
   docker-compose up --build -d
   ```

2. **Acesse o Swagger para testar a API:**
   - [http://localhost:5000](http://localhost:5000)
   - Ou [https://localhost:5001](https://localhost:5001)

---

## 🗄️ Configuração do Banco de Dados

- O SQL Server é iniciado automaticamente via Docker Compose.
- A string de conexão já está configurada no arquivo `appsettings.json`:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=db;Database=LojaDoManoel;User Id=sa;Password=TesteDev1234;TrustServerCertificate=True;"
  }
  ```
- O serviço do banco está definido no `docker-compose.yml` e não requer configuração manual.

---

## 📝 Observações

- Não é necessário instalar dependências locais, apenas Docker e Docker Compose.
- Toda a documentação dos endpoints está disponível no Swagger.
- Para parar a aplicação:
  ```bash
  docker-compose down
  ```

---

## 🛠️ Dicas

- Se quiser rodar localmente sem Docker, basta ter o .NET 8 SDK instalado e executar:
  ```bash
  dotnet run
  ```

---

## 🧪 Exemplo de JSON para testar no Swagger

Use este exemplo no endpoint `POST /api/embalagem`:

```json
{
  "pedidos": [
    {
      "pedidoId": 1,
      "produtos": [
        { "produtoId": "PS5", "dimensoes": { "altura": 40, "largura": 10, "comprimento": 25 } },
        { "produtoId": "Volante", "dimensoes": { "altura": 40, "largura": 30, "comprimento": 30 } },
        { "produtoId": "Fone", "dimensoes": { "altura": 15, "largura": 20, "comprimento": 10 } }
      ]
    },
    {
      "pedidoId": 2,
      "produtos": [
        { "produtoId": "Joystick", "dimensoes": { "altura": 15, "largura": 20, "comprimento": 10 } },
        { "produtoId": "Fifa 24", "dimensoes": { "altura": 10, "largura": 30, "comprimento": 10 } },
        { "produtoId": "Call of Duty", "dimensoes": { "altura": 30, "largura": 15, "comprimento": 10 } },
        { "produtoId": "Headset", "dimensoes": { "altura": 25, "largura": 15, "comprimento": 20 } }
      ]
    },
    {
      "pedidoId": 3,
      "produtos": [
        { "produtoId": "Mouse Gamer", "dimensoes": { "altura": 5, "largura": 8, "comprimento": 12 } },
        { "produtoId": "Teclado Mecânico", "dimensoes": { "altura": 4, "largura": 45, "comprimento": 15 } },
        { "produtoId": "Cadeira Gamer", "dimensoes": { "altura": 120, "largura": 60, "comprimento": 70 } }
      ]
    },
    {
      "pedidoId": 4,
      "produtos": [
        { "produtoId": "Webcam", "dimensoes": { "altura": 7, "largura": 10, "comprimento": 5 } },
        { "produtoId": "Microfone", "dimensoes": { "altura": 25, "largura": 10, "comprimento": 10 } },
        { "produtoId": "Monitor", "dimensoes": { "altura": 50, "largura": 60, "comprimento": 20 } },
        { "produtoId": "Notebook", "dimensoes": { "altura": 2, "largura": 35, "comprimento": 25 } }
      ]
    }
  ]
}
```

---