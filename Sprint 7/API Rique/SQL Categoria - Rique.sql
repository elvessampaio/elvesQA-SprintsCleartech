
SELECT * FROM categorias;

SELECT * FROM categorias WHERE Nome = 'Móveis';

SELECT * FROM categorias WHERE Status = 1;

SELECT Id, Nome, Status, DataEHoraCriacao, DataEHoraModificacao FROM categorias
ORDER BY Nome DESC;

SELECT Id, Nome, Status, DataEHoraCriacao, DataEHoraModificacao FROM categorias
ORDER BY Nome ASC;

SELECT Nome, DataEHoraCriacao, DataEHoraModificacao, 
CASE WHEN Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM categorias;

INSERT INTO categorias (Id, Nome, Status, DataEHoraCriacao, DataEHoraModificacao)
VALUES ('107ddbad-ace5-4237-97c0-7062a0000001', 'Brinquedos', 1, '2023-05-30', '2023-05-30');

UPDATE categorias SET Nome = 'Smartphones'
WHERE Nome = 'Celular';

UPDATE categorias SET Status = 0
WHERE Nome = 'Móveis';

DELETE FROM categorias WHERE Nome =  'Brinquedos';

