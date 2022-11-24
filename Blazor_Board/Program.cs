using Blazor_Board;
using Blazor_Board.Core.Http;
using Blazor_Board.Core.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddSingleton<ISectionService, SectionService>();
//builder.Services.AddSingleton<HttpClientHelper>();

builder.Services.AddHttpClient<ISectionService, SectionService>(client =>
{
	client.BaseAddress = new Uri("http://192.168.1.100:5004/");
	
});

builder.Services.AddMudServices();

await builder.Build().RunAsync();