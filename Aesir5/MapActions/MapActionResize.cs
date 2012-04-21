using System.Drawing;

namespace Aesir5.MapActions
{
    public class MapActionResize : IMapAction
    {
        public Point Tile { get; set; }
        private readonly Size oldSize, newSize;

        public MapActionResize(Size oldSize, Size newSize)
        {
            this.oldSize = oldSize;
            this.newSize = newSize;
        }

        public void Undo(Map map)
        {
            map.Size = oldSize;
        }

        public void Redo(Map map)
        {
            map.Size = newSize;
        }
    }
}
