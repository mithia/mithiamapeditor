using System.Drawing;

namespace Aesir5.MapActions
{
    public class MapActionPasteObject : IMapAction
    {
        public Point Tile { get; set; }
        private readonly int oldObjectNumber, newObjectNumber;

        public MapActionPasteObject(Point tile, int oldObjectNumber, int newObjectNumber)
        {
            Tile = tile;
            this.oldObjectNumber = oldObjectNumber;
            this.newObjectNumber = newObjectNumber;
        }

        public void Undo(Map map)
        {
            map[Tile.X, Tile.Y] = map[Tile.X, Tile.Y] ?? Map.Tile.GetDefault();
            map[Tile.X, Tile.Y].ObjectNumber = oldObjectNumber;
        }

        public void Redo(Map map)
        {
            map[Tile.X, Tile.Y] = map[Tile.X, Tile.Y] ?? Map.Tile.GetDefault();
            map[Tile.X, Tile.Y].ObjectNumber = newObjectNumber;
        }
    }
}
