using todo_api;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<TodoDatabaseSettings>(
    builder.Configuration.GetSection("TodoDatabase"));

builder.Services.AddTransient<TodoRepository>();

var corsOrigins = builder.Configuration["CorsOrigins"];

builder.Services.AddCors(x => x.AddDefaultPolicy(builder => builder.WithOrigins(corsOrigins).AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", () => "Hello World!");
    endpoints.MapGet("/todos", (TodoRepository repository) => repository.GetAll());
});



app.Run();