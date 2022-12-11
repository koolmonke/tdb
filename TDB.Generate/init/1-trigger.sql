-- 2.б
alter table students
    add column age int;


create function student_age_trigger() returns trigger as $student_age_trigger$
    begin
        NEW.age := date_part('year', age(NEW.dob));
        return NEW;
    end;
$student_age_trigger$ language plpgsql;

create trigger student_age_trigger1 before insert or update on students
    for each row execute procedure student_age_trigger();