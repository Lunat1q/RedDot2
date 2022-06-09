using System;
using System.Diagnostics;

namespace RD2.ViewModel
{
    public class UIProcess
    {
        public int PID { get; set; }
        public string Name { get; set; }
        public IntPtr WindowHandle { get; set; }
        public string Title { get; set; }

        public Process ProcessObj { get; set; }
    }
}