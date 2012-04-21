using System.Drawing;
using System.Drawing.Imaging;
using Aesir5;

namespace MapSplitJoinTool
{
    public static class MapRenderer
    {
        private static Bitmap tileBitmap;
        private static Bitmap objectBitmap;
        private static Bitmap mapBitmap;
        private static Bitmap bitmapClear;
        private static Map activeMap;

        public static void SaveToPng(Map map, string fileName)
        {
            activeMap = map;

            mapBitmap = new Bitmap(activeMap.Size.Width * 48, activeMap.Size.Height * 48);
            Graphics g = Graphics.FromImage(mapBitmap);
            g.Clear(Color.Gray);

            for (int x = 0; x < activeMap.Size.Width; x++)
            {
                for (int y = 0; y < activeMap.Size.Height; y++)
                {
                    RenderSingleMapTile(x, y, g, false);
                }
            }

            g.Dispose();
            System.Threading.Thread.Sleep(30);
            mapBitmap.Save(fileName, ImageFormat.Png);
            //mapBitmap.Dispose();
        }

        private static void RenderSingleMapTile(int x, int y, Graphics g, bool forceRenderEmpty)
        {
            tileBitmap = GetTileBitmap(x, y);
            objectBitmap = GetObjectBitmap(x, y);

            if (forceRenderEmpty)
            {
                bitmapClear = new Bitmap(48, 48);
                Graphics gClear = Graphics.FromImage(bitmapClear);
                gClear.Clear(Color.DarkGreen);
                gClear.Dispose();

                if (objectBitmap == null)
                {
                    if (tileBitmap == null) objectBitmap = bitmapClear;
                    if (tileBitmap != null) objectBitmap = new Bitmap(48, 48);
                }
                if (tileBitmap == null) tileBitmap = bitmapClear;
            }


            if (objectBitmap == null && tileBitmap != null) g.DrawImage(tileBitmap, x * 48, y * 48);
            if (objectBitmap != null && tileBitmap == null) g.DrawImage(objectBitmap, x * 48, y * 48);
            else if (objectBitmap != null)
            {
                Graphics tileGraphics = Graphics.FromImage(tileBitmap);
                tileGraphics.DrawImage(objectBitmap, 0, 0);
                tileGraphics.Dispose();
                g.DrawImage(tileBitmap, x * 48, y * 48);
            }


            //if (tileBitmap != null) tileBitmap.Dispose();
            //if (objectBitmap != null) objectBitmap.Dispose();
        }

        private static Bitmap GetTileBitmap(int x, int y)
        {
            Map.Tile mapTile = activeMap[x, y];
            if (mapTile == null || mapTile.TileNumber == 0) return null;
            if (mapTile.TileNumber >= TileManager.Epf[0].max) return null;

            return ImageRenderer.Singleton.GetTileBitmap(mapTile.TileNumber);
        }

        private static Bitmap GetObjectBitmap(int x, int y)
        {
            Bitmap bitmap = null;

            for (int i = 0; i < 10; i++)
            {
                if ((i + y) >= activeMap.Size.Height) break;

                Map.Tile mapTile = activeMap[x, y + i];
                if (mapTile == null) continue;

                int objectNumber = mapTile.ObjectNumber - 1;
                if (objectNumber < 0 || objectNumber >= TileManager.ObjectInfos.Length) continue;

                int objectHeight = TileManager.ObjectInfos[objectNumber].Indices.Length;
                if (objectHeight <= i) continue;

                int tile = TileManager.ObjectInfos[objectNumber].Indices[objectHeight - i - 1];
                if (bitmap == null) bitmap = ImageRenderer.Singleton.GetObjectBitmap(tile);
                else
                {
                    Graphics graphics = Graphics.FromImage(bitmap);
                    Bitmap tmpBitmap = ImageRenderer.Singleton.GetObjectBitmap(tile);
                    graphics.DrawImage(tmpBitmap, 0, 0);
                    tmpBitmap.Dispose();
                    graphics.Dispose();
                }
            }

            return bitmap;
        }
    }
}
