alter table students
    add column age int;


-- а
update students
set age = date_part('year', age(dob)); -- 5 sec

-- в

create procedure generate_age() as
$students_age$
begin
update students
set age = date_part('year', age(dob));
end;
$students_age$ language plpgsql;

call generate_age() -- 5 sec 435 ms

