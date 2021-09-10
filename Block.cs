using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Blockchain
{
    [Serializable]
    public struct Block
    {
        [JsonProperty("index")]
        public int index;
        [JsonProperty("hash")]
        public string hash;
        [JsonProperty("prev_hash")]
        public string prev_hash;
        [JsonProperty("timestamp")]
        public DateTime timestamp;
        [JsonProperty("data")]
        public string data;
        [JsonProperty("salt")]
        public string salt;

        public override string ToString()
        {
            return $"Block<p={prev_hash}>, h={hash}>";
        }

        public string GetHash()
        {
            string hash = "";
            using (MemoryStream ms = new MemoryStream())
            {
                do
                {
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);
                    salt = BlockManager.RandomString(10);
                    byte[] bytes;
                    bytes = BitConverter.GetBytes(index);
                    ms.Write(bytes, 0, bytes.Length);
                    bytes = Encoding.UTF8.GetBytes(salt + hash);
                    ms.Write(bytes, 0, bytes.Length);
                    bytes = Encoding.UTF8.GetBytes(salt + prev_hash);
                    ms.Write(bytes, 0, bytes.Length);
                    bytes = BitConverter.GetBytes(timestamp.Ticks);
                    ms.Write(bytes, 0, bytes.Length);
                    bytes = Encoding.UTF8.GetBytes(salt + data);
                    ms.Write(bytes, 0, bytes.Length);
                    hash = Hash.GetHash(bytes);
                }
                while(!hash.Contains("b0330b"));
            }

            return hash;
        }
    }
}
