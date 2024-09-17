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

                        int iteration = CalculateJuliaIterations(real, imag);
                        Color color = GetColorFromIterations(iteration); 

                        g.FillRectangle(new SolidBrush(color), x, y, 1, 1);
                    }
                   
                }
            }
            pictureBox1.Invalidate();
            currIteration++;
        }
        private int CalculateJuliaIterations(double real, double imag)
        {
            Complex z = new Complex(real, imag);
            for (int i = 0; i < MAXCOLOR; i++)
            {
                z = z * z + C;
                if (z.Module > 2)
                {
                    return i;
                }
            }

            // Точка принадлежит множеству Жулиа
            return MAXCOLOR;
        }

        private Color GetColorFromIterations(int iterations)
        {
            // Преобразовать число итераций в цвет
            if (iterations == MAXCOLOR)
            {
                return Color.Black; 
            }
            else
            {
                // Используем HSL для создания плавных цветовых переходов
                double hue = (iterations / (double)MAXCOLOR) * 360;
                return HSLToRGB(hue, 1.0, 0.5); // Насыщенность и светлота фиксированы
            }
        }

        // Вспомогательные функции для преобразования HSL в RGB
        private static Color HSLToRGB(double hue, double saturation, double lightness)
        {
            double r, g, b;
            if (saturation == 0)
            {
                r = g = b = lightness;
            }
            else
            {
                double q = lightness < 0.5 ? lightness * (1 + saturation) : (lightness + saturation) - (lightness * saturation);
                double p = 2 * lightness - q;
                r = HueToRGB(p, q, hue + (1.0 / 3.0));
                g = HueToRGB(p, q, hue);
                b = HueToRGB(p, q, hue - (1.0 / 3.0));
            }
            return Color.FromArgb((int)(r * 255), (int)(g * 255), (int)(b * 255));
        }

        private static double HueToRGB(double p, double q, double t)
        {
            if (t < 0) t += 1;
            if (t > 1) t -= 1;
            if (6 * t < 1) return p + (q - p) * 6 * t;
            if (2 * t < 1) return q;
            if (3 * t < 2) return p + (q - p) * 6 * (2 / 3 - t);
            return p;
        }

        //private Color CalculateJulia(double x, double y, int iteration)
        //{
        //    Complex z = new Complex(x, y);
        //    for (int i=0; i< MAXCOLOR; i++)
        //    {
        //        z = z * z + C;
        //        if (z.VectorLen > 2)
        //        {
        //            return outColor;
        //        }
        //        if (i == iteration) return GetColorFromNumner(MAXCOLOR);
        //    }
        //    return Color.FromArgb(0, MAXCOLOR, 0);
        //}
        private Color GetColorFromNumner(int num)
        {
            int red = num % 256;
            int green = (num/256) % 256;
            int blue = (num/65536) % 256;
            return Color.FromArgb(red, green, blue);
        }
    }
}
