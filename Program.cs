using System.Runtime.Serialization.DataContracts;
using blogapi.Serivces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<BlogItemSerivce>();
builder.Services.AddScoped<PasswordSerivce>();
builder.Services.AddScoped<UserSerivce>();

var connectionString = builder.Configuration.GetConnectionString("myBlogString2");
builder.Services.AddDbContext<DataContract>(options => options.UseSqlServer(connectionString));
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
     app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
