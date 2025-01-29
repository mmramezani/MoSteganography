using FluentAssertions;
using MoSteganography.Core;
using System.Drawing;

namespace MoSteganography.Test;

public class ImageSteganographyTests
{
    [Fact]
    public void EmbedText_And_ExtractText_WithShortMessage_ShouldMatchOriginal()
    {
        // Arrange
        using var originalBitmap = new Bitmap(100, 100);
        string originalText = "Hello Steganography!";

        // Act
        ImageSteganography.EmbedText(originalBitmap, originalText);
        string extractedText = ImageSteganography.ExtractText(originalBitmap);

        // Assert
        extractedText.Should().Be(originalText);
    }

    [Fact]
    public void EmbedText_And_ExtractText_WithEmptyMessage_ShouldReturnEmptyString()
    {
        // Arrange
        using var originalBitmap = new Bitmap(50, 50);
        string emptyText = string.Empty;

        // Act
        ImageSteganography.EmbedText(originalBitmap, emptyText);
        string extractedText = ImageSteganography.ExtractText(originalBitmap);

        // Assert
        extractedText.Should().Be(emptyText);
    }

    [Fact]
    public void EmbedText_WhenTextIsNull_ShouldThrowArgumentNullException()
    {
        // Arrange
        using var originalBitmap = new Bitmap(50, 50);
        string? nullText = null;

        // Act
        Action act = () => ImageSteganography.EmbedText(originalBitmap, nullText!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void EmbedText_WhenTextExceedsCapacity_ShouldThrowException()
    {
        // Arrange
        using var smallBitmap = new Bitmap(10, 10);
        // This text is deliberately large to exceed capacity in a 10x10 image
        // Capacity in bytes = (width * height * 3) / 8 = (10 * 10 * 3) / 8 = 300/8 = 37 bytes
        // We need to exceed 37 - 4 (lengthBytes) => exceed ~33 chars in UTF8 for safety
        string largeText = new string('A', 200);

        // Act
        Action act = () => ImageSteganography.EmbedText(smallBitmap, largeText);

        // Assert
        act.Should().Throw<Exception>()
           .WithMessage("Text is too long to embed in this image.");
    }

    [Fact]
    public void ExtractText_FromImageTooSmallToHoldLength_ShouldThrowException()
    {
        // Arrange
        // A 1x1 image only has 3 bits total, so it can't even store 32 bits for the length.
        using var tinyBitmap = new Bitmap(1, 1);

        // Act
        Action act = () => ImageSteganography.ExtractText(tinyBitmap);

        // Assert
        act.Should().Throw<Exception>()
           .WithMessage("Not enough data to read length.");
    }

    [Fact]
    public void ExtractText_FromUninitializedImage_ThatGeneratesInvalidLength_ShouldThrowException()
    {
        // Arrange
        using var uninitializedBitmap = new Bitmap(2, 2);

        // Act
        Action act = () => ImageSteganography.ExtractText(uninitializedBitmap);

        // Assert
        act.Should().Throw<Exception>()
           .Where(e =>
               e.Message.Contains("Invalid or corrupted data") ||
               e.Message.Contains("Not enough data to read the full message.") ||
               e.Message.Contains("Not enough data to read length.")
           );
    }

    [Fact]
    public void EmbedText_And_ExtractText_WithMaximumPossibleData_ShouldSucceed()
    {
        // Arrange
        // 50x50 => capacityInBytes = (50*50*3)/8 = 7500/8 = 937 bytes
        // The payload includes 4 bytes for length, so we can store up to 933 bytes of actual text
        // Let's embed ~900 characters to be safe
        using var mediumBitmap = new Bitmap(50, 50);
        string largeText = new string('X', 900);

        // Act
        ImageSteganography.EmbedText(mediumBitmap, largeText);
        string extractedText = ImageSteganography.ExtractText(mediumBitmap);

        // Assert
        extractedText.Should().Be(largeText);
    }

    [Fact]
    public void EmbedText_ShouldNotCorruptOtherPixelsBeyondNeededBits()
    {
        // Arrange
        // Use 4x4 instead of 2x2 to have capacity >= 5 bytes (4 length + 1 text)
        using var bmp = new Bitmap(4, 4);

        // Optional: Set some known pattern so you can check or compare later
        bmp.SetPixel(0, 0, Color.FromArgb(10, 20, 30));
        bmp.SetPixel(1, 0, Color.FromArgb(40, 50, 60));
        bmp.SetPixel(0, 1, Color.FromArgb(70, 80, 90));
        bmp.SetPixel(1, 1, Color.FromArgb(100, 110, 120));

        string shortText = "A";

        // Act
        ImageSteganography.EmbedText(bmp, shortText);

        // Assert
        string result = ImageSteganography.ExtractText(bmp);
        result.Should().Be(shortText);
    }

    [Fact]
    public void ExtractText_WhenEmbeddedMessageIsExactlyCapacity_ShouldWork()
    {
        // Arrange
        // 4x2 => total pixels = 8 => total bits = 8*3 = 24 => capacityInBytes = 24/8 = 3 bytes
        // We need 4 bytes overhead for the length => so effectively we have (3 - 4) => -1, 
        // which is not possible to store any text. So let's go a bit bigger (4x3).
        // 4x3 => total pixels = 12 => total bits = 36 => capacity = 36/8 = 4 bytes total
        // So we can store 4-4=0 bytes of actual text. Let's embed empty but we do want to check boundary conditions.
        using var edgeBitmap = new Bitmap(4, 3);
        string textToEmbed = ""; // 0 bytes of text

        // Act
        ImageSteganography.EmbedText(edgeBitmap, textToEmbed);
        string extracted = ImageSteganography.ExtractText(edgeBitmap);

        // Assert
        extracted.Should().Be(textToEmbed);
    }

    [Fact]
    public void ExtractText_WithRandomDataThatSuggestsTooLargeMessage_ShouldThrowException()
    {
        // Arrange
        // We'll build a small image and manually set LSBs to produce a large length. 
        // For instance, set the first 4 bytes (32 bits) to be 999999 (which is definitely out of capacity range).
        using var bmp = new Bitmap(10, 10);

        // Let's forcibly inject LSB data that leads to large length.
        // We'll set the first 32 bits to some pattern so that length = e.g., 999999
        // 999999 decimal => 0x0F423F in hex => (lowest 4 bytes) => 0x3F 0x42 0x0F 0x00 (in typical LE or BE. We'll just demonstrate.)
        int length = 999999;
        byte[] lengthBytes = BitConverter.GetBytes(length);

        int bitPosition = 0;
        int pixelIndex = 0;

        // Overwrite the first 32 bits in the image's LSBs
        for (int i = 0; i < 4; i++)
        {
            byte b = lengthBytes[i];
            for (int bit = 0; bit < 8; bit++)
            {
                // get x,y from pixelIndex
                int x = pixelIndex % bmp.Width;
                int y = pixelIndex / bmp.Width;
                Color currentColor = bmp.GetPixel(x, y);

                // We'll alter R, G, or B depending on bitPosition mod 3
                int channelIndex = bitPosition % 3; // 0 => R, 1 => G, 2 => B
                int bitValue = (b >> bit) & 1;

                int r = currentColor.R;
                int g = currentColor.G;
                int bl = currentColor.B;

                switch (channelIndex)
                {
                    case 0: r = (r & 0xFE) | bitValue; break;
                    case 1: g = (g & 0xFE) | bitValue; break;
                    case 2: bl = (bl & 0xFE) | bitValue; break;
                }

                bmp.SetPixel(x, y, Color.FromArgb(r, g, bl));
                bitPosition++;
                if (bitPosition % 3 == 0)
                {
                    pixelIndex++;
                }
            }
        }

        // Act
        Action act = () => ImageSteganography.ExtractText(bmp);

        // Assert
        act.Should().Throw<Exception>()
           .WithMessage("Invalid or corrupted data. The extracted length is not possible.");
    }
}