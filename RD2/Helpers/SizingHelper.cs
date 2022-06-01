using System;
using RD2.ViewModel;

namespace RD2.Helpers
{
    public class SizingHelper
    {
        internal static CrossHairSize GetSize(CrossHairSizeType size)
        {
            switch (size)
            {
                case CrossHairSizeType.Small:
                    return new CrossHairSize(5, 5);
                case CrossHairSizeType.Normal:
                    return new CrossHairSize(10, 10);
                case CrossHairSizeType.Big:
                    return new CrossHairSize(20, 20);
                case CrossHairSizeType.Huge:
                    return new CrossHairSize(40, 40);
                default:
                    throw new ArgumentOutOfRangeException(nameof(size), size, null);
            }
        }
    }
}