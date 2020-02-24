using System.Windows.Media;
using RD2.ViewModel;

namespace RD2.Crosshairs
{
    internal interface ICrosshair
    {
        void Draw(CrossHairSize size, Color color);

        bool AdjustOnlyLeft { get; }
    }
}