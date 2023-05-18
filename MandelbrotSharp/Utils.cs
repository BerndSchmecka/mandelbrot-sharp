public static class Utils
{
    // hue: 0-360, saturation: 0-1, luminance: 0-1
    public static Rgba32 HslToRgb(double hue, double saturation, double luminance)
    {
        double chroma = (1 - Math.Abs(2 * luminance - 1)) * saturation;
        double huePrime = hue / 60;
        double secondComponent = chroma * (1 - Math.Abs(huePrime % 2 - 1));

        huePrime = Math.Floor(huePrime);
        double red = 0;
        double green = 0;
        double blue = 0;

        if (huePrime == 0)
        {
            red = chroma;
            green = secondComponent;
            blue = 0;
        }
        else if (huePrime == 1)
        {
            red = secondComponent;
            green = chroma;
            blue = 0;
        }
        else if (huePrime == 2)
        {
            red = 0;
            green = chroma;
            blue = secondComponent;
        }
        else if (huePrime == 3)
        {
            red = 0;
            green = secondComponent;
            blue = chroma;
        }
        else if (huePrime == 4)
        {
            red = secondComponent;
            green = 0;
            blue = chroma;
        }
        else if (huePrime == 5)
        {
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
}