namespace ClearanceTrackerApp.Data;

public enum ClearanceState { Pending, InProgress, Approved, Rejected }

public class PersonClearance
{
    public int Id { get; set; }
    public string FullName { get; set; } = "";
    public string AgencyId { get; set; } = "";   // e.g., employee/badge id
    public string Position { get; set; } = "";
    public ClearanceState State { get; set; } = ClearanceState.Pending;
    public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedUtc { get; set; }
    public string? Note { get; set; }            // last action note
    public ICollection<ClearanceEvent> Events { get; set; } = new List<ClearanceEvent>();
}

public class ClearanceEvent
{
    public int Id { get; set; }
    public int PersonClearanceId { get; set; }
    public PersonClearance? Person { get; set; }
    public ClearanceState From { get; set; }
    public ClearanceState To { get; set; }
    public string? Comment { get; set; }
    public DateTime AtUtc { get; set; } = DateTime.UtcNow;
}
