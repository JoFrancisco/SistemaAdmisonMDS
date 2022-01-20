use DBSistemaAdmision
create table TLogin(
	id int Identity(1,1) not null primary key,
	codUsuario varchar(40) not null,
	nombreUsuario nvarchar(30) not null,
	contrasenia varchar(30) not null ,
	tipoUsuario varchar(40) not null check (tipoUsuario = 'Administrador' or tipoUsuario = 'Alumno')
);