using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Aesir5;

namespace MapSplitJoinTool
{
    public class SplitJoin
    {
        public static void SplitMap(Map map, int segmentSize, bool encrypted, string mapDirectory, bool saveImages)
        {
            Size size = map.Size;
            int xSegmentCount = size.Width / segmentSize;
            int ySegmentCount = size.Height / segmentSize;

            if (xSegmentCount * segmentSize < size.Width) xSegmentCount++;
            if (ySegmentCount * segmentSize < size.Height) ySegmentCount++;

            Environment.CurrentDirectory = mapDirectory;
            using (StreamWriter streamWriter = new StreamWriter(map.Name + string.Format(".map{0}s", (encrypted ? "e" : string.Empty))))
            {
                streamWriter.WriteLine(segmentSize);
                streamWriter.WriteLine(map.Size.Width);
                streamWriter.WriteLine(map.Size.Height);
                streamWriter.WriteLine(map.Name);
            }

            DirectoryInfo di = Directory.CreateDirectory(Path.Combine(mapDirectory, map.Name + " split"));
            Environment.CurrentDirectory = di.FullName;

            string currentMap = string.Empty;
            for (int xSeg = 0; xSeg < xSegmentCount; xSeg++)
            {
                for (int ySeg = 0; ySeg < ySegmentCount; ySeg++)
                {
                    string mapName = string.Format("{0}_{1}_{2}.map{3}", map.Name, xSeg, ySeg, (encrypted ? "e" : string.Empty));
                    string pngName = string.Format("{0}_{1}_{2}.png", map.Name, xSeg, ySeg);

                    if (currentMap != mapName)
                    {
                        Map tmpMap = new Map(segmentSize, segmentSize);
                        for (int i = xSeg * segmentSize; i < xSeg * segmentSize + segmentSize; i++)
                        {
                            for (int j = ySeg * segmentSize; j < ySeg * segmentSize + segmentSize; j++)
                            {
                                Map.Tile tile;
                                if (i >= map.Size.Width || j >= map.Size.Height) tile = Map.Tile.GetDefault();
                                else if (map[i, j] == null) tile = Map.Tile.GetDefault();
                                else tile = map[i, j];

                                tmpMap[i - xSeg*segmentSize, j - ySeg*segmentSize] = tile;
                            }
                        }
                        tmpMap.Save(mapName, encrypted);
                        if (saveImages) MapRenderer.SaveToPng(tmpMap, pngName);

                        currentMap = mapName;
                    }
                }
            }

            string message = string.Format("Original map size: {0}x{1}.{2}", map.Size.Width, map.Size.Height, Environment.NewLine);
            message += string.Format("Map split into {0} x-axis segments and {1} y-axis segments.{2}", xSegmentCount, ySegmentCount, Environment.NewLine);
            message += string.Format("Size of one segments is {0}x{1} tiles.{2}",
                segmentSize < size.Width ? segmentSize : size.Width, segmentSize < size.Height ? segmentSize : size.Height, Environment.NewLine);
            message += string.Format("A total of {0} segments created.", xSegmentCount * ySegmentCount);
            MessageBox.Show(message, @"Split details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void JoinMap(string path)
        {
            int segmentSize, mapWidth, mapHeight;
            string originalMapName;
            bool encrypted = path.ToLowerInvariant().EndsWith("mapes");

            using (StreamReader streamReader = new StreamReader(path))
            {
                string segmentSizeString = streamReader.ReadLine();
                string mapWidthString = streamReader.ReadLine();
                string mapHeightString = streamReader.ReadLine();
                originalMapName = streamReader.ReadLine();

                segmentSize = int.Parse(segmentSizeString);
                mapWidth = int.Parse(mapWidthString);
                mapHeight = int.Parse(mapHeightString);
            }

            Map map = new Map(mapWidth, mapHeight) {Name = originalMapName};
            Size size = map.Size;
            int xSegmentCount = size.Width / segmentSize;
            int ySegmentCount = size.Height / segmentSize;

            if (xSegmentCount * segmentSize < size.Width) xSegmentCount++;
            if (ySegmentCount * segmentSize < size.Height) ySegmentCount++;

            string mapsFolder = Path.GetDirectoryName(path) ?? Environment.CurrentDirectory;
            string splitFolder = Path.Combine(mapsFolder, originalMapName + " split");

            if (!Directory.Exists(splitFolder))
            {
                MessageBox.Show(splitFolder, @"Folder containing split files does not exist.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Environment.CurrentDirectory = splitFolder;

            for (int xSeg = 0; xSeg < xSegmentCount; xSeg++)
            {
                for (int ySeg = 0; ySeg < ySegmentCount; ySeg++)
                {
                    string mapName = string.Format("{0}_{1}_{2}.map{3}", originalMapName, xSeg, ySeg, (encrypted ? "e" : string.Empty));
                    Map tmpMap = new Map(mapName, encrypted);
                    for (int i = xSeg * segmentSize; i < xSeg * segmentSize + segmentSize; i++)
                    {
                        for (int j = ySeg * segmentSize; j < ySeg * segmentSize + segmentSize; j++)
                        {
                            Map.Tile tile;
                            int iLocal = i - xSeg * segmentSize, jLocal = j - ySeg * segmentSize;

                            if (iLocal >= tmpMap.Size.Width || jLocal >= tmpMap.Size.Height) tile = Map.Tile.GetDefault();
                            else if (tmpMap[iLocal, jLocal] == null) tile = Map.Tile.GetDefault();
                            else tile = tmpMap[iLocal, jLocal];

                            map[i, j] = tile;
                        }
                    }
                }
            }

            Environment.CurrentDirectory = mapsFolder;
            map.Save(originalMapName + ".map" + (encrypted ? "e" : string.Empty), encrypted);
            MapRenderer.SaveToPng(map, originalMapName + ".png");

            string message = string.Format("Original map size: {0}x{1}.{2}", map.Size.Width, map.Size.Height, Environment.NewLine);
            message += string.Format("Map created from {0} x-axis segments and {1} y-axis segments.{2}", xSegmentCount, ySegmentCount, Environment.NewLine);
            message += string.Format("Size of one segments is {0}x{1} tiles.{2}",
                segmentSize < size.Width ? segmentSize : size.Width, segmentSize < size.Height ? segmentSize : size.Height, Environment.NewLine);
            message += string.Format("A total of {0} segments used.", xSegmentCount * ySegmentCount);
            MessageBox.Show(message, @"Join details", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
