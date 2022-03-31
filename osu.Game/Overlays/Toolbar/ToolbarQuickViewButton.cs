// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Input.Events;
using osu.Framework.Screens;
using osu.Game.Configuration;
using osu.Game.Rulesets;
using osu.Game.Screens.Play;
using osu.Game.Screens.Select;

namespace osu.Game.Overlays.Toolbar
{
    public class ToolbarQuickViewButton : ToolbarButton
    {
        protected override Anchor TooltipAnchor => Anchor.TopRight;

        [Resolved(canBeNull: true)]
        private OsuGame game { get; set; }

        [Resolved]
        private IBindable<RulesetInfo> ruleset { get; set; }

        public ToolbarQuickViewButton()
        {
            Width *= 3f;
        }

        [BackgroundDependencyLoader]
        private void load(OsuConfigManager config)
        {
            TooltipMain = "quick view";
            Text = "Quick View";
            TooltipSub = "start quick autoplay";
            SetIcon("Icons/Hexacons/search");
        }

        protected override bool OnClick(ClickEvent e)
        {
            game?.PerformFromScreen(screen =>
                {
                    if (screen is Player)
                        return;

                    var replayGeneratingMod = ruleset.Value.CreateInstance().GetAutoplayMod();
                    if (replayGeneratingMod != null)
                        screen.Push(new PlayerLoader(() => new ReplayPlayer((beatmap, mods) => replayGeneratingMod.CreateReplayScore(beatmap, mods))));
                }, new[] { typeof(Player), typeof(SongSelect) });
            return base.OnClick(e);
        }
    }
}
