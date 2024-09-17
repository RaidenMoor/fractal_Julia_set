using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace fractal
{

    public partial class Form1 : Form
    {
        private const int WIDTH = 600;
        private const int HEIGHT = 600;

        private const double X1 = -1,
            Y1 = -1.2,
            X2 = 1,
            Y2 = 1.2;
        private const int MAXCOLOR = 255;

        private Complex C = new Complex(0, 1);

        private Color bgColor = Color.Black;
        private Color outColor = Color.Black;

        private int currIteration = 0;

        private Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(WIDTH, HEIGHT);

           pictureBox1.Image = bitmap;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;

            button1.Click += Button1_Click;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DrawStep();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DrawStep()
        {
            double real, imag;
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                for (int x = 0; x < WIDTH; x++)
                {
                    for(int y = 0; y < HEIGHT; y++)
                    {
                         real = X1 + (x / (double)WIDTH * (X2 - X1));
                         imag = Y2 - (y / (double)HEIGHT * (Y2 - Y1));

                        Color color = CalculateJulia(real, imag, currIteration);

                        g.FillRectangle(new SolidBrush(color), x, y, 1, 1);
                    }
                   
                }
            }
            pictureBox1.Invalidate();
            currIteration++;
        }
        private Color CalculateJulia(double x, double y, int iteration)
        {
            Complex z = new Complex(x, y);
            for (int i=0; i< MAXCOLOR; i++)
            {
                z = z * z + C;
                if (z.Module > 2)
                {
                    return outColor;
                }
                if (i == iteration) return GetColorFromNumner(MAXCOLOR);
            }
            return Color.FromArgb(0, MAXCOLOR, 0);
        }
        private Color GetColorFromNumner(int num)
        {
            int red = num % 256;
            int green = (num/256) % 256;
            int blue = (num/65536) % 256;
            return Color.FromArgb(red, green, blue);
        }
    }
}
