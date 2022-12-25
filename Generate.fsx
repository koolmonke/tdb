open System
open System.IO

let choiceFromArray<'a> (from: 'a array) = from[Random.Shared.Next(from.Length)]

let firstNames, middleNames, lastNames =
    let lines =
        File.ReadLines(__SOURCE_DIRECTORY__ + "/names.txt")
        |> Seq.map (fun line -> line.Split())
        |> Seq.toArray

    lines[0], lines[1], lines[2]


let startDate = DateOnly(1995, 1, 1)
let endDate = DateOnly(2005, 12, 31)

let generateInserts () =
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

    Seq.init 1000 (fun _ -> generateLines 1000)

let inserts =
    Seq.delay generateInserts
    |> Seq.chunkBySize 500
    |> Seq.indexed

for i, insert in inserts do
    File.WriteAllText(__SOURCE_DIRECTORY__ + $"/init/{i + 1}-insert.sql", String.concat "\n" insert)
