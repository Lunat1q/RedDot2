namespace RD2
{
    internal class ScreenInfo
    {
        private const double DefaultDpi = 96;
        public static Location GetCenterOfCoordinates(CrossHairSize size, bool leftOnly = false)
        {
            var windowRect = new User32.RECT();
            var desktopId = User32.GetDesktopWindow();
            User32.GetWindowRect(desktopId, ref windowRect);
            var dpi = DpiHelper.GetDpiForWindow(desktopId);

            var dpiMult = DefaultDpi / dpi;
            var height = windowRect.bottom - windowRect.top;
            var width = windowRect.right - windowRect.left;

            return new Location(
                width * dpiMult / 2 - size.Width * dpiMult / 2,
                height * dpiMult / 2 - (leftOnly ? 0 : size.Height * dpiMult / 2)
            );
        }
    }

    internal class Location
    {
        public Location(double fromLeft, double fromTop)
        {
            this.Top = fromTop;
            this.Left = fromLeft;
        }

        public double Top { get; set; }
        public double Left { get; set; }
    }
}