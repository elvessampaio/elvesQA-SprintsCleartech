
SELECT tbproduto.Produto, tbcategoria.Categoria, tbsubcategoria.Subcategoria, tbproduto.Criado_em, 
tbproduto.Modificado_em,tbproduto.Status FROM tbproduto
INNER JOIN tbcategoria ON tbproduto.tb_Categoria = tbcategoria.Categoria
INNER JOIN tbsubcategoria ON tbproduto.tb_Subcategoria = tbsubcategoria.Subcategoria;

SELECT tbsubcategoria.Subcategoria, tbcategoria.Categoria, tbsubcategoria.Criado_em, 
tbsubcategoria.Modificado_em, tbsubcategoria.Status FROM tbcategoria
INNER JOIN tbsubcategoria ON tbcategoria.Categoria = tbsubcategoria.tb_Categoria;