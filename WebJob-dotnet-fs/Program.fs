open System
open Microsoft.Azure.WebJobs
open Microsoft.Azure.WebJobs.Host
open Microsoft.Azure.WebJobs.Extensions.Timers


let HelloTimer ( [<TimerTrigger("0 */1 * * * *")>] timerInfo : TimerInfo) (log : TraceWriter) =
    log.Info("Hello from F# webjob!");


[<EntryPoint>]
let main argv =
    let config = new JobHostConfiguration()
    config.UseTimers()

    let host = new JobHost(config)
    host.RunAndBlock()

    0

