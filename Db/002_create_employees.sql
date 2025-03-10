create table if not exists employees (
  id serial primary key,
  first_name varchar(50) not null,
  middle_name varchar(50) not null,
  last_name varchar(50) not null,
  birth_date date not null,
  phone varchar(50) not null,
  tax_code varchar(50) unique not null,
  gender gender_enum not null,
  created_at timestamptz default now(),
  updated_at timestamptz default now()
  is_actual boolean
)

create type gender_enum as enum ('male', 'female');

