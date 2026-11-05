
open System

let workerTask (taskId: int) (waitTime: int) =
    async {
        printfn "Starting task %d" taskId

        do! Async.Sleep(waitTime)

        printfn "Completed task %d" taskId

        return sprintf "Task %d completed" taskId
    }

let main argv =

    printfn "Starting concurrent tasks"

    let jobs = [
        workerTask 1 2000
        workerTask 2 3000
        workerTask 3 4000
    ]

    let parallelTask = Async.Parallel jobs

    let results = Async.RunSynchronously parallelTask

    results
    |> Array.iter (fun result ->
        printfn "Result: %s" result
    )

    0

main [||]
