namespace QuickCashFinder.Models;

public class MoneyMethod
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public UrgencyLevel Urgency { get; set; }
    public List<PayoutMethod> PayoutMethods { get; set; } = new();
    public EffortLevel Effort { get; set; }
    public decimal? EstimatedPerHour { get; set; } // Optional hourly rate estimate
    public TimeSpan? TimeToPayout { get; set; } // How long to get paid
    public bool RequiresUpfrontCost { get; set; } = false;
    public List<string> Requirements { get; set; } = new();
    public List<string> Steps { get; set; } = new();
    public string? Warning { get; set; }
    public string? RecommendedFor { get; set; }
    
    // Calculated property for display
    public string PayoutSpeed => Urgency switch
    {
        UrgencyLevel.Immediate => "Same day",
        UrgencyLevel.Fast => "1-3 days",
        UrgencyLevel.Steady => "3-7 days",
        UrgencyLevel.LongTerm => "1+ weeks",
        _ => "Varies"
    };
    
    public string EffortEmoji => Effort switch
    {
        EffortLevel.Low => "😎",
        EffortLevel.Medium => "😊",
        EffortLevel.High => "💪",
        EffortLevel.Skilled => "🎯",
        _ => "⚡"
    };
    
    public string UrgencyEmoji => Urgency switch
    {
        UrgencyLevel.Immediate => "🚨",
        UrgencyLevel.Fast => "⚡",
        UrgencyLevel.Steady => "🐢",
        UrgencyLevel.LongTerm => "📈",
        _ => "⏳"
    };
}