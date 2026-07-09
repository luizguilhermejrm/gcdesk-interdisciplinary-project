public class SlaConfig
{
    public int SlaId { get; set; }
    public int Priority { get; set; }
    public int ResponseLimitHours { get; set; }
    public int ResolutionLimitHours { get; set; }
    public int EscalationHours { get; set; }

    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
    public string DeletedAt { get; set; }
}
