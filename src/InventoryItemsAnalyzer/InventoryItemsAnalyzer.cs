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
        private InventoryItemIcon _currentHoverItem;
        private readonly string[] _nameAttrib = { "Intelligence", "Strength", "Dexterity" };

        public override void Initialise()
        {
        }

        public override void Render()
        {
            if (!GameController.Game.IngameState.IngameUi.InventoryPanel.IsVisible)
                return;

            _currentHoverItem = GameController.Game.IngameState.UIHover.AsObject<InventoryItemIcon>();

            if (_currentHoverItem.ToolTipType == ToolTipType.InventoryItem && _currentHoverItem.Item != null)
            {
                _curInventRoot = _currentHoverItem.Parent;
            }

            ScanForMaps();
        }

        private void ScanForMaps()
        {
            if (_curInventRoot == null)
                return;

            _goodItemsPos = new List<RectangleF>();

            foreach (var child in _curInventRoot.Children)
            {
                var item = child.AsObject<InventoryItemIcon>().Item;


                var modsComponent = item?.GetComponent<Mods>();
                if (modsComponent?.ItemRarity != ItemRarity.Rare || modsComponent.Identified == false)
                    continue;
                List<ItemMod> itemMods = modsComponent.ItemMods;
                List<ModValue> mods = itemMods.Select(it => new ModValue(it, GameController.Files, modsComponent.ItemLevel)).ToList();

                var drawRect = child.GetClientRect();
                //fix star position
                drawRect.X -= 5;
                drawRect.Y -= 5;

                BaseItemType bit = GameController.Files.BaseItemTypes.Translate(item.Path);

                switch (bit.ClassName)
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
                }
            }

            if (!Settings.HideUnderMouse)
                DrawStar(_goodItemsPos);
            else if (_currentHoverItem.Item == null)
                DrawStar(_goodItemsPos);
        }

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

        private bool AnalyzeBodyArmour(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BaLife)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BaEnergyShield)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.BaStrength)
                    affixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.BaIntelligence)
                    affixCounter++;
            }

            if (elemRes >= Settings.BaTotalRes)
                affixCounter++;

            return hpOrEs && affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeHelmet(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.HLife)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.HEnergyShield)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Accuracy" && mod.StatValue[0] >= Settings.HAccuracy)
                    affixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.HIntelligence)
                    affixCounter++;
            }

            if (elemRes >= Settings.HTotalRes)
                affixCounter++;

            return hpOrEs && affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeBoots(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;
            bool moveSpeed = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "MovementVelocity" && mod.StatValue[0] > 20)
                    moveSpeed = true;


                else if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BLife)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BEnergyShield)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.BStrength)
                    affixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.BIntelligence)
                    affixCounter++;
            }

            if (elemRes >= Settings.BTotalRes)
                affixCounter++;

            return moveSpeed && hpOrEs && affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeGloves(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.GLife)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.GEnergyShield)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.GAccuracy)
                    affixCounter++;

                else if (mod.Record.Group == "AttackSpeed" && mod.StatValue[0] >= Settings.GAttackSpeed)
                    affixCounter++;

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.GDexterity)
                    affixCounter++;
            }

            if (elemRes >= Settings.GTotalRes)
                affixCounter++;

            return hpOrEs && affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeShield(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.SLife)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.SEnergyShield)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.SStrength)
                    affixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.SIntelligence)
                    affixCounter++;

                else if (mod.Record.Group == "SpellDamage" && mod.StatValue[0] >= Settings.SSpellDamage)
                    affixCounter++;

                else if (mod.Record.Group == "SpellCriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.SSpellCritChance)
                    affixCounter++;
            }

            if (elemRes >= Settings.STotalRes)
                affixCounter++;

            return hpOrEs && affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeBelt(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BeLife)
                    hpOrEs = true;

                //проверка на -1тир, когда пояс на ЕС
                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BeEnergyShield)
                    hpOrEs = true;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.BeStrength)
                    affixCounter++;

                else if (mod.Record.Group == "IncreasedPhysicalDamageReductionRating" && mod.StatValue[0] >= Settings.BeArmour)
                    affixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.BeWeaponElemDamage)
                    affixCounter++;
            }

            if (elemRes >= Settings.BeTotalRes)
                affixCounter++;

            return hpOrEs && affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeRing(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            int totalAttrib = 0;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.RLife)
                    affixCounter++;

                //проверка на -1тир, когда кольцо на ЕС
                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.REnergyShield)
                    affixCounter++;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.RAccuracy)
                    affixCounter++;

                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.RPhysDamage)
                    affixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.RWeaponElemDamage)
                    affixCounter++;

                else if (mod.Record.Group == "ManaRegeneration" && mod.StatValue[0] >= Settings.RManaRegen)
                    affixCounter++;

                else if (mod.Record.Group == "ItemFoundRarityIncrease" && mod.StatValue[0] >= Settings.RIncRarity)
                    affixCounter++;

                else if (mod.Record.Group == "HybridStat")
                    totalAttrib += mod.StatValue[0] * 3;

                else if (_nameAttrib.Contains(mod.Record.Group))
                    totalAttrib += mod.StatValue[0];
            }

            if (elemRes >= Settings.RTotalRes)
                affixCounter++;

            if (totalAttrib >= Settings.RTotalAttrib)
                affixCounter++;

            return affixCounter >= Settings.AmountAffixes;
        }

        private bool AnalyzeAmulet(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            int totalAttrib = 0;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.ALife)
                    affixCounter++;

                else if (mod.Record.Group.Contains("EnergyShield"))
                {
                    var tier = mod.Tier > 0 ? mod.Tier : FixTierES(mod.Record.Key);
                    if (tier <= Settings.AEnergyShield)
                        affixCounter++;
                }

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.AAccuracy)
                    affixCounter++;

                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.APhysDamage)
                    affixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.AWeaponElemDamage)
                    affixCounter++;

                else if (mod.Record.Group == "ManaRegeneration" && mod.StatValue[0] >= Settings.AManaRegen)
                    affixCounter++;

                else if (mod.Record.Group == "ItemFoundRarityIncrease" && mod.StatValue[0] >= Settings.AIncRarity)
                    affixCounter++;

                else if (mod.Record.Group == "CriticalStrikeMultiplier" && mod.StatValue[0] >= Settings.ACritMult)
                    affixCounter++;

                else if (mod.Record.Group == "CriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.ACritChance)
                    affixCounter++;

                else if (mod.Record.Group == "SpellDamage" && mod.StatValue[0] >= Settings.ATotalElemSpellDmg)
                    affixCounter++;

                else if (mod.Record.Group == "AllAttributes")
                    totalAttrib += mod.StatValue[0] * 3;

                else if (mod.Record.Group == "HybridStat")
                    totalAttrib += mod.StatValue[0] * 2;

                else if (_nameAttrib.Contains(mod.Record.Group))
                    totalAttrib += mod.StatValue[0];
            }

            if (elemRes >= Settings.ATotalRes)
                affixCounter++;

            if (totalAttrib >= Settings.ATotalAttrib)
                affixCounter++;

            return affixCounter >= Settings.AmountAffixes;
        }

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

        private static int FixTierES(string key) => 9 - int.Parse(key.Last().ToString());
    }
}