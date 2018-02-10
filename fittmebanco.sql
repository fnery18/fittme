CREATE TABLE Fornecedores(
	Codigo int identity,
	Nome varchar(100),
	Email varchar(100),
	Celular varchar(15),
	PRIMARY KEY (Codigo)
)

CREATE TABLE Tipos(
	Codigo int identity,
	Nome varchar(3)
	Primary key(Codigo)
)

CREATE TABLE Produtos(
	Codigo int identity,
	Codigo_ProdutoFornecedor varchar(50) NOT NULL, -- codigo utilizado pelo fornecedor
	Codigo_Produto varchar(20) NOT NULL, -- codigo gerado pela fiitme
	Nome varchar(100) NOT NULL,
	Codigo_Tipo int NOT NULL,
	Imagem varchar(500),
	Codigo_Fornecedor int,
	Preco_Custo decimal NOT NULL,
	Preco_Nota decimal NOT NULL,
	Preco_Venda decimal,
	Quantidade int NOT NULL,
	CONSTRAINT FK_Fornecedor FOREIGN KEY (Codigo_Fornecedor) REFERENCES Fornecedores(Codigo),
	CONSTRAINT FK_Tipo FOREIGN KEY (Codigo_Tipo) REFERENCES Tipos(Codigo),
	PRIMARY KEY(Codigo)
)

CREATE TABLE Usuarios(
	Codigo int identity,
	Usuario varchar(50),
	Senha varchar(20),
	Administrador bit,
	PRIMARY KEY(Codigo)
)

CREATE Table Cores(
	Codigo int identity,
	Nome varchar(50) NOT NULL,
	Codigo_Cor varchar(4) NOT NULL,
	Cor varchar(7) NOT NULL,
	PRIMARY KEY(Codigo)
)



