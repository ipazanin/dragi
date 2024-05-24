#pragma warning disable IDE0005 // Using directive is unnecessary.
using Dragi.WebUi;
#pragma warning restore IDE0005 // Using directive is unnecessary.
using Dragi.WebUi.Extensions;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.AddServices();

await builder.Build().RunAsync();
