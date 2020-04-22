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
using System.IO;
using System;
using ExileCore.Shared;
using System.Collections;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzer : BaseSettingsPlugin<InventoryItemsAnalyzerSettings>
    {
        private List<RectangleF> _goodItemsPos;
        private List<RectangleF> _allItemsPos;
        private List<RectangleF> _highItemsPos;
        private List<RectangleF> _VeilItemsPos;
        private Vector2 _windowOffset;
        private IngameState _ingameState;
        private readonly string[] _nameAttrib = {"Intelligence", "Strength", "Dexterity"};
        private readonly string[] _incElemDmg =
            {"FireDamagePercentage", "ColdDamagePercentage", "LightningDamagePercentage"};
        private List<string> GoodBaseTypes;
        private List<string> GoodUniquesList;
        int CountInventory = 0;
        int idenf = 0;
        private Coroutine CoroutineWorker;
        private const string coroutineName = "InventoryItemsAnalyzer";

        public InventoryItemsAnalyzer() 
        { 

        }

        public override bool Initialise()
        {
            base.Initialise();

            ParseConfig_BaseType();

            ParseConfig_Unique();            
            
            Name = "INV Item Analyzer";

            var combine = Path.Combine(DirectoryFullName, "img", "GoodItem.png").Replace('\\', '/');
            Graphics.InitImage(combine, false);

            combine = Path.Combine(DirectoryFullName, "img", "Syndicate.png").Replace('\\', '/');
            Graphics.InitImage(combine, false);

            _ingameState = GameController.Game.IngameState;
            _windowOffset = GameController.Window.GetWindowRectangle().TopLeft;

            Input.RegisterKey(Settings.HotKey.Value);
            Input.RegisterKey(Keys.LControlKey);

            Settings.HotKey.OnValueChanged += () => { Input.RegisterKey(Settings.HotKey.Value); };

            return true;
        }

        public override void Render()
        {
            if (!_ingameState.IngameUi.InventoryPanel.IsVisible)
            {
                CountInventory = 0;
                idenf = 0;
                return;
            }

            var normalInventoryItems = _ingameState.IngameUi.InventoryPanel[InventoryIndex.PlayerInventory].VisibleInventoryItems;

            int temp = normalInventoryItems.Count(t => t.Item?.GetComponent<Mods>()?.Identified == true);

            //LogMessage(normalInventoryItems.Count.ToString() + " " + CountInventory.ToString() + " // " + temp .ToString() + " " + idenf.ToString(), 3f);

            if (normalInventoryItems.Count != CountInventory || temp != idenf)
            {
                ScanInventory(normalInventoryItems);
                CountInventory = normalInventoryItems.Count;
                idenf = temp;
            }

            if (!Settings.HideUnderMouse)
            {
                DrawSyndicateItems(_VeilItemsPos);
                DrawGoodItems(_goodItemsPos);
                DrawHighItemLevel(_highItemsPos);
                if (Settings.HotKey.PressedOnce())
                {
                    CoroutineWorker = new Coroutine(ClickShit(), this, coroutineName);
                    Core.ParallelRunner.Run(CoroutineWorker);
                } 
            }
        }

        #region Load config

        private void ParseConfig_BaseType()
        {
            string path = $"{DirectoryFullName}\\BaseType.txt";

           CheckGoodBase(path);

            using (StreamReader reader = new StreamReader(path) )
            {
                string text = reader.ReadToEnd();

                GoodBaseTypes = text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                reader.Close();
            }
        }

        private void CheckGoodBase(string path)
        {
            if (File.Exists(path)) return;

            // Tier 1-3 NeverSink
            string text = "Opal Ring" + "\r\n"          + "Steel Ring" + "\r\n"         + "Vermillion Ring" + "\r\n"    + "Blue Pearl Amulet" + "\r\n"      + "Bone Helmet" + "\r\n" +
                          "Cerulean Ring" + "\r\n"      + "Convoking Wand" + "\r\n"     + "Crystal Belt" + "\r\n"       + "Fingerless Silk Gloves" + "\r\n" + "Gripped Gloves" + "\r\n" +
                          "Marble Amulet" + "\r\n"      + "Sacrificial Garb" + "\r\n"   + "Spiked Gloves" + "\r\n"      + "Stygian Vise" + "\r\n"           + "Two-Toned Boots" + "\r\n" +
                          "Vanguard Belt" + "\r\n"      + "Diamond Ring" + "\r\n"       + "Onyx Amulet" + "\r\n"        + "Two-Stone Ring" + "\r\n"         + "Colossal Tower Shield"  + "\r\n" + 
                          "Eternal Burgonet" + "\r\n"   + "Hubris Circlet" + "\r\n"     + "Lion Pelt" + "\r\n"          + "Sorcerer Boots" + "\r\n"         + "Sorcerer Gloves" + "\r\n" + 
                          "Titanium Spirit Shield" + "\r\n" + "Vaal Regalia" + "\r\n";


            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.Write(text);
                streamWriter.Close();
            }
        }
        
        private void ParseConfig_Unique()
        {
            string path = $"{DirectoryFullName}\\GoodUnique.txt";

            CheckGoodUnique(path);

            using (StreamReader reader = new StreamReader(path) )
            {
                string text = reader.ReadToEnd();

                GoodBaseTypes = text.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                reader.Close();
            }
        }
        
        private void CheckGoodUnique(string path)
        {
            if (File.Exists(path)) 
                return;

            GoodUniquesList = new List<string>
            {
                "Agnerod West",
                "Ahkeli's Meadow",
                "Ahkeli's Valley",
                "Alpha's Howl",
                "Arborix",
                "Astramentis",
                "Atziri's Disfavour",
                "Atziri's Splendour",
                "Beltimber Blade",
                "Berek's Respite",
                "Bisco's Collar",
                "Bisco's Leash",
                "Brain Rattler",
                "Briskwrap",
                "Brutal Restraint",
                "Bubonic Trail",
                "Carcass Jack",
                "Chaber Cairn",
                "Chains of Command",
                "Circle of Anguish",
                "Circle of Guilt",
                "Cloak of Defiance",
                "Cloak of Tawm'r Isley",
                "Corona Solaris",
                "Coward's Chains",
                "Craiceann's Carapace",
                "Crown of the Tyrant",
                "Daresso's Defiance",
                "Darkscorn",
                "Death's Harp",
                "Death's Opus",
                "Divinarius",
                "Doedre's Skin",
                "Doomsower",
                "Dreadbeak",
                "Dying Sun",
                "Elegant Hubris",
                "Energy From Within",
                "Fenumus' Shroud",
                "Fenumus' Spinnerets",
                "Frostferno",
                "Garb of the Ephemeral",
                "Garukhan's Flight",
                "Geofri's Legacy",
                "Geofri's Sanctuary",
                "Glorious Vanity",
                "Goldwyrm",
                "Greedtrap",
                "Grelwood Shank",
                "Grip of the Council",
                "Gruthkul's Pelt",
                "Hands of the High Templar",
                "Healthy Mind",
                "Hopeshredder",
                "Hrimburn",
                "Hyrri's Demise",
                "Hyrri's Ire",
                "Impresence",
                "Inpulsa's Broken Heart",
                "Kaom's Heart",
                "Karui Charge",
                "Kingmaker",
                "Kintsugi",
                "Kitava's Feast",
                "Lethal Pride",
                "Lioneye's Fall",
                "Lioneye's Vision",
                "Loreweave",
                "Magna Eclipsis",
                "Maligaro's Virtuosity",
                "Mark of the Shaper",
                "Marohi Erqi",
                "Martyr of Innocence",
                "Mask of the Spirit Drinker",
                "Might of the Meek",
                "Militant Faith",
                "Mother's Embrace",
                "Ngamahu's Flame",
                "Null and Void",
                "One With Nothing",
                "Pledge of Hands",
                "Precursor's Emblem",
                "Presence of Chayula",
                "Prism Guardian",
                "Putembo's Meadow",
                "Putembo's Valley",
                "Queen of the Forest",
                "Seven-League Step",
                "Shade of Solaris",
                "Shroud of the Lightless",
                "Silverbough",
                "Sin's Rebirth",
                "Sire of Shards",
                "Slivertongue",
                "Solstice Vigil",
                "Soul Taker",
                "Sporeguard",
                "Starforge",
                "The Anima Stone",
                "The Anticipation",
                "The Blue Nightmare",
                "The Coming Calamity",
                "The Dancing Duo",
                "The Devouring Diadem",
                "The Effigon",
                "The Enmity Divine",
                "The Goddess Scorned",
                "The Green Dream",
                "The Gryphon",
                "The Iron Fortress",
                "The Pariah",
                "The Poet's Pen",
                "The Primordial Chain",
                "The Queen's Hunger",
                "The Red Nightmare",
                "The Retch",
                "The Searing Touch",
                "The Signal Fire",
                "The Snowblind Grace",
                "The Sorrow of the Divine",
                "The Stampede",
                "The Tactician",
                "The Taming",
                "The Tempest",
                "Unending Hunger",
                "Uzaza's Mountain",
                "Ventor's Gamble",
                "Victario's Influence",
                "Viper's Scales",
                "Vivinsect",
                "Void Battery",
                "Voidforge",
                "Warrior's Legacy",
                "Whakatutuki o Matua",
                "Wildwrap",
                "Windripper",
                "Windshriek",
                "Xirgil's Crank",
                "Xoph's Blood",
                "Xoph's Inception",
                "Xoph's Nurture",
            };

            string text = "";
            foreach (var v in GoodUniquesList)
            {
                text += v + "\r\n";
            }

            using (StreamWriter streamWriter = new StreamWriter(path, true))
            {
                streamWriter.Write(text);
                streamWriter.Close();
            }
        }

        #endregion

        #region Scan Inventory
            private void ScanInventory(IList<NormalInventoryItem> normalInventoryItems)
            {

            _goodItemsPos = new List<RectangleF>();
            _allItemsPos = new List<RectangleF>();
            _highItemsPos = new List<RectangleF>();
            _VeilItemsPos = new List<RectangleF>();

            foreach (var normalInventoryItem in normalInventoryItems)
            {
                bool HighItemLevel = false;
                var item = normalInventoryItem.Item;
                if (item == null)
                    continue;

                var modsComponent = item?.GetComponent<Mods>();

                if (string.IsNullOrEmpty(item.Path))
                    continue;

                var drawRect = normalInventoryItem.GetClientRect();
                //fix star position
                drawRect.X -= 5;
                drawRect.Y -= 5;

                #region Filter trash uniques

                if (!item.HasComponent<ExileCore.PoEMemory.Components.Map>() &&
                    modsComponent?.ItemRarity == ItemRarity.Unique &&
                    item?.GetComponent<Sockets>()?.LargestLinkSize != 6 &&
                    !GoodUniquesList.Contains(modsComponent.UniqueName))
                {
                    LogMessage(modsComponent.UniqueName);
                    _allItemsPos.Add(drawRect);
                }

                #endregion
                
                if (modsComponent?.ItemRarity == ItemRarity.Normal || modsComponent?.ItemRarity == ItemRarity.Magic)
                {
                    if (item?.GetComponent<Sockets>()?.NumberOfSockets == 6)
                        _allItemsPos.Add(drawRect);

                    continue;
                }

                if (modsComponent?.ItemRarity != ItemRarity.Rare || modsComponent.Identified == false)
                    continue;

                List<ItemMod> itemMods = modsComponent.ItemMods;

                //foreach (ItemMod im in itemMods) if (!GameController.Files.Mods.records.ContainsKey(im.RawName)) return;

                List<ModValue> mods =
                    itemMods.Select(
                        it => new ModValue(it, GameController.Files, modsComponent.ItemLevel, GameController.Files.BaseItemTypes.Translate(item.Path))
                    ).ToList();

                #region Elder or Shaper
                {
                    var baseComponent = item?.GetComponent<Base>();
                    if (modsComponent.ItemLevel >= Settings.ItemLevel_ElderOrShaper && (baseComponent.isElder || baseComponent.isShaper))
                    {
                        HighItemLevel = true;
                    }
                }
                #endregion

                BaseItemType bit = GameController.Files.BaseItemTypes.Translate(item.Path);

                #region Item Level
                {
                    if (modsComponent.ItemLevel >= Settings.ItemLevel_BaseType)
                    {
                        foreach (string BaseType in GoodBaseTypes)
                        {
                            if (bit.BaseName == BaseType)
                            {
                                HighItemLevel = true;
                                break;
                            }
                        }
                    }
                }
                #endregion

                int count;

                switch (bit?.ClassName)
                {
                    case "Body Armour":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeBodyArmour(mods);
                        if ((count >= Settings.BaAffixes) && Settings.BodyArmour)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                            
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Quiver":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeQuiver(mods);
                        if ((count >= Settings.QAffixes) && Settings.Quiver)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                            _allItemsPos.Add(drawRect);
                        break;

                    case "Helmet":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeHelmet(mods);
                        if ((count >= Settings.HAffixes) && Settings.Helmet)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Boots":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeBoots(mods);
                        if ((count >= Settings.BAffixes) && Settings.Boots)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Gloves":

                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeGloves(mods);
                        if ((count >= Settings.GAffixes) && Settings.Gloves)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                            _allItemsPos.Add(drawRect);
                        break;


                    case "Shield":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeShield(mods);
                        if ((count >= Settings.SAffixes) && Settings.Shield)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Belt":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeBelt(mods);
                        if ((count >= Settings.BeAffixes) && Settings.Belt)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Ring":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeRing(mods);
                        if ((count >= Settings.RAffixes) && Settings.Ring)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Amulet":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeAmulet(mods);
                        if ((count >= Settings.AAffixes) && Settings.Amulet)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Dagger":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                            _allItemsPos.Add(drawRect);
                        break;

                    case "Rune Dagger":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                            _allItemsPos.Add(drawRect);
                        break;

                    case "Wand":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Sceptre":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Thrusting One Hand Sword":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;   
                        
                    case "Staff":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                           _allItemsPos.Add(drawRect);
                        break;

                    case "Warstaff":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else
                            _allItemsPos.Add(drawRect);
                        break;

                    case "Claw":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "One Hand Sword":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "Two Hand Sword":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "One Hand Axe":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "Two Hand Axe":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "One Hand Mace":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "Two Hand Mace":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                    case "Bow":
                        if (HighItemLevel) _highItemsPos.Add(drawRect);

                        count = AnalyzeWeaponCaster(mods);
                        if ((count >= Settings.WcAffixes) && Settings.WeaponCaster)
                        {
                            if (count >= 100)
                            {
                                _VeilItemsPos.Add(drawRect);
                            }
                            else
                            {
                                _goodItemsPos.Add(drawRect);
                            }
                        }
                        else if (AnalyzeWeaponAttack(item) && Settings.WeaponAttack)
                            _goodItemsPos.Add(drawRect);
                        else
                           _allItemsPos.Add(drawRect);
                        break;
                }
            }
        }
        #endregion

        #region DrawHighItemLevel

        private void DrawHighItemLevel(List<RectangleF> HighItemLevel)
        {
            foreach (var position in HighItemLevel)
            {
                if (Settings.StarOrBorder)
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = position.Width - 6, Height = position.Height - 6 };
                    Graphics.DrawFrame(border, Settings.ColorAll, 1);
                }
            }
        }

        #endregion

        #region Draw GoodItems
        private void DrawGoodItems(List<RectangleF> goodItems)
        {
            foreach (var position in goodItems)
                if (Settings.StarOrBorder)
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = (position.Width - 6) / 1.5f, Height = (position.Height - 6) / 1.5f };
    
                    if (!Settings.Text)
                    {
                        Graphics.DrawImage("GoodItem.png", border);
                    }
                    else
                    {
                        Graphics.DrawText(@" Good Item ", position.TopLeft, Settings.Color, 30);
                    }
                }
        }
        #endregion

        #region Draw Syndicate items
        private void DrawSyndicateItems(List<RectangleF> SyndicateItems)
        {
            foreach (var position in SyndicateItems)
                if (Settings.StarOrBorder)
                {
                    RectangleF border = new RectangleF { X = position.X + 8, Y = position.Y + 8, Width = (position.Width - 6) / 1.5f, Height = (position.Height - 6) / 1.5f };

                    if(!Settings.Text)
                    {
                        Graphics.DrawImage("Syndicate.png", border);
                    }
                    else
                    {
                        Graphics.DrawText(@" Syndicate ", position.TopLeft, Settings.Color, 30);
                    }

                }
        }       
        #endregion

        #region ClickShit
        private IEnumerator ClickShit()
        {
            Input.KeyDown(Keys.LControlKey);
            foreach (var position in _allItemsPos)
            {
                Vector2 vector2 = new Vector2(position.X + 25, position.Y + 25);

                yield return Input.SetCursorPositionSmooth(vector2 + _windowOffset);

                yield return new WaitTime(Settings.ExtraDelay.Value / 2);

                Input.Click(MouseButtons.Left);

                yield return new WaitTime(Settings.ExtraDelay.Value);
            }
            Input.KeyUp(Keys.LControlKey);

            yield break;
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
        private int AnalyzeGloves(List<ModValue> mods)
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
                    GaffixCounter += 100;
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
            return GaffixCounter;
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