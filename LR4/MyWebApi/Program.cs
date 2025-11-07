using FluentValidation;
using FluentValidation.AspNetCore;
using MyWebApi.Models;
using MyWebApi.Validators;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Реєструємо контролери і FluentValidation
builder.Services.AddControllers().AddFluentValidation();
// Реєструємо всі валідатори автоматично
builder.Services.AddValidatorsFromAssemblyContaining<TodoItemValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BookingValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MasterValidator>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();     
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();