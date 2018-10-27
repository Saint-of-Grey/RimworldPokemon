using UnityEngine;
using Verse;

namespace Pokemon
{
    public class Pokemon : Mod
    {
        public static MyModSettings latest;

        public Pokemon(ModContentPack content) : base(content)
        {
            this.Settings = GetSettings<MyModSettings>();
            latest = Settings;
        }

        public MyModSettings Settings { get; set; }

        public override string SettingsCategory() => "Pokemon";

        public override void DoSettingsWindowContents(Rect inRect)
        {
            Settings.BaseStat = Widgets.HorizontalSlider(
                inRect.TopHalf().TopHalf().TopHalf().ContractedBy(4f),
                Settings.BaseStat, 10, 100, true,
                "Base Stat : " + Settings.BaseStat + " Lower is stronger"
                , "10", "100");

            this.Settings.Write();
        }
    }

    public class MyModSettings : ModSettings
    {
        public float BaseStat = 40.0f;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref this.BaseStat, "BaseStat", 40.0f);
        }
    }
}