#r "nuget: MongoDB.Driver, 2.29.0"

open System
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open MongoDB.Driver

type GradeCategory =
    | Topper
    | Pass
    | Fail

[<CLIMutable>]
type Student = {
    [<BsonId>]
    Id: int
    Name: string
    Course: string
    Marks: int
}

let evaluateGrade marks =
    match marks with
    | m when m >= 80 -> Topper
    | m when m >= 50 -> Pass
    | _ -> Fail

let gradeToString grade =
    match grade with
    | Topper -> "Topped the exam"
    | Pass -> "Pass"
    | Fail -> "Fail"

let students = [
    { Id = 1; Name = "Subhrajeet"; Course = "Computer Science"; Marks = 85 }
    { Id = 2; Name = "Deep"; Course = "Information Technology"; Marks = 72 }
    { Id = 3; Name = "Harsh"; Course = "Data Science"; Marks = 45 }
]

let passedStudents = students |> List.filter (fun s -> s.Marks >= 50)

let studentSummaries = 
    students 
    |> List.map (fun s -> 
        let category = evaluateGrade s.Marks
        let result = gradeToString category
        sprintf "%s - Marks: %d (%s)" s.Name s.Marks result
    )

let mongoUri = "mongodb+srv://subhrajeet1950_db_user:HoQnaZj5wgcbM439@cluster0.tem1fwt.mongodb.net/?compressors=zlib"

let runMongoWorkflow () =
    async {
        printfn "Connecting to MongoDB..."
        let client = MongoClient(mongoUri)
        let database = client.GetDatabase("CollegeDB")
        let collection = database.GetCollection<Student>("Students")

        do! collection.Database.DropCollectionAsync("Students") |> Async.AwaitTask

        printfn "\n--- CREATE ---"
        do! collection.InsertManyAsync(students) |> Async.AwaitTask
        printfn "Inserted student records."

        printfn "\n--- READ ---"
        let! cursor = collection.FindAsync(Builders<Student>.Filter.Empty) |> Async.AwaitTask
        let! allStudents = cursor.ToListAsync() |> Async.AwaitTask
        for s in allStudents do
            let status = gradeToString (evaluateGrade s.Marks)
            printfn "ID: %d | Name: %-10s | Course: %-22s | Marks: %d | Status: %s" s.Id s.Name s.Course s.Marks status

        printfn "\n--- UPDATE ---"
        let updateFilter = Builders<Student>.Filter.Eq((fun s -> s.Id), 3)
        let updateDef = Builders<Student>.Update.Set((fun s -> s.Marks), 60)
        let! _ = collection.UpdateOneAsync(updateFilter, updateDef) |> Async.AwaitTask
        printfn "Updated Harsh marks to 60."

        printfn "\n--- DELETE ---"
        let deleteFilter = Builders<Student>.Filter.Eq((fun s -> s.Id), 2)
        let! _ = collection.DeleteOneAsync(deleteFilter) |> Async.AwaitTask
        printfn "Deleted Deep from database."

        printfn "\n--- FINAL STATE ---"
        let! finalCursor = collection.FindAsync(Builders<Student>.Filter.Empty) |> Async.AwaitTask
        let! finalList = finalCursor.ToListAsync() |> Async.AwaitTask
        for s in finalList do
            let status = gradeToString (evaluateGrade s.Marks)
            printfn "ID: %d | Name: %-10s | Course: %-22s | Marks: %d | Status: %s" s.Id s.Name s.Course s.Marks status
    }

printfn "=== Domain Processing (map, filter, pattern matching) ==="
printfn "\nAll Students:"
for summary in studentSummaries do
    printfn "  %s" summary

printfn "\nPassed Students (Marks >= 50):"
for s in passedStudents do
    printfn "  %s (%d marks)" s.Name s.Marks

printfn "\n=== Database CRUD with Async Workflow ==="
runMongoWorkflow () |> Async.RunSynchronously
