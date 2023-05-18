class Program {
    static void Main(string[] args) {
        Mandelbrot mandelbrot = new Mandelbrot();
        var watch = new System.Diagnostics.Stopwatch();

        watch.Start();
        mandelbrot.generateImage(3000);
        watch.Stop();

        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
    }
}