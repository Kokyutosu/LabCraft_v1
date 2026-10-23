var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.LabCraft_v1>("labcraft-v1");

builder.AddProject<Projects.LabCraft_v1_Web>("labcraft-v1-web");

builder.Build().Run();
