using System;
using System.Collections.Generic;
using System.Text;

namespace AssetTrackingWithEF.Helpers;
public static class LoadingAnimation
{
    private static bool _running;

    public static void ShowSpinner()
    {
        _running = true;
        char[] sequence = { '|', '/', '-', '\\' };
        int index = 0;

        while (_running)
        {
            Console.Write(sequence[index]);
            Thread.Sleep(100);
            Console.Write("\b");
            index = (index + 1) % sequence.Length;
        }
    }

    public static void Stop()
    {
        _running = false;
    }
}
