using AssetTrackingWithEF.Services;

bool hasQuit = false;

while (!hasQuit)
{
    Console.Clear();
    Console.WriteLine(); 
    Console.WriteLine("ASSET TRACKING - TRACK YOUR ASSETS HERE");
    Console.WriteLine();
  
    MenuActions.AddDemoData();
 
    MenuManager.ShowMenu();

    string choice = Console.ReadLine();
    Console.Clear();
    hasQuit = MenuManager.HandleChoice(choice);
}


