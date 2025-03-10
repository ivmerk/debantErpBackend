create table if not exists specialties (
    id serial primary key,
    name text unique not null,
    is_actual boolean,
    created_at timestamptz default now(),

);

create table if not exists employee_specialties (
    id serial primary key,
    employee_id int references employees(id) on delete cascade,
    specialty_id int references specialties(id) on delete cascade,
    is_actual boolean,
    created_at timestamptz default now(),
    updated_at timestamptz default now(),
    unique(employee_id, specialty_id)
);

create function update_modified_column() 
returns trigger as $$
begin
    new.updated_at = now();
    return new;
end;
$$ language 'plpgsql';

create trigger set_timestamp
before update on employee_specialties
for each row
execute function update_modified_column();
