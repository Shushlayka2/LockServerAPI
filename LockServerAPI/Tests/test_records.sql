delete from referencedata.codes returning *;
insert into referencedata.codes (id, code, lock_id) values (1, 'ASD-FGH-JKL', '1');