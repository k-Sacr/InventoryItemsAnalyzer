using System.Collections.Generic;
using System.Linq;
using PoeHUD.Plugins;
using PoeHUD.Poe;
using PoeHUD.Poe.RemoteMemoryObjects;
using PoeHUD.Poe.Elements;
using PoeHUD.Models;
using PoeHUD.Hud.Menu;
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
        #region Plugin Menu
        public override void InitialiseMenu(MenuItem mainMenu)
        {
            base.InitialiseMenu(mainMenu);
            MenuItem parent;
            MenuItem tmpNode;
            MenuItem tmpNodeInner;
            {
                #region Body Armour Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Body Armour",
                    Settings.BodyArmour);
                parent.TooltipText = "Body Armour Filter Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.BaLife);
                    tmpNode.TooltipText = "Set the minimum value to filter life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Shield (tier)",
                        Settings.BaLife);
                    tmpNode.TooltipText = "Set the minimum value to filter Energy Shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resistance",
                        Settings.BaTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Strength",
                        Settings.BaStrength);
                    tmpNode.TooltipText = "Set the minimum value to filter Strength value";

                    tmpNode = MenuPlugin.AddChild(parent, "Intelligence",
                        Settings.BaIntelligence);
                    tmpNode.TooltipText = "Set the minimum value to filter Intelligence value";
                }
                #endregion
                #region Helmet Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Helemts",
                    Settings.Helmet);
                parent.TooltipText = "Helmet Filter Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.HLife);
                    tmpNode.TooltipText = "Set the minimum value to filter Life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Sheild",
                        Settings.HEnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter Energy Shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resistance",
                        Settings.HTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter total tesistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Accuracy",
                        Settings.HAccuracy);
                    tmpNode.TooltipText = "Set the minimum value to filter total accuracy value";

                    tmpNode = MenuPlugin.AddChild(parent, "Intellligence",
                        Settings.HTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter Intelligence value";

                }
                #endregion
                #region Gloves Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Gloves",
                    Settings.Gloves);
                parent.TooltipText = "Glove Filter Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.GLife);
                    tmpNode.TooltipText = "Set the minimum value to filter for life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resistance",
                        Settings.GTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter for total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Sheild",
                        Settings.GEnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter for energy shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Accuracy",
                        Settings.GAccuracy);
                    tmpNode.TooltipText = "Set the minimum value to filter for accuracy value";

                    tmpNode = MenuPlugin.AddChild(parent, "Attack Speed",
                        Settings.GAttackSpeed);
                    tmpNode.TooltipText = "Set the minimum value to filter for attack speed value";

                    tmpNode = MenuPlugin.AddChild(parent, "Dexterity",
                        Settings.GDexterity);
                    tmpNode.TooltipText = "Set the minimum value to filter for dexterity value";

                }
                #endregion
                #region Boots Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Boots",
                    Settings.Boots);
                parent.TooltipText = "Boot Filter Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.BLife);
                    tmpNode.TooltipText = "Set the minimum value to filter for life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Sheild",
                        Settings.BEnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter for energy shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resistance",
                        Settings.BTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter for total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Strength",
                        Settings.BStrength);
                    tmpNode.TooltipText = "Set the minimum value to filter for strength value";

                    tmpNode = MenuPlugin.AddChild(parent, "Intelligence",
                        Settings.BIntelligence);
                    tmpNode.TooltipText = "Set the minimum value to filter for intelligince value";

                    tmpNode = MenuPlugin.AddChild(parent, "Dexterity",
                        Settings.GDexterity);
                    tmpNode.TooltipText = "Set the minimum value to filter for dexterity value";
                }
                #endregion
                #region Belts Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Belts",
                    Settings.Belt);
                parent.TooltipText = "Belt Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.BeLife);
                    tmpNode.TooltipText = "Set the minimum value to filter for life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Sheild",
                        Settings.BeEnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter for energy shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resistance",
                        Settings.BeTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter for total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Strength",
                        Settings.BeStrength);
                    tmpNode.TooltipText = "Set the minimum value to filter for strength value";

                    tmpNode = MenuPlugin.AddChild(parent, "Armour",
                        Settings.BeArmour);
                    tmpNode.TooltipText = "Set the minimum value to filter for armour value";

                    tmpNode = MenuPlugin.AddChild(parent, "Weapon Elemental Damage",
                        Settings.BeWeaponElemDamage);
                    tmpNode.TooltipText = "Set the minimum value to filter for weapon elemental damage value";
                }
                #endregion
                #region Rings Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Rings",
                    Settings.Ring);
                parent.TooltipText = "Ring Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.RLife);
                    tmpNode.TooltipText = "Set the minimum value to filter for life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Shield",
                        Settings.REnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter for energy shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resitance",
                        Settings.RTotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter for total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Attributes",
                        Settings.RTotalAttrib);
                    tmpNode.TooltipText = "Set the minimum value to filter for total attributes value";

                    tmpNode = MenuPlugin.AddChild(parent, "Physical Damage to Attacks",
                        Settings.RPhysDamage);
                    tmpNode.TooltipText = "Set the minimum value to filter for physical damage to attacks value";

                    tmpNode = MenuPlugin.AddChild(parent, "Weapon Elemental Damage",
                        Settings.RWeaponElemDamage);
                    tmpNode.TooltipText = "Set the minimum value to filter for weapon elemental damage value";

                    tmpNode = MenuPlugin.AddChild(parent, "Accuracy Rating",
                        Settings.RAccuracy);
                    tmpNode.TooltipText = "Set the minimum value to filter for accuracy rating value";

                    tmpNode = MenuPlugin.AddChild(parent, "Mana Regeneration",
                        Settings.RManaRegen);
                    tmpNode.TooltipText = "Set the minimum value to filter for mana regeneration value";

                    tmpNode = MenuPlugin.AddChild(parent, "Increased Rarity",
                        Settings.RIncRarity);
                    tmpNode.TooltipText = "Set the minimum value to filter for increased rarity value";

                    tmpNode = MenuPlugin.AddChild(parent, "Attack Speed",
                        Settings.RAttackSpeed);
                    tmpNode.TooltipText = "Set the minimum value to filter for increased attack speed value";

                    tmpNode = MenuPlugin.AddChild(parent, "Cast Speed",
                        Settings.RCastSpped);
                    tmpNode.TooltipText = "Set the minimum value to filter for increased cast speed value";
                }

                #endregion
                #region Amulets Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Amulets",
                    Settings.Amulet);
                parent.TooltipText = "Amulet Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.ALife);
                    tmpNode.TooltipText = "Set the minimum value to filter for life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Shield",
                        Settings.AEnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter for energy shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resitance",
                        Settings.ATotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter for total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Attributes",
                        Settings.ATotalAttrib);
                    tmpNode.TooltipText = "Set the minimum value to filter for total attributes value";

                    tmpNode = MenuPlugin.AddChild(parent, "Physical Damage to Attacks",
                        Settings.APhysDamage);
                    tmpNode.TooltipText = "Set the minimum value to filter for physical damage to attacks value";

                    tmpNode = MenuPlugin.AddChild(parent, "Weapon Elemental Damage",
                        Settings.AWeaponElemDamage);
                    tmpNode.TooltipText = "Set the minimum value to filter for weapon elemental damage value";

                    tmpNode = MenuPlugin.AddChild(parent, "Accuracy Rating",
                        Settings.AAccuracy);
                    tmpNode.TooltipText = "Set the minimum value to filter for accuracy rating value";

                    tmpNode = MenuPlugin.AddChild(parent, "Mana Regeneration",
                        Settings.AManaRegen);
                    tmpNode.TooltipText = "Set the minimum value to filter for mana regeneration value";

                    tmpNode = MenuPlugin.AddChild(parent, "Increased Rarity",
                        Settings.AIncRarity);
                    tmpNode.TooltipText = "Set the minimum value to filter for increased rarity value";

                    tmpNode = MenuPlugin.AddChild(parent, "Critical Strike Multiplier",
                        Settings.ACritMult);
                    tmpNode.TooltipText = "Set the minimum value to filter for critical strike multiplier value";

                    tmpNode = MenuPlugin.AddChild(parent, "Critical Strike Chance",
                        Settings.ACritChance);
                    tmpNode.TooltipText = "Set the minimum value to filter for critical strike chance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Elemental Spell Damage",
                        Settings.ATotalElemSpellDmg);
                    tmpNode.TooltipText = "Set the minimum value to filter for total elemental spell Damage value";
                }
                #endregion
                #region Sheilds Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Sheilds",
                    Settings.Shield);
                parent.TooltipText = "Sheild Filter Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Life",
                        Settings.Shield);
                    tmpNode.TooltipText = "Set the minimum value to filter for life value";

                    tmpNode = MenuPlugin.AddChild(parent, "Energy Sheild",
                        Settings.SEnergyShield);
                    tmpNode.TooltipText = "Set the minimum value to filter for energy shield value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Resistance",
                        Settings.STotalRes);
                    tmpNode.TooltipText = "Set the minimum value to filter for total resistance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Strength",
                        Settings.SStrength);
                    tmpNode.TooltipText = "Set the minimum value to filter for strength value";

                    tmpNode = MenuPlugin.AddChild(parent, "Intelligence",
                        Settings.SIntelligence);
                    tmpNode.TooltipText = "Set the minimum value to filter for intelligence value";

                    tmpNode = MenuPlugin.AddChild(parent, "Spell Damage",
                        Settings.SSpellDamage);
                    tmpNode.TooltipText = "Set the minimum value to filter for spell damage value";

                    tmpNode = MenuPlugin.AddChild(parent, "Spell Critical Strike Chance",
                        Settings.SSpellCritChance);
                    tmpNode.TooltipText = "Set the minimum value to filter for spell critical strike chance value";
                }
                #endregion
                #region Caster Dagger/Wand/Sceptre Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Cast Weapons",
                    Settings.WeaponCaster);
                parent.TooltipText = "Cast Weapon Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Total Elemental Spell Damage",
                        Settings.WcTotalElemSpellDmg);
                    tmpNode.TooltipText = "Set the minimum value to filter for total elemental spell damage value";

                    tmpNode = MenuPlugin.AddChild(parent, "Fire/Cold/Lightning Dmg to Spells",
                        Settings.WcToElemDamageSpell);
                    tmpNode.TooltipText = "Set the minimum value to filter for % added elemental damage to spells value";

                    tmpNode = MenuPlugin.AddChild(parent, "Spell Critical Chance",
                        Settings.WcSpellCritChance);
                    tmpNode.TooltipText = "Set the minimum value to filter for spell critical chance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Critical Strike Multiplier",
                        Settings.WcCritMult);
                    tmpNode.TooltipText = "Set the minimum value to filter for critical strike multiplier value";
                }
                #endregion
                #region Attack Thrusting/Dagger/Wand Menu
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Attack Weapons",
                    Settings.WeaponAttack);
                parent.TooltipText = "Attack Weapon Settings";
                {
                    tmpNode = MenuPlugin.AddChild(parent, "Physical Damage",
                        Settings.WaPhysDmg);
                    tmpNode.TooltipText = "Set the minimum value to filter for total physical damage value";

                    tmpNode = MenuPlugin.AddChild(parent, "Total Elemental Spell Damage",
                        Settings.WaElemDmg);
                    tmpNode.TooltipText = "Set the minimum value to filter for total elemental spell damage value";

                    tmpNode = MenuPlugin.AddChild(parent, "DPS",
                        Settings.WaDps);
                    tmpNode.TooltipText = "Set the minimum value to filter for total weapon DPS value";

                    tmpNode = MenuPlugin.AddChild(parent, "Spell Critical Chance",
                        Settings.WaCritChance);
                    tmpNode.TooltipText = "Set the minimum value to filter for attack critical chance value";

                    tmpNode = MenuPlugin.AddChild(parent, "Critical Strike Multiplier",
                        Settings.WaCritMulti);
                    tmpNode.TooltipText = "Set the minimum value to filter for  attack critical strike multiplier value";
                }
                #endregion
                #region Additional Menu Settings
                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Required sum affix:",
                    Settings.AmountAffixes);
                parent.TooltipText = "Set the minimum number of affxies an item must have";

                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Hide under Mouse",
                    Settings.HideUnderMouse);
                parent.TooltipText = "Set the minimum number of affxies an item must have";

                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Star or Border",
                    Settings.StarOrBorder);
                parent.TooltipText = "Display a star on or border around the filtered item";

                parent = MenuPlugin.AddChild(PluginSettingsRootMenu, "Star/Border Color",
                    Settings.Color);
                parent.TooltipText = "Display color of star on or border around the filtered item";

                #endregion
            }
        }
        #endregion
        #region Scan Inventory
        private void ScanInventory()
        {
            if (_curInventRoot == null)
                return;

            _goodItemsPos = new List<RectangleF>();

            foreach (var child in _curInventRoot.Children)
            {
                var item = child.AsObject<NormalInventoryItem>().Item;


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
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BaLife)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BaEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

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
        #endregion
        #region Helmets
        private bool AnalyzeHelmet(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.HLife)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.HEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

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
        #endregion
        #region Gloves
        private bool AnalyzeGloves(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.GLife)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.GEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

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
        #endregion
        #region Boots
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
                {
                    hpOrEs = true;
                    affixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

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
        #endregion
        #region Belts
        private bool AnalyzeBelt(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BeLife)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BeEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

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
        #endregion
        #region Rings
        private bool AnalyzeRing(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            int totalAttrib = 0;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.RLife)
                    affixCounter++;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.REnergyShield && mod.Tier > 0)
                    affixCounter++;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "AttackSpeed" && mod.StatValue[0] >= Settings.RAttackSpeed)
                    affixCounter++;

                else if (mod.Record.Group == "CastSpeed" && mod.StatValue[0] >= Settings.RCastSpped)
                    affixCounter++;

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
        #endregion
        #region Amulet
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
                    var tier = mod.Tier > 0 ? mod.Tier : FixTierEs(mod.Record.Key);
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
        #endregion
        #region Shields
        private bool AnalyzeShield(List<ModValue> mods)
        {
            int affixCounter = 0;
            int elemRes = 0;
            bool hpOrEs = false;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.SLife)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.SEnergyShield && mod.Tier > 0)
                {
                    hpOrEs = true;
                    affixCounter++;
                }

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
        #endregion
        #region Weapon Caster
        private bool AnalyzeWeaponCaster(List<ModValue> mods)
        {
            int affixCounter = 0;
            int totalSpellDamage = 0;
            int addElemDamage = 0;


            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "SpellCriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.WcSpellCritChance)
                    affixCounter++;

                else if (mod.Record.Group.Contains("SpellDamage"))
                    totalSpellDamage += mod.StatValue[0];

                else if (_incElemDmg.Contains(mod.Record.Group))
                    totalSpellDamage += mod.StatValue[0];

                else if (mod.Record.Group.Contains("SpellAddedElementalDamage"))
                    addElemDamage += Average(mod.StatValue);

                else if (mod.Record.Group == "SpellCriticalStrikeMultiplier" && mod.StatValue[0] >= Settings.WcCritMult)
                    affixCounter++;
            }

            if (totalSpellDamage >= Settings.WcTotalElemSpellDmg)
                affixCounter++;

            if (addElemDamage >= Settings.WcToElemDamageSpell)
                affixCounter++;

            return affixCounter >= Settings.AmountAffixes;
        } 
        #endregion
        #region Weapon Attack
        private bool AnalyzeWeaponAttack(Entity item)
        {
            int affixCounter = 1;

            var component = item.GetComponent<Weapon>();
            var mods = item.GetComponent<Mods>().ItemMods;

            float phyDmg = (component.DamageMin + component.DamageMax) / 2f + mods.GetAverageStatValue("LocalAddedPhysicalDamage");
            phyDmg *= 1f + (mods.GetStatValue("LocalIncreasedPhysicalDamagePercent") + 20) / 100f;
            if (phyDmg >= Settings.WaPhysDmg)
                affixCounter++;

            float elemDmg = mods.GetAverageStatValue("LocalAddedColdDamage") + mods.GetAverageStatValue("LocalAddedFireDamage")
                            + mods.GetAverageStatValue("LocalAddedLightningDamage");
            if (elemDmg >= Settings.WaElemDmg)
                affixCounter++;

            float attackSpeed = 1f / (component.AttackTime / 1000f);
            attackSpeed *= 1f + mods.GetStatValue("LocalIncreasedAttackSpeed") / 100f;
            var dps = (phyDmg + elemDmg) * attackSpeed;
            if (dps >= Settings.WaDps)
                affixCounter++;

            float critChance = component.CritChance / 100f;
            critChance *= 1f + mods.GetStatValue("LocalCriticalStrikeChance") / 100f;
            if (critChance >= Settings.WaCritChance)
                affixCounter++;

            var critMulti = 1f + mods.GetStatValue("LocalCriticalMultiplier") / 100f;
            if (critMulti >= Settings.WaCritMulti)
                affixCounter++;

            return affixCounter >= Settings.AmountAffixes;
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