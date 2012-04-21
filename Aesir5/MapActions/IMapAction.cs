using System.Drawing;

namespace Aesir5.MapActions
{
    public interface IMapAction
    {
        Point Tile { get; set; }
        void Undo(Map map);
        void Redo(Map map);
    }
}
