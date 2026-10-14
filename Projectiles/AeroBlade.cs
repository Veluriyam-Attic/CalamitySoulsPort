using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using CalamitySouls.Buffs;
using CalamityMod;
using static Terraria.ModLoader.ModContent;

namespace CalamitySouls.Projectiles
{
    public class AeroBlade : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = -1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
        }
        public override void SetDefaults()
        {
            ProjectileID.Sets.TrailingMode[Projectile.type] = -1;
            ProjectileID.Sets.TrailCacheLength[Projectile.type] = 10;
            Projectile.width = Projectile.height = 98;
            Projectile.friendly = true;
            Projectile.tileCollide = false;
            Projectile.ignoreWater = true;
            Projectile.timeLeft = 600;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
            Projectile.penetrate = -1;
        }
        public float Ai
        {
            get => Projectile.ai[1];
            set => Projectile.ai[1] = value;
        }
        private const float ChaseRange = 1000f;
        public override void AI()
        {
            Projectile.oldPos[0] = Projectile.Center;
            Projectile.oldRot[0] = Projectile.rotation;
            for (int i = Projectile.oldPos.Length - 1; i > 0; i--)
            {
                Projectile.oldPos[i] = Projectile.oldPos[i - 1];
                Projectile.oldRot[i] = Projectile.oldRot[i - 1];
            }
            Player player = Main.player[Projectile.owner];
            if (!player.active || player.dead || !player.CS().HasAeroBlade || Projectile.Distance(player.Center) > 2500f)
            {
                Projectile.Kill();
                return;
            }
            if (player.CS().GlobeBlade)
            {
                if (Ai % 2 == 0) Ai++;
                Projectile.width = Projectile.height = 126;
            }
            else
            {
                Projectile.width = Projectile.height = 98;
            }
            if (Projectile.type == ProjectileType<AeroBlade>()) Projectile.timeLeft = 2;
            //player.AddBuff(BuffType<AeroBladeBuff>(), 3600, true);
            Projectile.GetPlayerDamageType(player);
            float chaseRange = ChaseRange;
            if (player.CS().GlobeBlade) chaseRange *= 2;
            int index = -1;
            for (int i = 0; i < Main.npc.Length; i++)
            {
                NPC n = Main.npc[i];
                if (n.active && !n.friendly && n.CanBeChasedBy() && n.Distance(player.Center) < chaseRange)
                {
                    index = i;
                    chaseRange = n.Distance(player.Center);
                }
            }
            if (index == -1)
            {
                Vector2 pos = player.Center + new Vector2(player.direction * -32f, -16f);
                Projectile.FollowPlayer(pos, 1600f, 5f, player.velocity, 20f, true);
                Projectile.rotation = Projectile.rotation.AngleLerp(MathHelper.PiOver2, 0.1f);
                return;
            }
            NPC npc = Main.npc[index];
            Ai++;
            if (Ai % 60 < 40)
            {
                Projectile.velocity = Vector2.Lerp(Projectile.velocity,
                    Projectile.DirectionTo(npc.Center + npc.DirectionTo(Projectile.Center).RotatedBy(MathHelper.ToRadians(0.6f)) * 320f) * 50f, 0.1f);
                if (Ai % 16 == 0 && player.whoAmI == Main.myPlayer)
                {
                    Projectile beam = Projectile.NewProjectileDirect(null, Projectile.Center, Projectile.DirectionTo(npc.Center) * 25f, ProjectileID.SwordBeam,
                                              (int)((player.CS().GlobeBlade ? 150f : 30f) * player.AverageDamage()), 0f, player.whoAmI);
                    beam.GetPlayerDamageType(player);
                    beam.tileCollide = false;
                    beam.extraUpdates = 2;
                    beam.penetrate = 2;
                    beam.usesLocalNPCImmunity = true;
                    beam.localNPCHitCooldown = 45;
                    beam.timeLeft = 60;
                }
            }
            else if (Ai % 60 == 40)
            {
                Projectile.velocity = Projectile.DirectionTo(npc.Center) * 50f;
                SoundEngine.PlaySound(SoundID.Item71, Projectile.Center);
            }
            else Projectile.velocity *= 0.96f;
            Projectile.rotation = Projectile.rotation.AngleLerp(Projectile.AngleTo(npc.Center), 0.1f);
        }
        public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
        {
            // 1.3 里这里直接指定固定伤害(30/150)，1.4 用倍率表达
            bool globe = Main.player[Projectile.owner].CS().GlobeBlade;
            float want = globe ? 150f : 30f;
            modifiers.FinalDamage *= want / System.Math.Max(1, Projectile.damage);
            if (globe) modifiers.SetCrit(); else modifiers.DisableCrit();
            if (globe) modifiers.Knockback.Flat += 10f;
        }
        public override bool PreDraw(ref Color lightColor)
        {
            {
                //List<CustomVertexInfo> bars = new List<CustomVertexInfo>();
                //    int width = 49;
                //Vector2 normalDir = projectile.rotation.ToRotationVector2();
                //bars.Add(new CustomVertexInfo(projectile.Center + normalDir * width, Color.White, new Vector3(1f, 1, 1f)));
                //bars.Add(new CustomVertexInfo(projectile.Center + normalDir * -width, Color.White, new Vector3(1f, 0, 1f)));
                //for (int i = 1; i < projectile.oldPos.Length; ++i)
                //{
                //    if (projectile.oldPos[i] == Vector2.Zero) break;

                //    normalDir = projectile.oldRot[i].ToRotationVector2();

                //    float factor = i / (float)projectile.oldPos.Length;
                //    Color color = Color.Lerp(Color.White, Color.Black, factor);
                //    float opacity = MathHelper.Lerp(1f, 0.05f, factor);

                //    bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * width, color, new Vector3((float)System.Math.Sqrt(factor), 1, opacity)));
                //    bars.Add(new CustomVertexInfo(projectile.oldPos[i] + normalDir * -width, color, new Vector3((float)System.Math.Sqrt(factor), 0, opacity)));
                //}
                //List<CustomVertexInfo> triangleList = new List<CustomVertexInfo>();
                //if (bars.Count > 2)
                //{
                //    for (int i = 0; i < bars.Count - 2; i += 2)
                //    {
                //        triangleList.Add(bars[i]);
                //        triangleList.Add(bars[i + 2]);
                //        triangleList.Add(bars[i + 1]);
                //        triangleList.Add(bars[i + 1]);
                //        triangleList.Add(bars[i + 2]);
                //        triangleList.Add(bars[i + 3]);
                //    }
                //    Main.spriteBatch.End();
                //    Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.Default, RasterizerState.CullNone);

                //    Matrix projection = Matrix.CreateOrthographicOffCenter(0, Main.screenWidth, Main.screenHeight, 0, 0, 1);
                //    Matrix model = Matrix.CreateTranslation(new Vector3(-Main.screenPosition.X, -Main.screenPosition.Y, 0));
                //    CalamitySouls.TrailEffect.Parameters["uTransform"].SetValue(model * projection);
                //    Main.graphics.GraphicsDevice.Textures[0] = CSTexture.ShaderEffectAeroBladeColor;
                //    Main.graphics.GraphicsDevice.Textures[1] = CSTexture.ShaderEffectShining;
                //    Main.graphics.GraphicsDevice.Textures[2] = CSTexture.ShaderEffectBlade;
                //    CalamitySouls.TrailEffect.CurrentTechnique.Passes[0].Apply();

                //    Main.graphics.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, triangleList.ToArray(), 0, triangleList.Count / 3);

                //    Main.spriteBatch.End();
                //    Main.spriteBatch.Begin();
                //}
            }
            Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
            Texture2D aTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/AeroBlade_Afterimage").Value;
            Texture2D glowTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/AeroBlade_Glow").Value;
            if (Main.player[Projectile.owner].CS().GlobeBlade)
            {
                texture = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/GlobeBlade").Value;
                aTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/GlobeBlade_Afterimage").Value;
                glowTex = ModContent.Request<Texture2D>("CalamitySouls/Projectiles/GlobeBlade_Glow").Value;
            }
            Color color = Color.White;
            float scale = Projectile.scale;
            for (int i = 0; i < Projectile.oldPos.Length; i += 2)
            {
                color *= 0.7f;
                scale *= 0.9f;
                Main.spriteBatch.Draw(aTex, Projectile.oldPos[i] - Main.screenPosition, null, color, Projectile.oldRot[i],
                    aTex.Size() / 2f, scale, SpriteEffects.None, 0f);
            }
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            Main.spriteBatch.Draw(glowTex, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation,
                glowTex.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            Main.spriteBatch.End();
            Main.spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, Main.GameViewMatrix.ZoomMatrix);
            Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, null, Color.White, Projectile.rotation,
                texture.Size() / 2f, Projectile.scale, SpriteEffects.None, 0f);
            return false;
        }
        //private struct CustomVertexInfo : IVertexType
        //{
        //    private static VertexDeclaration _vertexDeclaration = new VertexDeclaration(new VertexElement[3]
        //    {
        //        new VertexElement(0, VertexElementFormat.Vector2, VertexElementUsage.Position, 0),
        //        new VertexElement(8, VertexElementFormat.Color, VertexElementUsage.Color, 0),
        //        new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.TextureCoordinate, 0)
        //    });
        //    public Vector2 Position;
        //    public Color Color;
        //    public Vector3 TexCoord;
        //    public CustomVertexInfo(Vector2 position, Color color, Vector3 texCoord)
        //    {
        //        Position = position;
        //        Color = color;
        //        TexCoord = texCoord;
        //    }

        //    public VertexDeclaration VertexDeclaration => _vertexDeclaration;
        //}
    }
}