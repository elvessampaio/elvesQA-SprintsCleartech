USE Loja;

INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES ('Pro Max', 'Celular', 'iPhone');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES ('Smirnoff ice', 'Bebidas', 'Vodka');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Samsung Book HD', 'Informática', 'Notebook');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Longo, P ao GG', 'Roupas', 'Vestidos');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Havaianas Branca', 'Calçados', 'Chinelo');
INSERT INTO tbproduto (
Produto, tb_Categoria, tb_Subcategoria) VALUES 
('Rasteirinha', 'Calçados', 'Sandália');


UPDATE tbproduto SET Produto = 'Samsung Book SSD'
WHERE Produto = 'Samsung Book HD';
UPDATE tbproduto SET Produto = 'Smirnoff Ice Kiwi'
WHERE Produto = 'Smirnoff ice';
UPDATE tbproduto SET Status = 0
WHERE Produto = 'Longo, P ao GG';

DELETE FROM tbproduto WHERE Produto =  'Rasteirinha';

SELECT * FROM tbcategoria;
SELECT * FROM tbsubcategoria;
SELECT * FROM tbproduto;


SELECT tb_Categoria, tb_Subcategoria, Produto, Criado_em, Modificado_em, 
CASE WHEN Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM tbproduto;


