using Blazor_Board;
using Blazor_Board.Core.Services;
using Blazor_Board.Core.Services.Cache;
using Blazor_Board.Models.Data;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net;
using System.Reflection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

Uri baseUri =  new Uri("http://192.168.1.100:5004/");

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorageAsSingleton();

#region Data services

builder.Services.AddHttpClient<IDataService<Section>, SectionService>(client =>
{
    client.BaseAddress = baseUri;
});

builder.Services.AddHttpClient<IDataService<SectionTask>, SectionTaskService>(client =>
{
    client.BaseAddress = baseUri;
});

builder.Services.AddHttpClient<IDataService<User>, UserService>(client =>
{
    client.BaseAddress = baseUri;
});

builder.Services.AddSingleton<DataService>();
#endregion

#region Cache Services

builder.Services.AddSingleton<ICacheService<SectionTask>, SectionTaskCacheService>();
builder.Services.AddSingleton<ICacheService<Section>, SectionCacheService>();
builder.Services.AddSingleton<ICacheService<User>, UserCacheService>();

builder.Services.AddSingleton<CacheServices>();


#endregion

await builder.Build().RunAsync();