/* MS SQL Server */
CREATE DATABASE people;

CREATE TABLE person (
	ssn char(8) NOT NULL PRIMARY KEY,
	first_name varchar(30) NOT NULL, 
	last_name varchar(30) NOT NULL,
	birthdate date NOT NULL,
	ethnicity varchar(30)
);

CREATE TABLE address (
	ssn char(8) FOREIGN KEY REFERENCES person(ssn),
	address_type varchar(30) NOT NULL,
	line1 varchar(255) NOT NULL,
	line2 varchar(255),
	city varchar(255) NOT NULL,
	state char(2) NOT NULL,
	zip_code varchar(10) NOT NULL,
	CONSTRAINT id PRIMARY KEY (ssn,address_type)
);

CREATE TABLE phone_number (
	ssn char(8) FOREIGN KEY REFERENCES person(ssn),
	number_type varchar(30),
	phone_number char(10),
	CONSTRAINT id PRIMARY KEY (ssn,number_type)
);

CREATE TABLE donation (
	id int NOT NULL PRIMARY KEY,
	ssn char(8) FOREIGN KEY REFERENCES person(ssn),
	donation_type varchar(30), 
	donation_date date,
	amount bigmoney,
	memo varchar(255)
);
