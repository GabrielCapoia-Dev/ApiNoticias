# API de Notícias

Este projeto é uma API RESTful para gerenciamento de notícias e autores, desenvolvida em C# com ASP.NET Core.

## Requisitos Atendidos

Conforme solicitado no PDF da atividade:

### Cadastro de Autor
- **Id**: Identificador único (int)
- **Nome**: Nome do autor (string)
- **E-mail**: E-mail do autor (string)

### Cadastro de Notícias
- **Id**: Identificador único (int)
- **Título**: Título da notícia (string)
- **Texto**: Conteúdo da notícia (string)
- **Data**: Data de criação (DateTime)
- **Autor**: Relacionamento com a entidade Autor

## Estrutura do Projeto

O projeto segue a arquitetura em camadas baseada no repositório de referência:

```
ApiNoticias/
├── Controllers/           # Controladores da API
│   ├── AutorController.cs
│   └── NoticiaController.cs
├── Models/               # Modelos de dados
│   ├── Autor.cs
│   └── Noticia.cs
├── Repositories/         # Camada de acesso a dados
│   ├── Interfaces/
│   │   ├── IAutorRepository.cs
│   │   └── INoticiaRepository.cs
│   ├── AutorRepository.cs
│   └── NoticiaRepository.cs
├── Services/             # Camada de serviços (vazia por enquanto)
├── Config/               # Configurações (vazia por enquanto)
├── Properties/           # Propriedades do projeto
├── NoticiasDb_Script.sql # Script do banco de dados
├── ApiNoticias.csproj    # Arquivo do projeto
├── Program.cs            # Configuração da aplicação
├── appsettings.json      # Configurações da aplicação
└── ApiNoticias.http      # Exemplos de requisições HTTP
```

## Tecnologias Utilizadas

- **ASP.NET Core 8.0**: Framework principal
- **C#**: Linguagem de programação
- **Swagger/OpenAPI**: Documentação da API
- **SQL Server**: Banco de dados (script fornecido)
- **Repository Pattern**: Padrão de acesso a dados
- **Dependency Injection**: Injeção de dependência

## Funcionalidades da API

### Endpoints de Autor

- `GET /api/autor` - Lista todos os autores
- `GET /api/autor/byid/{id}` - Busca autor por ID
- `POST /api/autor` - Adiciona novo autor
- `PUT /api/autor/{id}` - Atualiza autor existente
- `DELETE /api/autor/{id}` - Remove autor

### Endpoints de Notícia

- `GET /api/noticia` - Lista todas as notícias
- `GET /api/noticia/byid/{id}` - Busca notícia por ID
- `GET /api/noticia/byautor/{autorId}` - Lista notícias por autor
- `POST /api/noticia` - Adiciona nova notícia
- `PUT /api/noticia/{id}` - Atualiza notícia existente
- `DELETE /api/noticia/{id}` - Remove notícia

## Como Executar

### Pré-requisitos
- .NET 8.0 SDK
- SQL Server (opcional - a aplicação funciona com dados em memória)

### Passos para execução

1. **Clone ou baixe o projeto**

2. **Navegue até o diretório do projeto**
   ```bash
   cd ApiNoticias
   ```

3. **Restaure as dependências**
   ```bash
   dotnet restore
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run
   ```

5. **Acesse a API**
   - URL base: `http://localhost:5000`
   - Swagger UI: `http://localhost:5000/swagger`

### Configuração do Banco de Dados (Opcional)

Se desejar usar SQL Server em vez de dados em memória:

1. Execute o script `NoticiasDb_Script.sql` no SQL Server
2. Configure a string de conexão no `appsettings.json`
3. Implemente Entity Framework nos repositórios

## Exemplos de Uso

### Criar um novo autor
```http
POST /api/autor
Content-Type: application/json

{
  "nome": "João Silva",
  "email": "joao.silva@email.com"
}
```

### Criar uma nova notícia
```http
POST /api/noticia
Content-Type: application/json

{
  "titulo": "Título da Notícia",
  "texto": "Conteúdo da notícia aqui...",
  "autorId": 1
}
```

## Características Técnicas

- **CORS habilitado**: Permite requisições de qualquer origem
- **Swagger integrado**: Documentação automática da API
- **Validação de dados**: Validações básicas nos endpoints
- **Tratamento de erros**: Respostas HTTP apropriadas
- **Dados em memória**: Funciona sem banco de dados para testes
- **Relacionamento entre entidades**: Autor e Notícia estão relacionados

## Diferenças do Projeto Original

Conforme solicitado, foram removidas as funcionalidades de autenticação JWT presentes no projeto de referência, mantendo apenas a funcionalidade core de CRUD para Autores e Notícias.

## Banco de Dados

O script SQL fornecido (`NoticiasDb_Script.sql`) inclui:

- Criação do banco `NoticiasDb`
- Tabelas `Autores` e `Noticias` com relacionamento
- Índices para performance
- Dados iniciais para teste
- Views e Stored Procedures úteis
- Configurações otimizadas do banco

## Observações

- A aplicação está configurada para escutar em `0.0.0.0:5000` para permitir acesso externo
- Os dados são mantidos em memória durante a execução (reiniciar a aplicação limpa os dados)
- Para produção, recomenda-se implementar Entity Framework e usar banco de dados real

