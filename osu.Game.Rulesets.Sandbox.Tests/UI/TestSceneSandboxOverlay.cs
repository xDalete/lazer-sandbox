﻿using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics;
using osu.Game.Rulesets.Sandbox.UI.Overlays;
using osuTK;
using osuTK.Graphics;

namespace osu.Game.Rulesets.Sandbox.Tests.UI
{
    public class TestSceneSandboxOverlay : RulesetTestScene
    {
        private readonly TestOverlay overlay;

        public TestSceneSandboxOverlay()
        {
            Add(overlay = new TestOverlay());
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            AddStep("visibility", overlay.ToggleVisibility);
        }

        private class TestOverlay : SandboxOverlay
        {
            protected override SandboxButton[] CreateButtons() => new[]
            {
                new SandboxButton("Test1"),
                new SandboxButton("Test2")
            };

            protected override Drawable CreateContent() => new FillFlowContainer
            {
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                AutoSizeAxes = Axes.Y,
                RelativeSizeAxes = Axes.X,
                Direction = FillDirection.Vertical,
                Spacing = new Vector2(0, 20),
                Children = new Drawable[]
                {
                    new TextFlowContainer(f =>
                    {
                        f.Colour = Color4.Black;
                        f.Font = OsuFont.GetFont(size: 30, weight: FontWeight.Regular);
                    })
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Y,
                        RelativeSizeAxes = Axes.X,
                        TextAnchor = Anchor.TopCentre,
                        Text = "This is a very important tip with useful information, you should read it!"
                    },
                    new Container
                    {
                        Anchor = Anchor.Centre,
                        Origin = Anchor.Centre,
                        AutoSizeAxes = Axes.Y,
                        Width = 80,
                        Child = new SandboxCheckbox("Test")
                    }
                }
            };
        }
    }
}
