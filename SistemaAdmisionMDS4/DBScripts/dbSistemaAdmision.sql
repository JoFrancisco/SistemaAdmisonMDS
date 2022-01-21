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
	fecha date not null
);