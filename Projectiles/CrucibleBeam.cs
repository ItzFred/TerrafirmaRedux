﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TerrafirmaRedux.Buffs;
using TerrafirmaRedux.Particles;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static log4net.Appender.ColoredConsoleAppender;

namespace TerrafirmaRedux.Projectiles
{
    internal class CrucibleBeam : ModProjectile
    {
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void SetDefaults()
        {
            Projectile.damage = 16;
            Projectile.Size = new Vector2(16);
            Projectile.aiStyle = -1;
            Projectile.timeLeft = 40;
            Projectile.friendly = true;
        }
        public override void AI()
        {
            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver4;

            if(Projectile.timeLeft % 10 == 0)
            {
                Projectile.velocity = Projectile.velocity.RotatedByRandom(0.2f);
                Projectile.netUpdate = true;
            }

            ParticleSystem.AddParticle(new HiResFlame(), Projectile.Center + Projectile.velocity, Projectile.velocity.RotatedByRandom(0.2f) * -0.1f, Utils.getAgnomalumFlameColor());
            if(Main.rand.NextBool(5))
            ParticleSystem.AddParticle(new ColorDot(), Projectile.Center + Projectile.velocity, Projectile.velocity.RotatedByRandom(0.2f) * Main.rand.NextFloat(-1,1), Utils.getAgnomalumFlameColor(), 0.2f);

        }
        public override void OnKill(int timeLeft)
        {
            Projectile.maxPenetrate = -1;
            Projectile.penetrate = -1;

            SoundEngine.PlaySound(SoundID.DD2_ExplosiveTrapExplode, Projectile.position);
            Projectile.Resize(200, 200);

            Projectile.Damage();

            for (int i = 0; i < 20; i++)
            {
                ParticleSystem.AddParticle(new HiResFlame(), Projectile.Center, Main.rand.NextVector2Circular(5,5), Utils.getAgnomalumFlameColor());
                if (Main.rand.NextBool())
                {
                    ParticleSystem.AddParticle(new ColorDot(), Projectile.Center, Main.rand.NextVector2Circular(10, 10), Utils.getAgnomalumFlameColor(),0.4f);
                }
            }
        }
        public override bool PreDraw(ref Color lightColor)
        {
            //Texture2D tex = TextureAssets.Projectile[Type].Value;

            //Main.EntitySpriteDraw(tex,Projectile.Center - Main.screenPosition,new Rectangle(0,0,tex.Width,tex.Height),Color.White,Projectile.rotation,new Vector2(tex.Width - 8,8),Projectile.scale,SpriteEffects.None);
            return false;
        }
    }
}