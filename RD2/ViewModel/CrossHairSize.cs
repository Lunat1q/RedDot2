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
    }
}