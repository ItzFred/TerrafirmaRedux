﻿using TerrafirmaRedux.Items.Weapons.Melee.Shortswords;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Projectiles.Summons;

namespace TerrafirmaRedux.Items.Weapons.Summoner
{
    internal class ZombiemancyStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 8;
            Item.knockBack = 4f;

            Item.useAnimation = 80;
            Item.useTime = 80;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.UseSound = SoundID.Item8;

            Item.crit = 15;
            Item.width = 40;
            Item.height = 46;
            Item.DamageType = DamageClass.Summon;

            Item.autoReuse = true;
            Item.noMelee = true;
            Item.mana = 10;

            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(0, 0, 30, 0);

        }

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(default, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<ZombiemancedZombie>(), 14, 0, -1, 0, 0, 0);
            return true;
        }

    }
}
