﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;
using Terraria.GameInput;
using TerrafirmaRedux.Systems.MageClass;
using TerrafirmaRedux.Global.Structs;

namespace TerrafirmaRedux.Global
{
    public class TerrafirmaGlobalPlayer : ModPlayer
    {
        public bool Foregrip = false;
        public bool DrumMag = false;
        public bool AmmoCan = false;
        public bool CanUseAmmo = true;
        public bool BoxOfHighPiercingRounds = false;

        public bool SpringBoots = true;

        public Vector2 Momentum = Vector2.Zero;
        public int FloorTime = 0;
        public float JumpMultiplier = 1f;

        public bool SpellUI = false;
        internal int HeldMagicItem = 0;


        
        public override void ResetEffects()
        {
            Foregrip = false;
            DrumMag = false;
            AmmoCan = false;
            CanUseAmmo = true;
            BoxOfHighPiercingRounds = false;

            SpringBoots = false;
        }
        public override bool CanConsumeAmmo(Item weapon, Item ammo)
        {
            if (!CanUseAmmo)
            {
                return false;
            }

            if (ammo.ammo == AmmoID.Bullet && Main.rand.NextBool(2,3) && DrumMag)
            {
                return false;
            }
            return base.CanConsumeAmmo(weapon, ammo);

        }

        public override void PostUpdateRunSpeeds()
        {
            if (SpringBoots)
            {
                Player.runSlowdown = 0.2f;

                if (Player.velocity.Y != 0)
                {
                    FloorTime = 0;
                }
                else
                {
                    FloorTime++;
                }

                if (FloorTime > 10)
                {
                    JumpMultiplier = 1f;
                }

            }
        }

        public override void PreUpdateMovement()
        {
            
            if (SpringBoots)
            {
                Player.frogLegJumpBoost = true;

                if (Player.justJumped)
                {

                    if (JumpMultiplier > 1)
                    {
                        SoundStyle boing = new SoundStyle("TerrafirmaRedux/Sounds/Boing",SoundType.Sound);
                        boing.Volume = 0.8f;
                        boing.PitchRange = (-0.1f, 0.1f);
                        boing.Pitch -= JumpMultiplier / 10;

                        SoundEngine.PlaySound(boing, Player.position);
                    }

                    JumpMultiplier = MathHelper.Clamp(JumpMultiplier * 1.25f, 1f, 3f);
                }
            }
        }

        public override void PostUpdate()
        {
            if (SpellUI && Player.inventory[HeldMagicItem].type != 0) { ModContent.GetInstance<SpellUISystem>().Show(); }
            else { ModContent.GetInstance<SpellUISystem>().Hide(); }

        }

        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (triggersSet.MouseRight) {  
                
                if (ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(Player.inventory[Player.selectedItem].type))
                {
                    if (!SpellUI)
                    {
                        ModContent.GetInstance<SpellUISystem>().Create(Player.inventory[Player.selectedItem].type);
                        HeldMagicItem = Player.selectedItem;
                    }

                    if (HeldMagicItem != Player.selectedItem)
                    {
                        ModContent.GetInstance<SpellUISystem>().Create(Player.inventory[Player.selectedItem].type);
                        HeldMagicItem = Player.selectedItem;
                    }
                    ModContent.GetInstance<SpellUISystem>().UpdateMana(Player.manaCost);
                }
                SpellUI = true;
            }
            else
            { 
                SpellUI = false;
                if (ModContent.GetInstance<SpellUISystem>().SelectedSpell != -1 && Player.inventory[HeldMagicItem].type != 0 && Player.HasItemInAnyInventory(Player.inventory[Player.selectedItem].type) && ModContent.GetInstance<SpellIndex>().ItemCatalogue.ContainsKey(Player.inventory[HeldMagicItem].type) && ModContent.GetInstance<SpellUISystem>().Index <= ModContent.GetInstance<SpellIndex>().ItemCatalogue[Player.inventory[HeldMagicItem].type].Length)
                {

                    Player.inventory[HeldMagicItem].GetGlobalItem<GlobalItemInstanced>().Spell = ModContent.GetInstance<SpellIndex>().SpellCatalogue[ ModContent.GetInstance<SpellIndex>().ItemCatalogue[Player.inventory[HeldMagicItem].type][ ModContent.GetInstance<SpellUISystem>().Index ]].Item1;
                }
                ModContent.GetInstance<SpellUISystem>().Flush(); 
            }
        }

    }
}
