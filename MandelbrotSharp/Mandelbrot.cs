using System.Numerics;

public class Mandelbrot
{

    public static readonly int MAX_ITERATIONS = 1000;
    private Rgba32[] colors = new Rgba32[MAX_ITERATIONS];

    private void initPalette() {
        for (int i = 0; i < MAX_ITERATIONS; i++) {
            colors[i] = hslToRgb(((i / 100.0) * 360.0), 1.0, 0.5);
        }
    }

    // hue: 0-360, saturation: 0-1, luminance: 0-1
    private Rgba32 hslToRgb(double hue, double saturation, double luminance) {
        double chroma = (1 - Math.Abs(2 * luminance - 1)) * saturation;
        double huePrime = hue / 60;
        double secondComponent = chroma * (1 - Math.Abs(huePrime % 2 - 1));

        huePrime = Math.Floor(huePrime);
        double red = 0;
        double green = 0;
        double blue = 0;

        if (huePrime == 0) {
            red = chroma;
            green = secondComponent;
            blue = 0;
        } else if (huePrime == 1) {
            red = secondComponent;
            green = chroma;
            blue = 0;
        } else if (huePrime == 2) {
            red = 0;
            green = chroma;
            blue = secondComponent;
        } else if (huePrime == 3) {
            red = 0;
            green = secondComponent;
            blue = chroma;
        } else if (huePrime == 4) {
            red = secondComponent;
            green = 0;
            blue = chroma;
        } else if (huePrime == 5) {
            red = chroma;
            green = 0;
            blue = secondComponent;
        }

        double lightnessAdjustment = luminance - chroma / 2;
        red += lightnessAdjustment;
        green += lightnessAdjustment;
        blue += lightnessAdjustment;

        return new Rgba32((byte)(red * 255), (byte)(green * 255), (byte)(blue * 255));
    }

    public void generateImage(int height)
    {
        int width = (int)(height * 1.5);
        Image<Rgba32> image = new Image<Rgba32>(width, height);
        initPalette();

        Parallel.For(0, width, x =>
        {
            // Scale x to (-2, 1), zero is the center
            double real = (x - height) / (height / 2.0);
            for (int y = 0; y < height; y++)
            {
                // Scale y to (-1, 1), zero is the center
                double imaginary = (y - height / 2.0) / (height / 2.0);
                int iterations = mandelbrot(new Complex(real, imaginary));
                image[x, y] = iterations < MAX_ITERATIONS ? colors[iterations] : new Rgba32(0, 0, 0, 1.0f);
            }
        });

        image.Save("mandelbrot.png");
    }

    private int mandelbrot(Complex c)
    {
        Complex z = Complex.Zero;
        int iterations = 0;
        do
        {
            z = z * z + c;
            iterations++;
        } while (z.Magnitude < 1000 && iterations < MAX_ITERATIONS); // |z| < 1000
        return iterations;
    }
}