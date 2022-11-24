using BlazorBoard_Api.DataAccess;
using LinqToDB.AspNet;
using LinqToDB.AspNet.Logging;
using LinqToDB.Configuration;
using LinqToDB.SchemaProvider;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var MyAllowSpecficOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options => 
{
    options.AddPolicy(name: MyAllowSpecficOrigins,
        builder => { builder.WithOrigins("http://localhost:5068"); });
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
app.UseCors(MyAllowSpecficOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
