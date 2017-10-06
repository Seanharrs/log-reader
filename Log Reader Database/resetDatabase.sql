drop schema if exists kag_log_reader;
create schema kag_log_reader;

use kag_log_reader;

create table files(
	file_name varchar(30),
	creation_time datetime not null,
	primary key (file_name))
character set utf8;

create table chat(
	file_name varchar(30),
	line_number int,
	time_said time not null,
	who_said varchar(50) not null,
	what_said varchar(500) not null,
	primary key (file_name, line_number),
	foreign key (file_name) references files(file_name))
character set utf8;