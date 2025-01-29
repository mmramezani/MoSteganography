using System.Drawing;
using System.Text;

namespace MoSteganography.Core;

public static class ImageSteganography
{
    public static Bitmap EmbedText(Bitmap bmp, string text)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        int textLength = textBytes.Length;
        int capacityInBytes = (bmp.Width * bmp.Height * 3) / 8;
        if (textLength + 4 > capacityInBytes)
            throw new Exception("Text is too long to embed in this image.");

        byte[] lengthBytes = BitConverter.GetBytes(textLength);
        byte[] payload = new byte[lengthBytes.Length + textBytes.Length];
        Buffer.BlockCopy(lengthBytes, 0, payload, 0, lengthBytes.Length);
        Buffer.BlockCopy(textBytes, 0, payload, lengthBytes.Length, textBytes.Length);

        int payloadIndex = 0;
        int bitIndex = 0;

        for (int y = 0; y < bmp.Height; y++)
        {
            for (int x = 0; x < bmp.Width; x++)
            {
                if (payloadIndex >= payload.Length) return bmp;

                Color pixel = bmp.GetPixel(x, y);
                int r = pixel.R;
                int g = pixel.G;
                int b = pixel.B;

                r = SetLSB(r, GetBit(payload[payloadIndex], bitIndex++));
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    payloadIndex++;
                    if (payloadIndex >= payload.Length)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                        return bmp;
                    }
                }

                g = SetLSB(g, GetBit(payload[payloadIndex], bitIndex++));
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    payloadIndex++;
                    if (payloadIndex >= payload.Length)
                    {
                        bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                        return bmp;
                    }
                }

                b = SetLSB(b, GetBit(payload[payloadIndex], bitIndex++));
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    payloadIndex++;
                }

                bmp.SetPixel(x, y, Color.FromArgb(r, g, b));
                if (payloadIndex >= payload.Length) return bmp;
            }
        }
        return bmp;
    }

    public static string ExtractText(Bitmap bmp)
    {
        int width = bmp.Width;
        int height = bmp.Height;
        int totalPixels = width * height;

        // Gather all LSBs in order.
        var bits = new int[totalPixels * 3];
        int idx = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixel = bmp.GetPixel(x, y);
                bits[idx++] = (pixel.R & 1);
                bits[idx++] = (pixel.G & 1);
                bits[idx++] = (pixel.B & 1);
            }
        }

        // First 32 bits = length
        if (bits.Length < 32)
            throw new Exception("Not enough data to read length.");

        byte[] lengthBytes = new byte[4];
        for (int i = 0; i < 32; i++)
        {
            lengthBytes[i / 8] |= (byte)(bits[i] << (i % 8));
        }

        int messageLength = BitConverter.ToInt32(lengthBytes, 0);
        int capacityInBytes = (width * height * 3) / 8;
        if (messageLength < 0 || messageLength > capacityInBytes - 4)
            throw new Exception("Invalid or corrupted data. The extracted length is not possible.");

        int totalMessageBits = messageLength * 8;
        if (bits.Length < 32 + totalMessageBits)
            throw new Exception("Not enough data to read the full message.");

        byte[] resultBytes = new byte[messageLength];
        for (int i = 0; i < totalMessageBits; i++)
        {
            resultBytes[i / 8] |= (byte)(bits[32 + i] << (i % 8));
        }

        return Encoding.UTF8.GetString(resultBytes);
    }

    private static int GetBit(byte b, int bitIndex)
    {
        return (b >> bitIndex) & 1;
    }

    private static int SetLSB(int componentValue, int bit)
    {
        componentValue &= 0xFE;
        componentValue |= bit;
        return componentValue;
    }
}
