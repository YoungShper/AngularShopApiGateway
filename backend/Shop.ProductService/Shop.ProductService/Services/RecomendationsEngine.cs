using System.Collections.Specialized;
using Shop.ProductService.Interfaces;
using Shop.ProductService.Models;

namespace Shop.ProductService.Services;

public class RecomendationsEngine : IRecomendationEngine
{
    private IDbRepository<ProductModel> _productRepo;

    public RecomendationsEngine(IDbRepository<ProductModel> productRepo)
    {
        _productRepo = productRepo;
    }

    public async Task<List<ProductModel>> GetRecommendedAsync(UserProfileModel user, int topN,
        CancellationToken cancellationToken = default)
    {
        

        var bmr = 10 * user.Weight + 6.25 * user.Height - 5 * user.Age + 5;

        double targetCalories;
        switch (user.Goal?.ToLower())
        { 
            case "gain":
                targetCalories = bmr * 1.1;
                break;
            case "loss":
                targetCalories = bmr * 0.85;
                break;
            default:
                targetCalories = bmr;
                break;
        }
        
        double targetProtein = user.Weight * (user.Goal == "gain" ? 2.0 : (user.Goal == "loss" ? 1.8 : 1.5));
        double targetFat     = (targetCalories * 0.25) / 9.0;
        double targetCarbs   = (targetCalories * 0.45) / 4.0;

        var products = await _productRepo.GetAllAsync(new NameValueCollection(), cancellationToken = default);

        // 3. Оценка и сортировка
        var recommendations = products
            .Select(p => new
            {
                Product = p,
                Score = CalcComplexScore(p.Protein, p.Fats, p.Carbs, p.Calories,
                    targetProtein, targetFat, targetCarbs, targetCalories)
            })
            .OrderByDescending(x => x.Score)
            .Take(topN)
            .Select(x => x.Product)
            .ToList();

        return recommendations;
    }

    static double Ratio(double actual, double target)
    {
        if (target <= 0) return 0;
        var v = 1.0 - Math.Abs(actual - target) / target;
        return v < 0 ? 0 : v;
    }
    
    private double CalcComplexScore(
        double protein, double fat, double carbs, int calories,
        double tProt, double tFat, double tCarb, double tCal)
    {
        

        // вычисляем составляющие
        double sp = Ratio(protein, tProt);    // белок
        double sf = Ratio(fat,     tFat);     // жиры
        double sc = Ratio(carbs,   tCarb);    // углеводы
        double sca= Ratio(calories,tCal);     // калории

        // веса: Prot 0.35, Fat 0.25, Carb 0.25, Cal 0.15
        return sp * 0.35 + sf * 0.25 + sc * 0.25 + sca * 0.15;
    }
}