using System.Numerics;

public class Mandelbrot
{

    public static readonly int MAX_ITERATIONS = 1000;
    private Rgba32[] colors = new Rgba32[MAX_ITERATIONS];

    private void initPalette()
    {
        for (int i = 0; i < MAX_ITERATIONS; i++)
        {
            colors[i] = Utils.HslToRgb(((i / 100.0) * 360.0), 1.0, 0.5);
        }
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