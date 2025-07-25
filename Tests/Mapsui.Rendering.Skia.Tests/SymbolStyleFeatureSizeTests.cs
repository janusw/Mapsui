using System;
using System.Threading.Tasks;
using Mapsui.Styles;
using NUnit.Framework;

namespace Mapsui.Rendering.Skia.Tests;

[TestFixture]
public class SymbolStyleFeatureSizeTests
{
    [Test]
    public void DefaultSizeFeatureSize()
    {
        var symbolStyle = new SymbolStyle
        {
            SymbolType = SymbolType.Rectangle,
        };

        using var renderService = new RenderService();
        var size = SymbolStyleRenderer.FeatureSize(symbolStyle);

        Assert.That(size, Is.EqualTo(Math.Max(SymbolStyle.DefaultHeight, SymbolStyle.DefaultWidth) + 1));
    }

    [Test]
    public void DefaultSizeFeatureSize_Scaling()
    {
        var symbolStyle = new SymbolStyle
        {
            SymbolType = SymbolType.Rectangle,
            SymbolScale = 2,
        };

        using var renderService = new RenderService();
        var size = SymbolStyleRenderer.FeatureSize(symbolStyle);

        Assert.That(size, Is.EqualTo((Math.Max(SymbolStyle.DefaultHeight, SymbolStyle.DefaultWidth) + 1) * 2));
    }

    [Test]
    public void DefaultSizeFeatureSize_Offset_x()
    {
        var symbolStyle = new SymbolStyle
        {
            SymbolType = SymbolType.Rectangle,
            Offset = new Offset(2, 0),
        };

        using var renderService = new RenderService();
        var size = SymbolStyleRenderer.FeatureSize(symbolStyle);

        Assert.That(size, Is.EqualTo(Math.Max(SymbolStyle.DefaultHeight, SymbolStyle.DefaultWidth) + 2 * 2 + 1));
    }

    [Test]
    public void DefaultSizeFeatureSize_Offset_y()
    {
        var symbolStyle = new SymbolStyle
        {
            SymbolType = SymbolType.Rectangle,
            Offset = new Offset(0, 2),
        };

        using var renderService = new RenderService();
        var size = SymbolStyleRenderer.FeatureSize(symbolStyle);

        Assert.That(size, Is.EqualTo(Math.Max(SymbolStyle.DefaultHeight, SymbolStyle.DefaultWidth) + 2 * 2 + 1));
    }

    [Test]
    public void DefaultSizeFeatureSize_Offset_x_y()
    {
        var symbolStyle = new SymbolStyle
        {
            SymbolType = SymbolType.Rectangle,
            Offset = new Offset(2, 2),
        };

        using var renderService = new RenderService();
        var size = SymbolStyleRenderer.FeatureSize(symbolStyle);

        Assert.That(size, Is.EqualTo(Math.Max(SymbolStyle.DefaultHeight, SymbolStyle.DefaultWidth) + 1 + Math.Sqrt(2 * 2 + 2 * 2) * 2));
    }

    [Test]
    public async Task ImageFeatureSizeAsync()
    {
        // Arrange
        using var renderService = new RenderService();
        var symbolStyle = new ImageStyle
        {
            Image = "embedded://Mapsui.Resources.Images.Pin.svg",
        };

        await renderService.ImageSourceCache.TryRegisterAsync(symbolStyle.Image.SourceId, symbolStyle.Image.Source);

        // Act
        var size = ImageStyleRenderer.FeatureSize(symbolStyle, renderService);

        // Assert
        Assert.That(size, Is.EqualTo(56));
    }
}
