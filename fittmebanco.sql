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

CREATE Table Cores(
	Codigo int identity,
	Nome varchar(50) NOT NULL,
	CodigoCor varchar(4) NOT NULL,
	Cor varchar(7) NOT NULL,
	PRIMARY KEY(Codigo)
)

CREATE TABLE Produtos(
	Codigo int identity,
	CodigoProdutoFornecedor varchar(50) NOT NULL, -- codigo utilizado pelo fornecedor
	CodigoProduto varchar(20) NOT NULL, -- codigo gerado pela fiitme
	CodigoCor int NOT NULL,
	Nome varchar(100) NOT NULL,
	CodigoTipo int NOT NULL,
	Imagem varchar(500),
	CodigoFornecedor int,
	PrecoCusto money NOT NULL,
	PrecoNota money NOT NULL,
	PrecoVenda money NOT NULL,
	Quantidade int NOT NULL,
	CONSTRAINT FK_Fornecedor FOREIGN KEY (CodigoFornecedor) REFERENCES Fornecedores(Codigo),
	CONSTRAINT FK_Tipo FOREIGN KEY (CodigoTipo) REFERENCES Tipos(Codigo),
	CONSTRAINT FK_Cor FOREIGN KEY (CodigoCor) REFERENCES Cores(Codigo),
	PRIMARY KEY(Codigo)
)

CREATE TABLE Usuarios(
	Codigo int identity,
	Usuario varchar(50),
	Senha varchar(20),
	Administrador bit,
	PRIMARY KEY(Codigo)
)



