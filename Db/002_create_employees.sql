create table if not exists employees (
  id serial primary key,
  first_name varchar(50) not null,
  middle_name varchar(50) not null,
  last_name varchar(50) not null,
  employee_details_id int not null references employees_details(id) on delete cascade,
  is_actual boolean default true,
  created_at timestamptz default now(),
  updated_at timestamptz default now()
)

create table if not exists employees_details (
  id serial primary key,
  tax_code varchar(50) unique not null,
  address text not null,
  email varchar(50) unique not null,
  phone varchar(50) not null,
  birth_date date not null,
  gender gender_enum not null,
  picture varchar(100) not null,
  created_at timestamptz default now(),
  updated_at timestamptz default now()
)
create type gender_enum as enum ('male', 'female');

