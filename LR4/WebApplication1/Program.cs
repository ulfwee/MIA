using WebApplication1.Endpoints;
using WebApplication1.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var masters = new List<Master>();
var bookings = new List<Booking>();

builder.Services.AddSingleton(masters);
builder.Services.AddSingleton(bookings);

var app = builder.Build();

app.MapTodoEndpoints();
app.MapUsersEndpoints();
app.MapBookingEndpoints();
app.MapMastersEndpoints();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();      
    app.UseSwaggerUI();    
}

app.Run();