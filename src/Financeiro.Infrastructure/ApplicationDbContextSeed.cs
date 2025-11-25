namespace Financeiro.Infrastructure;

public partial class ApplicationDbContext
{
    public void Seed(IConfiguration app)
    {
        if (app.GetSection("Environment").Value == "Test") return;
        this.Database.EnsureDeleted();
        this.Database.EnsureCreated();
    }
}