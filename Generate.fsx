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


let startDate = DateOnly(1995, 1, 1)
let endDate = DateOnly(2005, 12, 31)


let createStmts =
    "create table students(first_name nvarchar(100), last_name nvarchar(100), middle_name nvarchar(100), dob date, id int identity(1, 1) primary key);"

let generateInserts =
    let generateLine endChar =
        let lastName = choiceFromArray lastNames
        let firstName = choiceFromArray firstNames
        let middleName = choiceFromArray middleNames

        let date =
            DateOnly
                .FromDayNumber(Random.Shared.Next(startDate.DayNumber, endDate.DayNumber))
                .ToString("yyyy-MM-dd")

        sprintf "(N'%s', N'%s', N'%s', '%s')%c" firstName lastName middleName date endChar

    let generateLines n =
        seq {
            yield "insert into students(first_name, last_name, middle_name, dob) values"
            for _ in 1 .. (n - 1) -> generateLine ','
            yield generateLine ';'
        }
        |> String.concat "\n"

    seq { for _ in 1..1000 -> generateLines 1000 }
    |> Seq.chunkBySize 500

File.WriteAllText(__SOURCE_DIRECTORY__ + "/init/0-create.sql", createStmts)

for i, insert in generateInserts |> Seq.indexed do
    File.WriteAllText(__SOURCE_DIRECTORY__ + $"/init/{i + 1}-insert.sql", String.concat "\n" insert)
