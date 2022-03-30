// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Framework.Input.Events;
using osu.Game.Configuration;
using osuTK;

namespace osu.Game.Overlays.Toolbar
{
    public class ToolbarClock : CompositeDrawable
    {
        private Bindable<ToolbarClockDisplayMode> clockDisplayMode;

        private DigitalClockDisplay digital;
        private AnalogClockDisplay analog;

        public ToolbarClock()
        {
            RelativeSizeAxes = Axes.Y;
            AutoSizeAxes = Axes.X;

            Padding = new MarginPadding(10);
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            clockDisplayMode = config.GetBindable<ToolbarClockDisplayMode>(OsuSetting.ToolbarClockDisplayMode);

            InternalChild = new FillFlowContainer
            {
                RelativeSizeAxes = Axes.Y,
                AutoSizeAxes = Axes.X,
                Direction = FillDirection.Horizontal,
                Spacing = new Vector2(5),
                Children = new Drawable[]
                {
                    analog = new AnalogClockDisplay
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    },
                    digital = new DigitalClockDisplay
                    {
                        Anchor = Anchor.CentreLeft,
                        Origin = Anchor.CentreLeft,
                    }
                }
            };
        }

        protected override void LoadComplete()
        {
            base.LoadComplete();

            clockDisplayMode.BindValueChanged(displayMode =>
            {
                bool showAnalog = displayMode.NewValue == ToolbarClockDisplayMode.Analog || displayMode.NewValue == ToolbarClockDisplayMode.Full;
                bool showDigital = displayMode.NewValue != ToolbarClockDisplayMode.Analog;
                bool showRuntime = displayMode.NewValue == ToolbarClockDisplayMode.DigitalWithRuntime || displayMode.NewValue == ToolbarClockDisplayMode.Full;

                digital.FadeTo(showDigital ? 1 : 0);
                digital.ShowRuntime = showRuntime;

                analog.FadeTo(showAnalog ? 1 : 0);
            }, true);
        }
    }
}
