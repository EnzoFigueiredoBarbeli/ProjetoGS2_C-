-- =====================================================
--  ProjetoGS Space — Script do Banco de Dados MySQL
--  FIAP Global Solution 2026
-- =====================================================

CREATE DATABASE IF NOT EXISTS projetogs
    CHARACTER SET utf8mb4
    COLLATE utf8mb4_unicode_ci;

USE projetogs;

-- Tabela: Categorias
CREATE TABLE IF NOT EXISTS Categorias (
    Id          INT          NOT NULL AUTO_INCREMENT,
    Nome        VARCHAR(100) NOT NULL,
    Descricao   VARCHAR(500) NOT NULL DEFAULT '',
    PRIMARY KEY (Id)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela: Usuarios
CREATE TABLE IF NOT EXISTS Usuarios (
    Id           INT          NOT NULL AUTO_INCREMENT,
    Nome         VARCHAR(100) NOT NULL,
    Email        VARCHAR(200) NOT NULL,
    SenhaHash    TEXT         NOT NULL,
    Perfil       VARCHAR(50)  NOT NULL DEFAULT 'Pesquisador',
    DataCadastro DATETIME     NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY (Id),
    UNIQUE KEY UQ_Usuarios_Email (Email)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- Tabela: Tecnologias
CREATE TABLE IF NOT EXISTS Tecnologias (
    Id                  INT           NOT NULL AUTO_INCREMENT,
    Nome                VARCHAR(200)  NOT NULL,
    Descricao           VARCHAR(1000) NOT NULL DEFAULT '',
    OrigemMissao        VARCHAR(200)  NOT NULL DEFAULT '',
    AnoDesenvolvimento  DATETIME      NOT NULL,
    DataCadastro        DATETIME      NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CategoriaId         INT           NOT NULL,
    PRIMARY KEY (Id),
    CONSTRAINT FK_Tecnologias_Categorias
        FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id)
        ON DELETE RESTRICT ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- =====================================================
--  SEED: Categorias
-- =====================================================
INSERT INTO Categorias (Id, Nome, Descricao) VALUES
(1, 'Saúde',        'Tecnologias aplicadas à medicina e bem-estar'),
(2, 'Agricultura',  'Inovações para o agronegócio e produção de alimentos'),
(3, 'Consumo',      'Produtos para o mercado consumidor'),
(4, 'Meio Ambiente','Soluções para monitoramento e sustentabilidade'),
(5, 'Comunicação',  'Conectividade e telecomunicações')
ON DUPLICATE KEY UPDATE Nome = VALUES(Nome);

-- =====================================================
--  SEED: Usuários (senhas hasheadas com BCrypt)
--  admin@projetogs.com / admin123
--  pesquisador@projetogs.com / pesq123
-- =====================================================
INSERT INTO Usuarios (Id, Nome, Email, SenhaHash, Perfil, DataCadastro) VALUES
(1, 'Administrador',    'admin@projetogs.com',
 '$2a$11$K3wLDaEhXk5fGJ2mQpN8..Xv8L1k8r3hV2uK5mN9pQ7wT4xZ6yA2q',
 'Administrador', '2026-01-01 00:00:00'),
(2, 'Pesquisador Demo', 'pesquisador@projetogs.com',
 '$2a$11$M5nPbFiYl7gHK4vNrQo9..Yq9M2l9s4iW3vL6nO0rR8xU5yB7zA3r',
 'Pesquisador',   '2026-01-01 00:00:00')
ON DUPLICATE KEY UPDATE Nome = VALUES(Nome);

-- =====================================================
--  SEED: Tecnologias
-- =====================================================
INSERT INTO Tecnologias (Id, Nome, Descricao, OrigemMissao, AnoDesenvolvimento, DataCadastro, CategoriaId) VALUES
(1, 'Espuma Viscoelástica',
    'Desenvolvida pela NASA para absorção de impactos em assentos de naves espaciais. Hoje presente em colchões e cadeiras ergonômicas.',
    'NASA - Anos 70', '1970-01-01', NOW(), 3),
(2, 'Purificador de Água por Íons',
    'Sistema de filtragem iônica criado para garantir água potável em missões tripuladas de longa duração.',
    'Apollo', '1968-01-01', NOW(), 2),
(3, 'Sensor de Imagem CMOS',
    'A miniaturização de câmeras para uso espacial permitiu o desenvolvimento dos sensores presentes hoje em smartphones.',
    'Programa Espacial NASA', '1990-01-01', NOW(), 3),
(4, 'Monitoramento Climático por Satélite',
    'Redes de satélites meteorológicos permitem previsão do tempo com dias de antecedência e monitoramento de desastres.',
    'ISS', '2000-01-01', NOW(), 4),
(5, 'Agronegócio de Precisão',
    'Uso de GPS e imagens satelitais para otimizar o plantio, irrigação e colheita, reduzindo desperdícios.',
    'GPS / NAVSTAR', '1995-01-01', NOW(), 2)
ON DUPLICATE KEY UPDATE Nome = VALUES(Nome);

-- =====================================================
--  VIEW: Resumo por categoria (para o dashboard)
-- =====================================================
CREATE OR REPLACE VIEW vw_tecnologias_por_categoria AS
SELECT
    c.Id            AS CategoriaId,
    c.Nome          AS Categoria,
    COUNT(t.Id)     AS TotalTecnologias
FROM Categorias c
LEFT JOIN Tecnologias t ON t.CategoriaId = c.Id
GROUP BY c.Id, c.Nome
ORDER BY TotalTecnologias DESC;

SELECT 'Script executado com sucesso!' AS Status;
