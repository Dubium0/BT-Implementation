using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Test.Restaurant
{
    public enum RecipeStatus
    {
        NotStarted,
        Buy,
        Prepeare,
        ReadyToServe

    }
    public class RestaurantBlackboardFactory : IAbstractBlackboardFactory
    {

        private Queue<string> orders;
        private Dictionary<string, int> ingredientStocks;
        private List<string> availableRecipes;
        private Dictionary<string, Dictionary<string, int>> recipeIngredients;
        private int mealPrepeareTime;
        
        public RestaurantBlackboardFactory(Queue<string> orders,
                                            Dictionary<string, int> ingredientStocks,
                                            List<string> availableRecipes,
                                            Dictionary<string, Dictionary<string, int>> recipeIngredients,
                                            int mealPrepeareTime)
        {
            this.orders = orders;
            this.ingredientStocks = ingredientStocks;
            this.availableRecipes = availableRecipes;
            this.recipeIngredients= recipeIngredients;
            this.mealPrepeareTime = mealPrepeareTime;

        }

        public Blackboard GetBlackboard()
        {
            Blackboard restaurantBlackboard = new Blackboard();
            restaurantBlackboard.Data["Orders"] = orders;
            restaurantBlackboard.Data["Ingredient Stocks"] = ingredientStocks;
            restaurantBlackboard.Data["Available Recipes"] = availableRecipes;
            restaurantBlackboard.Data["Recipe Ingredients"] = recipeIngredients;
            restaurantBlackboard.Data["Current Recipe"] = orders.First();
            restaurantBlackboard.Data["Current Recipe Status"] = RecipeStatus.NotStarted;
            restaurantBlackboard.Data["Meal Prepeare Time"] = mealPrepeareTime;
            return restaurantBlackboard;
        }
    }
}
