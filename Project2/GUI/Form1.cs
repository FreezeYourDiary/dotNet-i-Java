using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GUI
{
    public partial class Form1: Form
    {
    
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            // tryparse for input handling 
            String n = textBox1.Text;
            int numberOfItems = Int32.Parse(n);
            String s = textBox2.Text;
            int seed = Int32.Parse(s);
            String c = textBox3.Text;
            int capacity = Int32.Parse(c);
            // MessageBox.Show($"n of items: {numberOfItems}\nSeed: {seed}\nCapacity: {capacity}", "Input");

        }
    }
}
