var builder = DistributedApplication.CreateBuilder(args);

// API de dados
var api = builder.AddProject<Projects.ProjetoGS_ApiService>("apiservice");

// Front-end MVC consumindo a API
builder.AddProject<Projects.ProjetoGS_Web>("webfrontend")
    .WithReference(api);

builder.Build().Run();
