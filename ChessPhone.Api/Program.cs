using ChessPhone.Application;
using ChessPhone.Domain;
using ChessPhone.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<PhonePad>, Repository<PhonePad>>();
builder.Services.AddScoped<IRepository<ChessPiece>, Repository<ChessPiece>>();
builder.Services.AddScoped<IChessPieceService, ChessPieceService>();
builder.Services.AddScoped<IPhonePadService, PhonePadService>();
builder.Services.AddScoped<IPhoneNumberService, PhoneNumberService>();

builder.Services.AddCors(options => 
{
    options.AddPolicy(name: "ChessPhone", configurePolicy: policyBuilder => 
    {
        policyBuilder.AllowAnyHeader();
        policyBuilder.AllowAnyMethod();
        policyBuilder.AllowAnyOrigin();
    });
});

var app = builder.Build();

app.UseCors("ChessPhone");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
