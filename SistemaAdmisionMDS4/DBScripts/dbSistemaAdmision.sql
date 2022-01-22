use DBSistemaAdmision
create table TLogin(
	id int Identity(1,1) not null primary key,
	codUsuario varchar(8) not null unique,
	nombreUsuario varchar(40) not null,
	contrasenia varchar(40) not null ,
	tipoUsuario varchar(40) not null check (tipoUsuario = 'Administrador' or tipoUsuario = 'Postulante'),
	foreign key (codUsuario) references TPostulante(dni)
);

create table TPostulante(
	dni varchar(8) not null primary key,
	nombres varchar(40) not null,
	fecha datetime not null
);


use DBSistemaAdmision
go
--Insertar datos en TPersonal---------------------
insert into TPostulante Values ('12345678','CAYO ABEL QUEKQA�O QUISPE',21/01/2022)
insert into TPostulante Values ('23456789','JOSE FRANCISCO PUMA POTOCINO',21/01/2022)
insert into TPostulante Values ('34567891','ERICK USCACHI CCAPA',21/01/2022)
insert into TPostulante Values ('45678912','RICHARD MIHAYLOR PUMA SOTOMAYOR',21/01/2022)
insert into TPostulante Values ('56789123','RONALDO QUISPE YAHUIRA',21/01/2022)
go

insert into TLogin Values ('12345678','CAYO','CAYO','Administrador')
insert into TLogin Values ('23456789','JOSE','JOSE','Administrador')
insert into TLogin Values ('34567891','ERICK','ERICK','Administrador')
insert into TLogin Values ('45678912','RICHARD','RICHARD','Administrador')
insert into TLogin Values ('56789123','RONALDO','RONALDO','Administrador')