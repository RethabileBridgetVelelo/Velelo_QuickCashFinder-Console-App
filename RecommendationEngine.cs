namespace QuickCashFinder.Services;

public class RecommendationEngine
{
    public List<MoneyMethod> GetRecommendations(
        UrgencyLevel urgency,
        PayoutMethod? preferredPayout = null,
        EffortLevel? maxEffort = null,
        decimal? minAmount = null)
    {
        var allMethods = Data.MethodData.GetAllMethods();
        
        var recommendations = allMethods
            .Where(m => m.Urgency == urgency)
            .ToList();
        
        // Filter by preferred payout method if specified
        if (preferredPayout.HasValue)
        {
            recommendations = recommendations
                .Where(m => m.PayoutMethods.Contains(preferredPayout.Value))
                .ToList();
        }
        
        // Filter by maximum effort level if specified
        if (maxEffort.HasValue)
        {
            recommendations = recommendations
                .Where(m => m.Effort <= maxEffort.Value)
                .ToList();
        }
        
        // Filter by minimum amount if specified
        if (minAmount.HasValue && minAmount > 0)
        {
            recommendations = recommendations
                .Where(m => m.EstimatedPerHour >= minAmount || m.EstimatedPerHour == null)
                .OrderByDescending(m => m.EstimatedPerHour ?? 0)
                .ToList();
        }
        else
        {
            // Sort by urgency/estimated pay
            recommendations = recommendations
                .OrderBy(m => m.Urgency)
                .ThenByDescending(m => m.EstimatedPerHour ?? 0)
                .ToList();
        }
        
        return recommendations;
    }
    
    public List<MoneyMethod> FindMethodsForAmount(decimal targetAmount, TimeSpan timeAvailable)
    {
        var allMethods = Data.MethodData.GetAllMethods();
        var suitableMethods = new List<MoneyMethod>();
        
        foreach (var method in allMethods)
        {
            if (method.EstimatedPerHour.HasValue)
            {
                // Calculate if this method could reach target in available time
                decimal estimatedTotal = method.EstimatedPerHour.Value * 
                    (decimal)timeAvailable.TotalHours;
                
                if (estimatedTotal >= targetAmount * 0.5m) // At least 50% of target
                {
                    suitableMethods.Add(method);
                }
            }
        }
        
        return suitableMethods
            .OrderByDescending(m => m.EstimatedPerHour ?? 0)
            .ThenBy(m => m.Urgency)
            .ToList();
    }
}