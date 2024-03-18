using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using TodoApi;
using Microsoft.OpenApi.Models;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>();
  builder.Services.AddSwaggerGen();
  builder.Services.AddEndpointsApiExplorer();
  
  builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//שליפה
    app.MapGet("/", async (ToDoDbContext db) =>
    {var data= await db.Items.ToListAsync();
    return data;});
//הוספה
 app.MapPost("/add/{name}", async (string name, ToDoDbContext db) =>
 {Item todo=new Item();
 todo.Name=name;
 todo.IsComplete=false;
    db.Items.Add(todo);
    await db.SaveChangesAsync();
 });
//מחיקה
    app.MapDelete("/delete/{id}", async (int id, ToDoDbContext db) =>
{
    if (await db.Items.FindAsync(id) is Item todo)
    {
        db.Items.Remove(todo);
        await db.SaveChangesAsync();
        return Results.NoContent();
    }
    return Results.NotFound();
});
//עדכון
app.MapPut("/put/{id}/{isComplete}", async (int id,bool isComplete, ToDoDbContext db) =>
{
    var todo = await db.Items.FindAsync(id);

    if (todo is null) return Results.NotFound();
    todo.IsComplete = isComplete;

    await db.SaveChangesAsync();

    return Results.NoContent();
});
app.UseCors();
 app.Run();
 
