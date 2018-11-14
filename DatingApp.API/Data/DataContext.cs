

using DatingApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Data
{
    public class DataContext : DbContext
    {
        //Ctor
        public DataContext(DbContextOptions<DataContext> options) :base (options) {  }

        
        // creates Db Table Of the currect Type
        // and we call in the terminal for example: type in terminal 'dotnet ef migrations add AddedUserEntity' to Apply it to our database 
        // after migrations command we need to update our database  and this will add a new table in out database. command 'dotnet ef database update' do this after you see the migrations files.
        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
    }
}