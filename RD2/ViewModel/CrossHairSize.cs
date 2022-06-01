namespace RD2.ViewModel
{
    internal class CrossHairSize
    {
        public CrossHairSize(int w, int h)
        {
            this.Width = w;
            this.Height = h;
        }

        public int Width { get; set; }
        public int Height { get; set; }

        public static CrossHairSize operator *(CrossHairSize size, int factor) => new CrossHairSize(size.Width * factor, size.Height * factor);
        public static CrossHairSize operator *(CrossHairSize size, double factor) => new CrossHairSize((int)(size.Width * factor), (int)(size.Height * factor));
    }
}