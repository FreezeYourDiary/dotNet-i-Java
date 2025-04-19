using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Threads;
[assembly:
InternalsVisibleTo("Threads"), InternalsVisibleTo("GUI")]
namespace GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private Bitmap? img;
        private async void buttonLoad_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var file = openFileDialog1.FileName;
            if (string.IsNullOrEmpty(file)) return;

            Bitmap img = new Bitmap(file);
            pictureBox1.Image = img;

            int width = img.Width;
            int height = img.Height;

            Bitmap img2 = new Bitmap(img);
            Bitmap img3 = new Bitmap(img);
            Bitmap img4 = new Bitmap(img);

            // macierz szarosci
            Macierz macierz = new Macierz(height, width);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    Color pixel = img.GetPixel(j, i);
                    int gray = (pixel.R + pixel.G + pixel.B) / 3;
                    macierz.Set(i, j, gray);
                }
            }

            // black & white
            var taskBlackWhite = Task.Run(() =>
            {
                Macierz wynik = new Macierz(height, width);
                int prog = 128;

                Parallel.For(0, height, i =>
                {
                    for (int j = 0; j < width; j++)
                    {
                        int val = macierz.Get(i, j);
                        int bw = val < prog ? 0 : 255;
                        wynik.Set(i, j, bw);
                    }
                });

                Bitmap blackwhite = new Bitmap(width, height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int val = wynik.Get(i, j);
                        Color bwColor = Color.FromArgb(val, val, val);
                        blackwhite.SetPixel(j, i, bwColor);
                    }
                }

                return blackwhite;
            });

            // Task for negative
            var taskNegative = Task.Run(() =>
            {
                Bitmap negative = new Bitmap(width, height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Color pixel = img2.GetPixel(j, i);
                        int r = 255 - pixel.R;
                        int g = 255 - pixel.G;
                        int b = 255 - pixel.B;
                        negative.SetPixel(j, i, Color.FromArgb(r, g, b));
                    }
                }
                return negative;
            });
            var taskMirrored = Task.Run(() =>
            {
                Bitmap mirrored = new Bitmap(width, height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Color pixel = img3.GetPixel(j, i);
                        mirrored.SetPixel(width - j - 1, i, pixel);
                    }
                }
                return mirrored;
            });

            var taskThreshold = Task.Run(() =>
            {
                Bitmap progowanie = new Bitmap(width, height);
                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        Color pixel = img4.GetPixel(j, i);
                        progowanie.SetPixel(j, i, pixel.GetBrightness() < 0.3f ? Color.Black : Color.White);
                    }
                }
                return progowanie;
            });
            await Task.WhenAll(taskBlackWhite, taskNegative, taskMirrored, taskThreshold);

            pictureBox2.Image = taskBlackWhite.Result;
            pictureBox3.Image = taskNegative.Result;
            pictureBox4.Image = taskMirrored.Result;
            pictureBox5.Image = taskThreshold.Result;
        }

        // 


        //    private void button1_Click(object sender, EventArgs e)
        //    {
        //        int n, m;
        //        if (!int.TryParse(textBox3.Text, out n) || !int.TryParse(textBox2.Text, out m))
        //        {
        //            return;
        //        }

        //        Macierz matrix1 = new Macierz(m, n);

        //        Macierz matrix2 = new Macierz(m, n);

        //        Problem problem = new Problem(matrix1, matrix2);
        //        Macierz result = problem.Solve();

        //        textBox1.Text = matrix1.ToString();
        //        textBox4.Text = matrix2.ToString();
        //        textBox5.Text = result.ToString();
        //    }
    }
}
