namespace Financeiro.Infrastructure.BackgroundServices.DependencyInjection;

public static class QuartzModule
{
    public static IServiceCollection AddQuartzModule(
        this IServiceCollection services)
    {
        services.AddQuartz(q =>
        {
            var eventStorePublisherJobKey = new JobKey("EventStorePublisherJob");
            q.AddJob<EventStorePublisherJob>(opts => opts.WithIdentity(eventStorePublisherJobKey));
            q.AddTrigger(opts => opts
                .ForJob(eventStorePublisherJobKey)
                .WithIdentity("EventStorePublisherJob-trigger")
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(10)
                    .RepeatForever()
                )
            );
        });
        
        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

        return services;
    }
}