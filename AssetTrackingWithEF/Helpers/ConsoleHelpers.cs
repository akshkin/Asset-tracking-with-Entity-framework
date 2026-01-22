
namespace AssetTrackingWithEF.Helpers;

public static class ConsoleHelpers
{
    public static void WriteColoredText(ConsoleColor Color, string Message)
    {
        Console.ForegroundColor = Color;
        Console.WriteLine(Message);
        Console.ResetColor();
    }


    public static T RenderAndSelectFromList<T>(List<T> list, Action<List<T>, int> renderMethod)
    {
        Console.WriteLine("Use arrow keys to select");
        int index = 0;
        int startTop = Console.CursorTop;

        while (true)
        {
            Console.SetCursorPosition(0, startTop); //clear just the repeated rendered categories

            renderMethod(list, index); //render the list

            var key = Console.ReadKey(true).Key;

            switch (key) 
            {
                case ConsoleKey.UpArrow:
                    if (index > 0) index--; 
                    break;
                case ConsoleKey.DownArrow:
                    if (index < list.Count - 1) index++;
                    break;
                case ConsoleKey.Enter:
                    return list[index];
                case ConsoleKey.Escape:
                    return default;
            }
        }
    }

    public static void RenderList(List<string> list, int index)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (i == index)
            {
                Console.BackgroundColor = ConsoleColor.DarkCyan;
                Console.ForegroundColor = ConsoleColor.Black;
            }

            Console.WriteLine(list[i]);
            Console.ResetColor();
        }
    }
}
