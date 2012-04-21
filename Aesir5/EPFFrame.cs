using System.Drawing;

namespace Aesir5
{
    public class EPFFrame
    {
        public byte[] RawData { get; set; }
        public long StartAddress { get; set; }
        public long EndAddress { get; set; }
        public long Top { get; set; }
        public long Left { get; set; }
        public long Right { get; set; }
        public long Bottom { get; set; }
        public bool IsValid { get; set; }

        private Rectangle bounds;
        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        public Point Location
        {
            get {  return Bounds.Location; }
            set { bounds.Location = value; }
        }

        public byte this[int x, int y]
        {
            get
            {
                int boundsInt = (x * Bounds.Height) + y;
                return (boundsInt < RawData.Length) ? RawData[boundsInt] : (byte)0;
            }
            set
            {
                int boundsInt = (x * Bounds.Height) + y;
                RawData[boundsInt] = value;
            }
        }
    }
}

