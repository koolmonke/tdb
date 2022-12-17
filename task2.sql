alter table students
    add same_last_name_count int;

-- а
update students
set same_last_name_count = b.same_count
from (select last_name, count(*) same_count
      from students
      group by last_name) b
where students.last_name = b.last_name;

-- б
create index last_name_idx on students (last_name);

update students
set same_last_name_count = b.same_count
from (select last_name, count(*) same_count
      from students
      group by last_name) b
where students.last_name = b.last_name;

-- в
create procedure generate_same_last_name_count as
update students
set same_last_name_count = b.same_count
from (select last_name, count(*) same_count
      from students
      group by last_name) b
where students.last_name = b.last_name;
go;

exec generate_same_last_name_count