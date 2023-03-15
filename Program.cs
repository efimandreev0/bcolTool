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
                return;
            }
            if (args[0].Contains(".txt"))
            {
                Build(args[0]);
                return;
            }
        }
        public static void Extract(string binaryFile)
        {
            var reader = new BinaryReader(File.OpenRead(binaryFile));
            reader.ReadInt32();
            List<string> allText = new List<string>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int size = reader.ReadInt32();
                string text = Encoding.Unicode.GetString(reader.ReadBytes(size * 2));
                allText.Add(text);
            }
            File.WriteAllLines(binaryFile.Replace(".bin", ".txt"), allText);
        }
        public static void Build(string txtFile)
        {
            
            string[] textImp = File.ReadAllLines(txtFile);
            int[] strSize = new int[textImp.Length];
            using (BinaryWriter arcWriter = new BinaryWriter(File.Create(txtFile.Replace(".txt", ".bin"))))
            {
                arcWriter.Write(Encoding.UTF8.GetBytes("bcol"));
                for (int i = 0; i < textImp.Length; i++)
                {
                    arcWriter.Write(Encoding.UTF8.GetByteCount(textImp[i]));
                    arcWriter.Write(Encoding.Unicode.GetBytes(textImp[i]));
                }
            }
        }

    }
}
