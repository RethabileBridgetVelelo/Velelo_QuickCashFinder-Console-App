namespace QuickCashFinder.Services;

public class MethodService
{
    private readonly List<MoneyMethod> _allMethods;
    private readonly Dictionary<string, string> _categoryEmojis;
    
    public MethodService()
    {
        _allMethods = Data.MethodData.GetAllMethods();
        _categoryEmojis = Data.MethodData.GetCategoryEmojis();
    }
    
    public void DisplayMethod(MoneyMethod method)
    {
        var categoryEmoji = _categoryEmojis.TryGetValue(method.Category, out var emoji) 
            ? emoji 
            : "📌";
        
        Console.WriteLine("\n" + new string('=', 60));
        Console.WriteLine($"{categoryEmoji} {method.Name} {method.UrgencyEmoji}");
        Console.WriteLine(new string('-', 60));
        Console.WriteLine($"📋 {method.Description}");
        Console.WriteLine($"⏱️  Payout Speed: {method.PayoutSpeed}");
        Console.WriteLine($"💪 Effort Level: {method.Effort} {method.EffortEmoji}");
        
        if (method.EstimatedPerHour.HasValue)
        {
            Console.WriteLine($"💰 Estimated Rate: ${method.EstimatedPerHour:F2}/hour");
        }
        
        Console.WriteLine($"💳 Payout Methods: {string.Join(", ", method.PayoutMethods)}");
        
        if (method.Requirements.Any())
        {
            Console.WriteLine($"\n📋 Requirements:");
            foreach (var req in method.Requirements)
            {
                Console.WriteLine($"   • {req}");
            }
        }
        
        if (method.Steps.Any())
        {
            Console.WriteLine($"\n🚀 Quick Start Steps:");
            for (int i = 0; i < method.Steps.Count; i++)
            {
                Console.WriteLine($"   {i + 1}. {method.Steps[i]}");
            }
        }
        
        if (!string.IsNullOrEmpty(method.Warning))
        {
            Console.WriteLine($"\n⚠️  Warning: {method.Warning}");
        }
        
        if (!string.IsNullOrEmpty(method.RecommendedFor))
        {
            Console.WriteLine($"\n👍 Recommended for: {method.RecommendedFor}");
        }
        Console.WriteLine(new string('=', 60));
    }
    
    public List<MoneyMethod> GetMethodsByCategory(string category)
    {
        return _allMethods
            .Where(m => m.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
    
    public List<MoneyMethod> SearchMethods(string searchTerm)
    {
        return _allMethods
            .Where(m => m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       m.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       m.Category.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();
    }
}