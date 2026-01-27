using System;

namespace Helpers
{
    public static class StringHelper
    {
        private const int ZERO = 0;
        private const int THOUSAND = 1000;
        private const int TEN_THOUSAND = 10000;
        private const int HUNDRED_THOUSAND = 100000;
        private const int MILLION = 1000000;
        private const int HUNDRED_MILLION = 100000000;
        private const int TRILLION = 1000000000;

        public static string FormatChip(int number)
        {
            return number switch
            {
                >= ZERO and < THOUSAND => number.ToString(),
                >= THOUSAND and < HUNDRED_THOUSAND => $"{MathF.Round(number / (float)THOUSAND, 1)}K",
                >= HUNDRED_THOUSAND and < MILLION => $"{(int)MathF.Round(number / (float)THOUSAND)}K",
                >= MILLION and < HUNDRED_MILLION => $"{MathF.Round(number / (float)MILLION, 1)}M",
                >= HUNDRED_MILLION and < TRILLION => $"{(int)MathF.Round(number / (float)MILLION)}M",
                _ => "-"
            };
        }
    }
}