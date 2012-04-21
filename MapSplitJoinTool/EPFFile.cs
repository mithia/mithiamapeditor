using System.Drawing;
using System.IO;

namespace Aesir5
{
    public class EPFFile
    {
        //private string[] files;
        //private int fileCount;
        public int TOCAddress;
        public int frameCount;
        public EPFFrame[] frames;
        public int height;
        public byte[] rawData;
        public long tocAddress;
        public int unknown;
        public int width;
        public int max;

        public static EPFFile Init(int max)
        {
            EPFFile file2 = new EPFFile { frames = new EPFFrame[max] };
            return file2;
        }
        public static int Count(string file)
        {
            //byte[] buffer = File.ReadAllBytes(file);
            //MemoryStream input = new MemoryStream(buffer);

            BinaryReader reader = new BinaryReader(File.Open(file, FileMode.Open));
            int c = reader.ReadUInt16();

            reader.Close();
            return c;
        }

        public static int LoadEPF(EPFFile file2, string file, int offset)
        {
            byte[] buffer = File.ReadAllBytes(file);
            MemoryStream input = new MemoryStream(buffer);
            BinaryReader reader = new BinaryReader(input);

            //file2.fileName = file;
            //file2.rawData = buffer;
            file2.frameCount = reader.ReadUInt16();
            file2.width = reader.ReadUInt16();
            file2.height = reader.ReadUInt16();
            file2.unknown = reader.ReadUInt16();
            file2.tocAddress = reader.ReadUInt32() + 12;
            reader.BaseStream.Seek(file2.tocAddress, SeekOrigin.Begin);
            //file2.frames = new EPFFrame[file2.frameCount];
            //System.Windows.Forms.MessageBox.Show(reader.BaseStream.Position.ToString());
            for (int x = 0; x < file2.frameCount; x++)
            {
                int i = x + offset;
                file2.frames[i] = new EPFFrame();
                //file2[i] = new EPFFrame();

                int top = reader.ReadInt16();
                int left = reader.ReadInt16();
                int bottom = reader.ReadInt16();
                int right = reader.ReadInt16();
                //if ((short)left < 0) left = Math.Abs((short)left);
                //if ((short)top < 0) top = Math.Abs((short)top);
                //System.Windows.Forms.MessageBox.Show("Left " + left.ToString() + " Right " + right.ToString() + " Top " + top.ToString() + " Bottom " + bottom.ToString());
                if (left == 0 && top == 0 && right == 0 && bottom == 0)
                {
                    file2.frames[i].Bounds = Rectangle.FromLTRB(0, 0, file2.width, file2.height);
                }
                else
                {
                    file2.frames[i].Bounds = Rectangle.FromLTRB(top, left, bottom, right);
                    file2.frames[i].Top = top;
                    file2.frames[i].Left = left;
                    file2.frames[i].Right = right;
                    file2.frames[i].Bottom = bottom;

                }

                file2.frames[i].StartAddress = reader.ReadUInt32() + 12;
                file2.frames[i].EndAddress = reader.ReadUInt32() + 12;
                if (file2.frames[i].EndAddress == 0L)
                {
                    file2.frames[i].EndAddress = file2.TOCAddress;
                }

                int count = (int)(file2.frames[i].EndAddress - file2.frames[i].StartAddress);
                long position = reader.BaseStream.Position;
                reader.BaseStream.Seek(file2.frames[i].StartAddress, SeekOrigin.Begin);
                file2.frames[i].RawData = reader.ReadBytes(count);
                reader.BaseStream.Seek(position, SeekOrigin.Begin);
                if ((((file2.frames[i].Right - file2.frames[i].Left) * (file2.frames[i].Bottom - file2.frames[i].Top)) > file2.frames[i].RawData.Length) || (file2.frames[i].RawData.Length < 1))
                {
                    file2.frames[i].IsValid = false;
                }
                else
                {
                    file2.frames[i].IsValid = true;
                }
            }
            reader.Close();
            input.Dispose();
            file2.max = file2.frameCount + offset;
            return file2.frameCount + offset;
        }
    }
}
