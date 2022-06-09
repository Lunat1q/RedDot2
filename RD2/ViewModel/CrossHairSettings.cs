using System.Windows.Media;

namespace RD2.ViewModel
{
    public sealed class CrossHairSettings
    {
        public CrossHairSizeType Size { get; set; }

        public CrossHairType Type { get; set; }

        public Color SelectedColor { get; set; } = Colors.Red;

        public string SelectedProcessName { get; set; }

        public bool AutoStartAndMinimize { get; set; }

        public bool BoundToProcess { get; set; }
    }
}