CREATE DATABASE Loja;
USE Loja;

CREATE TABLE tbcategoria(
Categoria VARCHAR(128) NOT NULL UNIQUE,
Criado_em DATETIME DEFAULT CURRENT_TIMESTAMP,
Modificado_em DATETIME,
Status BOOLEAN DEFAULT true);

CREATE TABLE tbsubcategoria(
Subcategoria VARCHAR(128) NOT NULL UNIQUE,
Criado_em DATETIME DEFAULT CURRENT_TIMESTAMP,
Modificado_em DATETIME,
tb_Categoria VARCHAR(128) NOT NULL,
FOREIGN KEY (tb_Categoria) REFERENCES tbcategoria(Categoria),
Status BOOLEAN DEFAULT true);

CREATE TABLE tbproduto(
Produto VARCHAR(128) NOT NULL UNIQUE,
Criado_em DATETIME DEFAULT CURRENT_TIMESTAMP,
Modificado_em DATETIME,
tb_Categoria VARCHAR(128) NOT NULL,
tb_Subcategoria VARCHAR(128) NOT NULL,
FOREIGN KEY (tb_Categoria) REFERENCES tbcategoria(Categoria),
FOREIGN KEY (tb_Subcategoria) REFERENCES tbsubcategoria(Subcategoria),
Status BOOLEAN DEFAULT true);


CREATE TRIGGER tbcategoria_atualizada
BEFORE UPDATE ON tbcategoria
FOR EACH ROW
SET NEW.Modificado_em = IF(NEW.Categoria = OLD.Categoria AND NEW.Status = OLD.Status, OLD.Modificado_em, NOW());

CREATE TRIGGER tbsubcategoria_atualizada
BEFORE UPDATE ON tbsubcategoria
FOR EACH ROW
SET NEW.Modificado_em = IF(NEW.Subcategoria = OLD.Subcategoria AND NEW.Status = OLD.Status, OLD.Modificado_em, NOW());

CREATE TRIGGER tbproduto_atualizada
BEFORE UPDATE ON tbproduto
FOR EACH ROW
SET NEW.Modificado_em = IF(NEW.Produto = OLD.Produto AND NEW.Status = OLD.Status, OLD.Modificado_em, NOW());

SET FOREIGN_KEY_CHECKS=0; #DESTIVAR CHAVE ESTRAGEIRA#
SET FOREIGN_KEY_CHECKS=1; #ATIVAR CHAVE ESTRAGEIRA#

#ADICIONEI COLUNAS PARA ARMAZENAR AS FOREIGN KEY E ASSIM TRAZER REFERÊNCIA DAS TABELAS DE CATEGORIAS E SUBCATEGORIAS# 
#PRA UMA MELHOR VISÃO DE ASSOCIAÇÃO DOS DADOS NO ITEM CORRETO#
#USEI O NOT NULL PARA GARANTIR QUE O CAMPO É OBRIGATÓRIO E NÃO PODE SER NULO, OU SEJA, VAZIO#
#A RESTRIÇÃO UNIQUE É PARA QUE NÃO SEJA FEITO OUTRO CADASTRO COM O MESMO NOME#
#PARA GRAVAR AS DATA DE HORA DE CRIAÇÃO USEI O O CURRENT_TIMESTAMP PARA ARMAZANAMENTO#

#PARA QUE OS DATA E HORA SEJAM ATUALIZADOS SOMENTE NA MODIFICAÇÃO DO DADO, USEI TRIGGER#
#É ACIONADA QUANDO HOUVER UPDATE NAS TABELAS, COLOQUEI VERIFICAÇÃO NOS CAMPOS QUE DESEJO SABER SE HOUVE#
#A ATUALIZAÇÃO USANDO O FOR EACH ROW, E USANDO A FUNÇÃO NOW QUANDO MODIFICADO, ELE ATUALIZA COM DATA E HORA ATUAIS#
#CASO CONTRÁRIO, O VALOR É MANTIDO#
