alter table students
    add age int;


-- а
update students
set age = datediff(yy, dob, getdate()) - case when (month(dob) > month(getdate())) or (month(dob) = month(getdate()) and day(dob) > day(getdate())) then 1 else 0 end;

-- б
create trigger students_age
    on [dbo].[students]
    after insert as
begin
    update students
    set age = datediff(yy, dob, getdate()) - case when (month(dob) > month(getdate())) or (month(dob) = month(getdate()) and day(dob) > day(getdate())) then 1 else 0 end
    where age is null;
end

-- в
create procedure generate_age as
update students
set age = datediff(yy, dob, getdate()) - case when (month(dob) > month(getdate())) or (month(dob) = month(getdate()) and day(dob) > day(getdate())) then 1 else 0 end;
go

exec generate_age