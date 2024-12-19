using BT_Implementation.Leaf;



namespace BT_Implementation.Test.Restaurant.ActionNodes
{
    public class BringIngredientsAction : ActionNode
    {
        
        public BringIngredientsAction(string name,Blackboard blackBoard) : base(name,blackBoard)
        {
          
        }

        public override BTResult Execute()
        {

            var currentRecipe = blackBoard.GetValue<string>("Current Recipe");

            Dictionary<string, Dictionary<string, int>> recipeIngredients = blackBoard.GetValue<Dictionary<string, Dictionary<string, int>>>("Recipe Ingredients");

            var ingredientStocks = blackBoard.GetValue<Dictionary<string, int>>("Ingredient Stocks");

            var requiredIngredient = recipeIngredients[currentRecipe];

            Console.WriteLine($"Bringing Ingredients for {currentRecipe} ...");

            foreach ( var ingredient in requiredIngredient.Keys)
            {
                
                if (ingredientStocks[ingredient] < requiredIngredient[ingredient])
                {
                    Console.WriteLine($"{ingredient} is scarce, bringin more");
                    ingredientStocks[ingredient]++;
                    blackBoard.SetValue<RecipeStatus>("Current Recipe Status", RecipeStatus.Buy);

                    return BTResult.Running;

                }
            }
            Console.WriteLine($"All ingredients are ready for {currentRecipe}!");
            return BTResult.Success;

        }
    }
}
