using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;

namespace Aesir5
{
    public class Map
    {
        public string Name { get; set; }
        public Size Size { get; set; }
        public bool IsModified { get; set; }
        public bool IsEditable { get; set; }
        public Dictionary<Point, Tile> MapData { get; private set; }

        public class Tile
        {
            public int TileNumber { get; set; }
            public bool Passability { get; set; }
            public int ObjectNumber { get; set; }

            public Tile(int tileNumber, bool passability, int objectNumber)
            {
                TileNumber = tileNumber;
                Passability = passability;
                ObjectNumber = objectNumber;
            }

            public static Tile GetDefault()
            {
                return new Tile(0, true, 0);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof (Tile)) return false;
                return Equals((Tile) obj);
            }

            public bool Equals(Tile other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return other.TileNumber == TileNumber && other.Passability.Equals(Passability) && other.ObjectNumber == ObjectNumber;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int result = TileNumber;
                    result = (result*397) ^ Passability.GetHashCode();
                    result = (result*397) ^ ObjectNumber;
                    return result;
                }
            }
        }

        public Map(string mapPath, bool encrypted)
        {
            Name = Path.GetFileNameWithoutExtension(mapPath);
            IsEditable = false;

            Stream stream = encrypted ? LoadStream(mapPath) : File.Open(mapPath, FileMode.Open);

            BinaryReader reader = new BinaryReader(stream);
            int sx = reader.ReadByte() * 256 + reader.ReadByte();
            int sy = reader.ReadByte() * 256 + reader.ReadByte();

            CreateEmptyMap(sx, sy);

            for (int y = 0; y < sy; y++)
            {
                for (int x = 0; x < sx; x++)
                {
                    byte[] tile = reader.ReadBytes(2);
                    byte[] pass = reader.ReadBytes(2);
                    byte[] @object = reader.ReadBytes(2);

                    int tileNumber = tile[1] + tile[0]*256;
                    bool passability = (pass[0] == 0 && pass[1] == 0) ? false : true;
                    int objectNumber = @object[1] + @object[0] * 256;
                    MapData.Add(new Point(x, y), new Tile(tileNumber, passability, objectNumber));
                }
            }

            reader.Close();
            stream.Close();
            IsModified = false;
        }

        public void Save(string mapPath, bool encrypted)
        {
            if (File.Exists(mapPath)) File.Delete(mapPath);

            Stream stream = encrypted ? (Stream)new MemoryStream() : File.Create(mapPath);
            BinaryWriter writer = new BinaryWriter(stream);

            SaveInt(writer, Size.Width);
            SaveInt(writer, Size.Height);

            for (int y = 0; y < Size.Height; y++)
            {
                for (int x = 0; x < Size.Width; x++)
                {
                    SaveInt(writer, (this[x,y] != null) ? this[x,y].TileNumber : 0);
                    SaveBool(writer, (this[x, y] != null) ? this[x, y].Passability : true);
                    SaveInt(writer, (this[x,y] != null) ? this[x,y].ObjectNumber : 0);
                }
            }

            if (encrypted) SaveStream((MemoryStream)stream, mapPath);
            writer.Close();
            stream.Close();
            Name = Path.GetFileNameWithoutExtension(mapPath);
            IsModified = false;
        }

        private static void SaveBool(BinaryWriter writer, bool boolValue)
        {
            writer.Write((byte)0);
            if (boolValue) writer.Write((byte) 1);
            else writer.Write((byte)0);
        }

        private static void SaveInt(BinaryWriter writer, int intValue)
        {
            byte byte1, byte2;
            IntTo2Bytes(intValue, out byte1, out byte2);
            writer.Write(byte1);
            writer.Write(byte2);
        }

        private static void IntTo2Bytes(int intValue, out byte byte1, out byte byte2)
        {
            byte1 = Convert.ToByte(intValue / 256);
            byte2 = Convert.ToByte(intValue - (intValue / 256) * 256);
        }

        public Tile this[int x, int y]
        {
            get
            {
                Point point = new Point(x, y);
                if (MapData.ContainsKey(point)) return MapData[point];
                return null;
            }
            set
            {
                if (!IsEditable) return;
                Point point = new Point(x, y);
                if (MapData.ContainsKey(point)) MapData.Remove(point);
                MapData.Add(point, value);
                IsModified = true;
            }
        }

        public Map(int width, int height)
        {
            CreateEmptyMap(width, height);
            IsEditable = true;
        }

        private void CreateEmptyMap(int width, int height)
        {
            Size = new Size(width, height);
            MapData = new Dictionary<Point, Tile>();
        }

        #region Compression/Encryption

        private static Stream LoadStream(string mapPath)
        {
            MemoryStream memoryStream = new MemoryStream();
            using (FileStream inFile = File.Open(mapPath, FileMode.Open))
            {
                using (GZipStream decompress = new GZipStream(inFile, CompressionMode.Decompress))
                {
                    byte[] buffer = new byte[4096];
                    int numRead;
                    while ((numRead = decompress.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memoryStream.Write(buffer, 0, numRead);
                    }
                }
            }

            return EncryptionHelper.Decrypt(memoryStream);
        }

        private static void SaveStream(MemoryStream inStream, string mapPath)
        {
            using (Stream encryptedStream = EncryptionHelper.Encrypt(inStream))
            {
                using (FileStream outFile = File.Create(mapPath))
                {
                    using (GZipStream compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        byte[] buffer = new byte[4096];
                        int numRead;
                        while ((numRead = encryptedStream.Read(buffer, 0, buffer.Length)) != 0)
                        {
                            compress.Write(buffer, 0, numRead);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
