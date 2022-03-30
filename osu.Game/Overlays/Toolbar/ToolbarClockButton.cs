// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Game.Configuration;
using osu.Game.Input.Bindings;

namespace osu.Game.Overlays.Toolbar
{
    public class ToolbarClockButton : ToolbarButton
    {
        protected override Anchor TooltipAnchor => Anchor.TopRight;

        private Bindable<ToolbarClockDisplayMode> clockDisplayMode;

        public ToolbarClockButton()
        {
            // Width *= 2.2f;
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            clockDisplayMode = config.GetBindable<ToolbarClockDisplayMode>(OsuSetting.ToolbarClockDisplayMode);
            TooltipMain = "clock";
            // Text = "Clock";
            TooltipSub = "cycle clock display mode";
            SetIcon("Icons/Hexacons/calendar");
        }

        protected override bool OnClick(ClickEvent e)
        {
            cycleDisplayMode();
            return base.OnClick(e);
        }

        private void cycleDisplayMode()
        {
            switch (clockDisplayMode.Value)
            {
                case ToolbarClockDisplayMode.Analog:
                    clockDisplayMode.Value = ToolbarClockDisplayMode.Full;
                    break;

                case ToolbarClockDisplayMode.Digital:
                    clockDisplayMode.Value = ToolbarClockDisplayMode.Analog;
                    break;

                case ToolbarClockDisplayMode.DigitalWithRuntime:
                    clockDisplayMode.Value = ToolbarClockDisplayMode.Digital;
                    break;

                case ToolbarClockDisplayMode.Full:
                    clockDisplayMode.Value = ToolbarClockDisplayMode.DigitalWithRuntime;
                    break;
            }
        }
    }
}
