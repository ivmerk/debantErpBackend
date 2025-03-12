create table if not exists specialities (
    id serial primary key,
    name text unique not null,
    is_actual boolean default true,
    created_at timestamptz default now(),

);

create table if not exists employee_specialities (
    id serial primary key,
    employee_id int references employees(id) on delete cascade,
    speciality_id int references specialities(id) on delete cascade,
    is_actual boolean,
    created_at timestamptz default now(),
    updated_at timestamptz default now(),
    unique(employee_id, speciality_id)
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
