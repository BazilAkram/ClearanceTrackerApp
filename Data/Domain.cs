namespace ClearanceTrackerApp.Data;

public static class Domain
{
    public static ClearanceState Next(ClearanceState s) => s switch
    {
        ClearanceState.Pending    => ClearanceState.InProgress,
        ClearanceState.InProgress => ClearanceState.Approved,
        _ => s
    };
}
