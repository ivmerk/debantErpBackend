create table if not exists production_rates (
  id serial primary key,
  employee_id int references employees(id) on delete cascade,
  product_id int references products(id) on delete cascade,
  is_actual boolean default true,
  created_at timestamptz default now(),
  updated_at timestamptz default now(),
  unique(employee_id, product_id)
);
