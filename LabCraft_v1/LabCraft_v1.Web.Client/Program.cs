using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using LabCraft_v1.Shared.Services;
using LabCraft_v1.Web.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Add device-specific services used by the LabCraft_v1.Shared project
builder.Services.AddSingleton<IFormFactor, FormFactor>();

await builder.Build().RunAsync();
