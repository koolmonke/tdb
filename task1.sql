alter table students
    add age int;


-- а
update students
set age = datediff(year, dob, getdate());

-- б
create trigger StudentsAge
    on [dbo].[students]
    after insert as
begin
    update students
    set age = datediff(year, dob, getdate())
    where age is null;
end

-- в
create procedure GenerateAge as
update students
set age = datediff(year, dob, getdate());
GO

exec GenerateAge