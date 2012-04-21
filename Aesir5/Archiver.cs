using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Aesir5
{
    class ArchiveInfo
    {
        public struct File
        {
            private readonly int offset, size;
            public int Offset { get { return offset; } }
            public int Size { get { return size; } }
            internal File(int offset, int size)
            {
                this.offset = offset;
                this.size = size;
            }
        }
        private Dictionary<string, File> files;
        public ICollection<string> FileNames { get { return files.Keys; } }
        public File GetFile(string targetFileName)
        {
            if (files == null) throw new InvalidOperationException();
            foreach (string fileName in files.Keys)
            {
                if (fileName.ToLower() == targetFileName.ToLower())
                    return files[fileName];
            }
            throw new Exception();
        }
        public void Read(Stream stream)
        {
            BinaryReader binaryReader = new BinaryReader(stream);
            files = new Dictionary<string, File>();
            int count = (int)(binaryReader.ReadUInt32() - 1);
            int[] offsets = new int[count];
            string[] fileNames = new string[count];
            for (int index = 0; index < count; ++index)
            {
                offsets[index] = (int)binaryReader.ReadUInt32();
                byte[] fileNameBuffer = binaryReader.ReadBytes(13);
                Array.Resize(ref fileNameBuffer, Array.IndexOf<byte>(fileNameBuffer, 0));
                fileNames[index] = Encoding.ASCII.GetString(fileNameBuffer);
            }
            for (int index = 0; index < count; ++index)
            {
                int size;
                if (index < count - 1) size = offsets[index + 1] - offsets[index];
                else size = (int)stream.Length - offsets[index];
                files.Add(fileNames[index], new File(offsets[index], size));
            }
        }
        public ArchiveInfo() { }
        public ArchiveInfo(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
                Read(stream);
        }
        public ArchiveInfo(Stream stream)
        {
            Read(stream);
        }
    }
}