-- а
select top 1 last_name, count(*) same_count
from students
group by last_name
order by same_count desc;

-- б
create index last_name_idx on students (last_name);

select top 1 last_name, count(*) same_count
from students
group by last_name
order by same_count desc;

-- в
create procedure same_last_name_count as
select top 1 last_name, count(*) same_count
from students
group by last_name
order by same_count desc;
go;

exec same_last_name_count