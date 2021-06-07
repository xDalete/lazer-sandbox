﻿using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Graphics.Containers;
using osuTK;

namespace osu.Game.Rulesets.Sandbox.Screens.Main.Components
{
    public class SandboxButtonSystem : CompositeDrawable
    {
        public SandboxSelectionButton[] Buttons
        {
            set => buttonsFlow.Children = value;
        }

        private readonly FillFlowContainer<SandboxSelectionButton> buttonsFlow;

        public SandboxButtonSystem()
        {
            RelativeSizeAxes = Axes.Both;
            InternalChild = new OsuScrollContainer(Direction.Horizontal)
            {
                RelativeSizeAxes = Axes.Both,
                Height = 0.7f,
                Anchor = Anchor.Centre,
                Origin = Anchor.Centre,
                ScrollbarVisible = false,
                Masking = false,
                Child = buttonsFlow = new FillFlowContainer<SandboxSelectionButton>
                {
                    RelativeSizeAxes = Axes.Y,
                    AutoSizeAxes = Axes.X,
                    Direction = FillDirection.Horizontal,
                    Spacing = new Vector2(20, 0)
                }
            };
        }

        protected override void Update()
        {
            base.Update();
            buttonsFlow.Margin = new MarginPadding { Horizontal = DrawWidth / 2 - SandboxSelectionButton.WIDTH / 2 };
        }
    }
}
