WebApplication.CreateBuilder(args);

public static class StartupExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(c => c.RegisterServicesFromAssembly(typeof(Program).Assembly));
        //services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static WebApplication SharedSetup(WebApplicationBuilder builder)
    {
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
    
    public static void ConfigureApplication(this WebApplicationBuilder builder)
    {
        var app = SharedSetup(builder);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }

    public static WebApplication ConfigureLambdaApplication(this WebApplicationBuilder builder)
    {
        return SharedSetup(builder);
    }
}
    
    