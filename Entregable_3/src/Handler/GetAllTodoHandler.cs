namespace TodoApi.src.Handler;

using TodoApi.src.Config;
using TodoApi.src.Entity;
using System.Collections.Generic;
public class GetAllTodoHandler
{
    private DbMemory _db;

    internal GetAllTodoHandler(
        DbMemory db
    ){
        this._db = db;
    }

    public IEnumerable<Todo> Handle()
    {
        return this._db.Todos.ToList<Todo>();
    }
}