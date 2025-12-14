namespace QuickCashFinder.Data;

public static class MethodData
{
    public static List<MoneyMethod> GetAllMethods()
    {
        return new List<MoneyMethod>
        {
            // IMMEDIATE URGENCY (Today)
            new MoneyMethod
            {
                Name = "Sell Items on Facebook Marketplace",
                Description = "Sell items you already own for quick cash",
                Category = "Selling",
                Urgency = UrgencyLevel.Immediate,
                PayoutMethods = new() { PayoutMethod.CashInPerson },
                Effort = EffortLevel.High,
                EstimatedPerHour = 30.00m,
                TimeToPayout = TimeSpan.FromHours(2),
                Requirements = new() { "Items to sell", "Phone with camera", "Transportation for meetup" },
                Steps = new() { 
                    "Take clear photos of items", 
                    "Post with 'NEED GONE TODAY' in title", 
                    "Price competitively", 
                    "Meet in safe public place" 
                },
                Warning = "Meet in police station parking lots for safety",
                RecommendedFor = "Anyone with items they can part with"
            },
            
            new MoneyMethod
            {
                Name = "Plasma Donation",
                Description = "Donate plasma at certified centers",
                Category = "Donation",
                Urgency = UrgencyLevel.Immediate,
                PayoutMethods = new() { PayoutMethod.PrepaidCard },
                Effort = EffortLevel.Low,
                EstimatedPerHour = 50.00m, // First time bonuses are high
                TimeToPayout = TimeSpan.FromMinutes(0), // Loaded onto card immediately
                Requirements = new() { "Valid ID", "Proof of address", "Good health", "2-3 hours available" },
                Steps = new() { 
                    "Call local plasma center (Biolife, CSL)", 
                    "Ask about new donor promotions", 
                    "Schedule first appointment", 
                    "Bring required documents" 
                },
                Warning = "Not for everyone - check eligibility requirements",
                RecommendedFor = "Healthy adults needing immediate cash"
            },
            
            // FAST URGENCY (1-3 days)
            new MoneyMethod
            {
                Name = "UserTesting",
                Description = "Test websites and apps for cash",
                Category = "Online Tasks",
                Urgency = UrgencyLevel.Fast,
                PayoutMethods = new() { PayoutMethod.PayPal },
                Effort = EffortLevel.Medium,
                EstimatedPerHour = 30.00m, // $10 per 20-minute test
                TimeToPayout = TimeSpan.FromDays(7),
                Requirements = new() { "Computer with microphone", "Quiet environment", "PayPal account" },
                Steps = new() { 
                    "Sign up at UserTesting.com", 
                    "Complete your profile", 
                    "Take practice test", 
                    "Keep dashboard open for screeners" 
                },
                RecommendedFor = "People with good communication skills"
            },
            
            new MoneyMethod
            {
                Name = "Fetch Rewards",
                Description = "Scan grocery receipts for points",
                Category = "Rewards",
                Urgency = UrgencyLevel.Steady,
                PayoutMethods = new() { PayoutMethod.GiftCard, PayoutMethod.PrepaidCard },
                Effort = EffortLevel.Low,
                EstimatedPerHour = 10.00m,
                TimeToPayout = TimeSpan.FromDays(1),
                Requirements = new() { "Smartphone", "Grocery receipts" },
                Steps = new() { 
                    "Download Fetch Rewards", 
                    "Use referral code for bonus", 
                    "Scan all receipts from last 14 days", 
                    "Redeem for Visa gift card" 
                },
                RecommendedFor = "Anyone who buys groceries"
            },
            
            // STEADY URGENCY (Weekly)
            new MoneyMethod
            {
                Name = "Prolific Academic",
                Description = "Academic research studies and surveys",
                Category = "Surveys",
                Urgency = UrgencyLevel.Steady,
                PayoutMethods = new() { PayoutMethod.PayPal },
                Effort = EffortLevel.Medium,
                EstimatedPerHour = 12.00m,
                TimeToPayout = TimeSpan.FromDays(5),
                Requirements = new() { "PayPal account", "Consistent internet" },
                Steps = new() { 
                    "Sign up on Prolific.co", 
                    "Complete profile surveys", 
                    "Keep studies tab open", 
                    "Cash out at £5 minimum" 
                },
                RecommendedFor = "Students, researchers, survey takers"
            },
            
            // GAMING APPS (Long term)
            new MoneyMethod
            {
                Name = "Skillz Platform Games",
                Description = "Compete in skill-based mobile game tournaments",
                Category = "Gaming",
                Urgency = UrgencyLevel.LongTerm,
                PayoutMethods = new() { PayoutMethod.PayPal },
                Effort = EffortLevel.Skilled,
                EstimatedPerHour = null, // Varies wildly by skill
                TimeToPayout = TimeSpan.FromDays(3),
                RequiresUpfrontCost = false,
                Requirements = new() { "Mobile device", "Skill in specific games", "May need small deposit to withdraw" },
                Steps = new() { 
                    "Download Skillz-based game (Solitaire Cube, 21 Blitz)", 
                    "Play free practice games", 
                    "Enter beginner tournaments", 
                    "Link PayPal for withdrawals" 
                },
                Warning = "Highly addictive. Top players are often professionals.",
                RecommendedFor = "Skilled gamers looking for side income"
            },
            
            new MoneyMethod
            {
                Name = "Mistplay",
                Description = "Earn points for trying new mobile games",
                Category = "Gaming",
                Urgency = UrgencyLevel.LongTerm,
                PayoutMethods = new() { PayoutMethod.GiftCard },
                Effort = EffortLevel.Low,
                EstimatedPerHour = 2.00m,
                TimeToPayout = TimeSpan.FromDays(2),
                Requirements = new() { "Android device (iOS limited)", "Time to try games" },
                Steps = new() { 
                    "Download Mistplay", 
                    "Play suggested games to level up", 
                    "Earn units", 
                    "Redeem for gift cards" 
                },
                RecommendedFor = "Mobile gamers with spare time"
            }
        };
    }
    
    public static Dictionary<string, string> GetCategoryEmojis()
    {
        return new Dictionary<string, string>
        {
            { "Selling", "💰" },
            { "Donation", "🩸" },
            { "Online Tasks", "💻" },
            { "Rewards", "🏆" },
            { "Surveys", "📝" },
            { "Gaming", "🎮" },
            { "Gig Work", "⚡" }
        };
    }
}