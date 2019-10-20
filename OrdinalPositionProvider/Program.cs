using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyLibrary.TextNumbers;
using MyLibrary;

namespace OrdinalPositionProvider
{
    class Program
    {
        static readonly PositionProvider provider = new PositionProvider();
        static void Main(string[] args)
        {
            decimal[] ordinals = new decimal[] { 20, 99, 525, 82510, 9999,10000 };
            decimal[] scalars = new decimal[] 
            {
                25,
                1250,
                8256,
                15786,
                10000,
                125_123_128,
                25_000_000,
                125_845_000_000_999 };

            ShowItems(ordinals, ShowOrdinalPostion);
            ShowItems(scalars, ShowScalarPosition);
            foreach (var item in provider.EnumeratePositions(20,30,PositionProvider.EnumerateType.Positional))
            {
                Console.WriteLine($"position is {item}");
            }
            foreach (var item in provider.EnumeratePositions(1,10,PositionProvider.EnumerateType.Ordinal))
            {
                Console.WriteLine($"position is {item}");
            }

            SpoolDefinitions definitions = new SpoolDefinitions();
            FilamentRemaining remaining = new FilamentRemaining(35, 35, definitions["Solutech 1Kg"]);
            var lengthRemaining = remaining.CalcRemaining() / 1000;
            Console.WriteLine($"Length Remaining : {Math.Round(lengthRemaining,2,MidpointRounding.ToEven)} meters");

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
        private static void ShowItems(IEnumerable<decimal> items,Action<decimal> action)
        {
            foreach (var item in items)
            {
                action(item);
            }
        }
        private static void ShowOrdinalPostion(decimal position)
        {
            Console.WriteLine($"Ordinal Position of {position} is {provider.OrdinalPosition(position)}");

        }
        private static void ShowScalarPosition(decimal position)
        {
            Console.WriteLine($"Scalar Position of {position} is {provider.ScalarPosition(position)}");
        }
    }
}
