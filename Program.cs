using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace daHuyZnaetKakNazvat_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args[0].Contains(".bin"))
            {
                Extract(args[0]);
            }
        }
        public static void Extract(string binaryFile)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(binaryFile);
            long size = file.Length;
            Console.WriteLine($"File size is {size}");
            var reader = new BinaryReader(File.OpenRead(binaryFile));
            string magic = Encoding.ASCII.GetString(reader.ReadBytes(4));
            Console.WriteLine($"Magic: {magic}");
            int[] sizeStr = new int[1230];
            for (int i = 0; i < size; i++)
            {
                if (reader.BaseStream.Position == size)
                {
                    break;
                }
                else
                {
                    sizeStr[i] = reader.ReadInt32();
                    reader.BaseStream.Position += sizeStr[i] * 2;
                    Console.WriteLine($"String {i + 1} have size {sizeStr[i]} ");
                }
            }
            reader.Close();
            var reader1 = new BinaryReader(File.OpenRead(binaryFile));
            reader1.BaseStream.Position += 8;
            string[] strings = new string[1231];
            for (int i = 0; i < 1230; i++)
            {
                if (i == 1230 || i == 1229)
                {
                    strings[i] = Encoding.Unicode.GetString(reader1.ReadBytes(sizeStr[i] * 2));
                    Console.WriteLine($"String number {i + 1}: {strings[i]}");
                }
                else
                {
                    strings[i] = Encoding.Unicode.GetString(reader1.ReadBytes(sizeStr[i] * 2));
                    reader1.ReadInt32();
                    Console.WriteLine($"String number {i + 1}: {strings[i]}");
                }

            }
            reader1.Close();
            File.WriteAllLines(binaryFile + ".txt", strings);
            Console.ReadKey();
        }
        public static void Build()
        {

        }

    }
}
