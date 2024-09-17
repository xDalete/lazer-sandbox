﻿using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics.Containers;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Sandbox.Screens.Visualizer.Components.MusicHelpers
{
    public partial class CurrentBeatmapProvider : Container
    {
        protected IBindable<WorkingBeatmap> Beatmap = new Bindable<WorkingBeatmap>();

        [BackgroundDependencyLoader]
        private void load(IBindable<WorkingBeatmap> working)
        {
            Beatmap.BindTo(working);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            Beatmap.BindValueChanged(OnBeatmapChanged, true);
        }

        protected virtual void OnBeatmapChanged(ValueChangedEvent<WorkingBeatmap> beatmap)
        {
        }
    }
}
