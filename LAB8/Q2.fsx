#r "nuget: Microsoft.Data.Sqlite, 8.0.11"
#r "nuget: MongoDB.Driver, 2.29.0"

open System
open Microsoft.Data.Sqlite
open MongoDB.Bson
open MongoDB.Bson.Serialization.Attributes
open MongoDB.Driver

[<CLIMutable>]
type Student = {
    [<BsonId>]
    Id: int
    Name: string
    Age: int
    Course: string
}

let runSqliteCrud () =
    printfn " PART 1: Relational Database CRUD Operations (SQLite)"


    let connectionString = "Data Source=Shareable;Mode=Memory;Cache=Shared"
    use connection = new SqliteConnection(connectionString)
    connection.Open()

    let executeNonQuery (sql: string) (parameters: (string * obj) list) =
        use cmd = connection.CreateCommand()
        cmd.CommandText <- sql
        for (name, value) in parameters do
            cmd.Parameters.AddWithValue(name, value) |> ignore
        cmd.ExecuteNonQuery() |> ignore

    let printCurrentState (stepName: string) =
        printfn "\n--- State after: %s ---" stepName
        use cmd = connection.CreateCommand()
        cmd.CommandText <- "SELECT Id, Name, Age, Course FROM Students ORDER BY Id"
        use reader = cmd.ExecuteReader()
        let mutable count = 0
        while reader.Read() do
            count <- count + 1
            let id = reader.GetInt32(0)
            let name = reader.GetString(1)
            let age = reader.GetInt32(2)
            let course = reader.GetString(3)
            printfn "  [Id: %d] Name: %-10s | Age: %d | Course: %s" id name age course
        if count = 0 then
            printfn "  (Table is empty)"

    let createTableSql = """
        CREATE TABLE IF NOT EXISTS Students (
            Id INTEGER PRIMARY KEY,
            Name TEXT NOT NULL,
            Age INTEGER NOT NULL,
            Course TEXT NOT NULL
        );
    """
    executeNonQuery createTableSql []
    printCurrentState "Table Creation"

    // --- 1. CREATE ---
    printfn "\n>> [CREATE] Inserting students: Subhrajeet, Deep, Harsh..."
    let insertSql = "INSERT INTO Students (Id, Name, Age, Course) VALUES (@id, @name, @age, @course);"
    executeNonQuery insertSql [ ("@id", box 1); ("@name", box "Subhrajeet"); ("@age", box 20); ("@course", box "Computer Science") ]
    executeNonQuery insertSql [ ("@id", box 2); ("@name", box "Deep"); ("@age", box 22); ("@course", box "Mathematics") ]
    executeNonQuery insertSql [ ("@id", box 3); ("@name", box "Harsh"); ("@age", box 21); ("@course", box "Physics") ]
    printCurrentState "CREATE (3 Students Inserted)"

    // --- 2. READ ---
    printfn "\n>> [READ] Querying student with Id = 2..."
    use readCmd = connection.CreateCommand()
    readCmd.CommandText <- "SELECT Id, Name, Age, Course FROM Students WHERE Id = @id"
    readCmd.Parameters.AddWithValue("@id", box 2) |> ignore
    use readReader = readCmd.ExecuteReader()
    if readReader.Read() then
        printfn "  Found: [Id: %d] %s, Age %d, Course: %s" 
            (readReader.GetInt32(0)) 
            (readReader.GetString(1)) 
            (readReader.GetInt32(2)) 
            (readReader.GetString(3))
    printCurrentState "READ (Verification)"

    // --- 3. UPDATE ---
    printfn "\n>> [UPDATE] Updating Deep (Id = 2): Age -> 23, Course -> 'Data Science'..."
    let updateSql = "UPDATE Students SET Age = @age, Course = @course WHERE Id = @id;"
    executeNonQuery updateSql [ ("@id", box 2); ("@age", box 23); ("@course", box "Data Science") ]
    printCurrentState "UPDATE (Deep Updated)"

    // --- 4. DELETE ---
    printfn "\n>> [DELETE] Deleting Harsh (Id = 3)..."
    let deleteSql = "DELETE FROM Students WHERE Id = @id;"
    executeNonQuery deleteSql [ ("@id", box 3) ]
    printCurrentState "DELETE (Harsh Removed)"

    printfn "\n[SQLite CRUD Completed Successfully]\n"



let mongoConnectionString = "mongodb+srv://subhrajeet1950_db_user:BqIwWulFqPpI6rYz@cluster0.2kltg2g.mongodb.net/?retryWrites=true&w=majority"
let databaseName = "Lab8DB"
let collectionName = "Students"

let runMongoCrud () =

    printfn " PART 2: NoSQL Database CRUD Operations (MongoDB)"


    if mongoConnectionString = "PASTE_YOUR_MONGODB_CONNECTION_STRING_HERE" then
        printfn "\n[INFO] Please set 'mongoConnectionString' with your MongoDB Atlas or local URI."
        printfn "       Example: mongodb+srv://<username>:<password>@cluster0.mongodb.net/?retryWrites=true&w=majority"
    else
        try
            printfn "\nConnecting to MongoDB..."
            let client = MongoClient(mongoConnectionString)
            let database = client.GetDatabase(databaseName)
            let collection = database.GetCollection<Student>(collectionName)

            collection.Database.DropCollection(collectionName)

            let printMongoState (stepName: string) =
                printfn "\n--- State after: %s ---" stepName
                let students = collection.Find(Builders<Student>.Filter.Empty).ToList()
                if students.Count = 0 then
                    printfn "  (Collection is empty)"
                else
                    for s in students do
                        printfn "  [Id: %d] Name: %-10s | Age: %d | Course: %s" s.Id s.Name s.Age s.Course

            // --- 1. CREATE ---
            printfn "\n>> [CREATE] Inserting documents: Subhrajeet, Deep, Harsh..."
            let initialStudents = [
                { Id = 1; Name = "Subhrajeet"; Age = 20; Course = "Computer Science" }
                { Id = 2; Name = "Deep"; Age = 22; Course = "Mathematics" }
                { Id = 3; Name = "Harsh"; Age = 21; Course = "Physics" }
            ]
            collection.InsertMany(initialStudents)
            printMongoState "CREATE (3 Documents Inserted)"

            // --- 2. READ ---
            printfn "\n>> [READ] Querying student with Id = 2..."
            let filterById2 = Builders<Student>.Filter.Eq((fun s -> s.Id), 2)
            let foundStudent = collection.Find(filterById2).FirstOrDefault()
            if not (isNull (box foundStudent)) then
                printfn "  Found: [Id: %d] %s, Age: %d, Course: %s" foundStudent.Id foundStudent.Name foundStudent.Age foundStudent.Course
            printMongoState "READ (Verification)"

            // --- 3. UPDATE ---
            printfn "\n>> [UPDATE] Updating Deep (Id = 2): Age -> 23, Course -> 'Data Science'..."
            let updateDef = 
                Builders<Student>.Update
                    .Set((fun s -> s.Age), 23)
                    .Set((fun s -> s.Course), "Data Science")
            collection.UpdateOne(filterById2, updateDef) |> ignore
            printMongoState "UPDATE (Deep Updated)"

            // --- 4. DELETE ---
            printfn "\n>> [DELETE] Deleting Harsh (Id = 3)..."
            let filterById3 = Builders<Student>.Filter.Eq((fun s -> s.Id), 3)
            collection.DeleteOne(filterById3) |> ignore
            printMongoState "DELETE (Harsh Removed)"

            printfn "\n[MongoDB CRUD Completed Successfully]\n"
        with
        | ex ->
            printfn "\n[ERROR connecting to MongoDB]: %s" ex.Message

runSqliteCrud ()
runMongoCrud ()

