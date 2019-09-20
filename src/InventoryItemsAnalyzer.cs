using System.Collections.Generic;
using System.Linq;
using ExileCore;
using ExileCore.PoEMemory;
using ExileCore.PoEMemory.Components;
using ExileCore.PoEMemory.MemoryObjects;
using ExileCore.PoEMemory.Elements;
using ExileCore.Shared.Enums;
using ExileCore.PoEMemory.Models;
using AdvancedTooltip;
using ExileCore.PoEMemory.Elements.InventoryElements;
using SharpDX;
using System.Threading;
using System.Windows.Forms;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzer : BaseSettingsPlugin<InventoryItemsAnalyzerSettings>
    {
        private List<RectangleF> _goodItemsPos;
        private List<RectangleF> _allItemsPos;
        private List<RectangleF> _MBItems;
        private List<RectangleF> _VeilItems;
        private Element _curInventRoot;
        private HoverItemIcon _currentHoverItem;
        private Vector2 _windowOffset;
        private readonly string[] _nameAttrib = {"Intelligence", "Strength", "Dexterity"};
        private readonly string[] _incElemDmg =
            {"FireDamagePercentage", "ColdDamagePercentage", "LightningDamagePercentage"};
        public InventoryItemsAnalyzer() {  }

        public override bool Initialise()
        {
            base.Initialise();

            Name = "INV Item Analyzer";
            return true;
        }

        public override void Render()
        {
          if (!GameController.Game.IngameState.IngameUi.InventoryPanel.IsVisible || GameController.Game.IngameState.IngameUi.OpenLeftPanel.IsVisible == true)
            return;

            _windowOffset = GameController.Window.GetWindowRectangle().TopLeft;
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
            _allItemsPos = new List<RectangleF>();
            _MBItems = new List<RectangleF>();
            _VeilItems = new List<RectangleF>();

            foreach (var child in _curInventRoot.Children)
            {
                bool f = false;
                var item = child.AsObject<NormalInventoryItem>().Item;
                if (item == null)
                    continue;

                var modsComponent = item?.GetComponent<Mods>();
                var baseComponent = item?.GetComponent<Base>();
                if (modsComponent?.ItemRarity != ItemRarity.Rare || modsComponent.Identified == false || string.IsNullOrEmpty(item.Path))
                    continue;
                List<ItemMod> itemMods = modsComponent.ItemMods;
                List<ModValue> mods =
                    itemMods.Select(
                        it => new ModValue(it, GameController.Files, modsComponent.ItemLevel, GameController.Files.BaseItemTypes.Translate(item.Path))
                    ).ToList();

                if (modsComponent.ItemLevel >= Settings.ItemLevel || modsComponent.Synthesised == true || baseComponent.isElder == true || baseComponent.isShaper == true)
                {
                    f = true;
                }

                var drawRect = child.GetClientRect();
                //fix star position
                drawRect.X -= 5;
                drawRect.Y -= 5;

                var drawRectAll = child.GetClientRect();
                drawRectAll.X -= 5;
                drawRectAll.Y -= 5;


                BaseItemType bit = GameController.Files.BaseItemTypes.Translate(item.Path);

                int count;

                switch (bit?.ClassName)
                {
                    case "Body Armour":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeBodyArmour(mods);
                        if ((count >= Settings.BaAffixes) && Settings.BodyArmour)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                            
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Quiver":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeQuiver(mods);
                        if ((count >= Settings.QAffixes) && Settings.Quiver)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                            _allItemsPos.Add(drawRectAll);
                        break;

                    case "Helmet":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeHelmet(mods);
                        if ((count >= Settings.HAffixes) && Settings.Helmet)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Boots":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeBoots(mods);
                        if ((count >= Settings.BAffixes) && Settings.Boots)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Gloves":
                        if (f) _MBItems.Add(drawRectAll);

                        if (AnalyzeGloves(mods) && Settings.Gloves)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Shield":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeShield(mods);
                        if ((count >= Settings.SAffixes) && Settings.Shield)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Belt":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeBelt(mods);
                        if ((count >= Settings.BeAffixes) && Settings.Belt)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Ring":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeRing(mods);
                        if ((count >= Settings.RAffixes) && Settings.Ring)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Amulet":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeAmulet(mods);
                        if ((count >= Settings.AAffixes) && Settings.Amulet)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Dagger":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                            _allItemsPos.Add(drawRectAll);
                        break;

                    case "Rune Dagger":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                            _allItemsPos.Add(drawRectAll);
                        break;

                    case "Wand":
                        if (f) _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Sceptre":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Thrusting One Hand Sword":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;   
                        
                    case "Staff":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;

                    case "Warstaff":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                            _allItemsPos.Add(drawRectAll);
                        break;

                    case "Claw":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "One Hand Sword":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "Two Hand Sword":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "One Hand Axe":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "Two Hand Axe":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "One Hand Mace":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "Two Hand Mace":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                    case "Bow":
                        if (f)
                            _MBItems.Add(drawRectAll);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItems.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRectAll);
                        break;
                }
            }

            if (!Settings.HideUnderMouse)
            {
                DrawСomet(_VeilItems);
                DrawStar(_goodItemsPos);
                DrawMB(_MBItems);
                ClickShit(_allItemsPos);
            }
            else if (_currentHoverItem.Item == null)
            {
                DrawСomet(_VeilItems);
                DrawStar(_goodItemsPos);
                DrawMB(_MBItems);
                ClickShit(_allItemsPos);
            }
        }
        #endregion

        #region DrawMB

        private void DrawMB(List<RectangleF> MBItems)
        {
            foreach (var position in MBItems)
            {
                if (Settings.StarOrBorder)
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                    Graphics.DrawFrame(border, Settings.ColorAll, 1);
                }
                else
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                    Graphics.DrawFrame(border, Settings.ColorAll, 3);
                }
            }
        }

        #endregion

        #region Draw Star/Rec
        private void DrawStar(List<RectangleF> goodItems)
        {
            foreach (var position in goodItems)
                if (Settings.StarOrBorder)
                    Graphics.DrawText(" Good item ", position.TopLeft, Settings.Color, 30);
                else
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                    Graphics.DrawFrame(border, Settings.Color, 3);
                }
        }
        #endregion

        #region Draw Сomet/Rec
        private void DrawСomet(List<RectangleF> goodItems)
        {
            foreach (var position in goodItems)
                if (Settings.StarOrBorder) Graphics.DrawText(" Syndicate ", position.TopLeft, Settings.Color, 30);
                else
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                    Graphics.DrawFrame(border, Settings.Color, 3);
                }
        }
        #endregion

        #region ClickShit
        private void ClickShit(List<RectangleF> AllItems)
        {
            bool a = Keyboard.IsKeyToggled(Settings.HotKey.Value);

            if (a)
            {
                Keyboard.KeyDown(Keys.LControlKey);
                foreach (var position in AllItems)
                {

                    Vector2 vector2 = new Vector2(position.X + 25, position.Y + 25);

                    Mouse.SetCursorPosAndLeftClick(vector2, Settings.ExtraDelay.Value, _windowOffset);
                    Thread.Sleep(Constants.WHILE_DELAY + Settings.ExtraDelay.Value);

                }
                Keyboard.KeyUp(Keys.LControlKey);

                a = false;

                Keyboard.KeyPress(Settings.HotKey.Value);
            }

        }
        #endregion

        #region Body Armour
        private int AnalyzeBodyArmour(List<ModValue> mods)
        {
            int BaaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BaLife)
                {
                    BaaffixCounter++;
                }

                else if ((mod.Record.Group.Contains("DefencesPercent") || mod.Record.Group.Contains("BaseLocalDefences")) && mod.Tier <= Settings.BaEnergyShield && mod.Tier > 0)
                {
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

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.BaDexterity)
                    BaaffixCounter++;

                else if (mod.Record.Group.Contains("BaseLocalDefencesAndLife") && mod.Tier <= Settings.BaLifeCombo && mod.Tier > 0)
                {
                    BaaffixCounter++;
                }

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    BaaffixCounter+= 100;
                }

                //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                    {
                        LogMessage(mod.Record.Group, 10f);
                    }
                }
            }
            if (elemRes >= Settings.BaTotalRes)
                BaaffixCounter++;

            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + BaaffixCounter, 10f);
            }
            return BaaffixCounter;
        } 
        #endregion
        #region Helmets
        private int AnalyzeHelmet(List<ModValue> mods)
        {
            int HaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.HLife)
                {
                    HaffixCounter++;
                }

                else if ((mod.Record.Group.Contains("DefencesPercent") || mod.Record.Group.Contains("BaseLocalDefences")) && mod.Tier <= Settings.HEnergyShield && mod.Tier > 0)
                {
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

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.HStrength)
                    HaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.HIntelligence)
                    HaffixCounter++;

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.HDexterity)
                    HaffixCounter++;

                else if (mod.Record.Group == "IncreasedMana" && mod.StatValue[0] >= Settings.HMana)
                    HaffixCounter++;

                else if (mod.Record.Group.Contains("BaseLocalDefencesAndLife") && mod.Tier <= Settings.HLifeCombo && mod.Tier > 0)
                {
                    HaffixCounter++;
                }

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    HaffixCounter += 100;
                }

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
            return HaffixCounter;
        }
        #endregion
        #region Gloves
        private bool AnalyzeGloves(List<ModValue> mods)
        {
            int GaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.GLife)
                {
                    GaffixCounter++;
                }

                else if ((mod.Record.Group.Contains("DefencesPercent") || mod.Record.Group.Contains("BaseLocalDefences")) && mod.Tier <= Settings.GEnergyShield && mod.Tier > 0)
                {
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

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.GStrength)
                    GaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.GIntelligence)
                    GaffixCounter++;

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.GDexterity)
                    GaffixCounter++;

                else if (mod.Record.Group == "IncreasedMana" && mod.StatValue[0] >= Settings.GMana)
                    GaffixCounter++;

                else if (mod.Record.Group.Contains("BaseLocalDefencesAndLife") && mod.Tier <= Settings.GLifeCombo && mod.Tier > 0)
                {
                    GaffixCounter++;
                }

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    GaffixCounter += 3;
                }


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
        private int AnalyzeBoots(List<ModValue> mods)
        {
            int BaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "MovementVelocity" && mod.StatValue[0] >= Settings.BMoveSpeed)
                { 
                    BaffixCounter++;
                }
            
                else if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BLife)
                {
                    BaffixCounter++;
                }

                else if ((mod.Record.Group.Contains("DefencesPercent") || mod.Record.Group.Contains("BaseLocalDefences")) && mod.Tier <= Settings.BEnergyShield && mod.Tier > 0)
                {
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
                
                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.BDexterity)
                    BaffixCounter++;

                else if (mod.Record.Group == "IncreasedMana" && mod.StatValue[0] >= Settings.BMana)
                    BaffixCounter++;

                else if (mod.Record.Group.Contains("BaseLocalDefencesAndLife") && mod.Tier <= Settings.BLifeCombo && mod.Tier > 0)
                {
                    BaffixCounter++;
                }

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    BaffixCounter += 100;
                }

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
            return BaffixCounter; 
           }
        #endregion
        #region Belts
        private int AnalyzeBelt(List<ModValue> mods)
        {
            int BeaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.BeLife)
                {
                    BeaffixCounter++;
                }

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.BeEnergyShield && mod.Tier > 0)
                {
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

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.BeWeaponElemDamage)
                    BeaffixCounter++;

                else if (mod.Record.Group == "BeltFlaskCharges" && mod.StatValue[0] >= Settings.BeFlaskReduced)
                    BeaffixCounter++;

                else if (mod.Record.Group == "BeltFlaskDuration" && mod.StatValue[0] >= Settings.BeFlaskDuration)
                    BeaffixCounter++;

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    BeaffixCounter += 100;
                }


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
            return BeaffixCounter;
        }
        #endregion
        #region Rings
        private int AnalyzeRing(List<ModValue> mods)
        {

            int RaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in SumAffix(mods))
            {
                
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.RLife)
                    RaffixCounter++;

                else if (mod.Record.Group.Contains("EnergyShield") && mod.Tier <= Settings.REnergyShield && mod.Tier > 0)
                    RaffixCounter++;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAttackSpeed" && mod.StatValue[0] >= Settings.RAttackSpeed)
                    RaffixCounter++;
             
                else if (mod.Record.Group == "IncreasedCastSpeed" && mod.StatValue[0] >= Settings.RCastSpped)
                    RaffixCounter++;

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.RAccuracy)
                    RaffixCounter++;

                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.RPhysDamage)
                    RaffixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.RWeaponElemDamage)
                    RaffixCounter++;

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.RStrength)
                    RaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.RIntelligence)
                    RaffixCounter++;

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.RDexterity)
                    RaffixCounter++;

                else if (mod.Record.Group == "IncreasedMana" && mod.StatValue[0] >= Settings.RMana)
                    RaffixCounter++;

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    RaffixCounter += 100;
                }

                //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                    LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.RTotalRes)
                RaffixCounter++;

            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + RaffixCounter, 10f);
            }

            return RaffixCounter;
        }
        #endregion
        #region Amulet
        private int AnalyzeAmulet(List<ModValue> mods)
        {
            int AaffixCounter = 0;
            int elemRes = 0;

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

                else if (mod.Record.Group == "CriticalStrikeMultiplier" && mod.StatValue[0] >= Settings.ACritMult)
                    AaffixCounter++;

                else if (mod.Record.Group == "CriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.ACritChance)
                    AaffixCounter++;

                else if (mod.Record.Group == "SpellDamage" && mod.StatValue[0] >= Settings.ATotalElemSpellDmg)
                    AaffixCounter++;

                else if (mod.Record.Group == "Strength" && mod.StatValue[0] >= Settings.AStrength)
                    AaffixCounter++;

                else if (mod.Record.Group == "Intelligence" && mod.StatValue[0] >= Settings.AIntelligence)
                    AaffixCounter++;

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.ADexterity)
                    AaffixCounter++;

                else if (mod.Record.Group == "IncreasedMana" && mod.StatValue[0] >= Settings.AMana)
                    AaffixCounter++;

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    AaffixCounter += 100;
                }
                //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.ATotalRes)
                AaffixCounter++;

            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + AaffixCounter, 10f);
            }
            return AaffixCounter;
        }
        #endregion
        #region Quiver
        private int AnalyzeQuiver(List<ModValue> mods)
        {
            int QaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in SumAffix(mods))
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.QLife)
                    QaffixCounter++;

                else if (mod.Record.Group.Contains("Resist"))
                    if (mod.Record.Group == "AllResistances")
                        elemRes += mod.StatValue[0] * 3;
                    else if (mod.Record.Group.Contains("And"))
                        elemRes += mod.StatValue[0] * 2;
                    else
                        elemRes += mod.StatValue[0];

                else if (mod.Record.Group == "IncreasedAccuracy" && mod.StatValue[0] >= Settings.QAccuracy)
                    QaffixCounter++;

                else if (mod.Record.Group == "PhysicalDamage" && Average(mod.StatValue) >= Settings.QPhysDamage)
                    QaffixCounter++;

                else if (mod.Record.Group == "IncreasedWeaponElementalDamagePercent" && mod.StatValue[0] >= Settings.QWeaponElemDamage)
                    QaffixCounter++;

                else if (mod.Record.Group == "CriticalStrikeMultiplier" && mod.StatValue[0] >= Settings.QCritMult)
                    QaffixCounter++;

                else if (mod.Record.Group == "CriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.QCritChance)
                    QaffixCounter++;

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    QaffixCounter += 100;
                }
                //DEBUG TEST BLOCK
                {
                    if (Settings.DebugMode != false)
                        LogMessage(mod.Record.Group, 10f);
                }
            }
            if (elemRes >= Settings.ATotalRes)
                QaffixCounter++;

            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                    LogMessage("# of Affixes:" + QaffixCounter, 10f);
            }
            return QaffixCounter;
        }
        #endregion
        #region Shields
        private int AnalyzeShield(List<ModValue> mods)
        {
            int SaffixCounter = 0;
            int elemRes = 0;

            foreach (var mod in mods)
            {
                if (mod.Record.Group == "IncreasedLife" && mod.StatValue[0] >= Settings.SLife)
                {
                    SaffixCounter++;
                }

                else if ((mod.Record.Group.Contains("DefencesPercent") || mod.Record.Group.Contains("BaseLocalDefences")) && mod.Tier <= Settings.SEnergyShield && mod.Tier > 0)
                {
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

                else if (mod.Record.Group == "Dexterity" && mod.StatValue[0] >= Settings.SDexterity)
                    SaffixCounter++;

                else if (mod.Record.Group == "SpellDamage" && mod.StatValue[0] >= Settings.SSpellDamage)
                    SaffixCounter++;

                else if (mod.Record.Group == "SpellCriticalStrikeChanceIncrease" && mod.StatValue[0] >= Settings.SSpellCritChance)
                    SaffixCounter++;

                else if (mod.Record.Group.Contains("BaseLocalDefencesAndLife") && mod.Tier <= Settings.SLifeCombo && mod.Tier > 0)
                {
                    SaffixCounter++;
                }

                else if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    SaffixCounter += 100;
                }
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
            return SaffixCounter;
        }
        #endregion
        #region Weapon Caster
        private int AnalyzeWeaponCaster(List<ModValue> mods)
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

                else if (mod.Record.Group == "DamageOverTimeMultiplier")
                    WcaffixCounter += 3;

                if (mod.Record.Group.Contains("VeiledSuffix") || mod.Record.Group.Contains("VeiledPrefix"))
                {
                    WcaffixCounter += 100;
                }
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
            return WcaffixCounter;
        } 
        #endregion
        #region Weapon Attack
        private bool AnalyzeWeaponAttack(Entity item)
        {
            int WaaffixCounter = 0;

            var component = item.GetComponent<Weapon>();
            var mods = item.GetComponent<Mods>().ItemMods;

            float attackSpeed = 1f / (component.AttackTime / 1000f);
            attackSpeed *= 1f + mods.GetStatValue("LocalIncreasedAttackSpeed") / 100f;

            float phyDmg = (component.DamageMin + component.DamageMax) / 2f + mods.GetAverageStatValue("LocalAddedPhysicalDamage");
            phyDmg *= 1f + (mods.GetStatValue("LocalIncreasedPhysicalDamagePercent") + 20) / 100f;
            if (phyDmg * attackSpeed >= Settings.WaPhysDmg)
                WaaffixCounter++;

            float elemDmg = mods.GetAverageStatValue("LocalAddedColdDamage") + mods.GetAverageStatValue("LocalAddedFireDamage")
                            + mods.GetAverageStatValue("LocalAddedLightningDamage");
            if (elemDmg * attackSpeed >= Settings.WaElemDmg)
                WaaffixCounter++;

            //DEBUG TEST BLOCK
            {
                if (Settings.DebugMode != false)
                {
                    LogMessage(component.DumpObject(), 10f);
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