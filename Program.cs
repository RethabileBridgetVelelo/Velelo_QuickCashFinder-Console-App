using QuickCashFinder.Models;
using QuickCashFinder.Services;

namespace QuickCashFinder;

class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("╔═══════════════════════════════════════════════════════╗");
        Console.WriteLine("║      🚀 QUICK CASH FINDER - Emergency $30 App        ║");
        Console.WriteLine("╚═══════════════════════════════════════════════════════╝");
        Console.ResetColor();
        
        var methodService = new MethodService();
        var recommendationEngine = new RecommendationEngine();
        
        bool running = true;
        
        while (running)
        {
            DisplayMainMenu();
            
            string choice = Console.ReadLine() ?? "";
            
            switch (choice)
            {
                case "1":
                    FindImmediateCash(methodService, recommendationEngine);
                    break;
                    
                case "2":
                    FindMethodsByUrgency(methodService, recommendationEngine);
                    break;
                    
                case "3":
                    FindMethodsByPayout(methodService);
                    break;
                    
                case "4":
                    SearchMethods(methodService);
                    break;
                    
                case "5":
                    EmergencyPlan();
                    break;
                    
                case "6":
                    DisplayAllMethods(methodService);
                    break;
                    
                case "0":
                    running = false;
                    Console.WriteLine("\n💸 Good luck with your cash hunt! Remember: ");
                    Console.WriteLine("   • Never pay to earn money");
                    Console.WriteLine("   • Meet buyers in safe locations");
                    Console.WriteLine("   • Read all terms carefully");
                    break;
                    
                default:
                    Console.WriteLine("\n❌ Invalid choice. Please try again.");
                    break;
            }
            
            if (running && choice != "0")
            {
                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
    
    static void DisplayMainMenu()
    {
        Console.WriteLine("\n📱 MAIN MENU");
        Console.WriteLine("══════════════════════════════════════════════");
        Console.WriteLine("1. 🚨 I need $30 NOW (Immediate Cash)");
        Console.WriteLine("2. 🔍 Find Methods by Urgency Level");
        Console.WriteLine("3. 💳 Find Methods by Payout Method");
        Console.WriteLine("4. 🔎 Search All Methods");
        Console.WriteLine("5. 📋 View Emergency $30 Action Plan");
        Console.WriteLine("6. 📊 View All Available Methods");
        Console.WriteLine("0. ❌ Exit");
        Console.WriteLine("══════════════════════════════════════════════");
        Console.Write("\nEnter your choice: ");
    }
    
    static void FindImmediateCash(MethodService methodService, RecommendationEngine engine)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("🚨 IMMEDIATE CASH OPTIONS (Today/Tonight)");
        Console.ResetColor();
        Console.WriteLine("══════════════════════════════════════════════\n");
        
        var immediateMethods = engine.GetRecommendations(UrgencyLevel.Immediate);
        
        if (!immediateMethods.Any())
        {
            Console.WriteLine("No immediate cash methods found.");
            return;
        }
        
        Console.WriteLine($"Found {immediateMethods.Count} methods for immediate cash:\n");
        
        for (int i = 0; i < immediateMethods.Count; i++)
        {
            var method = immediateMethods[i];
            Console.WriteLine($"{i + 1}. {method.Name}");
            Console.WriteLine($"   ⏱️  {method.PayoutSpeed} | 💪 {method.Effort} | 💰 ${method.EstimatedPerHour:F2}/hr");
            Console.WriteLine();
        }
        
        Console.Write("\nEnter method number for details (or 0 to go back): ");
        if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= immediateMethods.Count)
        {
            methodService.DisplayMethod(immediateMethods[selection - 1]);
        }
    }
    
    static void FindMethodsByUrgency(MethodService methodService, RecommendationEngine engine)
    {
        Console.Clear();
        Console.WriteLine("⏰ SELECT URGENCY LEVEL");
        Console.WriteLine("══════════════════════════════════════════════");
        Console.WriteLine("1. 🚨 Immediate (Today)");
        Console.WriteLine("2. ⚡ Fast (1-3 days)");
        Console.WriteLine("3. 🐢 Steady (Within a week)");
        Console.WriteLine("4. 📈 Long Term (Building income)");
        Console.WriteLine("══════════════════════════════════════════════");
        Console.Write("\nEnter choice: ");
        
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 4)
        {
            var urgency = (UrgencyLevel)(choice - 1);
            var methods = engine.GetRecommendations(urgency);
            
            Console.WriteLine($"\n📋 Found {methods.Count} methods for {urgency} urgency:\n");
            
            for (int i = 0; i < methods.Count; i++)
            {
                var method = methods[i];
                Console.WriteLine($"{i + 1}. {method.Name}");
                Console.WriteLine($"   📁 {method.Category} | 💳 {string.Join(", ", method.PayoutMethods)}");
                Console.WriteLine();
            }
            
            Console.Write("\nEnter method number for details (or 0 to go back): ");
            if (int.TryParse(Console.ReadLine(), out int selection) && selection > 0 && selection <= methods.Count)
            {
                methodService.DisplayMethod(methods[selection - 1]);
            }
        }
    }
    
    static void FindMethodsByPayout(MethodService methodService)
    {
        Console.Clear();
        Console.WriteLine("💳 SELECT PAYOUT METHOD");
        Console.WriteLine("══════════════════════════════════════════════");
        Console.WriteLine("1. PayPal (Digital)");
        Console.WriteLine("2. Cash (In Person)");
        Console.WriteLine("3. Bank Transfer");
        Console.WriteLine("4. Gift Cards");
        Console.WriteLine("5. Prepaid Cards");
        Console.WriteLine("══════════════════════════════════════════════");
        Console.Write("\nEnter choice: ");
        
        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= 5)
        {
            var payout = (PayoutMethod)(choice - 1);
            var allMethods = Data.MethodData.GetAllMethods();
            var methods = allMethods.Where(m => m.PayoutMethods.Contains(payout)).ToList();
            
            Console.WriteLine($"\n📋 Found {methods.Count} methods with {payout} payout:\n");
            
            foreach (var method in methods)
            {
                Console.WriteLine($"• {method.Name}");
                Console.WriteLine($"  ⏱️  {method.PayoutSpeed} | 📁 {method.Category}");
                Console.WriteLine();
            }
        }
    }
    
    static void SearchMethods(MethodService methodService)
    {
        Console.Clear();
        Console.Write("🔍 Enter search term: ");
        string searchTerm = Console.ReadLine() ?? "";
        
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("❌ Please enter a search term.");
            return;
        }
        
        var results = methodService.SearchMethods(searchTerm);
        
        Console.WriteLine($"\n📊 Found {results.Count} results for '{searchTerm}':\n");
        
        foreach (var method in results)
        {
            Console.WriteLine($"• {method.Name} ({method.Category})");
            Console.WriteLine($"  {method.Description.Substring(0, Math.Min(60, method.Description.Length))}...");
            Console.WriteLine($"  ⏱️  {method.PayoutSpeed} | 💪 {method.Effort}");
            Console.WriteLine();
        }
    }
    
    static void EmergencyPlan()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("📋 EMERGENCY $30 ACTION PLAN");
        Console.ResetColor();
        Console.WriteLine("══════════════════════════════════════════════\n");
        
        Console.WriteLine("🚨 STEP 1: Sell Something NOW (1-2 hours)");
        Console.WriteLine("   • List 2-3 items on Facebook Marketplace");
        Console.WriteLine("   • Use title: 'PRICE DROP - NEED GONE TODAY - $30'");
        Console.WriteLine("   • Meet at police station parking lot");
        Console.WriteLine("   • Good items: Electronics, games, designer items\n");
        
        Console.WriteLine("💉 STEP 2: Plasma Donation (2-3 hours)");
        Console.WriteLine("   • Call Biolife or CSL Plasma");
        Console.WriteLine("   • Ask about new donor promotions ($100+ often)");
        Console.WriteLine("   • Bring ID, proof of address, SSN card\n");
        
        Console.WriteLine("📱 STEP 3: Cash Advance Apps (Immediate)");
        Console.WriteLine("   • Dave/Earnin if you have regular direct deposit");
        Console.WriteLine("   • Can advance $20-$100 instantly");
        Console.WriteLine("   • Requires job with direct deposit\n");
        
        Console.WriteLine("⚠️  WHAT TO AVOID:");
        Console.WriteLine("   • ❌ 'Pay to unlock earnings' apps");
        Console.WriteLine("   • ❌ Survey apps for immediate cash");
        Console.WriteLine("   • ❌ Gaming apps for fast money");
        Console.WriteLine("   • ❌ Anything requiring your SSN upfront");
    }
    
    static void DisplayAllMethods(MethodService methodService)
    {
        Console.Clear();
        var allMethods = Data.MethodData.GetAllMethods();
        
        Console.WriteLine($"📊 ALL AVAILABLE METHODS ({allMethods.Count} total)\n");
        
        // Group by urgency
        var grouped = allMethods.GroupBy(m => m.Urgency)
                               .OrderBy(g => g.Key);
        
        foreach (var group in grouped)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"⏰ {group.Key.ToString().ToUpper()} CASH ({group.Count()} methods)");
            Console.ResetColor();
            Console.WriteLine(new string('-', 50));
            
            foreach (var method in group)
            {
                Console.WriteLine($"• {method.Name}");
                Console.WriteLine($"  📁 {method.Category} | 💪 {method.Effort} | 💰 ${method.EstimatedPerHour:F2}/hr");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine($"\n💡 Tip: Use option 1-4 to filter and get detailed steps!");
    }
}