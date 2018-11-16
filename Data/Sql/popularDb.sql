USE [ReservasDataBase]
GO

INSERT INTO [dbo].[Hoteis]
           ([Nome], [Endereco], [Numero], [Complemento], [Cep], [Cidade], [Bairro], [Uf], [Ddd], [Telefone], [Descricao])
     VALUES
           ('Hotel Teste', 'Rua Padre', 2 ,'Comp Teste', '11222-121', 'Curitiba', 'Teste', 'PR', '(41)', '5555-5555', 'Desc um'),
		   ('Hotel Positivo', 'Rua Bispo', 2 ,'Comp Teste', '11222-121', 'Curitiba', 'Teste', 'PR', '(41)', '6666-6666', 'Desc dois'),
		   ('Hotel Curitiba', 'Rua Peao', 2 ,'Comp Teste', '11222-121', 'Curitiba', 'Teste', 'PR', '(41)', '8888-8888', 'Desc tres'),
		   ('Hotel Bola', 'Rua Torre', 2 ,'Comp Teste', '11222-121', 'Curitiba', 'Teste', 'PR', '(41)', '2222-2222', 'Desc quatro'),
		   ('Hotel Quadrado', 'Rua Cavalo', 2 ,'Comp Teste', '11222-121', 'Curitiba', 'Teste', 'PR', '(41)', '1111-1111', 'Desc cinco');

INSERT INTO [dbo].[Quartos]
           ([HotelId],[Titulo],[Descricao],[Quantidade],[Disponiveis],[MaximoOcupantes],[ValorDiaria],[ValorDiariaCrianca],[DiariaPorOcupante])
     VALUES
           (2,'qtr21','desc qtr21',4,4,2,'120.40','50.00',0),
		   (1,'qtr11','desc qtr11',2,2,4,'145.90','45.80',0),
		   (1,'qtr12','desc qtr12',3,3,2,'230.50','100.70',1),
		   (4,'qtr41','desc qtr41',4,4,3,'230.10','68.40',0),
		   (4,'qtr42','desc qtr42',5,5,2,'90.60','25.10',1),
		   (4,'qtr43','desc qtr43',8,8,2,'178.99','20.90',1),
		   (2,'qtr22','desc qtr22',6,6,1,'147.71','10.20',1),
		   (2,'qtr23','desc qtr23',2,2,2,'158.20','78.0',0),
		   (5,'qtr51','desc qtr51',10,10,2,'110.40','46.45',1),
		   (5,'qtr52','desc qtr52',2,2,2,'100.80','130.20',0);

INSERT INTO [dbo].[Turistas]
           ([Nome],[Email],[Sexo],[DataNascimento],[Cpf],[Senha])
     VALUES
           ('João da Silva','joao@teste.com',1,'10-05-1990','111.111.111-11','12345'),
		   ('Maria da Silva','maria@teste.com',0,'06-04-1992','222.222.222-22','12345'),
		   ('Manoel Ferreira','mano@teste.com',1,'10-08-1991','333.333.333-33','12345'),
		   ('Pedro da Rocha','pedro@teste.com',1,'12-02-1995','444.444.444-44','12345'),
		   ('Paulo Pereira','paulo@teste.com',1,'02-01-1986','555.555.555-55','12345'),
		   ('Manoela Santos','mm@teste.com',0,'01-04-1989','666.666.666-66','12345'),
		   ('Fernando','fer@teste.com',1,'09-04-1990','777.777.777-77','12345'),
		   ('Fernanda Lacerda','fernanda@te.com',0,'10-06-1990','888.888.888-88','12345'),
		   ('Julio de Paula','ju@teste.com',1,'04-01-1991','999.999.999-99','12345'),
		   ('Gustavo Andre','gus@teste.com',1,'10-04-1985','123.456.111-11','12345');

INSERT INTO [dbo].[Reservas]
           ([DataReserva],[TuristaId],[Chegada],[Partida],[ValorDiaria],[QuartoId])
     VALUES
           ('2018-05-10',1,'2018-06-12','2018-06-14','230.00',2),
		   ('2018-05-11',1,'2018-05-14','2018-05-15','120.00',1),
		   ('2018-06-15',2,'2018-06-17','2018-06-18','124.00',3),
		   ('2018-07-20',4,'2018-07-21','2018-07-22','90.00',4),
		   ('2018-02-05',1,'2018-03-08','2018-03-09','156.00',5),
		   ('2018-08-14',3,'2018-08-16','2018-08-18','178.00',5),
		   ('2018-03-11',3,'2018-03-12','2018-03-14','100.00',2),
		   ('2018-05-23',1,'2018-06-16','2018-06-20','320.00',3),
		   ('2018-04-13',4,'2018-05-12','2018-05-17','177.00',2),
		   ('2018-04-03',1,'2018-04-11','2018-04-14','189.00',1);

GO
