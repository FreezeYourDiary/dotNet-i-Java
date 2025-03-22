using BackPack;
namespace BackPack_GUI
{
    public partial class Form1 : Form
    {
        Problem problem;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.ForeColor = Color.Black;
            textBox2.ForeColor = Color.Black;
            // tryparse for input handling 
            String n = textBox1.Text;
            int numberOfItems;
            bool isNumberValid = Int32.TryParse(n, out numberOfItems);
            String s = textBox2.Text;
            int seed;
            bool isSeedValid = Int32.TryParse(s, out seed);
            //String c = textBox3.Text;
            //int capacity = Int32.Parse(c);

            if (!isNumberValid || numberOfItems < 0)
            {
                MessageBox.Show("Input integer numbers > 0");
                textBox1.ForeColor = Color.Red;
                return;
            }
           if (!isSeedValid)
            {
                MessageBox.Show("Input integer numbers");
                textBox2.ForeColor = Color.Red;
                return;
            }
            // MessageBox.Show($"n of items: {numberOfItems}\nSeed: {seed}\nCapacity: {capacity}", "Input");
            problem = new Problem(numberOfItems, seed);
            // !! wazne  !! dodac przycisk sortuj?? czy sortowac liste po wcisnieciu generate przy generacji 
            // bo w result wypisuje indeksy juz posortowanych elementow
            // mozna tez dodac pole indeks przy konstrukcji problem przypisujemy kazdemy +1, zawsze bedzie ten indeks niewlasciwy
            textBox4.Text = problem.ToString();
            textBox4.ScrollBars = ScrollBars.Vertical;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
        // for button 2 to be clicked, instance has to be created! default bool?? or block input of capacity
        // until instance created idk
        private void button2_Click(object sender, EventArgs e)
        {
            if (problem == null)
            {
                MessageBox.Show("Create instance to test");
            }
            String c = textBox3.Text;
            int capacity = Int32.Parse(c);
            textBox5.Text = problem.Solve(capacity).ToString();

        }
    }
}
