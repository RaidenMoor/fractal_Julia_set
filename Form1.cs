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

         int red = 255, green = 0, blue = 151;
        private Color bgColor = Color.Black, outColor;
  

        private int currIteration = 0;

        private Bitmap bitmap;

        public Form1()
        {
            InitializeComponent();
            bitmap = new Bitmap(WIDTH, HEIGHT);

            pictureBox1.Image = bitmap;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            pictureBox1.BackColor = Color.Black;

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
                if (red >= 14 && red <= 255)
                    red -= 14;
                else red = 255;
             

                outColor = Color.FromArgb(red, green, blue);
                for (int x = 0; x < WIDTH; x++)
                {
                  
                    for (int y = 0; y < HEIGHT; y++)
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
            for (int i = 0; i < MAXCOLOR; i++)
            {
                z = z * z + C;
                if (z.Module > 2)
                {
                   
                    return Color.Transparent;
                }
                if (i == iteration) return outColor;
            }
            return Color.FromArgb(0, MAXCOLOR, 0);
        }
    
    }
}