USE Loja;

INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES ('14 Pro Max, 128G', 'Celular', 'iPhone');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES ('Smirnoff ice', 'Bebidas', 'Vodka');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Samsung Book, 4GB, 256GB HD', 'Informática', 'Notebook');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Longo', 'Roupas', 'Vestidos');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Havaianas Branca', 'Calçados', 'Chinelo');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Rasteirinha', 'Calçados', 'Sandália');


UPDATE tbproduto SET Produto = 'Samsung Book, 4GB, 256GB SSD'
WHERE Produto = 'Samsung Book, 4GB, 256GB HD';
UPDATE tbproduto SET Produto = 'Smirnoff Ice Kiwi'
WHERE Produto = 'Smirnoff ice';
UPDATE tbproduto SET Status = 0
WHERE Produto = 'Longo';

DELETE FROM tbproduto WHERE Produto =  ' Samsung Book, 4GB, 256GB SSD';

SELECT * FROM tbcategoria;
SELECT * FROM tbsubcategoria;
SELECT * FROM tbproduto;


SELECT Produto, tb_Categoria, tb_Subcategoria,Criado_em, Modificado_em, 
CASE WHEN Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM tbproduto;


