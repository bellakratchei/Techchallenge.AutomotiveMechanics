using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text.Json.Serialization;
using TechChallenge.AutomotiveMechanics.Crosscutting.Ioc;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services.AddControllers()
    .AddNewtonsoftJson()
    .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles)
    .AddNewtonsoftJson(options =>
     {
         options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
         options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
     });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationConfiguration(builder.Configuration);
builder.Services.AddInfrastructureConfiguration(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseReDoc(c =>
{
    c.DocumentTitle = "TechChallenge - Automotive Mechanics - Plataforma de Gest�o para Mec�nica 100% Digital";
    c.SpecUrl = "/swagger/v1/swagger.json";
});

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();