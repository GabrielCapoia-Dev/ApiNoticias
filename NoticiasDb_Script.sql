-- Script de criação do banco de dados para API de Notícias
-- Baseado no padrão do repositório GitHub analisado

-- Criação do banco de dados
CREATE DATABASE [NoticiasDb] 
CONTAINMENT = NONE 
ON PRIMARY 
( 
    NAME = N'NoticiasDb', 
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\NoticiasDb.mdf', 
    SIZE = 8192KB, 
    MAXSIZE = UNLIMITED, 
    FILEGROWTH = 65536KB 
) 
LOG ON 
( 
    NAME = N'NoticiasDb_log', 
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\NoticiasDb_log.ldf', 
    SIZE = 8192KB, 
    MAXSIZE = 2048GB, 
    FILEGROWTH = 65536KB 
) 
WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF;
GO

-- Usar o banco de dados criado
USE [NoticiasDb];
GO

-- Criação da tabela Autores
CREATE TABLE [dbo].[Autores] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Nome] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(150) NOT NULL,
    CONSTRAINT [PK_Autores] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Autores_Email] UNIQUE ([Email])
);
GO

-- Criação da tabela Noticias
CREATE TABLE [dbo].[Noticias] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Titulo] NVARCHAR(200) NOT NULL,
    [Texto] NTEXT NOT NULL,
    [Data] DATETIME NOT NULL DEFAULT GETDATE(),
    [AutorId] INT NOT NULL,
    CONSTRAINT [PK_Noticias] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Noticias_Autores] FOREIGN KEY ([AutorId]) REFERENCES [dbo].[Autores]([Id])
);
GO

-- Criação de índices para melhor performance
CREATE NONCLUSTERED INDEX [IX_Noticias_AutorId] ON [dbo].[Noticias] ([AutorId]);
GO

CREATE NONCLUSTERED INDEX [IX_Noticias_Data] ON [dbo].[Noticias] ([Data] DESC);
GO

-- Inserção de dados iniciais para teste
INSERT INTO [dbo].[Autores] ([Nome], [Email]) VALUES 
('João Silva', 'joao.silva@email.com'),
('Maria Santos', 'maria.santos@email.com'),
('Carlos Oliveira', 'carlos.oliveira@email.com');
GO

INSERT INTO [dbo].[Noticias] ([Titulo], [Texto], [Data], [AutorId]) VALUES 
('Primeira Notícia', 'Este é o conteúdo da primeira notícia de teste. Aqui temos informações importantes sobre o assunto abordado.', GETDATE() - 2, 1),
('Segunda Notícia', 'Conteúdo da segunda notícia com informações relevantes para os leitores.', GETDATE() - 1, 2),
('Terceira Notícia', 'Mais uma notícia interessante com conteúdo de qualidade.', GETDATE(), 1),
('Quarta Notícia', 'Notícia sobre tecnologia e inovação no mercado atual.', GETDATE(), 3);
GO

-- Criação de views úteis
CREATE VIEW [dbo].[vw_NoticiasCompletas] AS
SELECT 
    n.[Id] as NoticiaId,
    n.[Titulo],
    n.[Texto],
    n.[Data],
    a.[Id] as AutorId,
    a.[Nome] as AutorNome,
    a.[Email] as AutorEmail
FROM [dbo].[Noticias] n
INNER JOIN [dbo].[Autores] a ON n.[AutorId] = a.[Id];
GO

-- Stored Procedures úteis
CREATE PROCEDURE [dbo].[sp_GetNoticiasByAutor]
    @AutorId INT
AS
BEGIN
    SELECT 
        n.[Id],
        n.[Titulo],
        n.[Texto],
        n.[Data],
        a.[Nome] as AutorNome,
        a.[Email] as AutorEmail
    FROM [dbo].[Noticias] n
    INNER JOIN [dbo].[Autores] a ON n.[AutorId] = a.[Id]
    WHERE n.[AutorId] = @AutorId
    ORDER BY n.[Data] DESC;
END
GO

CREATE PROCEDURE [dbo].[sp_GetNoticiasRecentes]
    @Dias INT = 7
AS
BEGIN
    SELECT 
        n.[Id],
        n.[Titulo],
        n.[Texto],
        n.[Data],
        a.[Nome] as AutorNome,
        a.[Email] as AutorEmail
    FROM [dbo].[Noticias] n
    INNER JOIN [dbo].[Autores] a ON n.[AutorId] = a.[Id]
    WHERE n.[Data] >= DATEADD(DAY, -@Dias, GETDATE())
    ORDER BY n.[Data] DESC;
END
GO

-- Configurações adicionais do banco
ALTER DATABASE [NoticiasDb] SET ANSI_NULL_DEFAULT OFF;
GO
ALTER DATABASE [NoticiasDb] SET ANSI_NULLS OFF;
GO
ALTER DATABASE [NoticiasDb] SET ANSI_PADDING OFF;
GO
ALTER DATABASE [NoticiasDb] SET ANSI_WARNINGS OFF;
GO
ALTER DATABASE [NoticiasDb] SET ARITHABORT OFF;
GO
ALTER DATABASE [NoticiasDb] SET AUTO_CLOSE OFF;
GO
ALTER DATABASE [NoticiasDb] SET AUTO_SHRINK OFF;
GO
ALTER DATABASE [NoticiasDb] SET AUTO_UPDATE_STATISTICS ON;
GO
ALTER DATABASE [NoticiasDb] SET CURSOR_CLOSE_ON_COMMIT OFF;
GO
ALTER DATABASE [NoticiasDb] SET CURSOR_DEFAULT GLOBAL;
GO
ALTER DATABASE [NoticiasDb] SET CONCAT_NULL_YIELDS_NULL OFF;
GO
ALTER DATABASE [NoticiasDb] SET NUMERIC_ROUNDABORT OFF;
GO
ALTER DATABASE [NoticiasDb] SET QUOTED_IDENTIFIER OFF;
GO
ALTER DATABASE [NoticiasDb] SET RECURSIVE_TRIGGERS OFF;
GO
ALTER DATABASE [NoticiasDb] SET DISABLE_BROKER;
GO

PRINT 'Banco de dados NoticiasDb criado com sucesso!';

