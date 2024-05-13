namespace TodoApi.src.Config;

using Microsoft.EntityFrameworkCore;
using TodoApi.src.Entity;

class DbMemory : DbContext
{
    
    public DbMemory(DbContextOptions<DbMemory> options) : base(options) {}

    public DbSet<Todo> Todos => Set<Todo>();
}