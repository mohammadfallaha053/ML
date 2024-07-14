using ML.EF;
using Microsoft.EntityFrameworkCore;

using System.Text.Json.Serialization;
using ML.Core;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(Program));
var c= builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.
                AddDbContext<AppDbContext>(
                 options => options.
                 UseSqlServer(c,b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

builder.Services.AddCors();

//builder.Services.AddTransient(typeof(IBaseRepository<>),typeof(BaseRepository<>));

builder.Services.AddTransient<IUintOfWork,UintOfWork>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{

    app.UseSwagger();
    app.UseSwaggerUI();
//}



app.UseHttpsRedirection();


app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();



app.MapControllers();

app.Run();
