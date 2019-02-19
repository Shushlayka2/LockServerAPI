begin transaction;

drop table if exists referencedata.users cascade;
drop table if exists referencedata.devices cascade;
drop table if exists referencedata.codes cascade;
drop trigger if exists encrypt_code on referencedata.codes;
drop trigger if exists encrypt_device_id on referencedata.devices;
drop function if exists encrypt_code();
drop function if exists encrypt_device_id();
drop function if exists search_device(text);
drop extension if exists pgcrypto;

create table referencedata.users
(
  id integer primary key,
  name text not null,
  email text not null unique
);

create table referencedata.devices
(
  id text primary key,
  user_id integer references referencedata.users(id) on delete cascade
);

create table referencedata.codes
(
  id integer primary key,
  code text not null,
  user_id integer references referencedata.users(id) on delete cascade
);

create extension pgcrypto;

create or replace function encrypt_code() returns trigger as $encrypt_code$
begin
    new.code = crypt(new.code, gen_salt('bf'));
    return new;
end;
$encrypt_code$ language plpgsql;

create trigger encrypt_code before insert or update on referencedata.codes
for each row execute procedure encrypt_code();

create or replace function encrypt_device_id() returns trigger as $encrypt_device_id$
begin
    new.id = crypt(new.id, gen_salt('bf'));
    return new;
end;
$encrypt_device_id$ language plpgsql;

create trigger encrypt_device_id before insert or update on referencedata.devices
for each row execute procedure encrypt_device_id();

create or replace function search_device(text)
returns boolean as $$
begin
    perform  from referencedata.devices where id = crypt($1, id);
    return FOUND;
end;
$$ language plpgsql;

commit;