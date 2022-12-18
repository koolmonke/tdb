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

CREATE FUNCTION get_same_last_name_count(@lastname NVARCHAR(100)) RETURNS INT AS
BEGIN
    DECLARE @return INT;
    SELECT @return = count(*) from students where last_name = @lastname;
    RETURN @return;
END;


update students
set students.same_last_name_count = dbo.get_same_last_name_count(last_name);
