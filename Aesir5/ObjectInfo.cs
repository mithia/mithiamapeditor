using System;
using System.IO;

namespace Aesir5
{
    public class ObjectInfo
    {
        private int height;
        private int[] indices;
        /// <value>
        /// The height of this object slice, in tiles.
        /// </value>
        public int Height
        {
            get { return height; }
        }
        public int[] Indices
        {
            get { return indices; }
        }
        public static ObjectInfo[] ReadCollection(string path)
        {
            Stream stream = new FileStream(path, FileMode.Open);

            BinaryReader binaryReader = new BinaryReader(stream);
            int count = binaryReader.ReadInt32();
            ObjectInfo[] infoCollection = new ObjectInfo[count];
            infoCollection[0] = new ObjectInfo {height = 0, indices = new int[0]};
            for (int index = 0; index < count; ++index)
            {
                if (index == 0)
                {
                    ObjectInfo info = infoCollection[index] = new ObjectInfo();
                    info.height = binaryReader.ReadByte();
                    info.indices = new int[info.height];
                    info.indices[0] = binaryReader.ReadByte();
                    stream.Seek(6, SeekOrigin.Current);
                }
                else
                {
                    ObjectInfo info = infoCollection[index] = new ObjectInfo();
                    info.height = binaryReader.ReadByte();
                    info.indices = new int[info.height];
                    for (int subIndex = 0; subIndex < info.height; subIndex++)
                        info.indices[subIndex] = binaryReader.ReadUInt16();
                    Array.Reverse(info.indices);
                    stream.Seek(6, SeekOrigin.Current);
                }
            }

            binaryReader.Close();
            stream.Dispose();
            return infoCollection;
        }
    }
}
