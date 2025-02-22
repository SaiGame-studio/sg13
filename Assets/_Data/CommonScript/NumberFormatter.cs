using System;
using UnityEngine;

public static class NumberFormatter
{
    /// <summary>
    /// Converts a large float or double number into a compact string format (e.g., 1K, 1M, 2B).
    /// </summary>
    /// <param name="number">The number to format.</param>
    /// <returns>A formatted string representing the number.</returns>
    public static string FormatNumber(float number)
    {
        if (number < 1_000) // Less than 1,000
        {
            return number.ToString("0.###"); // Keep as it is
        }
        else if (number < 1_000_000) // From 1,000 to less than 1,000,000
        {
            return (number / 1_000f).ToString("0.##") + "K";
        }
        else if (number < 1_000_000_000) // From 1,000,000 to less than 1,000,000,000
        {
            return (number / 1_000_000f).ToString("0.##") + "M";
        }
        else if (number < 1_000_000_000_000) // From 1,000,000,000 to less than 1,000,000,000,000
        {
            return (number / 1_000_000_000f).ToString("0.##") + "B";
        }
        else if (number < 1_000_000_000_000_000) // From 1,000,000,000,000 to less than 1 quadrillion
        {
            return (number / 1_000_000_000_000f).ToString("0.##") + "T";
        }
        else // Greater than or equal to 1 quadrillion
        {
            return (number / 1_000_000_000_000_000f).ToString("0.#") + "Q";
        }
    }
}
