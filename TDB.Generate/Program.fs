open System
open System.IO

let choiceFromArray<'a> (from: 'a []) = from[Random.Shared.Next(from.Length)]

let firstNames, middleNames, lastNames =
    let lines =
        File.ReadLines(__SOURCE_DIRECTORY__ + "/names.txt")
        |> Seq.map (fun item -> item.Split())
        |> Seq.toArray

    let firstNames = lines[0]
    let middleNames = lines[1]
    let lastNames = lines[2]
    firstNames, middleNames, lastNames


let startDate = new DateOnly(1995, 1, 1)
let endDate = new DateOnly(2005, 12, 31)

let generateLine endChar =
    let lastName, firstName, middleName, date =
        let date =
            DateOnly.FromDayNumber(Random.Shared.Next(startDate.DayNumber, endDate.DayNumber))

        choiceFromArray firstNames, choiceFromArray lastNames, choiceFromArray middleNames, date.ToString("yyyy-MM-dd")


    sprintf "('%s', '%s', '%s', '%s')%c" firstName lastName middleName date endChar


File.WriteAllLines(
    __SOURCE_DIRECTORY__ + "/init/generated.sql",
    seq {
        yield
            "create table students(first_name varchar(100), last_name varchar(100), middle_name varchar(100), dob date, id serial primary key);"

        yield "insert into students(first_name, last_name, middle_name, dob) values"

        for i in 1..1_000_000 ->
            if i = 1_000_000 then
                generateLine ';'
            else
                generateLine ','
    }
)
