open System
open System.IO

let firstNames, middleNames, lastNames =
    let lines =
        File.ReadLines(__SOURCE_DIRECTORY__ + "/names.txt")
        |> Seq.map (fun item -> item.Split())
        |> Seq.toArray

    let firstNames = lines[0]
    let middleNames = lines[1]
    let lastNames = lines[2]
    firstNames, middleNames, lastNames


let createGenerator =
    seq {
        "create table students(first_name varchar(100), last_name varchar(100), middle_name varchar(100), dob date, id serial primary key);"
        "insert into students(first_name, last_name, middle_name, dob) values "
    }

let startDate = new DateOnly(1995, 1, 1)
let endDate = new DateOnly(2005, 12, 31)

let generateLine () =
    let lastName, firstName, middleName, date =
        let random = Random.Shared
        let firstNamaeIndex = random.Next(firstNames.Length - 1)
        let middleNameIndex = random.Next(middleNames.Length - 1)
        let lastNameIndex = random.Next(lastNames.Length - 1)

        let date =
            DateOnly.FromDayNumber(random.Next(startDate.DayNumber, endDate.DayNumber))

        lastNames[lastNameIndex], firstNames[firstNamaeIndex], middleNames[middleNameIndex], date.ToString("yyyy-MM-dd")


    sprintf "('%s', '%s', '%s', '%s')%s" firstName lastName middleName date


let result =
    Seq.concat [ createGenerator
                 seq {
                     for i in 1..1_000_000 ->
                         if i = 1_000_000 then
                             generateLine () ";"
                         else
                             generateLine () ","
                 } ]

File.WriteAllLines(__SOURCE_DIRECTORY__ + "/init/generated.sql", result)
