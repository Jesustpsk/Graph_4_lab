using System;

namespace Graph_4_lab.Models;

public class HelpFunctions
{
    public static decimal RoundUp(decimal number, int digits)
    {
        var factor = Convert.ToDecimal(Math.Pow(10, digits));
        return Math.Ceiling(number * factor) / factor;
    }
}