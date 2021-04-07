create database dblojainformatica;
use dblojainformatica;

create table tbCliente (
cod_cli int primary key auto_increment,
nome_cli varchar(50),
email_cli varchar(50),
cpf_cli varchar(14),
telefone_cli varchar(20)
);

create table tbFornecedor (
cod_fornecedor int primary key auto_increment,
marca varchar(50),
email_fornecedor varchar(50)
);

create table tbLogin (
nome_usuario varchar(20) primary key,
senha varchar(20)
);

create table tbPagamento (
cod_pagamento int primary key auto_increment,
desc_pagamento varchar(30)
);

create table tbFuncionario (
cod_func int primary key auto_increment,
nome_func varchar(50),
email_func varchar(50),
nome_usuario varchar(20),
foreign key (nome_usuario) references tbLogin(nome_usuario)
);

create table tbGerente (
cod_gerente int primary key auto_increment,
nome_gerente varchar(50),
email_gerente varchar(50),
nome_usuario varchar(20),
foreign key (nome_usuario) references tbLogin(nome_usuario)
);

create table tbProduto (
cod_produto int primary key auto_increment,
nome_produto varchar(50),
desc_produto text,
qtd_estoque int,
valor_unitario float,
cod_fornecedor int,
foreign key (cod_fornecedor) references tbFornecedor(cod_fornecedor)
);

select * from tbproduto;

create table tbPedido(
cod_pedido int primary key auto_increment,
qtd_pedido int,
valor_total float,
data_pedido datetime,
situacao varchar(20),
cod_pagamento int,
cod_cli int,
cod_produto int,
foreign key (cod_pagamento) references tbPagamento(cod_pagamento),
foreign key (cod_cli) references tbCliente(cod_cli),
foreign key (cod_produto) references tbProduto(cod_produto)
);


/*criação de procedures para consulta*/
delimiter $$
create procedure selecionaProduto()
begin
	select cod_produto,nome_produto,  replace(valor_unitario, '.',',') as valor, marca,
	case when qtd_estoque = 0 then 'Indisponível' else qtd_estoque end as 'quantidade estoque' from tbproduto pd 
    inner join tbfornecedor as fn on  pd.cod_fornecedor = fn.cod_fornecedor order by cod_produto;
end $$
delimiter $$;

call selecionaProduto();

delimiter $$
create procedure selecionaIdDoProduto(cod int)
begin
	select cod_produto,nome_produto, valor_unitario, marca, qtd_estoque from tbproduto pd 
    inner join tbfornecedor as fn on pd.cod_fornecedor = fn.cod_fornecedor where cod_produto = cod;
end $$
delimiter $$;

call selecionaIdDoProduto(20);

delimiter $$
create procedure buscaProdutoPorId(cod int)
begin
	select * from tbproduto pd where cod_produto = cod;
end $$
delimiter $$;

call buscaProdutoPorId(15);

delimiter $$
create procedure selecionaProdutoPorNome(nome varchar(50))
begin
	select cod_produto,nome_produto,  replace(valor_unitario, '.',',') as valor, marca,
	case when qtd_estoque = 0 then 'Indisponível' else qtd_estoque end as 'quantidade estoque' from tbproduto pd 
    inner join tbfornecedor as fn on  pd.cod_fornecedor = fn.cod_fornecedor where nome_produto like concat('%',nome,'%') order by cod_produto;
end $$
delimiter $$;

call selecionaProdutoPorNome('Tv');

delimiter $$
create procedure selecionaPedido()
begin
	select cod_pedido, nome_produto, DATE_FORMAT(data_pedido, '%d/%m/%Y') as data, desc_produto, situacao, qtd_pedido, valor_total, desc_pagamento, nome_cli from tbPedido pd
    inner join tbProduto pt on pd.cod_produto = pt.cod_produto
    inner join tbPagamento pg on pd.cod_pagamento = pg.cod_pagamento
    inner join tbCliente cl on pd.cod_cli = cl.cod_cli order by cod_pedido;
end $$
delimiter $$;

call selecionaPedido();

delimiter $$
create procedure selecionaPedidoPorId(cod int)
begin
	select cod_pedido, nome_produto, data_pedido, desc_produto, situacao, qtd_pedido, valor_total, desc_pagamento, nome_cli from tbPedido pd
    inner join tbProduto pt on pd.cod_produto = pt.cod_produto
    inner join tbPagamento pg on pd.cod_pagamento = pg.cod_pagamento
    inner join tbCliente cl on pd.cod_cli = cl.cod_cli where cod_pedido = cod;
end $$
delimiter $$;

call selecionaPedidoPorId(2);

/*criação de procedures para cadastro*/
delimiter $$
create procedure cadastraProduto(in nome varchar(50),in descricao text, in qtd int, valor float, cod_fornecedor int)
begin
	insert into tbproduto (nome_produto, desc_produto, qtd_estoque, valor_unitario, cod_fornecedor) values(nome, descricao, qtd, valor, cod_fornecedor);
end $$
delimiter $$;

call cadastraProduto('Computador','Computador com processador AMD Ryzen 5 8GB RAM DDR4 + SSD 480GB', 2, 2948.90, 1);

delimiter $$
create procedure cadastraPedido(in qtd int,in valor float, in data datetime, in situacao varchar(20) , in cod_pagamento int,in cod_cli int, cod_produto int)
begin
	insert into tbpedido (qtd_pedido, valor_total, data_pedido, situacao, cod_pagamento,cod_cli, cod_produto) values(qtd, valor, data,situacao, cod_pagamento,cod_cli, cod_produto);
end $$
delimiter $$;

call cadastraPedido(2, 4080, '2021-04-03', 'Concluído',2, 1, 2);

/*criação de procedure para alteração*/
delimiter $$
create procedure alteraProduto(in cod int,in nome varchar(50),in descricao text, in qtd int, valor float, cod_fornecedor int)
begin
	update tbProduto set nome_produto = nome, desc_produto = descricao, qtd_estoque = qtd, valor_unitario = valor, cod_fornecedor = cod_fornecedor 
    where cod_produto = cod;
end $$
delimiter $$;

call alteraProduto(8, 'Teclado Membrana', 'teclado bom para digitar muito', '1', '29.90', 2);

/*criação de procedure para exclusão*/
delimiter $$
create procedure excluirProduto(in cod int)
begin
	delete from tbProduto where cod_produto = cod;
end $$
delimiter $$;

call excluirProduto(7);

/*populando o banco*/

/*inserindo produto*/
insert into tbProduto values (default, 'Mouse Óptico', 'mouse bom para jogos', 0, 29.90, 1);
insert into tbProduto values (default, 'Teclado Mecânico', 'teclado bom para digitar', 2, 49.90, 1);

/*cadastrando gerente*/
insert into tbGerente values(default, 'Wellington', 'well.city@gmail.com', 'Well');

/*cadastrando funcionário*/
insert into tbFuncionario values(default, 'Leonardo', 'leonardoiigd2013@gmail.com', 'Leonardo');
insert into tbFuncionario values(default, 'Cleber', 'clebin.souza@gmail.com', 'Cleber');
insert into tbFuncionario values(default, 'Railson', 'railson.cipriano@gmail.com', 'Railson');
insert into tbFuncionario values(default, 'Ítalo', 'italo.araujo@gmail.com', 'Ítalo');
insert into tbFuncionario values(default, 'Eduardo', 'eduardo.miklos@gmail.com', 'Eduardo');
insert into tbFuncionario values(default, 'Gustavo', 'gustavo.bittencourt@gmail.com', 'Gustavo');

/*cadastrando login*/
insert into tbLogin values('Leonardo', '123', 1);
insert into tbLogin values('Cleber', '123', 1);
insert into tbLogin values('Railson', '123', 1);
insert into tbLogin values('Ítalo', '123', 1);
insert into tbLogin values('Eduardo', '123', 1);
insert into tbLogin values('Gustavo', '123', 1);
insert into tbLogin values('Well', '123', 0);

/*cadastrando os clientes*/
insert into tbCliente values(default, 'Cecília', 'cecilia.neves@gmail.com', '563.122.759-55', '(11) 95813-4758');
insert into tbCliente values(default, 'Letícia', 'leeh.kevelly@gmail.com', '456.759.127-42', '(11) 94293-1540');
insert into tbCliente values(default, 'Matheus', 'matheus.rodrigues@gmail.com', '459.127.418-84', '(11) 94174-4621');
insert into tbCliente values(default, 'Luanderson', 'luanderson.alo@gmail.com', '125.759.462-21', '(11) 92488-4236');
insert into tbCliente values(default, 'Gustavo', 'gustavo.araujo.mendes@gmail.com', '317.428.123-78', '(11) 45693-1234');
insert into tbCliente values(default, 'Leandro', 'leandro.albuquerque@gmail.com', '123.456.789-10', '(11) 96488-1236');

/*cadastrando os métodos de pagamento*/
insert into tbPagamento values(default, 'Crédito');
insert into tbPagamento values(default, 'Débito');
insert into tbPagamento values(default, 'Boleto Bancário');
insert into tbPagamento values(default, 'Paypal');
insert into tbPagamento values(default, 'Picpay');

/*cadastrando os fornecedores dos produtos*/
insert into tbFornecedor values(default, 'Phillips', 'phillips@gmail.com');
insert into tbFornecedor values(default, 'ArchProd', 'archprod@gmail.com');
insert into tbFornecedor values(default, 'Samsung', 'samsung@gmail.com');
insert into tbFornecedor values(default, 'HyperX', 'hyperx@gmail.com');
insert into tbFornecedor values(default, 'JBL', 'jbl@gmail.com');
insert into tbFornecedor values(default, 'Dell', 'dell@gmail.com');
insert into tbFornecedor values(default, 'Sony', 'sony@gmail.com');
insert into tbFornecedor values(default, 'TPLink', 'tplink@gmail.com');

/*consultas*/
select nome_func, email_func, senha from tbFuncionario as func inner join tbLogin as lg on func.nome_usuario = lg.nome_usuario;
 
select * from tbCliente;
select * from tbFornecedor;
select * from tbLogin;
select * from tbPagamento;
select * from tbFuncionario;
select * from tbGerente;
select * from tbProduto;
select * from tbPedido;