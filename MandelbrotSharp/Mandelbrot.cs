using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.ColorSpaces.Conversion;

public class Mandelbrot
{

    public static readonly int MAX_ITERATIONS = 1000;

    public void generateImage(int height)
    {
        int width = (int)(height * 1.5);
        Image<Rgba32> image = new Image<Rgba32>(width, height);

        Parallel.For(0, width, x =>
        {
            // Scale x to (-2, 1), zero is the center
            double real = (x - height) / (height / 2.0);
            Parallel.For(0, height, y =>
            {
                // Scale y to (-1, 1), zero is the center
                double imaginary = (y - height / 2.0) / (height / 2.0);
                Complex c = new Complex(real, imaginary);
                Complex z = new Complex(0, 0);
                int iterations = mandelbrot(z, c);

                var color = new Hsl(((iterations / 100.0f) * 360.0f), 1.0f, iterations < MAX_ITERATIONS ? 0.5f : 0);
                var rgb = ColorSpaceConverter.ToRgb(color);

                image[x, y] = new Rgba32(rgb.R, rgb.G, rgb.B);
            });
        });

        image.Save("mandelbrot.png");
    }

    private int mandelbrot(Complex z, Complex c)
    {
        int iterations = 0;
        do
        {
            z = z * z + c;
            iterations++;
        } while (z.magnitudeSquared() < 1000000 && iterations < MAX_ITERATIONS); // |z| < 1000
        return iterations;
    }
}