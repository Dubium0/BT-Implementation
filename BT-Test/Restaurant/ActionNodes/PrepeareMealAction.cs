using BT_Implementation.Leaf;

namespace BT_Implementation.Test.Restaurant.ActionNodes
{
    public class PrepeareMealAction : ActionNode
    {
        Utility.Timer mealPreaperingTime;
        private bool isPrepearingMeal = false;

        public PrepeareMealAction(string name,Blackboard blackBoard) : base(name, blackBoard)
        {

            mealPreaperingTime = new(blackBoard.GetValue<int>("Meal Prepeare Time"));
        }

        public override BTResult Execute()
        {


            var currentRecipe = blackBoard.GetValue<string>("Current Recipe");
            if (!isPrepearingMeal) { 
                isPrepearingMeal=true;

                
                Dictionary<string, Dictionary<string, int>> recipeIngredients = blackBoard.GetValue<Dictionary<string, Dictionary<string, int>>>("Recipe Ingredients");

                var ingredientStocks = blackBoard.GetValue<Dictionary<string, int>>("Ingredient Stocks");

                var requiredIngredients = recipeIngredients[currentRecipe];

                foreach(var ingredient in requiredIngredients)
                {
                    ingredientStocks[ingredient.Key] -= ingredient.Value;
                }


                mealPreaperingTime.Start();
                blackBoard.SetValue<RecipeStatus>("Current Recipe Status", RecipeStatus.Prepeare);
                Console.WriteLine($"Prepearing the meal {currentRecipe} ...");
                return BTResult.Running;
            }

            if (mealPreaperingTime.IsComplete())
            {
                Console.WriteLine($"The meal {currentRecipe} finished!");
                blackBoard.SetValue<RecipeStatus>("Current Recipe Status", RecipeStatus.ReadyToServe);
                isPrepearingMeal = false;
                return BTResult.Success;
            }
            else
            {
                Console.WriteLine($"Prepearing..");
                
                return BTResult.Running;
            }

        }

       
    }
}
