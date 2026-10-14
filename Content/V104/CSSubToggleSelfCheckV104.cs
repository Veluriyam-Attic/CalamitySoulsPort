using System;
using System.Collections.Generic;
using System.Reflection;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalamitySouls.Content.V104.Souls;

namespace CalamitySouls.Content.V104
{
    // ==========================================================================================
    // [self-check 2026-10-03] Level-2 soul sub-toggle coverage self-check.
    //
    // Design ("declared batch"): a soul is only expected to have sub-toggles if it is listed in
    //   CSToggleV104.DeclaredSubToggleSouls. For every declared soul we walk its MaterialList and
    //   verify that the matching config property exists (named <SoulShort>_<AccessoryClassName>).
    //
    //   * declared + missing mapping  => Logger.Warn  (a real defect: typo / forgotten batch)
    //   * not declared                => silent      (expected; future souls will not spam logs)
    //
    // NEVER throws: any failure is logged as a warning and swallowed, so the mod still loads.
    // Expected output once all batches are done: "76/76 mapped" and zero warnings.
    // ==========================================================================================
    public class CSSubToggleSelfCheckV104 : ModSystem
    {
        // PROBE-A: proves this ModSystem is autoloaded and that OnModLoad runs.
        public override void OnModLoad()
        {
            Mod.Logger.Info("[PROBE-A] OnModLoad reached");
        }

        // Resolve an item type to its simple name: mod item -> class name, vanilla -> ItemID name.
        private static string ResolveName(int type)
        {
            try
            {
                if (type <= 0) return null;
                ModItem mi = ModContent.GetModItem(type);
                if (mi != null) return mi.GetType().Name;
                if (type < ItemID.Count) return ItemID.Search.GetName(type);
            }
            catch { }
            return null;
        }
        public override void PostSetupContent()
        {
            // PROBE-B: proves PostSetupContent is actually invoked (before any self-check logic).
            Mod.Logger.Info("[PROBE-B] PostSetupContent reached");
            try
            {
                var declared = new HashSet<string>(CSToggleV104.DeclaredSubToggleSouls);
                Type cfgType = typeof(CSConfigV104);
                int total = 0;
                int missing = 0;
                var declaredSoulsSeen = new HashSet<string>();

                foreach (ModItem mi in ModContent.GetContent<ModItem>())
                {
                    if (!(mi is V104SoulBase soul)) continue;
                    string toggle = soul.ToggleName;
                    if (string.IsNullOrEmpty(toggle)) continue;
                    if (!declared.Contains(toggle)) continue;      // not declared => silent (expected)
                    declaredSoulsSeen.Add(toggle);

                    List<int> mats = soul.MaterialList;
                    if (mats == null) continue;
                    string soulShort = toggle.EndsWith("V104") ? toggle.Substring(0, toggle.Length - 4) : toggle;

                    foreach (int matType in mats)
                    {
                        total++;
                        string bare = ResolveName(matType);

                        string propName = bare == null ? null : soulShort + "_" + bare;
                        bool mapped = propName != null && cfgType.GetProperty(propName, BindingFlags.Public | BindingFlags.Instance) != null;
                        if (!mapped)
                        {
                            missing++;
                            Mod.Logger.Warn("[CS V104] sub-toggle MISSING: " + toggle + " / itemType=" + matType + " / expected config prop=" + (propName ?? "<unknown>"));
                        }
                    }
                }

                // declared souls that were never seen in content (e.g. a renamed soul)
                foreach (string d in declared)
                {
                    if (!declaredSoulsSeen.Contains(d))
                        Mod.Logger.Warn("[CS V104] declared sub-toggle soul not found in content: " + d);
                }

                Mod.Logger.Info("[CS V104] sub-toggle self-check: " + (total - missing) + "/" + total
                    + " mapped, declared souls=" + declaredSoulsSeen.Count + ", missing=" + missing);
            }
            catch (Exception e)
            {
                Mod.Logger.Warn("[CS V104] sub-toggle self-check failed (ignored): " + e.Message);
            }
        }
    }
}
