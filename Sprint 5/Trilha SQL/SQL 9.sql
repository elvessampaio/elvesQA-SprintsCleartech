
SELECT * FROM tbproduto WHERE PRODUTO = '544931';

SELECT * FROM tbcliente WHERE CIDADE = 'Rio de Janeiro';

SELECT * FROM tbproduto WHERE SABOR = 'Citricos';

UPDATE tbproduto SET SABOR = 'Citricos' WHERE SABOR = 'Limão';

SELECT * FROM tabela_de_vendedores WHERE NOME = 'Cláudia Morais';

SELECT * FROM tbcliente;

SELECT * FROM tbcliente WHERE IDADE = 22;

SELECT * FROM tbcliente WHERE IDADE > 22;
SELECT * FROM tbcliente WHERE IDADE < 22;
SELECT * FROM tbcliente WHERE IDADE <= 22;
SELECT * FROM tbcliente WHERE IDADE <> 22;

SELECT * FROM tbcliente WHERE NOME >= 'Fernando Cavalcante';
SELECT * FROM tbcliente WHERE NOME <> 'Fernando Cavalcante';

SELECT * FROM tbproduto;

SELECT * FROM tbproduto WHERE PRECO_LISTA > 16.008;
SELECT * FROM tbproduto WHERE PRECO_LISTA < 16.008;
SELECT * FROM tbproduto WHERE PRECO_LISTA <> 16.008;

SELECT * FROM tbproduto WHERE PRECO_LISTA BETWEEN 16.007 AND 16.009;

SELECT * FROM tabela_de_vendedores WHERE PERCENTUAL_COMISSAO > 10;

