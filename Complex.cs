using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fractal
{
    internal class Complex
    {
        public double real { get; set; }
        public double imaginary { get; set; }
        public Complex(double real, double imaginary)
        {
            this.real = real;
            this.imaginary = imaginary;
        }
        public double Module
        {
            get { return Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imaginary, 2)); }
        }
        public static Complex operator +(Complex a, Complex b)
        {
            return new Complex(a.real + b.real, a.imaginary + b.imaginary);
        }
        public static Complex operator *(Complex a, Complex b)
        {
            return new Complex(a.real * b.real - a.imaginary * b.imaginary, a.real * b.imaginary + a.imaginary * b.real);
        }
    }
}

