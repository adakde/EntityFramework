using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBoards.Entities;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDbContext<MyboardsContext>(
    option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyBoardsConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<MyboardsContext>();

var pendingMigrations = dbContext.Database.GetPendingMigrations();
if (pendingMigrations.Any())
{
    dbContext.Database.Migrate();
}

var users = dbContext.Users.ToList();
if (!users.Any())
{
    
    var user1 = new User()
    {
        Email = "kuba-kasprzak12@wp.pl",
        FirstName = "Jakub",
        LastName = "Kasprzak",
        Address = new Address()
        {
            City = "Wieruszow",
            Street = "Handlowa"
        }
    };
    
    var user2 = new User()
    {
        Email = "kuba-kasprzak21@wp.pl",
        FirstName = "Jakub",
        LastName = "Kasprzak",
        Address = new Address()
        {
            City = "Kepno",
            Street = "Handlowa"
        }
    };
    
    dbContext.Users.Add(user1);
    dbContext.Users.Add(user2);
    dbContext.SaveChanges();
}


app.MapGet("data", async (MyboardsContext db) =>
    {
        var user = await db.Users
        .Include(u => u.Comments).ThenInclude(c => c.WorkItem)
        .Include(u => u.Address)
        .FirstAsync(user => user.Id == Guid.Parse("68366dbe-0809-490f-cc1d-08da10ab0e61"));

        Console.WriteLine(user.Comments.Count);
        return user;
    });
app.MapPost("Update", async (MyboardsContext db) =>
{
    Epic epic = await db.Epics.FirstAsync(e => e.Id ==1);

    var rejestedState = await db.States.FirstAsync(a => a.Value == "On Hold");

    epic.State = rejestedState;
    epic.area = "Updated area";
    epic.Priority = 1;
    epic.StartDate = DateTime.Now;

    await db.SaveChangesAsync();


    return epic;

}

);
app.MapPost("Create", async (MyboardsContext db) =>

{

    var address = new Address()

    {

        Id = Guid.Parse("b323dd7c-776a-4cf6-a92a-12df154b4a2c"),

        City = "Kraków",

        Country = "Poland",

        Street = "D³uga"



    };



    var user = new User()

    {

        Email = "user@test.com",

        FirstName = "Test User",

        Address = address,

    };



    db.Users.Add(user);

    await db.SaveChangesAsync();



    return user;

});

app.MapPost("Delete", async (MyboardsContext db) =>
{
    var workiitemTag = await db.WorkItems.Where(a => a.Id == 12).ToListAsync();
    db.WorkItems.RemoveRange(workiitemTag);
    var workItem = await db.WorkItems.FirstAsync(a => a.Id == 16);
    db.RemoveRange(workItem);

    await db.SaveChangesAsync();

}

);

app.Run();
