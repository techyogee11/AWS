using Amazon.SQS;
using AWS_SQS.Helpers;
using AWS_SQS.Models;
using AWS_SQS.Service;

var builder = WebApplication.CreateBuilder(args);

string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


// Add services to the container.
builder.Services.AddControllers();
var appsettings = builder.Configuration.GetSection("ServiceConfiguration");
builder.Services.AddAWSService<IAmazonSQS>();
builder.Services.Configure<ServiceConfiguration>(appsettings);

builder.Services.AddScoped<IAWSSQSService, AWSSQSService>();
builder.Services.AddScoped<IAWSSQSHelper, AWSSQSHelper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, builder =>
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(MyAllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
