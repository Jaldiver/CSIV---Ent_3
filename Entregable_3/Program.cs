using Microsoft.EntityFrameworkCore;
using TodoApi.src.Config;
using TodoApi.src.Entity;
using TodoApi.src.Handler;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbMemory>(opt => opt.UseInMemoryDatabase("TodoList"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

app.MapGet("/todoitems", (DbMemory db) => {
    try{
        GetAllTodoHandler handle = new GetAllTodoHandler(db);
        var todos = handle.Handle();
        return Results.Ok(todos);
    } catch (Exception e){
        return Results.Problem(e.Message);
    }
    
});

app.MapPost("/todoitems", async (HttpContext context, DbMemory db) =>
{   
    try {

    
    Todo? todo = await context.Request.ReadFromJsonAsync<Todo>();
    if (todo == null)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        return;
    }
    CreateTodoHandler handler = new CreateTodoHandler(db);
    await handler.HandleAsync(todo);
    } catch (Exception e){
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync(e.Message);
        return;

    }
    
});


app.MapPut("/todoitems/{id:int}", async (int id, Todo todoUpdate, DbMemory db) =>
{
    var todo = db.Todos.FirstOrDefault(
        item => item.Id == id
    );

    if (todo == null) {
        return Results.NotFound();
    }

    db.Entry(todo).CurrentValues.SetValues(todoUpdate);
    
    await db.SaveChangesAsync();

    return Results.Ok();
}); 

app.MapGet("/todoitems/PokeID/{id:int}", (int id, DbMemory db) => {

    var todo = db.Todos.FirstOrDefault(
        item => item.Id == id
    );

    if (todo == null) {
        return Results.NotFound();
    }

    return Results.Ok(todo);
});

app.MapGet("/todoitems/Nombre/{name}", (string name, DbMemory db) => {

    var todo = db.Todos.FirstOrDefault(
        item => item.Nombre == name
    );

    if (todo == null) {
        return Results.NotFound();
    }

    return Results.Ok(todo);
});

app.MapGet("/todoitems/Tipo/{tipo}", (string tipo, DbMemory db) => {
    var todos = db.Todos.Where(
        item => item.Tipo == tipo
    );

    return Results.Ok(todos);
});

app.MapDelete("/todoitems/Liberar/{id:int}", async(int id, DbMemory db) => {
    var todo = db.Todos.FirstOrDefault(
        item => item.Id == id
    );

    if (todo == null) {
        return Results.NotFound();
    }

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.MapDelete("/todoitems/Liberar/Nombre/{name}", async(string name, DbMemory db) => {
    var todo = db.Todos.FirstOrDefault(
        item => item.Nombre == name
    );

    if (todo == null) {
        return Results.NotFound();
    }

    db.Todos.Remove(todo);
    await db.SaveChangesAsync();
    return Results.Ok();
});

app.Run();
