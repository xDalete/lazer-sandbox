﻿using osu.Framework.Graphics.Containers;
using osu.Framework.Graphics;

namespace osu.Game.Rulesets.Sandbox.Screens.Visualizer.Components.Layouts
{
    public abstract partial class DrawableVisualizerLayout : CompositeDrawable
    {
        public DrawableVisualizerLayout()
        {
            RelativeSizeAxes = Axes.Both;
        }
    }
}
