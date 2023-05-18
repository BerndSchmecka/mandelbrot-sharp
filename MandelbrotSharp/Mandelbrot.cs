using SixLabors.ImageSharp;

public class Mandelbrot {

    public static readonly int MAX_ITERATIONS = 1000;

    public void generateImage(int height) {
        int width = (int) (height * 1.5);
        Image<Rgba32> image = new Image<Rgba32>(width, height);

        Parallel.For(0, width, x => {
            // Scale x to (-2, 1), zero is the center
            double real = (x - height) / (height / 2.0);
            for (int y = 0; y < height; y++) {
                // Scale y to (-1, 1), zero is the center
                double imaginary = (y - height / 2.0) / (height / 2.0);
                Complex c = new Complex(real, imaginary);
                Complex z = new Complex(0, 0);
                int iterations = mandelbrot(z, c);
                image[x, y] = hslToRgb(((iterations / 100.0) * 360.0), 100, iterations < MAX_ITERATIONS ? 50 : 0);
            }
        });

        image.Save("mandelbrot.png");
    }

    private int mandelbrot(Complex z, Complex c) {
        int iterations = 0;
        do {
            z = z * z + c;
            iterations++;
        } while (z.magnitude() < 1000 && iterations < MAX_ITERATIONS);
        return iterations;
    }

    // Hue: 0-360
    // Saturation: 0-100
    // Lightness: 0-100
    private Rgba32 hslToRgb(double hue, double saturation, double lightness) {
        double h = hue / 360.0;
        double s = saturation / 100.0;
        double l = lightness / 100.0;
        double q = l < 0.5 ? l * (1 + s) : l + s - l * s;
        double p = 2 * l - q;
        double r = hueToRgb(p, q, h + 1 / 3.0);
        double g = hueToRgb(p, q, h);
        double b = hueToRgb(p, q, h - 1 / 3.0);
        return new Rgba32((byte)(r * 255), (byte)(g * 255), (byte)(b * 255));
    }

    private double hueToRgb(double p, double q, double t) {
        if (t < 0) {
            t += 1;
        }
        if (t > 1) {
            t -= 1;
        }
        if (t < 1 / 6.0) {
            return p + (q - p) * 6 * t;
        }
        if (t < 1 / 2.0) {
            return q;
        }
        if (t < 2 / 3.0) {
            return p + (q - p) * (2 / 3.0 - t) * 6;
        }
        return p;
    }

}