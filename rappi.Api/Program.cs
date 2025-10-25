using rappi.Infrastructure.Data;
using rappi.Infrastructure.Extensions;  // para traer la funcion de conexion 

var builder = WebApplication.CreateBuilder(args);


//obtener la cadena de conexion 

var connetionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Inyectar infrastructura (DbContext)
builder.Services.AddInfrastructure(builder.Configuration);


// Add services to the container.
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

app.UseHttpsRedirection();

app.Run();
