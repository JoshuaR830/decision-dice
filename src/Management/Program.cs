var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.ConfigureApplication();