using BlazorBoard_Api.DataAccess;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using LinqToDB.SchemaProvider;
using Microsoft.AspNetCore.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var policyName = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(name: policyName,
					  builder =>
					  {
						  builder
							//.WithOrigins("http://localhost:5068")
							.AllowAnyMethod()
							.AllowAnyOrigin()
							.AllowAnyHeader();
					  });
});

builder.Services.AddLinqToDBContext<BlazorBoardDB>((provider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"))
    .UseDefaultLogging(provider);
    provider = builder.Services.BuildServiceProvider();
    ;
    
    
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(policyName);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
