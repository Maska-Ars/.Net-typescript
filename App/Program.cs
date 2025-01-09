using App.DataAccess;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddCors(options =>{
    options.AddPolicy("AllowMyOrigin", policy =>{
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();

using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
await dbContext.Database.EnsureCreatedAsync();



app.UseCors("AllowMyOrigin");
app.MapControllers();
app.Run();
