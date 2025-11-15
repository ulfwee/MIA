using FluentValidation;
using FluentValidation.AspNetCore;
using MongoDB.Driver;
using MyWebApi.Models;
using MyWebApi.Profiles;
using MyWebApi.Repositories.Implementations;
using MyWebApi.Repositories.Interfaces;
using MyWebApi.Services.Implementations;
using MyWebApi.Services.Interfaces;
using MyWebApi.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(MasterProfile));
builder.Services.AddAutoMapper(typeof(BookingProfile));
builder.Services.AddAutoMapper(typeof(TodoItemProfile));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<MasterValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<BookingValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TodoItemValidator>();

builder.Services.AddScoped<IMasterRepository, MasterRepository> ();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMasterService, MasterService>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IUserService, UserService>();

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
