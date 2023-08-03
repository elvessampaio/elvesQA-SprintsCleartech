
SELECT * FROM categorias;
SELECT * FROM subcategorias;
SELECT * FROM centrosdistribuicoes;
SELECT * FROM produtos;


SELECT * FROM centrosdistribuicoes WHERE Status = 1;

SELECT id, categoriaId, SubcategoriaID, CentroDistribuicaoIDFROM produtos WHERE Nome = 'CAHMHSAYIY';

SELECT Id, Nome, Status, DataCriacao, DataModificacao FROM categorias
ORDER BY DataCriacao DESC;

SELECT Id, Nome, Status, DataCriacao, DataModificacao FROM categorias
ORDER BY DataCriacao ASC;

SELECT Nome, DataCriacao, DataModificacao, 
CASE WHEN Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM categorias;

INSERT INTO categorias (Id, Nome, Status, DataCriacao, DataModificacao)
VALUES (3, 'Brinquedos', 1, '2023-05-07', '2023-05-07');

UPDATE categorias SET Nome = 'BRINQUEDOS'
WHERE Nome = 'Brinquedos';

UPDATE categorias SET Status = 0
WHERE Nome = 'ROUPAS';


DELETE FROM categorias WHERE Nome =  'CATEGORYNAME';