USE Loja;

INSERT INTO tbsubcategoria (
Subcategoria, tb_Categoria) VALUES ('iPhone', 'Celular');
INSERT INTO tbsubcategoria (
Subcategoria, tb_Categoria) VALUES ('Notebook', 'Informática');
INSERT INTO tbsubcategoria (
Subcategoria, tb_Categoria) VALUES ('Vestidos', 'Roupas');
INSERT INTO tbsubcategoria (
Subcategoria, tb_Categoria) VALUES ('Vodca', 'Bebidas');
INSERT INTO tbsubcategoria (
Subcategoria, tb_Categoria) VALUES ('Chinelo', 'Calçados');
INSERT INTO tbsubcategoria (
Subcategoria, tb_Categoria) VALUES ('Rasteira', 'Calçados');

UPDATE tbsubcategoria SET Subcategoria = 'Vodka'
WHERE Subcategoria = 'Vodca';
UPDATE tbsubcategoria SET Status = 0
WHERE Subcategoria = 'Vestidos';
UPDATE tbsubcategoria SET Subcategoria = 'Sandália'
WHERE Subcategoria = 'Rasteirinha';

SELECT * FROM tbsubcategoria;
SELECT * FROM tbcategoria;


SELECT Subcategoria, tb_Categoria, Criado_em, Modificado_em, 
CASE WHEN Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM tbsubcategoria;