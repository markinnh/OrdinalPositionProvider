using System;
using System.Collections.Generic;
using System.Text;

namespace OrdinalPositionProvider.Services
{
    public static class Extensions
    {
        public static bool Between(this int target, int minValue, int maxValue, bool includeMin = false, bool includeMax = false)
        {
            if (includeMin && includeMax)
                return (target >= minValue && target <= maxValue);
            else if (includeMin && !includeMax)
                return (target >= minValue && target < maxValue);
            else if (!includeMin && includeMax)
                return (target > minValue && target <= maxValue);
            else
                return (target > minValue && target < maxValue);
        }

    }
    internal class PositionDefn
    {
        public string ScalarName { get; set; }
        public string OrdinalName { get; set; }
        internal PositionDefn(string positionName, string ordinalName)
        {
            ScalarName = positionName;
            OrdinalName = ordinalName;
        }
    }
    internal class Provider
    {
        const int maxPositions = 100000;
        const int maxScalar = 100000;
        const int minThousandAsHundred = 1099;
        const int maxThousandAsHundred = 10000;
        const int minTenThousand = 10000;
        const int maxTenThousand = 100000;
        //const int minTeenPosition = 10;
        //const int maxTeenPosition = 20;
        const int positionBase = 10;
        const int hundredBase = 100;
        const int thousandBase = 1000;

        readonly static Dictionary<int, PositionDefn> keyPositions = new Dictionary<int, PositionDefn>()
        {
            {1,new PositionDefn("One","First") },
            {2,new PositionDefn("Two","Second") },
            {3,new PositionDefn("Three","Third") },
            {4,new PositionDefn("Four","Fourth") },
            {5,new PositionDefn("Five","Fifth") },
            {6,new PositionDefn("Six","Sixth") },
            {7,new PositionDefn("Seven","Seventh") },
            {8,new PositionDefn("Eigth","Eighth") },
            {9,new PositionDefn("Nine","Nineth") },
            {10,new PositionDefn("Ten","Tenth") },
            {11,new PositionDefn("Eleven","Eleventh") },
            {12,new PositionDefn("Twelve","Twelveth") },
            {13,new PositionDefn("Thirteen","Thirteenth") },
            {14,new PositionDefn("Fourteen","Fourteenth") },
            {15,new PositionDefn("Fifteen","Fifteenth") },
            {16,new PositionDefn("Sixteen","Sixteenth") },
            {17, new PositionDefn("Seventeen","Seventeenth") },
            {18, new PositionDefn("Eighteen","Eighteenth") },
            {19,new PositionDefn("Nineteen","Nienteenth") },
            {20,new PositionDefn("Twenty","Twentieth") },
            {30,new PositionDefn("Thirty","Thirtieth") },
            {40,new PositionDefn("Fourty","Fourtieth") },
            {50,new PositionDefn("Fifty","Fiftieth") },
            {60,new PositionDefn("Sixty","Sixtieth") },
            {70,new PositionDefn("Seventy","Seventieth") },
            {80,new PositionDefn("Eighty","Eightieth") },
            {90,new PositionDefn("Ninety","Ninetieth") },
            {100,new PositionDefn("Hundred","Hundredth") },
            {1000,new PositionDefn("Thousand","Thousandth")}
        };
        //readonly static string[] teens = new string[]
        //{
        //    "Eleventh","Twelveth","Thirteenth","Fourteenth","Fifteenth",
        //    "Sixteenth","Seventeenth","Eighteenth","Nineteenth"
        //};
        //readonly static string[] positions = new string[]
        //{
        //    "First", "Second", "Third", "Fourth", "Fifth",
        //    "Sixth", "Seventh", "Eighth", "Nineth", "Tenth",
        //    "Eleventh","Twelveth","Thirteenth","Fourteenth","Fifteenth",
        //    "Sixteenth","Seventeenth","Eighteenth","Nineteenth","Twentieth",
        //    "Twenty-First","Twenty-Second","Twenty-Third","Twenty-Fourth","Twenty-Fifth",
        //    "Twenty-Sixth","Twenty-Seventh","Twenty-Eighth","Twenty-Ninth","Thirtieth",
        //    "Thirty-First","Thirty-Second","Thirty-Third","Thirty-Fourth","Thirty-Fifth",
        //    "Thirty-Sixth","Thirty-Seventh","Thirty-Eighth","Thirty-Nineth","Fourtieth",
        //    "Fourty-First","Fourty-Second","Fourty-Third","Fourty-Fourth","Fourty-Fifth",
        //    "Fourty-Sixth","Fourty-Seventh","Fourty-Eighth","Fourty-Nineth","Fiftieth",
        //    "Fifty-First","Fifty-Second","Fifty-Third","Fifty-Fourth","Fifty-Fifth",
        //    "Fifty-Sixth","Fifty-Seventh","Fifty-Eigth","Fifty-Nineth","Sixtieth",
        //    "Sixty-First","Sixty-Second","Sixty-Third","Sixty-Fourth","Sixty-Fifth",
        //    "Sixty-Sixth","Sixty-Seventh","Sixty-Eighth","Sixty-Nineth","Seventieth",
        //    "Seventy-First","Seventy-Second","Seventy-Third","Seventy-Fourth","Seventy-Fifth",
        //    "Seventy-Sixth","Seventy-Seventh","Seventy-Eighth","Seventy-Nineth","Eightieth",
        //    "Eigthy-First","Eighty-Second","Eighty-Third","Eighty-Fourth","Eighty-Fifth",
        //    "Eighty-Sixth","Eighty-Seventh","Eighty-Eighth","Eighty-Nineth","Ninetieth",
        //    "Ninety-First","Ninety-Second","Ninety-Third","Ninety-Fourth","Ninety-Fifth",
        //    "Ninety-Sixth","Ninety-Seventh","Ninety-Eighth","Ninety-Ninth","One Hundredth"
        //};
        internal static string OrdinalPosition(int position)
        {
            if (position > maxPositions - 1)
                throw new ArgumentOutOfRangeException(nameof(position), $"{nameof(position)} which equals {position} is greater than the maximum allowed positions of {maxPositions - 1}");
            else
            {
                return resolveOrdinalPosition(position, maxPositions);
            }
        }
        internal static string ScalarPosition(int position)
        {
            if (position > maxScalar - 1)
                throw new ArgumentOutOfRangeException(nameof(position), $"{nameof(position)} which = {position} is greater than max allowed of {maxScalar - 1}");
            else
                return resolveScalarPosition(position, maxScalar);
        }
        private static string resolveOrdinalPosition(int position, int maxPositions)
        {
            while (maxPositions > 0)
            {
                var remainder = position % positionBase;
                if (keyPositions.ContainsKey(position))
                    return keyPositions[position].OrdinalName;
                else if (position.Between(minThousandAsHundred, maxThousandAsHundred))
                {
                    return ordinalPositionBetween(position, maxPositions, hundredBase, positionBase);
                    //var thousands = resolveOrdinalPosition(position / 100, 10);
                    //var thousandRemainder = resolveOrdinalPosition(position % 100, 10);
                    //return position % hundredBase > 0 ? $"{resolveScalarPosition(position / hundredBase, positionBase)} {keyPositions[hundredBase].ScalarName} {resolveOrdinalPosition(position % hundredBase, positionBase)}" :
                    //    $"{resolveScalarPosition(position / hundredBase, positionBase)} {keyPositions[hundredBase].OrdinalName}";
                }
                else if (position.Between(minTenThousand, maxTenThousand))
                {
                    return ordinalPositionBetween(position, maxPositions, thousandBase, hundredBase);
                    //return position % thousandBase > 0 ? $"{resolveScalarPosition(position / thousandBase, positionBase)} {keyPositions[thousandBase].ScalarName} {resolveOrdinalPosition(position % thousandBase, hundredBase)}" :
                    //    $"{ resolveScalarPosition(position / thousandBase, positionBase)} {keyPositions[thousandBase].OrdinalName}";
                }
                else if (keyPositions.TryGetValue(position - remainder, out PositionDefn defn1) && keyPositions.TryGetValue(remainder, out PositionDefn defn2))
                    return $"{defn1.ScalarName}-{defn2.OrdinalName}";
                else
                {
                    var digit = position / maxPositions;
                    if (digit > 0)
                    {
                        var interim = position % maxPositions > 0 ? $"{keyPositions[digit].ScalarName} {keyPositions[maxPositions].ScalarName} " :
                            $"{ keyPositions[digit].ScalarName} {keyPositions[maxPositions].OrdinalName}";

                        return position % maxPositions > 0 ? interim + resolveOrdinalPosition(position % maxPositions, maxPositions / positionBase) : interim;
                    }
                    else
                        return resolveOrdinalPosition(position % maxPositions, maxPositions / positionBase);
                }
            }
            // control flow should never reach this point
            throw new NotImplementedException();
        }
        private static string resolveScalarPosition(int position, int maxPosition)
        {
            while (maxPosition > 0)
            {
                var remainder = position % positionBase;
                if (keyPositions.ContainsKey(position))
                    return keyPositions[position].ScalarName;
                else if (position.Between(minThousandAsHundred, maxThousandAsHundred))
                {
                    return scalarPositionBetween(position, maxPosition, hundredBase, positionBase);
                    //return position % maxPosition > 0 ? $"{resolveScalarPosition(position / hundredBase, positionBase)} {keyPositions[hundredBase].ScalarName} {resolveScalarPosition(position % hundredBase, positionBase)}" :
                    //    $"{resolveScalarPosition(position / hundredBase, positionBase)} {keyPositions[hundredBase].ScalarName}";
                }
                else if (position.Between(minTenThousand, maxTenThousand))
                {
                    return scalarPositionBetween(position, maxPosition, thousandBase, hundredBase);
                    //return position % maxPosition > 0 ? $"{resolveScalarPosition(position/thousandBase,positionBase)} {keyPositions[thousandBase].ScalarName} {resolveScalarPosition(position % thousandBase,hundredBase)}" :
                    //    $"{resolveScalarPosition(position / thousandBase, positionBase)} {keyPositions[thousandBase].ScalarName}";
                }
                else if (keyPositions.TryGetValue(position - remainder, out PositionDefn defn1) && keyPositions.TryGetValue(remainder, out PositionDefn defn2))
                    return $"{defn1.ScalarName}-{defn2.ScalarName}";
                else
                {
                    var digit = position / maxPosition;
                    if (digit > 0)
                    {
                        var interim = $"{keyPositions[digit].ScalarName} {keyPositions[maxPosition].ScalarName} ";
                        return position % maxPosition > 0 ? interim + resolveScalarPosition(position % maxPosition, maxPosition / positionBase) : interim;
                    }
                    else
                        return resolveScalarPosition(position % maxPosition, maxPosition / positionBase);
                }
            }
            throw new NotImplementedException();
        }
        private static string scalarPositionBetween(int position,int maxPosition,int scale,int remainderBase)
        {
            return position % maxPosition>0 ? $"{resolveScalarPosition(position / scale,positionBase)} {keyPositions[scale].ScalarName} {resolveScalarPosition(position% scale,remainderBase)}" : 
                $"{resolveScalarPosition(position/scale,positionBase)} {keyPositions[scale].ScalarName}";
        }
        private static string ordinalPositionBetween(int position, int maxPosition, int scale, int remainderBase)
        {
            return position % maxPosition > 0 ? $"{resolveScalarPosition(position / scale, positionBase)} {keyPositions[scale].ScalarName} {resolveOrdinalPosition(position % scale, remainderBase)}" :
                $"{resolveScalarPosition(position / scale, positionBase)} {keyPositions[scale].OrdinalName}";
        }

        /// <summary>
        /// Provides a unique 24 character string
        /// </summary>
        /// <returns>a unique 24 character string</returns>
        internal static string FriendlyId()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}
