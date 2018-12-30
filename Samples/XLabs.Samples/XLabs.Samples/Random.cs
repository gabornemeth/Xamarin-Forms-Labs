using System;
using Xamarin.Forms;

namespace XLabs.Samples
{
    static class Random
    {
        private static readonly System.Random _random = new System.Random();

        public static System.Random Base => _random;

        public static Color GetColor() => Color.FromRgb(_random.Next(255), _random.Next(255), _random.Next(255));
    }
}
