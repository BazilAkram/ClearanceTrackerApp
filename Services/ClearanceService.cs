using ClearanceTrackerApp.Data;
using Microsoft.EntityFrameworkCore;

namespace ClearanceTrackerApp.Services;

public class ClearanceService
{
    private readonly AppDb _db;
    public ClearanceService(AppDb db) => _db = db;

    public async Task AdvanceAsync(int id, string comment = "advanced via service")
    {
        var pc = await _db.Clearances.Include(x => x.Events).FirstOrDefaultAsync(x => x.Id == id);
        if (pc is null) throw new InvalidOperationException("Not found");
        var to = Domain.Next(pc.State);
        if (to == pc.State) return;

        _db.Events.Add(new ClearanceEvent
        {
            PersonClearanceId = id,
            From = pc.State, To = to, Comment = comment, AtUtc = DateTime.UtcNow
        });
        pc.State = to; pc.LastUpdatedUtc = DateTime.UtcNow; pc.Note = comment;
        await _db.SaveChangesAsync();
    }
}
