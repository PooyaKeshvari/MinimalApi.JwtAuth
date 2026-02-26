using MinimalApi.JwtAuth.Composition;
using MinimalApi.JwtAuth.Presentation.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMinimalJwtAuth(builder.Configuration);

var app = builder.Build();
app.UseMinimalJwtAuth();
app.MapIdentityEndpoints();

app.Run();
