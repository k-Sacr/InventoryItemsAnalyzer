using System.Collections.Generic;
using System.Linq;
using PoeHUD.Plugins;
using PoeHUD.Poe;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Poe.Elements;
using PoeHUD.Models;
using PoeHUD.Poe.Components;
using PoeHUD.Hud.AdvancedTooltip;
using PoeHUD.Models.Enums;
using SharpDX;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzer : BaseSettingsPlugin<InventoryItemsAnalyzerSettings>
    {
        private List<RectangleF> _goodItemsPos;
        private Element _curInventRoot;
        private HoverItemIcon _currentHoverItem;
        private readonly string[] _nameAttrib = {"Intelligence", "Strength", "Dexterity"};
        private readonly string[] _incElemDmg =
            {"FireDamagePercentage", "ColdDamagePercentage", "LightningDamagePercentage"};
        public InventoryItemsAnalyzer() { PluginName = "INV Item Analyzer"; }
        public override void Render()
        {
          if (!GameController.Game.IngameState.IngameUi.InventoryPanel.IsVisible)
                return;

            _currentHoverItem = GameController.Game.IngameState.UIHover.AsObject<HoverItemIcon>();

            if (_currentHoverItem.ToolTipType == ToolTipType.InventoryItem && _currentHoverItem.Item != null)
            {
                _curInventRoot = _currentHoverItem.Parent;
            }
            ScanInventory();
        }

        #region Scan Inventory
        private void ScanInventory()
        {
            if (_curInventRoot == null)
                return;

            _goodItemsPos = new List<RectangleF>();

            foreach (var child in _curInventRoot.Children)
            {
                var item = child.AsObject<NormalInventoryItem>().Item;
                if (item == null)
                    continue;

                var modsComponent = item?.GetComponent<Mods>();
                if (modsComponent?.ItemRarity != ItemRarity.Rare || modsComponent.Identified == false || string.IsNullOrEmpty(item.Path))
                    continue;
                List<ItemMod> itemMods = modsComponent.ItemMods;
                List<ModValue> mods =
                    itemMods.Select(
                        it => new ModValue(it, GameController.Files, modsComponent.ItemLevel, GameController.Files.BaseItemTypes.Translate(item.Path))).ToList();

                var drawRect = child.GetClientRect();
                //fix star position
                drawRect.X -= 5;
                drawRect.Y -= 5;

                BaseItemType bit = GameController.Files.BaseItemTypes.Translate(item.Path);

                switch (bit?.ClassName)
                {
                    case "Body Armour":
                        if (AnalyzeBodyArmour(mods) && Settings.BodyArmour)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Helmet":
                        if (AnalyzeHelmet(mods) && Settings.Helmet)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Boots":
                        if (AnalyzeBoots(mods) && Settings.Boots)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Gloves":
                        if (AnalyzeGloves(mods) && Settings.Gloves)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Shield":
                        if (AnalyzeShield(mods) && Settings.Shield)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Belt":
                        if (AnalyzeBelt(mods) && Settings.Belt)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Ring":
                        if (AnalyzeRing(mods) && Settings.Ring)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Amulet":
                        if (AnalyzeAmulet(mods) && Settings.Amulet)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Dagger":
                        if (AnalyzeWeaponCaster(mods) && Settings.WeaponCaster)
                            _goodItemsPos.Add(drawRect);
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;

                    case "Wand":
                        if (AnalyzeWeaponCaster(mods) && Settings.WeaponCaster)
                            _goodItemsPos.Add(drawRect);
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Sceptre":
                        if (AnalyzeWeaponCaster(mods) && Settings.WeaponCaster)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Thrusting One Hand Sword":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;               
                    case "Staff":
                        if (AnalyzeWeaponCaster(mods) && Settings.WeaponCaster)
                            _goodItemsPos.Add(drawRect);
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Claw":
                        if (AnalyzeWeaponCaster(mods) && Settings.WeaponCaster)
                            _goodItemsPos.Add(drawRect);
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "One Hand Sword":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Two Hand Sword":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "One Hand Axe":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Two Hand Axe":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "One Hand Mace":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Two Hand Mace":
                        if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                    case "Bow":
                        if (AnalyzeWeaponCaster(mods) && Settings.WeaponCaster)
                            _goodItemsPos.Add(drawRect);
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        break;
                }
            }

            if (!Settings.HideUnderMouse)
                DrawStar(_goodItemsPos);
            else if (_currentHoverItem.Item == null)
                DrawStar(_goodItemsPos);
        }
        #endregion
        #region Draw Star/Rec
        private void DrawStar(List<RectangleF> goodItems)
        {
            foreach (var position in goodItems)
                if (Settings.StarOrBorder)
                    Graphics.DrawText(" \u2605 ", 30, position.TopLeft, Settings.Color);
                else
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                    Graphics.DrawFrame(border, 3, Settings.Color);
                }
        } 
        #endregion
        
        #region Body Armour
        private bool AnalyzeBodyArmour(List<ModValue> mods)
        {
            int BaaffixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BaLife)
                {
                    hpOrEs = true;
                    BaaffixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BaEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    BaaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.BaStrength)
                    BaaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.BaIntelligence)
                    BaaffixCounter++;

            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.BaTotalRes)
                BaaffixCounter++;

            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + BaaffixCounter, 10f);
            }
            return BaaffixCounter >= Settings.BaAffixes;
        } 
        #endregion
        #region Helmets
        private bool AnalyzeHelmet(List<ModValue> mods)
        {
            int HaffixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.HLife)
                {
                    hpOrEs = true;
                    HaffixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.HEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    HaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.HAccuracy)
                    HaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.HIntelligence)
                    HaffixCounter++;
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }

            if (elemRes >= Settings.HTotalRes)
                HaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + HaffixCounter, 10f);
            }
            return HaffixCounter >= Settings.HAffixes;
        }
        #endregion
        #region Gloves
        private bool AnalyzeGloves(List<ModValue> mods)
        {
            int GaffixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.GLife)
                {
                    hpOrEs = true;
                    GaffixCounter++;
                    }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.GEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    GaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.GAccuracy)
                    GaffixCounter++;

                else if (mod.Record.Group == "IncreasedAttackSpeed" && mod.StatValue[0] >= Settings.GAttackSpeed)
                    GaffixCounter++;

                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.GPhysDamage)
                    GaffixCounter++;

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.GDexterity)
                    GaffixCounter++;
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.GTotalRes)
                GaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + GaffixCounter, 10f);
            }
            return GaffixCounter >= Settings.GAffixes;
        }
        #endregion
        #region Boots
        private bool AnalyzeBoots(List<ModValue> mods)
        {
            int BaffixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;
            bool moveSpeed = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "MovementVelocity" && mod.StatValue[0] >= Settings.BMoveSpeed)
                { 
                    moveSpeed = true;
                    BaffixCounter++;
                }
            
                else if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BLife)
                {
                    hpOrEs = true;
                    BaffixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    BaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.BStrength)
                    BaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.BIntelligence)
                    BaffixCounter++;
                
                //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.BTotalRes)
                BaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + BaffixCounter, 10f);
            }
            return BaffixCounter >= Settings.BAffixes; 
           }
        #endregion
        #region Belts
        private bool AnalyzeBelt(List<ModValue> mods)
        {
            int BeaffixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BeLife)
                {
                    hpOrEs = true;
                    BeaffixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BeEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    BeaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.BeStrength)
                    BeaffixCounter++;

                else if (mod.Record.Group == "IncreasedPhysicalDamageReductionRating" && mod.StatValue[0] >= Settings.BeArmour)
                    BeaffixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.BeWeaponElemDamage)
                    BeaffixCounter++;
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.BeTotalRes)
                BeaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + BeaffixCounter, 10f);
            }
            return BeaffixCounter >= Settings.BeAffixes;
        }
        #endregion
        #region Rings
        private bool AnalyzeRing(List<ModValue> mods)
        {

            int RaffixCounter = 0;
            int elemRes = 0;
            int totalAttrib = 0;

            foreach (var mod in SumAffix(mods))
            {
                
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.RLife)
                    RaffixCounter++;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.REnergyShield &&
                         mod.Tier > 0)
                    RaffixCounter++;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if
                (mod.Record.Group == "IncreasedAttackSpeed" && mod.StatValue[0] >= Settings.RAttackSpeed)
                    RaffixCounter++;
             
            else if (mod.Record.Group == "IncreasedCastSpeed" && mod.StatValue[0] >= Settings.RCastSpped)
                    RaffixCounter++;

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.RAccuracy)
                    RaffixCounter++;

                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.RPhysDamage)
                    RaffixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.RWeaponElemDamage)
                    RaffixCounter++;

                else if (mod.Record.Group == "ManaRegeneration" && mod.StatValue[0] >= Settings.RManaRegen)
                    RaffixCounter++;

                else if (mod.Record.Group == "ItemFoundRarityIncrease" && mod.StatValue[0] >= Settings.RIncRarity)
                    RaffixCounter++;

                else if (mod.Record.Group == "HybridStat")
                    totalAttrib += mod.StatValue[0] * 3;

                else if (_nameAttrib.Contains(mod.Record.Group))
                    totalAttrib += mod.StatValue[0];
                
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                    LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.RTotalRes)
                RaffixCounter++;

            if (totalAttrib >= Settings.RTotalAttrib)
                RaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + RaffixCounter, 10f);
            }

            return RaffixCounter >= Settings.RAffixes;
        }
        #endregion
        #region Amulet
        private bool AnalyzeAmulet(List<ModValue> mods)
        {
            int AaffixCounter = 0;
            int elemRes = 0;
            int totalAttrib = 0;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.ALife)
                    AaffixCounter++;

                else if (mod.Record.Group.Contains("EnergyShield"))
                {
                    var tier = mod.Tier > 0 ? mod.Tier : FixTierEs(mod.Record.Key);
                    if (tier <= Settings.AEnergyShield)
                        AaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.AAccuracy)
                    AaffixCounter++;
                
                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.APhysDamage)
                    AaffixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.AWeaponElemDamage)
                    AaffixCounter++;

                else if (mod.Record.Group == "ManaRegeneration" && mod.StatValue[0] >= Settings.AManaRegen)
                    AaffixCounter++;

                else if (mod.Record.Group == "ItemFoundRarityIncrease" && mod.StatValue[0] >= Settings.AIncRarity)
                    AaffixCounter++;

                else if (mod.Record.Group == "CriticalStrikeMultiplier" && mod.StatValue[0] >= Settings.ACritMult)
                    AaffixCounter++;

                else if (mod.Record.Group == "CriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.ACritChance)
                    AaffixCounter++;

                else if (mod.Record.Group == "SpellDamage" && mod.StatValue[0] >= Settings.ATotalElemSpellDmg)
                    AaffixCounter++;

                else if (mod.Record.Group == "AllAttributes")
                    totalAttrib += mod.StatValue[0] * 3;

                else if (mod.Record.Group == "HybridStat")
                    totalAttrib += mod.StatValue[0] * 2;

                else if (_nameAttrib.Contains(mod.Record.Group))
                    totalAttrib += mod.StatValue[0];
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.ATotalRes)
                AaffixCounter++;

            if (totalAttrib >= Settings.ATotalAttrib)
                AaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + AaffixCounter, 10f);
            }
            return AaffixCounter >= Settings.AAffixes;
        }
        #endregion
        #region Shields
        private bool AnalyzeShield(List<ModValue> mods)
        {
            int SaffixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.SLife)
                {
                    hpOrEs = true;
                    SaffixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.SEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    SaffixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.SStrength)
                    SaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.SIntelligence)
                    SaffixCounter++;

                else if (mod.Record.Group == "SpellDamage" && mod.StatValue[0] >= Settings.SSpellDamage)
                    SaffixCounter++;

                else if (mod.Record.Group == "SpellCriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.SSpellCritChance)
                    SaffixCounter++;
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.STotalRes)
                SaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + SaffixCounter, 10f);
            }
            return SaffixCounter >= Settings.SAffixes;
        }
        #endregion
        #region Weapon Caster
        private bool AnalyzeWeaponCaster(List<ModValue> mods)
        {
            int WcaffixCounter = 0;
            int totalSpellDamage = 0;
            int addElemDamage = 0;


            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "SpellCriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.WcSpellCritChance)
                    WcaffixCounter++;

                else if (mod.Record.Group.Contains("SpellDamage"))
                    totalSpellDamage += mod.StatValue[0];

                else if (_incElemDmg.Contains(mod.Record.Group))
                    totalSpellDamage += mod.StatValue[0];

                else if (mod.Record.Group.Contains("SpellAddedElementalDamage"))
                    addElemDamage += Average(mod.StatValue);

                else if (mod.Record.Group == "SpellCriticalStrikeMultiplier" && mod.StatValue[0] >= Settings.WcCritMult)
                    WcaffixCounter++;
            //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }

            if (totalSpellDamage >= Settings.WcTotalElemSpellDmg)
                WcaffixCounter++;

            if (addElemDamage >= Settings.WcToElemDamageSpell)
                WcaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + WcaffixCounter, 10f);
            }
            return WcaffixCounter >= Settings.WcAffixes;
        } 
        #endregion
        #region Weapon Attack
        private bool AnalyzeWeaponAttack(Entity item)
        {
            int WaaffixCounter = 0;

            var component = item.GetComponent<Weapon>();
            var mods = item.GetComponent<Mods>().ItemMods;

            float phyDmg = (component.DamageMin + component.DamageMax) / 2f + mods.GetAverageStatValue("LocalAddedPhysicalDamage");
            phyDmg *= 1f + (mods.GetStatValue("LocalIncreasedPhysicalDamagePercent") + 20) / 100f;
            if (phyDmg >= Settings.WaPhysDmg)
                WaaffixCounter++;

            float elemDmg = mods.GetAverageStatValue("LocalAddedColdDamage") + mods.GetAverageStatValue("LocalAddedFireDamage")
                            + mods.GetAverageStatValue("LocalAddedLightningDamage");
            if (elemDmg >= Settings.WaElemDmg)
                WaaffixCounter++;

            float attackSpeed = 1f / (component.AttackTime / 1000f);
            attackSpeed *= 1f + mods.GetStatValue("LocalIncreasedAttackSpeed") / 100f;
            var dps = (phyDmg + elemDmg) * attackSpeed;
            if (dps >= Settings.WaDps)
                WaaffixCounter++;

            float critChance = component.CritChance / 100f;
            critChance *= 1f + mods.GetStatValue("LocalCriticalStrikeChance") / 100f;
            if (critChance >= Settings.WaCritChance)
                WaaffixCounter++;

            var critMulti = 1f + mods.GetStatValue("LocalCriticalMultiplier") / 100f;
            if (critMulti >= Settings.WaCritMulti)
                WaaffixCounter++;
            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                {
                    LogMessage(component, 10f);
                    LogMessage("# of Affixes:" + WaaffixCounter, 10f);
                }
            }
            return WaaffixCounter >= Settings.WaAffixes;
        }
        #endregion
        
        #region Sum Affix
        private static int Average(IReadOnlyList<int> x) => (x[0] + x[1]) / 2;

        private static List<ModValue> SumAffix(List<ModValue> mods)
        {
            foreach (var mod in mods)
                foreach (var mod2 in mods.Where(x => x != mod && mod.Record.Group == x.Record.Group))
                {
                    mod2.StatValue[0] += mod.StatValue[0];
                    mod2.StatValue[1] += mod.StatValue[1];
                    mods.Remove(mod);
                    return mods;
                }
            return mods;
        }

        private static int FixTierEs(string key) => 9 - int.Parse(key.Last().ToString());
    }
    #endregion
        #region Get item Stats
    public static class ModsExtension
    {
        public static float GetStatValue(this List<ItemMod> mods, string name)
        {
            var m = mods.FirstOrDefault(mod => mod.Name == name);
            return m?.Value1 ?? 0;
        } 
        public static float GetAverageStatValue(this List<ItemMod> mods, string name)
        {
            var m = mods.FirstOrDefault(mod => mod.Name == name);
            return (m?.Value1 + m?.Value2) / 2 ?? 0;
        }
    }
    #endregion
    }