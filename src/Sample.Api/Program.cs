using Sample.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistence();
builder.Services.AddDomain();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

/// <summary>
///  Only here to allow integration testing
/// </summary>
public partial class Program { }
