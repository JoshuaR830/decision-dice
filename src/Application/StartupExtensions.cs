WebApplication.CreateBuilder(args);

public static class StartupExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
        services.AddSingleton<IAWSHelper, AWSHelper>();

        return services;
    }
}