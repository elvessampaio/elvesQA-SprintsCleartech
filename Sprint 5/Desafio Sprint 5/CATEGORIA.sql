USE Loja;

INSERT INTO tbcategoria (
Categoria) VALUES ('Celular');
INSERT INTO tbcategoria (
Categoria) VALUES ('Informática');
INSERT INTO tbcategoria (
Categoria) VALUES ('Roupas');
INSERT INTO tbcategoria (
Categoria) VALUES ('Bebida');
INSERT INTO tbcategoria (
Categoria) VALUES ('Calçados');


UPDATE tbcategoria SET Categoria = 'Bebidas'
WHERE Categoria = 'Bebida';

UPDATE tbcategoria SET Status = 0
WHERE Categoria = 'Roupas';

DELETE FROM tbcategoria WHERE Categoria = 'Roupas';

SELECT * FROM tbcategoria WHERE Categoria = 'Roupas';
SELECT * FROM tbcategoria;

SELECT Categoria, Criado_em, Modificado_em, 
CASE WHEN Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM tbcategoria;
