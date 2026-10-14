using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.UI;
using Terraria.ModLoader;
using Terraria.ModLoader.Config.UI;
using Terraria.UI.Chat;

namespace CalamitySouls.Common
{
    /// <summary>
    /// 带物品图标的配置元素：在每行文字【前面】画一个物品贴图，其余外观与 tML 自带开关一致。
    /// 用法：给配置属性加 [CustomModConfigItem(typeof(ItemIconBoolElement))]，
    ///       并在 IconMap 里登记“属性名 -> 物品”的对应关系。
    /// </summary>
    public class ItemIconBoolElement : ConfigElement<bool>
    {
        private static Asset<Texture2D> _toggle;

        /// <summary>属性名 -> 物品完整名（“模组/物品名”）。</summary>
                public static readonly Dictionary<string, string> IconMap = new()
        {
            { "DisableSoulSpeed", "Terraria/TerrasparkBoots" },
            { "EnchAero", "CalamitySouls/Ench/EnchAerospec" },
            { "EnchAstral", "CalamitySouls/Ench/EnchAstral" },
            { "EnchAuric", "CalamitySouls/Ench/EnchAuric" },
            { "EnchBlood", "CalamitySouls/Ench/EnchBloodflare" },
            { "EnchBrim", "CalamitySouls/Ench/EnchBrimflame" },
            { "EnchDaedalus", "CalamitySouls/Ench/EnchDaedalus" },
            { "EnchDemonshade", "CalamitySouls/Ench/EnchDemonshade" },
            { "EnchDesert", "CalamitySouls/Ench/EnchDesertprowler" },
            { "EnchEmpyrean", "CalamitySouls/Ench/EnchEmpyrean" },
            { "EnchFathom", "CalamitySouls/Ench/EnchFathom" },
            { "EnchFearmonger", "CalamitySouls/Ench/EnchFearmonger" },
            { "EnchGodslayer", "CalamitySouls/Ench/EnchGodslayer" },
            { "EnchGodslayerAncient", "CalamitySouls/Ench/EnchGodslayerAncient" },
            { "EnchHydrothermic", "CalamitySouls/Ench/EnchHydrothermic" },
            { "EnchMollusk", "CalamitySouls/Ench/EnchMollusk" },
            { "EnchOmega", "CalamitySouls/Ench/EnchOmega" },
            { "EnchPlagueBringer", "CalamitySouls/Ench/EnchPlaguebringer" },
            { "EnchPlagueReaper", "CalamitySouls/Ench/EnchPlagueReaper" },
            { "EnchPrismatic", "CalamitySouls/Ench/EnchPrismatic" },
            { "EnchReaver", "CalamitySouls/Ench/EnchReaver" },
            { "EnchSilva", "CalamitySouls/Ench/EnchSilva" },
            { "EnchSnowRuffian", "CalamitySouls/Ench/EnchSnowruffian" },
            { "EnchStatigel", "CalamitySouls/Ench/EnchStatigel" },
            { "EnchSulphurous", "CalamitySouls/Ench/EnchSulphurous" },
            { "EnchTarragon", "CalamitySouls/Ench/EnchTarragon" },
            { "EnchTitan", "CalamitySouls/Ench/EnchTitan" },
            { "EnchUmbraphile", "CalamitySouls/Ench/EnchUmbraphile" },
            { "EnchVictide", "CalamitySouls/Ench/EnchVictide" },
            { "EnchWulfrum", "CalamitySouls/Ench/EnchWulfrum" },
            { "ForceAnnihilation", "CalamitySouls/Force/ForceAnnihilation" },
            { "ForceCreature", "CalamitySouls/Force/ForceCreature" },
            { "ForceRevolution", "CalamitySouls/Force/ForceRevolution" },
            { "ForceTechnical", "CalamitySouls/Force/ForceTechnical" },
            { "ForceTyranny", "CalamitySouls/Force/ForceTyranny" },
            { "SpellAquatic", "CalamitySouls/Spell/SpellAquatic" },
            { "SpellBetrayal", "CalamitySouls/Spell/SpellBetrayal" },
            { "SpellDespair", "CalamitySouls/Spell/SpellDespair" },
            { "SpellEleum", "CalamitySouls/Spell/SpellEleum" },
            { "SpellGlobe", "CalamitySouls/Spell/SpellGlobe" },
            { "SpellHeavenshade", "CalamitySouls/Spell/SpellHeavenshade" },
            { "SpellOasis", "CalamitySouls/Spell/SpellOasis" },
            { "SpellPlague", "CalamitySouls/Spell/SpellPlague" },
            { "SpellStarlight", "CalamitySouls/Spell/SpellStarlight" },
        };

        private static readonly Dictionary<string, int> _cache = new();

        private static int ResolveIcon(string prop)
        {
            if (_cache.TryGetValue(prop, out int cached))
                return cached;

            int result = 0;
            try
            {
            if (!string.IsNullOrEmpty(prop) && IconMap.TryGetValue(prop, out string full))
            {
                if (full.StartsWith("Terraria/"))
                {
                    int id = ItemID.Search.GetId(full.Substring("Terraria/".Length));
                    if (id > 0)
                        result = id;
                }
                else
                {
                    // 用 TryFind：Find 在找不到时会抛 KeyNotFoundException（会把配置界面搞崩）
                    try
                    {
                        if (ModContent.TryFind<ModItem>(full, out ModItem mi) && mi != null)
                            result = mi.Type;
                    }
                    catch { result = 0; }
                }
            }
            }
            catch { result = 0; }
            _cache[prop] = result;
            return result;
        }

        public override void OnBind()
        {
            base.OnBind();
            DrawLabel = false;
            OnLeftClick += (_, __) => Value = !Value;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            base.DrawSelf(spriteBatch);

            CalculatedStyle dim = GetDimensions();

            string prop = MemberInfo?.Name ?? string.Empty;
            int itemType = ResolveIcon(prop);
            if (itemType > 0)
            {
                Main.instance.LoadItem(itemType);
                Texture2D tex = TextureAssets.Item[itemType].Value;
                Rectangle src = Main.itemAnimations[itemType] != null
                    ? Main.itemAnimations[itemType].GetFrame(tex)
                    : tex.Frame();
                float scale = 1f;
                if (src.Width > 24 || src.Height > 24)
                    scale = 24f / System.Math.Max(src.Width, src.Height);
                spriteBatch.Draw(tex, new Vector2(dim.X + 20f, dim.Y + dim.Height / 2f), src, Color.White, 0f,
                    new Vector2(src.Width / 2f, src.Height / 2f), scale, SpriteEffects.None, 0f);
            }

            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.MouseText.Value, Label,
                new Vector2(dim.X + 40f, dim.Y + 8f), Color.White, 0f, Vector2.Zero, Vector2.One);

            if (_toggle == null)
                _toggle = Main.Assets.Request<Texture2D>("Images/UI/Settings_Toggle");
            ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value,
                Value ? Lang.menu[126].Value : Lang.menu[124].Value,
                new Vector2(dim.X + dim.Width - 60f, dim.Y + 8f), Color.White, 0f, Vector2.Zero, new Vector2(0.8f));
            Rectangle frame = new(Value ? (_toggle.Width() - 2) / 2 + 2 : 0, 0, (_toggle.Width() - 2) / 2, _toggle.Height());
            spriteBatch.Draw(_toggle.Value,
                new Vector2(dim.X + dim.Width - frame.Width - 10f, dim.Y + 8f),
                frame, Color.White, 0f, Vector2.Zero, Vector2.One, SpriteEffects.None, 0f);
        }
    }
}
