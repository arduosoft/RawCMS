
# Background Jobs
It is possible to schedule background jobs inside the application. This job can be done in two ways.

1. C# code using the plugin
2. JS code using UI

## C# code using the plugin
Inside your plugin you have to implement a Lamba with background job role. Here a sample:

```cs 
 public class PingJob : BackgroundJobInstance

{
    public override string CronExpression => Hangfire.Cron.Minutely();

    public override string Name => "Ping Every minute";

    public override string Description => "Ping Every minute";

    protected ILogger logger;

    public PingJob(ILogger logger)
    {
        this.logger = logger;
    }

    public override void Execute(JObject data)
    {
        this.logger.LogInformation($"Job triggered, with data {data}");
    }
}
```

## JS code using UI

This feature is in progess. It will be possible to add a schedulation to a js lambda from the user interface.