﻿using RD2.ViewModel;
using RD2.WinApi;

namespace RD2.Helpers
{
    internal class ScreenInfo
    {
        private const double DefaultDpi = 96;
        public static Location GetMonitorCenterLocation()
        {
            var windowRect = new User32.RECT();
            var desktopId = User32.GetDesktopWindow();
            User32.GetWindowRect(desktopId, ref windowRect);
            var dpi = DpiHelper.GetDpiForWindow(desktopId);

            var dpiMult = DefaultDpi / dpi;
            var height = windowRect.bottom - windowRect.top;
            var width = windowRect.right - windowRect.left;

            return new Location(
                width * dpiMult / 2,
                height * dpiMult / 2
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