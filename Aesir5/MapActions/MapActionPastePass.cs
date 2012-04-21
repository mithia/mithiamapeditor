using System.Drawing;

namespace Aesir5.MapActions
{
    public class MapActionPastePass : IMapAction
    {
        public Point Tile { get; set; }
        private readonly int newPass;

        public MapActionPastePass(Point tile, int newPass)
        {
            Tile = tile;
            this.newPass = newPass;
        }

        public void Undo(Map map)
        {
            map[Tile.X, Tile.Y] = map[Tile.X, Tile.Y] ?? Map.Tile.GetDefault();
            map[Tile.X, Tile.Y].Passability = !(newPass == 0 ? true : false);
        }

        public void Redo(Map map)
        {
            map[Tile.X, Tile.Y] = map[Tile.X, Tile.Y] ?? Map.Tile.GetDefault();
            map[Tile.X, Tile.Y].Passability = (newPass == 0 ? true : false);
        }
    }
}
