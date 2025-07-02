using System;

namespace Mapsui.Widgets;

public abstract class GroupableWidget : Widget
{
    public GroupableWidget(bool grouped)
    {
        Grouped = grouped;
        if (grouped)
        {
            // position/envelope needs to be initialized
            // (but will be overridden by the group)
            Position = new MPoint(0, 0);
        }
    }

    /// <summary>
    /// The size of the widget (width / height).
    /// </summary>
    abstract public (float width, float height) Size { get; set; }

    /// <summary>
    /// Position of the widget (top left corner).
    /// </summary>
    public MPoint Position
    {
        get { return Envelope?.TopLeft ?? new MPoint(0, 0); }
        set
        {
            Envelope = new MRect(value.X, value.Y, value.X + Size.width, value.Y + Size.height);
        }
    }

    public bool Grouped { get; private set; }
}
