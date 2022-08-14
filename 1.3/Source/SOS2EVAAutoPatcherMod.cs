using RimWorld;
using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace SOS2EVAAutoPatcher
{
    [StaticConstructorOnStartup]
    public static class Startup
    {
        static Startup()
        {
            foreach (var thingDef in DefDatabase<ThingDef>.AllDefs)
            {
                if (thingDef.IsSuitableAsEva())
                {
                    if (thingDef.apparel.tags?.Contains("EVA") ?? false)
                    {
                        continue;
                    }
                    thingDef.apparel.tags ??= new List<string>();
                    thingDef.apparel.tags.Add("EVA");
                    thingDef.SetStatBaseValue(StatDefOf.Insulation_Cold, 100);
                    Log.Message("SOS2 EVA Autopatcher patched " + thingDef.label + " from " + thingDef.modContentPack?.Name ?? "unknown mod to be usable as an EVA apparel.");
                }
            }
        }

        private static bool IsSuitableAsEva(this ThingDef thingDef)
        {
            if (thingDef.IsApparel && (thingDef.techLevel >= TechLevel.Spacer
                || (thingDef.tradeTags?.Contains("Warcasket") ?? false))
                && thingDef.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.Torso)
                && thingDef.apparel.LastLayer == ApparelLayerDefOf.Shell)
            {
                if (thingDef.tradeTags != null && thingDef.tradeTags.Contains("PsychicApparel"))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
