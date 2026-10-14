using Terraria;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using CalamitySouls.Buffs;
using static Terraria.ModLoader.ModContent;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace CalamitySouls
{
    public class CSProjectile : GlobalProjectile
    {
        public override void GrapplePullSpeed(Projectile projectile, Player player, ref float speed)
        {
            if (projectile.type == ProjectileID.BatHook && (!NPC.downedHalloweenKing || !NPC.downedHalloweenTree)) speed = 11f;
            if (projectile.type == ProjectileID.LunarHookNebula || projectile.type == ProjectileID.LunarHookSolar
                || projectile.type == ProjectileID.LunarHookStardust || projectile.type == ProjectileID.LunarHookVortex) speed = 18.5f;
        }
        public override bool ShouldUpdatePosition(Projectile projectile)
        {
            if (FearmongerFrozen > 0)
            {
                FearmongerFrozen--;
                return false;
            }
            if (projectile.active && projectile.friendly && projectile.minion && projectile.damage > 0 && Main.player[projectile.owner].active)
            {
                CSPlayer mplayer = Main.player[projectile.owner].CS();
                if (mplayer.WeaponFireRateBoost > 1f)
                {
                    float radius = mplayer.WeaponFireRateBoost - 1f;
                    ExtraSummonUpdate += radius;
                    projectile.position += projectile.velocity * (radius + 1f);
                    return false;
                }
            }
            return true;
        }
        public override void AI(Projectile projectile)
        {
            if (projectile.type == ProjectileID.LunarHookNebula || projectile.type == ProjectileID.LunarHookSolar
                || projectile.type == ProjectileID.LunarHookStardust || projectile.type == ProjectileID.LunarHookVortex)
                if (projectile.ai[0] == 2f) projectile.extraUpdates = 1;
                else projectile.extraUpdates = 0;
            while (ExtraSummonUpdate >= 1)
            {
                ExtraSummonUpdate--;
                if (projectile.active) ProjectileLoader.AI(projectile);
            }
        }
        public override void PostAI(Projectile projectile)
        {
            Player player = Main.player[projectile.owner];
            CSPlayer mplayer = player.CS();
            if (projectile.type == 0)
            {
                if (mplayer.ReaverPlantera) projectile.extraUpdates = 1;
                else projectile.extraUpdates = 0;
            }
            if ((projectile.type == ProjectileType<CalamityMod.Projectiles.Magic.DarkSparkPrism>() ||
                projectile.type == ProjectileType<CalamityMod.Projectiles.Magic.KeelhaulBubble>())
                && Main.player[projectile.owner].CS().AuricWeaponBoost)
            {
                int index = -1;
                float dist = 1000f;
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    NPC npc = Main.npc[i];
                    if (npc.active && !npc.friendly & npc.CanBeChasedBy(projectile) && npc.Distance(player.Center) < dist)
                    {
                        dist = npc.Distance(player.Center);
                        index = i;
                    }
                }
                if (index != -1)
                {
                    NPC target = Main.npc[index];
                    projectile.velocity = projectile.DirectionTo(target.Center) * 20f;
                }
            }
            if (projectile.type == ProjectileType<CalamityMod.Projectiles.Magic.DarkSparkPrism>()
                && Main.player[projectile.owner].CS().AuricWeaponBoost)
            {
                projectile.extraUpdates = 9;
            }
            if (projectile.type == ProjectileType<CalamityMod.Projectiles.Magic.DarkSparkBeam>()
            && Main.player[projectile.owner].CS().AuricWeaponBoost)
            {
                projectile.timeLeft++;
            }
        }
        public override bool PreDraw(Projectile projectile, ref Color lightColor)
        {
            if (Main.LocalPlayer.CS().PrismaticTrail && !projectile.friendly && projectile.active)
            {
                Texture2D prism = TextureAssets.Item[ItemType<Ench.EnchPrismatic>()].Value;
                Rectangle sRect = new Rectangle(6, 10, 4, 2);
                Vector2 origin = new Vector2(0, 1);
                Rectangle dRect = new Rectangle((int)(projectile.Center - Main.screenPosition).X, (int)(projectile.Center - Main.screenPosition).Y,
                    (int)(projectile.velocity.Length() * projectile.timeLeft), 2);
                Main.spriteBatch.Draw(prism, dRect, sRect, Color.White * 0.5f, projectile.velocity.ToRotation(), origin, SpriteEffects.None, 0f);
            }
            return true;
        }
        public override bool InstancePerEntity => true;
        public int FearmongerFrozen = 0;
        public float ExtraSummonUpdate;
    }
}