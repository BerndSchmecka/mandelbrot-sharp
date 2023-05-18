public class Complex {
    public double real;
    public double imaginary;

    public Complex(double real, double imaginary) {
        this.real = real;
        this.imaginary = imaginary;
    }

    public static Complex operator +(Complex a, Complex b) {
        return new Complex(a.real + b.real, a.imaginary + b.imaginary);
    }

    public static Complex operator *(Complex a, Complex b) {
        return new Complex(a.real * b.real - a.imaginary * b.imaginary, a.real * b.imaginary + a.imaginary * b.real);
    }

    public double magnitude() {
        return Math.Sqrt(real * real + imaginary * imaginary);
    }

    public static Complex operator -(Complex a, Complex b) {
        return new Complex(a.real - b.real, a.imaginary - b.imaginary);
    }

    public static Complex operator /(Complex a, Complex b) {
        double denominator = b.real * b.real + b.imaginary * b.imaginary;
        return new Complex((a.real * b.real + a.imaginary * b.imaginary) / denominator, (a.imaginary * b.real - a.real * b.imaginary) / denominator);
    }

    public static Complex operator -(Complex a) {
        return new Complex(-a.real, -a.imaginary);
    }
}