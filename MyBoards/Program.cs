using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MyBoards.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        var authorsCommentCountsQuery = db.comments
         .GroupBy(c => c.AuthorId)
         .Select(g => new { g.Key, Count = g.Count() });
        
        var authorsCommentCountsQueryToList = await authorsCommentCountsQuery.ToListAsync();

        var topAuthor = authorsCommentCountsQueryToList.First(a => a.Count == authorsCommentCountsQueryToList.Max(acc => acc.Count));
        
        var userDetails = db.Users.First(u => u.Id == topAuthor.Key);
        return new {userDetails, comentCount = topAuthor.Count};
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

});

app.Run();
