using System.Drawing;
using System.IO;
using System.Text;

namespace Aesir5
{
    public class Palette256
    {
        private readonly Color[] colors = new Color[0x100];
        public Color this[int index]
        {
            get
            {
                return colors[index];
            }
            set
            {
                colors[index] = value;
            }
        }

        public static Palette256[] FromFile(string file)
        {
            byte[] bytes = File.ReadAllBytes(file);
            MemoryStream input = new MemoryStream(bytes);
            BinaryReader reader = new BinaryReader(input);
            if (Encoding.ASCII.GetString(bytes, 0, 9) == "DLPalette")
            {
                return null;
            }
            int count = reader.ReadUInt16();
            Palette256[] pal = new Palette256[count];
            for (int x = 0; x < count; x++)
            {
                int headchk;

                pal[x] = new Palette256();
                do
                {
                    headchk = reader.ReadInt32();
                    reader.BaseStream.Seek(-3, SeekOrigin.Current);
                } while (headchk != 1632652356);

                reader.BaseStream.Seek(23, SeekOrigin.Current);

                int formatchk = reader.ReadByte();
                if (formatchk == 3)
                {
                    reader.BaseStream.Seek(13, SeekOrigin.Current);
                }
                else if (formatchk == 1)
                {
                    reader.BaseStream.Seek(9, SeekOrigin.Current);
                }
                else
                {
                    reader.BaseStream.Seek(7, SeekOrigin.Current);
                }

                for (int i = 0; i < 256; i++)
                {
                    int r = reader.ReadByte();
                    int g = reader.ReadByte();
                    int b = reader.ReadByte();
#pragma warning disable 168
                    int a = reader.ReadByte();
#pragma warning restore 168
                    pal[x][i] = Color.FromArgb(r, g, b);
                }
            }
            reader.Close();

            return pal;
        }
    }
}


