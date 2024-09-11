create database bd1;
use bd1;

create table cliente(
codigo int auto_increment primary key,
nome varchar(40) not null,
telefone varchar(20) not null,
email varchar(40) not null,
senha varchar(20)
);

insert into cliente (nome, telefone, email, senha) values('admin', '111111111', 'admin@gmail.com', 123456);

select * from cliente;