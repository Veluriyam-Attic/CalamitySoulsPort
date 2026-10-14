using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalamityMod;
using CalamityMod.World;
using System.Collections.Generic;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls
{
    public class CSNPC : GlobalNPC
    {
        private List<int> notDeadBossList => new List<int>
        {
            ModContent.NPCType<CalamityMod.NPCs.Leviathan.Leviathan>(),
        };
        private List<int> notDeadBossList2 => new List<int>
        {
            ModContent.NPCType<CalamityMod.NPCs.HiveMind.HiveMind>(),
            ModContent.NPCType<CalamityMod.NPCs.Leviathan.Leviathan>(),
            ModContent.NPCType<CalamityMod.NPCs.StormWeaver.StormWeaverHead>(),
            ModContent.NPCType<CalamityMod.NPCs.StormWeaver.StormWeaverBody>(),
            ModContent.NPCType<CalamityMod.NPCs.StormWeaver.StormWeaverTail>(),
            ModContent.NPCType<CalamityMod.NPCs.DevourerofGods.DevourerofGodsHead>(),
            ModContent.NPCType<CalamityMod.NPCs.DevourerofGods.DevourerofGodsBody>(),
            ModContent.NPCType<CalamityMod.NPCs.DevourerofGods.DevourerofGodsTail>(),
        };
        public override void SetDefaults(NPC npc)
        {
            if (npc.boss && CalamityWorld.revenge && !notDeadBossList.Contains(npc.type) && npc.Calamity().newAI[0] != 0f
                && Main.netMode != NetmodeID.Server && !Main.LocalPlayer.dead && Main.LocalPlayer.active)
            {
                Main.LocalPlayer.CS().PlagueReaperHasAdrenalineCurrently = false;
            }
        }
        public override void ResetEffects(NPC npc)
        {
            if (Frozen > 0) Frozen--;
            ExtraPositionUpdate = 0f;
            if (CSWorld.HyperMode && !CSUtils.ManicModeNerf)
            {
                ExtraPositionUpdate = ExtraUpdateAmount;
                ExtraUpdate += ExtraUpdateAmount * 2;
            }
            UpdateExtraPosition(npc);
        }
        public void UpdateExtraPosition(NPC npc)
        {
            if (FearmongerFrozen)
            {
                ExtraPositionUpdate = -1;
            }
            if (ExtraPositionUpdate != 0f)
            {
                if (!npc.noTileCollide) npc.velocity
                          = Collision.TileCollision(npc.position, npc.velocity * ExtraPositionUpdate, npc.width, npc.height, false, false, 1) / ExtraPositionUpdate;
                npc.position += npc.velocity * ExtraPositionUpdate;
            }
        }
        // 1.4：GlobalNPC.SetupShop(int type, Chest shop, ref int nextSlot) 改为 ModifyActiveShop(NPC npc, string shopName, Item[] items)
        public override void ModifyActiveShop(NPC npc, string shopName, Item[] items)
        {
            if (npc.type == NPCID.DyeTrader)
            {
                int nextSlot = 0;
                while (nextSlot < items.Length && items[nextSlot] != null && items[nextSlot].type != ItemID.None) nextSlot++;
                if (nextSlot >= items.Length) return;
                items[nextSlot].SetDefaults(ItemType<Items.SummonItems.SuspiciousLookingBox>());
                int coinAmt = 10;
                if (Main.hardMode) coinAmt = 50;
                if (NPC.downedMoonlord) coinAmt = 100;
                items[nextSlot].value = Item.sellPrice(0, coinAmt);
            }
        }
        public override void AI(NPC npc)
        {
            while (ExtraUpdate >= 1)
            {
                ExtraUpdate--;
                if (npc.active)
                {
                    npc.position -= npc.velocity * (1 + ExtraPositionUpdate);
                    npc.UpdateNPC(npc.whoAmI);
                    ExtraUpdate -= ExtraUpdateAmount * 2;
                }
            }
        }
        public override void PostAI(NPC npc)
        {
            if (Frozen > 0)
            {
                Lighting.AddLight(npc.Center, new Vector3(1, 1, 1));
                if (!CSUtils.AnyBossAlive)
                {
                    npc.velocity.X = 0;
                    npc.velocity.Y += 0.05f;
                    npc.velocity.Y *= 0.9f;
                }
            }
            if (npc.HasBuff(BuffType<Buffs.TerminalMark>())) npc.dontTakeDamage = false;
            {/* Unused */
                //if (CSWorld.HyperMode)
                //{
                //    if (npc.type == NPCID.CultistBoss)
                //    {
                //        if (npc.target >= 0 && npc.target < 255)
                //        {
                //            Player player = Main.player[npc.target];
                //            if (player.active && !player.dead)
                //            {
                //                npc.Center = Vector2.Lerp(npc.Center, player.Center - Vector2.UnitY * 300f, 0.01f);
                //            }
                //        }
                //    }
                //    if (npc.type == NPCID.CultistBossClone)
                //    {
                //        if (npc.target >= 0 && npc.target < 255)
                //        {
                //            Player player = Main.player[npc.target];
                //            if (player.active && !player.dead)
                //            {
                //                npc.Center = Vector2.Lerp(npc.Center, player.Center - new Vector2(Main.rand.Next(-300, 300), Main.rand.Next(450, 601)), 0.01f);
                //            }
                //        }
                //    }
                //}
            }
            FearmongerFrozen = false;
        }
        public override void OnKill(NPC npc)
        {
            if (npc.boss && CalamityWorld.revenge && !notDeadBossList2.Contains(npc.type)
                && Main.netMode != NetmodeID.Server && !Main.player[Main.myPlayer].dead && Main.player[Main.myPlayer].active)
            {
                Main.LocalPlayer.CS().PlagueReaperHasAdrenalineCurrently = false;
            }
        }
        public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
        {
            if (CSWorld.HyperMode && CSUtils.ManicModeNerf)
            {
                spawnRate *= 2;
                maxSpawns /= 2;
            }
        }
        // 1.4：StrikeNPC 已移除，拆成按来源的 ModifyHitByItem / ModifyHitByProjectile
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (CSWorld.HyperMode && !CSUtils.ManicModeNerf) modifiers.FinalDamage *= 0.7f;
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (CSWorld.HyperMode && !CSUtils.ManicModeNerf) modifiers.FinalDamage *= 0.7f;
        }
        public override bool CanHitPlayer(NPC npc, Player target, ref int cooldownSlot)
        {
            if (Frozen > 0 && !CSUtils.AnyBossAlive) return false;
            return true;
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Main.LocalPlayer.CS().PrismaticTrail && !npc.friendly && npc.active)
            {
                Texture2D prism = TextureAssets.Item[ItemType<Ench.EnchPrismatic>()].Value;
                Rectangle sRect = new Rectangle(14, 8, 4, 2);
                Vector2 origin = new Vector2(0, 1);
                Rectangle dRect = new Rectangle((int)(npc.Center - Main.screenPosition).X, (int)(npc.Center - Main.screenPosition).Y,
                    (int)(npc.velocity.Length() * npc.timeLeft), 2);
                Main.spriteBatch.Draw(prism, dRect, sRect, Color.White * 0.5f, npc.velocity.ToRotation(), origin, SpriteEffects.None, 0f);
            }
            if (Frozen > 0)
            {
                spriteBatch.End();
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default,
                    RasterizerState.CullNone, CalamitySouls.FrozenEffect, Main.GameViewMatrix.TransformationMatrix);
                originalState = Main.graphics.GraphicsDevice.RasterizerState;
                CalamitySouls.FrozenEffect.Parameters["uImageSize"].SetValue(TextureAssets.Npc[npc.type].Value.Size());
                CalamitySouls.FrozenEffect.CurrentTechnique.Passes["Frozen"].Apply();
                //Main.graphics.GraphicsDevice.Textures[1] = CalamitySouls.FrostTexture;
            }
            return true;
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (Frozen > 0)
            {
                Main.graphics.GraphicsDevice.RasterizerState = originalState;
                spriteBatch.End();
                spriteBatch.Begin();
            }
            if (npc.whoAmI == Main.LocalPlayer.CS().FearmongerMark)
            {
                spriteBatch.Draw(CSTexture.FearmongerMark, npc.Center - Vector2.UnitY * (npc.height / 2f + 14f) - Main.screenPosition, null, drawColor,
                    0f, CSTexture.FearmongerMark.Size() / 2f, 1f, SpriteEffects.None, 0f);
            }
        }
        private RasterizerState originalState;
        public override bool InstancePerEntity => true;
        public int Frozen = 0;
        public bool FearmongerFrozen;
        public float ExtraUpdate = 0;
        public float ExtraPositionUpdate = 0;
        private const float ExtraUpdateAmount = 0.25f;
    }
}