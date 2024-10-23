using SkiaSharp;
using ZXing;
using ZXing.Common;

namespace QuickResponseCodeCrafter;

public static class CodeCrafter
{
    public static void CreateQuickResponseCode(string text, string outputPath, Image QRCodeImage)
    {
        var options = new EncodingOptions
        {
            Height = 250,
            Width = 250,
            Margin = 1
        };
        var writer = new BarcodeWriterPixelData
        {
            Format = BarcodeFormat.QR_CODE,
            Options = options
        };

        var pixelData = writer.Write(text);
        using var bitmap = new SKBitmap(pixelData.Width, pixelData.Height, SKColorType.Rgba8888, SKAlphaType.Premul);
        var ptr = bitmap.GetPixels();
        byte[] grayColor = { 128, 128, 128, 255 };

        for (int i = 0; i < pixelData.Pixels.Length; i += 4)
        {
            if (pixelData.Pixels[i] == 255 && pixelData.Pixels[i + 1] == 255 && pixelData.Pixels[i + 2] == 255 && pixelData.Pixels[i + 3] == 255)
            {
                Array.Copy(grayColor, 0, pixelData.Pixels, i, 4);
            }
        }
        System.Runtime.InteropServices.Marshal.Copy(pixelData.Pixels, 0, ptr, pixelData.Pixels.Length);

        using var image = SKImage.FromBitmap(bitmap);
        using var data = image.Encode(SKEncodedImageFormat.Png, 100);
        using (var stream = File.OpenWrite(outputPath))
        {
            data.SaveTo(stream);
        }

        QRCodeImage.Source = new StreamImageSource { Stream = token => Task.FromResult(data.AsStream()) };
    }
}
