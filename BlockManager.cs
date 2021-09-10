using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    public static class BlockManager
    {
        private const string PATH = @"Blocks\";

        public static Dictionary<string, Block> Blocks = new Dictionary<string, Block>();
        private static readonly Random random = new Random();
        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        public static void CreateBlock(string data)
        {
            Block block = new Block()
            {
                data = data,
                timestamp = DateTime.Now,
                index = Blocks.Count + 1
            };

            block.prev_hash = Blocks.Count == 0 ? "" : Blocks.Values.Last().hash;
            block.hash = block.GetHash();

            Blocks.Add(block.hash, block);
        }

        public static void Load()
        {
            Blocks.Clear();
            foreach (string block_path in Directory.GetFiles(PATH))
            {
                string json = File.ReadAllText(block_path);
                Block block = JsonConvert.DeserializeObject<Block>(json);
                Blocks.Add(block.hash, block);
            }
        }

        public static void Save()
        {
            foreach (Block block in Blocks.Values)
            {
                string json = JsonConvert.SerializeObject(block, Formatting.Indented);
                File.WriteAllText(PATH + $"Block_{block.index}.json", json);
            }
        }

        public static string RandomString(int length)
        {
            return new string(Enumerable.Repeat(CHARS, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
