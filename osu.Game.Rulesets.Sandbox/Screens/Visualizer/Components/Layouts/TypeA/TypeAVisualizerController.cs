using osu.Framework.Graphics;
using osu.Framework.Bindables;
using osu.Framework.Allocation;
using System;
using osuTK;
using osu.Game.Rulesets.Sandbox.Screens.Visualizer.Components.MusicHelpers;
using osu.Game.Rulesets.Sandbox.Configuration;
using osu.Game.Rulesets.Sandbox.Screens.Visualizer.Components.Visualizers;
using osu.Game.Rulesets.Sandbox.Screens.Visualizer.Components.Visualizers.Circular;
using osu.Framework.Audio.Track;
using osu.Game.Beatmaps;

namespace osu.Game.Rulesets.Sandbox.Screens.Visualizer.Components.Layouts.TypeA
{
    public partial class TypeAVisualizerController : MusicAmplitudesProvider
    {
        [Resolved(canBeNull: true)]
        private SandboxRulesetConfigManager config { get; set; }

        private readonly Bindable<int> visuals = new Bindable<int>(3);
        private readonly Bindable<double> barWidth = new Bindable<double>(1.0);
        private readonly Bindable<int> totalBarCount = new Bindable<int>(3500);
        private readonly Bindable<int> rotationsPerMinute = new Bindable<int>(0);
        private readonly Bindable<int> decay = new Bindable<int>(200);
        private readonly Bindable<int> multiplier = new Bindable<int>(400);
        private readonly Bindable<CircularBarType> type = new Bindable<CircularBarType>(CircularBarType.Basic);
        private readonly Bindable<bool> symmetry = new Bindable<bool>(true);
        private readonly Bindable<int> smoothness = new Bindable<int>();

        private Track track;

        protected override void OnBeatmapChanged(ValueChangedEvent<WorkingBeatmap> beatmap)
        {
            base.OnBeatmapChanged(beatmap);
            track = beatmap.NewValue?.Track;
        }

        [BackgroundDependencyLoader]
        private void load()
        {
            Origin = Anchor.Centre;
            Size = new Vector2(348);
            RelativePositionAxes = Axes.Both;

            config?.BindWith(SandboxRulesetSetting.VisualizerAmount, visuals);
            config?.BindWith(SandboxRulesetSetting.BarWidthA, barWidth);
            config?.BindWith(SandboxRulesetSetting.BarsPerVisual, totalBarCount);
            config?.BindWith(SandboxRulesetSetting.RotationsPerMinute, rotationsPerMinute);
            config?.BindWith(SandboxRulesetSetting.CircularBarType, type);
            config?.BindWith(SandboxRulesetSetting.DecayA, decay);
            config?.BindWith(SandboxRulesetSetting.MultiplierA, multiplier);
            config?.BindWith(SandboxRulesetSetting.Symmetry, symmetry);
            config?.BindWith(SandboxRulesetSetting.SmoothnessA, smoothness);
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();
            totalBarCount.BindValueChanged(_ => updateBarCount());
            visuals.BindValueChanged(_ => updateVisuals());
            symmetry.BindValueChanged(_ => updateVisuals());
            type.BindValueChanged(_ => updateVisuals(), true);
        }

        private void updateVisuals()
        {
            Clear();

            var degree = 360f / trueVisualsCount;

            for (int i = 0; i < trueVisualsCount; i++)
            {
                Add(createVisualizer().With(v =>
                {
                    v.Anchor = Anchor.Centre;
                    v.Origin = Anchor.Centre;
                    v.RelativeSizeAxes = Axes.Both;
                    v.Rotation = i * degree;
                    v.DegreeValue.Value = degree;
                    v.BarWidth.BindTo(barWidth);
                    v.Decay.BindTo(decay);
                    v.HeightMultiplier.BindTo(multiplier);
                    v.Smoothness.BindTo(smoothness);

                    if (symmetry.Value)
                        v.Reversed.Value = i % 2 == 0;
                }));
            }

            updateBarCount();
            rotationsPerMinute.TriggerChange();
        }

        private CircularMusicVisualizerDrawable createVisualizer()
        {
            switch (type.Value)
            {
                default:
                case CircularBarType.Basic:
                    return new BasicMusicVisualizerDrawable();

                case CircularBarType.Rounded:
                    return new RoundedMusicVisualizerDrawable();

                case CircularBarType.Fall:
                    return new FallMusicVisualizerDrawable();

                case CircularBarType.Dots:
                    return new DotsMusicVisualizerDrawable();
            }
        }

        private void updateBarCount()
        {
            var barsPerVis = (int)Math.Round((float)totalBarCount.Value / trueVisualsCount);

            foreach (var c in Children)
                ((MusicVisualizerDrawable)c).BarCount.Value = barsPerVis;
        }

        protected override void Update()
        {
            base.Update();
            var progress = (track == null || track.Length == 0) ? 0 : track.CurrentTime % 60000 / 60000;
            Rotation = rotationsPerMinute.Value * (360f * (float)progress) % 360 + (symmetry.Value ? 180f / visuals.Value : 0);
        }

        protected override void OnAmplitudesUpdate(float[] amplitudes)
        {
            foreach (var c in Children)
                ((MusicVisualizerDrawable)c).SetAmplitudes(amplitudes);
        }
        private int trueVisualsCount => visuals.Value * (symmetry.Value ? 2 : 1);
    }
}
