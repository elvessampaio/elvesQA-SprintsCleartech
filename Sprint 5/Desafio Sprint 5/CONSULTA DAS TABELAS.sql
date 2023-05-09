
SELECT tbproduto.Produto, tbcategoria.Categoria, tbsubcategoria.Subcategoria, tbproduto.Criado_em, 
tbproduto.Modificado_em,
CASE WHEN tbproduto.Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM tbproduto
INNER JOIN tbcategoria ON tbproduto.tb_Categoria = tbcategoria.Categoria
INNER JOIN tbsubcategoria ON tbproduto.tb_Subcategoria = tbsubcategoria.Subcategoria WHERE Produto = 'Longo';

SELECT tbsubcategoria.Subcategoria, tbcategoria.Categoria, tbsubcategoria.Criado_em, 
tbsubcategoria.Modificado_em,
CASE WHEN tbsubcategoria.Status = 1 THEN 'Ativo' ELSE 'Inativo' END AS Status FROM tbcategoria
INNER JOIN tbsubcategoria ON tbcategoria.Categoria = tbsubcategoria.tb_Categoria WHERE Categoria = 'Roupas';

