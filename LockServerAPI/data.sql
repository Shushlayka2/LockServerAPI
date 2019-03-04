begin transaction;

drop table if exists referencedata.locks cascade;
drop table if exists referencedata.codes cascade;
drop trigger if exists encrypt_code on referencedata.codes;
drop trigger if exists encrypt_device_id on referencedata.locks;
drop function if exists encrypt_code();
drop function if exists encrypt_device_id();
drop function if exists search_code(text);
drop function if exists search_device(text);
drop extension if exists pgcrypto;

create table referencedata.locks
(
  id text primary key,
  device_id text not null
);

create table referencedata.codes
(
  id integer primary key,
  code text not null,
  lock_id text not null
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

create or replace function search_code(code_param text, out result text) as $$
begin
  select lock_id into result from referencedata.codes where code = crypt(code_param, code);
end;
$$ language plpgsql;

create or replace function encrypt_device_id() returns trigger as $encrypt_device_id$
begin
    new.id = crypt(new.id, gen_salt('bf'));
    return new;
end;
$encrypt_device_id$ language plpgsql;

create trigger encrypt_device_id before insert or update on referencedata.locks
for each row execute procedure encrypt_device_id();

create or replace function search_device(text)
returns boolean as $$
begin
    perform  from referencedata.locks where id = crypt($1, id);
    return FOUND;
end;
$$ language plpgsql;

commit;