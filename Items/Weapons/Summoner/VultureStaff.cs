﻿using TerrafirmaRedux.Items.Weapons.Melee.Shortswords;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using TerrafirmaRedux.Projectiles.Summons;

namespace TerrafirmaRedux.Items.Weapons.Summoner
{
    internal class VultureStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Item.staff[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.damage = 14;
            Item.knockBack = 4f;
            Item.mana = 15;
            Item.DamageType = DamageClass.Summon;

            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 80;
            Item.useTime = 80;
            Item.UseSound = SoundID.Item8;

            Item.width = 38;
            Item.height = 46;

            Item.autoReuse = true;
            Item.noMelee = true;

            Item.rare = ItemRarityID.Blue;
            Item.value = Item.sellPrice(0, 0, 30, 0);

        }

        public override bool? UseItem(Player player)
        {
            Projectile.NewProjectile(default, Main.MouseWorld, Vector2.Zero, ModContent.ProjectileType<SummonedVulture>(), 14, 0, -1, 0, 0, 0);
            return true;
        }
    }
}
