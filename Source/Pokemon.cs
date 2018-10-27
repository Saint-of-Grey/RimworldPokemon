using RimWorld;
using Verse;

namespace Pokemon
{
    [StaticConstructorOnStartup]
    public static class Main
    {
        static Main()
        {
//            var harmony = HarmonyInstance.Create("humanlike.life.stages");
//            harmony.PatchAll(Assembly.GetExecutingAssembly());
            DefDatabase<PokedexDef>.AddAllInMods();
            //Log.Error("Starting the Pokemon Rap!");
            foreach (var pokedexDef in DefDatabase<PokedexDef>.AllDefs)
            {
                ThingDef thingDef = DefDatabase<ThingDef>.GetNamed(pokedexDef.defName);
                PawnKindDef pawnKindDef = DefDatabase<PawnKindDef>.GetNamed(pokedexDef.defName);

                if (thingDef == null)
                {
                    Error(pokedexDef, "No thing def");
                    continue;
                }

                if (pawnKindDef == null)
                {
                    Error(pokedexDef, "No thing def");
                    continue;
                }

                SetupSounds(thingDef, pokedexDef);

                thingDef.race.trainability = TrainabilityDefOf.Advanced;
                thingDef.race.baseBodySize = pokedexDef.Mass / 50f;
                var healthScale = 1f * pokedexDef.HP / Pokemon.latest.BaseStat 
                                          + 1f * pokedexDef.Def / Pokemon.latest.BaseStat / 2F 
                                          + 1f * pokedexDef.SpD / Pokemon.latest.BaseStat / 2F;
                thingDef.race.baseHealthScale *= healthScale;
                
                GetAddStatDef(thingDef, StatDefOf.MarketValue).value *= pokedexDef.Total / Pokemon.latest.BaseStat;
                
                GetAddStatDef(thingDef, StatDefOf.MaxHitPoints).value = healthScale;
                
                GetAddStatDef(thingDef, StatDefOf.MoveSpeed).value = 3.5f*pokedexDef.Spe / Pokemon.latest.BaseStat;
                
                
                GetAddStatDef(thingDef, StatDefOf.ArmorRating_Blunt).value = pokedexDef.Def / Pokemon.latest.BaseStat * 10f;
                GetAddStatDef(thingDef, StatDefOf.ArmorRating_Heat).value = pokedexDef.SpD / Pokemon.latest.BaseStat * 10f;
                GetAddStatDef(thingDef, StatDefOf.ArmorRating_Sharp).value = pokedexDef.Def / Pokemon.latest.BaseStat * 10f;
                
                
                foreach (var tool in thingDef.tools)
                {
                    if (tool.untranslatedLabel.Contains("special"))
                        tool.power *= 1f * pokedexDef.SpA / Pokemon.latest.BaseStat;
                    else tool.power *= 1f * pokedexDef.Atk / Pokemon.latest.BaseStat;
                }

                Log.Message("Setup " + thingDef.label);
            }
        }

        private static StatModifier GetAddStatDef(ThingDef thingDef, StatDef statDef)
        {
            var stat = thingDef.statBases.Find(x => x.stat == statDef) ??
                       new StatModifier() {stat = statDef, value = float.NaN};
            if (stat.value == float.NaN) thingDef.statBases.Add(stat);
            return stat;
        }

        private static void SetupSounds(ThingDef thingDef, PokedexDef pokedexDef)
        {
            foreach (var pawnKindLifeStage in thingDef.race.lifeStageAges)
            {
                SoundDef sound = SoundDef.Named("Cries/" + pokedexDef.Nat.ToString().PadLeft(3, '0') + ".wav");
                if (sound != null)
                    pawnKindLifeStage.soundAngry =
                        pawnKindLifeStage.soundCall =
                            pawnKindLifeStage.soundDeath =
                                pawnKindLifeStage.soundWounded = sound;
                else Error(pokedexDef, "No sound file?");
            }
        }

        private static void Error(PokedexDef pokedexDef, string msg)
        {
            Log.Error(pokedexDef.defName + ":" + msg);
        }
    }
}