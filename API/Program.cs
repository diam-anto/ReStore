using API.Data;
using API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StoreContext>(opt => 
{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//to protect us from different dommains api and client
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseDeveloperExceptionPage(); -> there is in background
app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    // requests headers from our client to server
    opt.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:3000");
});

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// we need to get hold of our DB context service, this is why we create scope variable
var scope = app.Services.CreateScope();

//save our context in
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();

//logger so that we can log any error that we get
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

try
{
    // if we do not have database creates one
    context.Database.Migrate();

    // because we create a static class we can just use the initialize method and pass the context
    DbInitializer.Initialize(context);
}
catch (Exception ex)
{   
    logger.LogError(ex, "A problem occured during migration");
}

app.Run();
