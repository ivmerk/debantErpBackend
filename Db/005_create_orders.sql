create table if not exists orders (
  id serial primary key,
  number varchar(50) unique not null,
  created_at timestamptz default now(),
  updated_at timestamptz default now()
);
