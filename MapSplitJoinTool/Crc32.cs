namespace Aesir5
{
    public class Crc32
    {
        readonly uint[] table;

        public uint ComputeChecksum(byte[] bytes)
        {
            uint crc = 0xffffffff;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte index = (byte)(((crc) & 0xff) ^ bytes[i]);
                crc = ((crc >> 8) ^ table[index]);
            }
            return ~crc;
        }

        public Crc32()
        {
            const uint poly = 0xedb88320;
            table = new uint[256];
            for (int i = 0; i < table.Length; i++)
            {
                uint temp = (uint)i;
                for (int j = 8; j > 0; j--)
                {
                    if ((temp & 1) == 1)
                    {
                        temp = ((temp >> 1) ^ poly);
                    }
                    else
                    {
                        temp >>= 1;
                    }
                }
                table[i] = temp;
            }
        }
    }
}