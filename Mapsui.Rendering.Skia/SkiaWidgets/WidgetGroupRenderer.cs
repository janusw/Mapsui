using System;
using SkiaSharp;
using Mapsui.Rendering.Skia.SkiaWidgets;
using Mapsui.Widgets;

namespace Mapsui.Rendering.Skia;

/// <summary>
/// Renderer for WidgetGroup. Only does positioning
/// of the child widgets, no actual drawing.
/// </summary>
public class WidgetGroupSkiaRenderer : ISkiaWidgetRenderer
{
    public void Draw(SKCanvas canvas, Viewport viewport, IWidget widget, float layerOpacity)
    {
        var wg = widget as WidgetGroup;
        float width = 0, height = 0;
        float spc = wg.Spacing;
        foreach (var wdgt in wg.Children)
        {
            var (w, h) = wdgt.Size;
            if (wg.Orientation == Orientation.Vertical)
            {
                height += h + spc;
                width = Math.Max(width, w);
            }
            else
            {
                height = Math.Max(height, h);
                width += w + spc;
            }
        }
        height -= spc;

        var posX = wg.CalculatePositionX(0, (float)viewport.Width, width);
        var posY = wg.CalculatePositionY(0, (float)viewport.Height, height);
        wg.Envelope = new MRect(posX, posY, posX + width, posY + height);

        float x = posX, y = posY;
        foreach (var wdgt in wg.Children)
        {
            var (w, h) = wdgt.Size;
            if (wg.Orientation == Orientation.Vertical)
            {
                if (wg.HorizontalAlignment == HorizontalAlignment.Left)
                    x = posX;
                else
                    x = posX + width - w;
            }
            else
            {
                if (wg.VerticalAlignment == VerticalAlignment.Top)
                    y = posY;
                else
                    y = posY + height - h;
            }

            wdgt.Position = new MPoint(x, y);

            if (wg.Orientation == Orientation.Vertical)
                y += h + spc;
            else
                x += w + spc;
        }
    }
}
