using Microsoft.EntityFrameworkCore;

namespace ClearanceTrackerApp.Data;

public class AppDb : DbContext
{
    public AppDb(DbContextOptions<AppDb> opts) : base(opts) { }
    public DbSet<PersonClearance> Clearances => Set<PersonClearance>();
    public DbSet<ClearanceEvent> Events => Set<ClearanceEvent>();
}

public static class SeedData
{
    public static void SeedIfEmpty(AppDb db)
    {
        if (db.Clearances.Any()) return;
        var people = new []
        {
            new PersonClearance { FullName="Jane Doe", AgencyId="A1001", Position="Systems Analyst" },
            new PersonClearance { FullName="John Doe", AgencyId="A1002", Position="Field Tech", State=ClearanceState.InProgress },
            new PersonClearance { FullName="James Doe", AgencyId="A1003", Position="Aerospace Eng", State=ClearanceState.Approved }
        };
        db.Clearances.AddRange(people);
        db.SaveChanges();
    }
}
