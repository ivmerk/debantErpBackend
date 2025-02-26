create table if not exists  app_user (
    id serial primary key,
    first_name varchar(50),
    last_name varchar(50),
    phone varchar(50),
    role int,
    email varchar(50),
    password varchar(100),
    salt varchar(50),
    status int
);

create table if not exists person  (
    id serial primary key,
    first_name varchar(50),
    middle_name varchar(50),
    last_name varchar(50),
    phone varchar(50),
    user_role varchar(50),
    tax_code varchar(50),
    address varchar(50),
    email varchar(50),
    password varchar(100),
    salt varchar(50),
    status int
);
