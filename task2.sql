alter table students
    add column same_last_name_count int;

-- а
update students a 
set same_last_name_count = b.same_count
from (select last_name, count(*) same_count
      from students
      group by last_name) b
where a.last_name = b.last_name;

-- б
create index last_name_idx on students using brin (last_name) ;

update students a
set same_last_name_count = b.same_count
from (select last_name, count(*) same_count
      from students
      group by last_name) b
where a.last_name = b.last_name;

-- в
create procedure generate_same_last_name_count() as
$same_last_name_count$
begin
update students a
set same_last_name_count = b.same_count
from (select last_name, count(*) same_count
      from students
      group by last_name) b
where a.last_name = b.last_name;
end;
$same_last_name_count$ LANGUAGE plpgsql;

call generate_same_last_name_count()