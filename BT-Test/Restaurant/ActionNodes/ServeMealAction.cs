using BT_Implementation.Leaf;


namespace BT_Implementation.Test.Restaurant.ActionNodes
{
    public class ServeMealAction : ActionNode
    {
        public ServeMealAction(string name, Blackboard blackBoard) : base(name, blackBoard)
        {
        }

        public override BTResult Execute()
        {
            var currentRecipe = blackBoard.GetValue<string>("Current Recipe");
            Console.WriteLine($"Served the meal {currentRecipe}");
            Queue<string> orders = blackBoard.GetValue<Queue<string>>("Orders");
            orders.Dequeue();
            if (orders.Count > 0)
            {
                blackBoard.SetValue<string>("Current Recipe", orders.First());

                currentRecipe = blackBoard.GetValue<string>("Current Recipe");
                Console.WriteLine($"New Order is {currentRecipe}");
                blackBoard.SetValue<RecipeStatus>("Current Recipe Status", RecipeStatus.NotStarted);
            }
            else
            {
                Console.WriteLine($"All Orders are finished!");
            }
            return BTResult.Success;
        }
    }
}
