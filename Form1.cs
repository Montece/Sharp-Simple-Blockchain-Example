using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            ShowBlockchain();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BlockManager.Save();
            ShowBlockchain();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BlockManager.Load();
            ShowBlockchain();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BlockManager.CreateBlock(textBox1.Text);
            textBox1.Text = "";
            ShowBlockchain();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 50; i++)
            {
                BlockManager.CreateBlock($"info#{i}");
            }
        }

        private void ShowBlockchain()
        {
            var _priceDataArray = from block in BlockManager.Blocks.Values select new
            {
                Index = block.index,
                Hash = block.hash,
                PrevHash = block.prev_hash,
                Timestamp = block.timestamp,
                Data = block.data,
                Salt = block.salt,
            };
            dataGridView1.DataSource = _priceDataArray.ToArray();
        }
    }
}
