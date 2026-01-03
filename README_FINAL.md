# MyMEDIA - Trabalho Prático de Programação Web

Este projeto é uma solução completa para a plataforma de venda de produtos multimédia MyMEDIA, desenvolvida em **.NET 8** utilizando **Blazor Web** e **Web API**.

## 📋 Pré-requisitos

Para executar este projeto, necessita de ter instalado:
1.  **Visual Studio Code** (ou Visual Studio 2022).
2.  **.NET 8 SDK** (Verifique com `dotnet --version`).
3.  **(Opcional)** Workload MAUI se pretender correr a versão nativa Android/iOS.

---

## 🚀 Como Executar o Projeto

O projeto está dividido em duas partes principais que devem correr simultaneamente: o **Backend (API)** e o **Frontend (Web/Cliente)**.

### Passo 1: Abrir o Projeto
Extraia o ficheiro ZIP e abra a pasta raiz no VS Code.

### Passo 2: Iniciar a API (Backend)
A API é responsável pela Base de Dados e Autenticação.

1.  Abra um terminal no VS Code (`Terminal -> New Terminal`).
2.  Execute o seguinte comando:
    ```bash
    dotnet run --project src/MyMEDIA/MyMEDIA.API
    ```
3.  Aguarde até ver a mensagem: `Now listening on: http://localhost:5000`.
    *Nota: A Base de Dados será criada automaticamente na primeira execução.*

### Passo 3: Iniciar a Aplicação Web (Frontend)
1.  Abra um **segundo terminal** (Clique no botão `+` no painel do terminal).
2.  Execute o seguinte comando:
    ```bash
    dotnet run --project src/MyMEDIA/MyMEDIA.Web
    ```
3.  O terminal mostrará um link (ex: `http://localhost:5002`). Clique nele (Cmd+Click) para abrir no browser.

---

## 🔑 Credenciais de Acesso (Dados de Teste)

O sistema cria automaticamente utilizadores de teste quando inicia a API.

| Perfil | Email | Password | Funcionalidades |
| :--- | :--- | :--- | :--- |
| **Administrador** | `admin@mymedia.com` | `Admin123!` | Gerir Encomendas, Definir Preços, Ver Stocks |
| **Fornecedor** | `supplier@mymedia.com` | `Supplier123!` | Menu "My Products" e "My Sales", Criar Produtos |
| **Cliente** | (Registe-se na app) | (Sua escolha) | Comprar, Ver Histórico de Encomendas |

---

## 🛠 Funcionalidades Implementadas

1.  **Loja Online (Frontend):**
    *   Listagem de produtos com imagens.
    *   Filtragem por Categorias (Music, Movies, etc.).
    *   **Carrinho de Compras** funcional.
    *   **Gestão de Stock**: Impede compra se esgotado.

2.  **Área do Fornecedor:**
    *   Página "My Products": Inserir/Editar produtos (Ficam "Pendentes").
    *   Página "My Sales": Ver histórico de artigos vendidos.

3.  **Área de Gestão (Backoffice):**
    *   Aceder via `http://localhost:5002/orders` (como Admin).
    *   Aprovação de produtos pendentes.
    *   Definição de Margem de Lucro (Preço Base vs Final).
    *   Gestão de Estados de Encomenda (Confirmar Pagamento, Enviar).

4.  **API & Dados:**
    *   REST API completa em .NET 8.
    *   Autenticação JWT segura.
    *   Code-First com Seeding automático (não requer comandos manuais de SQL).

## 🆘 Resolução de Problemas

*   **Erro "Address already in use":** Certifique-se que não tem terminais antigos abertos. Feche tudo e tente de novo.
*   **Imagens não aparecem:** Certifique-se que a API está a correr (é ela que fornece os dados).
*   **Base de Dados:** O sistema usa `LocalDB`. Se tiver erros de SQL, verifique se instalou o componente "SQL Server Express LocalDB" no instalador do Visual Studio/SQL Server.

---
**Desenvolvido para PWeb 2025/2026**
