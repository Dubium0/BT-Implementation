using BT_Implementation.Control;
using BT_Implementation.Leaf;
using BT_Implementation.Test.Restaurant;
using BT_Implementation.Test.Restaurant.ActionNodes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BT_Implementation.Test.Restaurant
{
    public class RestaurantAIFacade : BTRoot
    {
        private BTNode entryPoint = new NullBTNode();
        public RestaurantAIFacade(Blackboard blackBoard) : base(blackBoard)
        {
        }
        public override void ConstructBT()
        {
          
            SequenceNode mealServeSequence = new SequenceNode("mealServeSequence");

            // left branch
           
            SequenceNode mealBuyAndPrepeareSequence = new SequenceNode("mealBuyAndPrepeareSequence");
            mealServeSequence.AddChild(mealBuyAndPrepeareSequence);


            SelectorNode prepearMealOrBuyIngredientSelector = new SelectorNode("prepearMealOrBuyIngredientSelector");

            mealBuyAndPrepeareSequence.AddChild(prepearMealOrBuyIngredientSelector);

            ConditionNode areIngredientsOnStock = new ConditionNode("areIngredientsOnStock", blackBoard, blackBoard =>
                {
                    var currentRecipe = blackBoard.GetValue<string>("Current Recipe");

                    var currentRecipeIngredients = blackBoard.GetValue<Dictionary<string, Dictionary<string, int>>>("Recipe Ingredients")[currentRecipe];
                    //Console.WriteLine($"in the conditionnode");

                    var ingredientStock = blackBoard.GetValue<Dictionary<string, int>>("Ingredient Stocks");

                    foreach (var item in currentRecipeIngredients)
                    {
                        if (ingredientStock[item.Key] < item.Value)
                        {
                            Console.WriteLine($"{item.Key} not enough ingredients");
                            return false;
                        }

                    }
                    return true;

                }
               );
            BringIngredientsAction bringIngredientsAction = new BringIngredientsAction("bringIngredientsAction", blackBoard);

            prepearMealOrBuyIngredientSelector.AddChild(areIngredientsOnStock);
            prepearMealOrBuyIngredientSelector.AddChild(bringIngredientsAction);

            PrepeareMealAction prepeareMealAction = new PrepeareMealAction("prepeareMealAction", blackBoard);

            mealBuyAndPrepeareSequence.AddChild(prepeareMealAction);



            ServeMealAction serveMealAction = new ServeMealAction("serveMealAction", blackBoard);

            mealServeSequence.AddChild(serveMealAction);

            entryPoint = mealServeSequence;

        }

        public override void ExecuteBT()
        {
            Console.WriteLine("Start execution");
            var prevResult = BTResult.Running;
            while (blackBoard.GetValue<Queue<string>>("Orders").Count > 0) 
            {

                prevResult = entryPoint.Execute();
                Thread.Sleep(100); 
            }
            Console.WriteLine("End execution");
        }

    }
}


public static class TestSuiteRestaurant
{

    public static RestaurantAIFacade CreateRestaurantAI()
    {
        Queue<string> orders = new Queue<string>();
        Dictionary<string, int> ingredientStocks = new Dictionary<string, int>();
        List<string> availableRecipes = new List<string>();
        Dictionary<string, Dictionary<string, int>> recipeIngredients = new();
        int mealPrepeareTime = 200;



        orders.Enqueue("Spaghetti Bolognese");
        orders.Enqueue("Grilled Cheese Sandwich");
        orders.Enqueue("Caesar Salad");
        orders.Enqueue("Spaghetti Bolognese");
        orders.Enqueue("Grilled Cheese Sandwich");
        orders.Enqueue("Caesar Salad");
        orders.Enqueue("Spaghetti Bolognese");
        orders.Enqueue("Grilled Cheese Sandwich");
        orders.Enqueue("Caesar Salad");



        ingredientStocks["Tomato Sauce"] = 2;
        ingredientStocks["Ground Beef"] = 2;
        ingredientStocks["Spaghetti"] = 3;
        ingredientStocks["Bread"] = 4;
        ingredientStocks["Cheese"] = 2;
        ingredientStocks["Lettuce"] = 2;
        ingredientStocks["Croutons"] = 2;
        ingredientStocks["Caesar Dressing"] = 2;


        availableRecipes.Add("Spaghetti Bolognese");
        availableRecipes.Add("Grilled Cheese Sandwich");
        availableRecipes.Add("Caesar Salad");


        recipeIngredients["Spaghetti Bolognese"] = new Dictionary<string, int>
        {
            { "Tomato Sauce", 1 },
            { "Ground Beef", 1 },
            { "Spaghetti", 1 }
        };
        recipeIngredients["Grilled Cheese Sandwich"] = new Dictionary<string, int>
        {
            { "Bread", 2 },
            { "Cheese", 1 }
        };
        recipeIngredients["Caesar Salad"] = new Dictionary<string, int>
        {
            { "Lettuce", 1 },
            { "Croutons", 1 },
            { "Caesar Dressing", 1 }
        };

        RestaurantBlackboardFactory blackboardFactory = new(orders, ingredientStocks, availableRecipes, recipeIngredients, mealPrepeareTime);

        return new RestaurantAIFacade(blackboardFactory.GetBlackboard());

    }

}

