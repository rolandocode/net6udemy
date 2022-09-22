using Microsoft.EntityFrameworkCore;
using Infrastructure;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var serverVerion = new MySqlServerVersion(new Version(8,0,30));

builder.Services.AddDbContext<TiendaContext>(options => {
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), serverVerion);
});



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope= app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    var  loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try {
        var context = services.GetRequiredService<TiendaContext>();
        await context.Database.MigrateAsync();
        await TiendaContextSeed.SeedAsync(context, loggerFactory);

    }
    catch(Exception ex){
        var logger =  loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "Ocurrio un error durante la migración");
    }

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
