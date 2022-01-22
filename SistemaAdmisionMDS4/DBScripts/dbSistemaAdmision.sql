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
	fecha datetime not null,
	foreign key (dni) references TRecibo(dni)
);

create table TRecibo(
	id int Identity(1,1) not null primary key,
	nroRecibo varchar(20) not null unique,
	dni varchar(8) not null unique,
);



select*
from TRecibo

use DBSistemaAdmision
go

insert into TRecibo Values ('R-00001','12345678')
insert into TRecibo Values ('R-00002','23456789')
insert into TRecibo Values ('R-00003','34567891')
insert into TRecibo Values ('R-00004','45678912')
insert into TRecibo Values ('R-00005','56789123')





insert into TPostulante Values ('12345678','CAYO ABEL QUEKQAÑO QUISPE',21/01/2022)
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