using System;
using System.Collections.Generic;

namespace Mapsui.Widgets;

public class WidgetGroup : Widget
{
    public WidgetGroup(List<GroupableWidget> ch, Orientation o)
    {
        Children = ch;
        Orientation = o;
    }

    public List<GroupableWidget> Children { get; private set; }

    public Orientation Orientation { get; private set; }

    public float Spacing { get; set; }

    public override bool HandleWidgetTouched(Navigator navigator, MPoint position)
    {
        return false;
    }
}
