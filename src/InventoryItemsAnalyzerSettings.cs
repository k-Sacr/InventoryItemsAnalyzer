using System.Collections.Generic;
using ExileCore.Shared.Interfaces;
using ExileCore.Shared.Nodes;
using ExileCore.Shared.Attributes;
using SharpDX;
using System.Windows.Forms;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzerSettings : ISettings
    {
        public InventoryItemsAnalyzerSettings()
        {
            //Defaults
            #region Enable/Additional Settings
            Enable = new ToggleNode(false);
            HotKey = Keys.F2;
            ExtraDelay = new RangeNode<int>(20, 0, 100);
            HideUnderMouse = new ToggleNode(false);
            StarOrBorder = new ToggleNode(true);
            Color = new ColorBGRA(0, 255, 0, 255);
            ColorAll = new ColorBGRA(255, 255, 255, 255);
            ItemLevel_ElderOrShaper = new RangeNode<int>(84, 0, 100);
            ItemLevel_BaseType = new RangeNode<int>(84, 0, 100);
            DebugMode = new ToggleNode(false);
            Text = new ToggleNode(false);
            #endregion
            #region BodyArmour
            BodyArmour = new ToggleNode(true);
            BaLife = new RangeNode<int>(100, 60, 120);
            BaTotalRes = new RangeNode<int>(100, 60, 100);
            BaEnergyShield = new RangeNode<int>(2, 1, 5);
            BaStrength = new RangeNode<int>(40, 30, 55);
            BaIntelligence = new RangeNode<int>(40, 30, 55);
            BaDexterity = new RangeNode<int>(40, 30, 55);
            BaLifeCombo = new RangeNode<int>(4, 1, 4);
            BaAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Helmet
            Helmet = new ToggleNode(true);
            HLife = new RangeNode<int>(65, 60, 100);
            HEnergyShield = new RangeNode<int>(2, 1, 5);
            HTotalRes = new RangeNode<int>(65, 60, 100);
            HAccuracy = new RangeNode<int>(350, 200, 500);
            HStrength = new RangeNode<int>(30, 30, 55);
            HIntelligence = new RangeNode<int>(30, 30, 55);
            HDexterity = new RangeNode<int>(30, 30, 55);
            HMana = new RangeNode<int>(60, 50, 80);
            HLifeCombo = new RangeNode<int>(3, 1, 3);
            HAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Gloves
            Gloves = new ToggleNode(true);
            GLife = new RangeNode<int>(60, 60, 100);
            GTotalRes = new RangeNode<int>(65, 40, 130);
            GEnergyShield = new RangeNode<int>(2, 1, 5);
            GAccuracy = new RangeNode<int>(350, 200, 500);
            GAttackSpeed = new RangeNode<int>(11, 8, 16);
            GPhysDamage = new RangeNode<int>(7, 2, 10);
            GStrength = new RangeNode<int>(30, 30, 55);
            GIntelligence = new RangeNode<int>(30, 30, 55);
            GDexterity = new RangeNode<int>(30, 30, 55);
            GMana = new RangeNode<int>(60, 50, 80);
            GLifeCombo = new RangeNode<int>(2, 1, 2);
            GAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Boots
            Boots = new ToggleNode(true);
            BLife = new RangeNode<int>(60, 40, 100);
            BTotalRes = new RangeNode<int>(65, 40, 130);
            BEnergyShield = new RangeNode<int>(2, 1, 5);
            BStrength = new RangeNode<int>(30, 25, 55);
            BIntelligence = new RangeNode<int>(30, 25, 55);
            BDexterity = new RangeNode<int>(30, 25, 55);
            BMoveSpeed = new RangeNode<int>(20, 15, 30);
            BMana = new RangeNode<int>(60, 50, 80);
            BLifeCombo = new RangeNode<int>(2, 1, 2);
            BAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Belts
            Belt = new ToggleNode(true);
            BeLife = new RangeNode<int>(60, 45, 100);
            BeEnergyShield = new RangeNode<int>(2, 1, 5);
            BeTotalRes = new RangeNode<int>(65, 40, 130);
            BeStrength = new RangeNode<int>(30, 25, 55);
            BeWeaponElemDamage = new RangeNode<int>(25, 20, 40);
            BeFlaskReduced = new RangeNode<int>(-20, -20, -10);
            BeFlaskDuration = new RangeNode<int>(10, 10, 20);
            BeAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Rings
            Ring = new ToggleNode(true);
            RLife = new RangeNode<int>(55, 35, 80);
            REnergyShield = new RangeNode<int>(2, 1, 5);
            RTotalRes = new RangeNode<int>(65, 50, 100);
            RPhysDamage = new RangeNode<int>(10, 6, 12);
            RWeaponElemDamage = new RangeNode<int>(25, 20, 40);
            RAccuracy = new RangeNode<int>(350, 200, 400);
            RAttackSpeed = new RangeNode<int>(5, 5, 20);
            RCastSpped = new RangeNode<int>(5, 5, 20);
            RStrength = new RangeNode<int>(30, 25, 55);
            RIntelligence = new RangeNode<int>(30, 25, 55);
            RDexterity = new RangeNode<int>(30, 25, 55);
            RMana = new RangeNode<int>(60, 50, 80);
            RAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Amulets
            Amulet = new ToggleNode(true);
            ALife = new RangeNode<int>(55, 40, 80);
            AEnergyShield = new RangeNode<int>(2, 1, 5);
            ATotalRes = new RangeNode<int>(65, 40, 130);
            APhysDamage = new RangeNode<int>(10, 6, 12);
            AWeaponElemDamage = new RangeNode<int>(25, 20, 40);
            AAccuracy = new RangeNode<int>(350, 200, 400);
            ACritMult = new RangeNode<int>(25, 20, 40);
            ACritChance = new RangeNode<int>(25, 20, 40);
            ATotalElemSpellDmg = new RangeNode<int>(13, 10, 30);
            AStrength = new RangeNode<int>(30, 25, 55);
            AIntelligence = new RangeNode<int>(30, 25, 55);
            ADexterity = new RangeNode<int>(30, 25, 55);
            AMana = new RangeNode<int>(60, 50, 80);
            AAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Shields
            Shield = new ToggleNode(true);
            SLife = new RangeNode<int>(65, 60, 110);
            SEnergyShield = new RangeNode<int>(2, 1, 5);
            STotalRes = new RangeNode<int>(65, 70, 130);
            SStrength = new RangeNode<int>(30, 25, 55);
            SIntelligence = new RangeNode<int>(30, 25, 55);
            SDexterity = new RangeNode<int>(30, 25, 55);
            SSpellDamage = new RangeNode<int>(25, 25, 80);
            SSpellCritChance = new RangeNode<int>(75, 60, 110);
            SLifeCombo = new RangeNode<int>(3, 1, 3);
            SAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Weapons Caster
            WeaponCaster = new ToggleNode(true);
            WcTotalElemSpellDmg = new RangeNode<int>(80, 70, 130);
            WcSpellCritChance = new RangeNode<int>(80, 70, 150);
            WcToElemDamageSpell = new RangeNode<int>(30, 20, 90);
            WcCritMult = new RangeNode<int>(25, 20, 40);
            WcAffixes = new RangeNode<int>(2, 0, 5);
            #endregion
            #region Weapons Attack
            WeaponAttack = new ToggleNode(true);
            WaPhysDmg = new RangeNode<int>(300, 250, 500);
            WaElemDmg = new RangeNode<int>(250, 200, 300);
            WaAffixes = new RangeNode<int>(1, 0, 2);
            #endregion
            #region Quivers

            Quiver = new ToggleNode(true);
            QLife = new RangeNode<int>(55, 40, 80);
            QTotalRes = new RangeNode<int>(65, 40, 130);
            QPhysDamage = new RangeNode<int>(10, 6, 12);
            QWeaponElemDamage = new RangeNode<int>(25, 20, 40);
            QAccuracy = new RangeNode<int>(350, 200, 400);
            QCritMult = new RangeNode<int>(25, 20, 40);
            QCritChance = new RangeNode<int>(25, 20, 40);
            QAffixes = new RangeNode<int>(2, 0, 5);

            #endregion

            #region PoeNinjaUnique

            Update = new ToggleNode(false);

            List<string> listLeagues = new List<string>()
            {
                "Temp SC", "Temp HC"
            };
            League = new ListNode();
            League.SetListValues(listLeagues);

            ChaosValue = new RangeNode<int>(2, 0, 50);
            #endregion
        }
        //Get Set

        public ToggleNode Enable { get; set; }

        #region Body Armour
        [Menu("Body Armour", 1, Tooltip = "Body Armour Filter Settings")]
        public ToggleNode BodyArmour { get; set; }

        [Menu("Life", parentIndex = 1, Tooltip = "Set the minimum value to filter life value")]
        public RangeNode<int> BaLife { get; set; }

        [Menu("Energy Shield (tier)", parentIndex = 1, Tooltip = "Set the minimum value to filter defence tier value")]
        public RangeNode<int> BaEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 1, Tooltip = "Set the minimum value to filter total resistance value")]
        public RangeNode<int> BaTotalRes { get; set; }

        [Menu("Strength", parentIndex = 1, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<int> BaStrength { get; set; }

        [Menu("Intelligence", parentIndex = 1, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<int> BaIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 1, Tooltip = "Set the minimum value to filter dexterity value")]
        public RangeNode<int> BaDexterity { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 1, Tooltip = "Set the minimum value to filter lifecombo tier value")]
        public RangeNode<int> BaLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 1, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> BaAffixes { get; set; }
        #endregion
        #region Helmet
        [Menu("Helmets", 2, Tooltip = "Helmet Filter Settings")]
        public ToggleNode Helmet { get; set; }

        [Menu("Life", parentIndex = 2, Tooltip = "Set the minimum value to filter Life value")]
        public RangeNode<int> HLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 2, Tooltip = "Set the minimum value to filter defence tier value")]
        public RangeNode<int> HEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 2, Tooltip = "Set the minimum value to filter total resistance value")]
        public RangeNode<int> HTotalRes { get; set; }

        [Menu("Accuracy", parentIndex = 2, Tooltip = "Set the minimum value to filter total accuracy value")]
        public RangeNode<int> HAccuracy { get; set; }

        [Menu("Strength", parentIndex = 2, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<int> HStrength { get; set; }

        [Menu("Intelligence", parentIndex = 2, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<int> HIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 2, Tooltip = "Set the minimum value to filter Dexterity value")]
        public RangeNode<int> HDexterity { get; set; }

        [Menu("Mana", parentIndex = 2, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<int> HMana { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 2, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> HLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 2, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> HAffixes { get; set; }
        #endregion
        #region Gloves
        [Menu("Gloves", 3, Tooltip = "Glove Filter Settings")]
        public ToggleNode Gloves { get; set; }

        [Menu("Life", parentIndex = 3, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> GLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 3, Tooltip = "Set the minimum value to filter for defence tier value")]
        public RangeNode<int> GEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 3, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> GTotalRes { get; set; }

        [Menu("Strength", parentIndex = 3, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<int> GStrength { get; set; }

        [Menu("Intelligence", parentIndex = 3, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<int> GIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 3, Tooltip = "Set the minimum value to filter Dexterity value")]
        public RangeNode<int> GDexterity { get; set; }

        [Menu("Accuracy", parentIndex = 3, Tooltip = "Set the minimum value to filter for accuracy value")]
        public RangeNode<int> GAccuracy { get; set; }

        [Menu("Attack Speed", parentIndex = 3, Tooltip = "Set the minimum value to filter for attack speed value")]
        public RangeNode<int> GAttackSpeed { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 3, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<int> GPhysDamage { get; set; }

        [Menu("Mana", parentIndex = 3, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<int> GMana { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 3, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> GLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 3, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> GAffixes { get; set; }
        #endregion
        #region Boots
        [Menu("Boots", 4, Tooltip = "Boot Filter Settings")]
        public ToggleNode Boots { get; set; }

        [Menu("Life", parentIndex = 4, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> BLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 4, Tooltip = "Set the minimum value to filter for defence tier value")]
        public RangeNode<int> BEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 4, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> BTotalRes { get; set; }

        [Menu("Strength", parentIndex = 4, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<int> BStrength { get; set; }

        [Menu("Intelligence", parentIndex = 4, Tooltip = "Set the minimum value to filter for intelligence value")]
        public RangeNode<int> BIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 4, Tooltip = "Set the minimum value to filter for Dexterity value")]
        public RangeNode<int> BDexterity { get; set; }

        [Menu("Movement Speed", parentIndex = 4, Tooltip = "Set the minimum value to filter for movement speed value")]
        public RangeNode<int> BMoveSpeed { get; set; }

        [Menu("Mana", parentIndex = 4, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<int> BMana { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 4, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> BLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 4, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> BAffixes { get; set; }
        #endregion  
        #region Belt
        [Menu("Belts", 5, Tooltip = "Belt Settings")]
        public ToggleNode Belt { get; set; }

        [Menu("Life", parentIndex = 5, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> BeLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 5, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> BeEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 5, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> BeTotalRes { get; set; }

        [Menu("Strength", parentIndex = 5, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<int> BeStrength { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 5, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<int> BeWeaponElemDamage { get; set; }

        [Menu("Flask Reduced", parentIndex = 5, Tooltip = "Set the minimum value to filter for Flask Reduced value")]
        public RangeNode<int> BeFlaskReduced { get; set; }

        [Menu("Flask Duration", parentIndex = 5, Tooltip = "Set the minimum value to filter for Flask Duration value")]
        public RangeNode<int> BeFlaskDuration { get; set; }

        [Menu("Affix Count", parentIndex = 5, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> BeAffixes { get; set; }
        //        [Menu("Other Flask Suffix", 67, 60)]
        //        public RangeNode<int> BeFlaskSuffix { get; set; }
        #endregion
        #region Ring
        [Menu("Rings", 6, Tooltip = "Ring Settings")]
        public ToggleNode Ring { get; set; }

        [Menu("Life", parentIndex = 6, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> RLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 6, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> REnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 6, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> RTotalRes { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 6, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<int> RPhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 6, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<int> RWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 6, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<int> RAccuracy { get; set; }

        [Menu("Attack Speed", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased attack speed value")]
        public RangeNode<int> RAttackSpeed { get; set; }

        [Menu("Cast Speed", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased cast speed value")]
        public RangeNode<int> RCastSpped { get; set; }

        [Menu("Strength", parentIndex = 6, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<int> RStrength { get; set; }

        [Menu("Intelligence", parentIndex = 6, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<int> RIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 6, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<int> RDexterity { get; set; }

        [Menu("Mana", parentIndex = 6, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<int> RMana { get; set; }

        [Menu("Affix Count", parentIndex = 6, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> RAffixes { get; set; }
        #endregion
        #region Amulet
        [Menu("Amulets", 7, Tooltip = "Amulet Settings")]
        public ToggleNode Amulet { get; set; }

        [Menu("Life", parentIndex = 7, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> ALife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 7, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> AEnergyShield { get; set; }

        [Menu("Total Resitance", parentIndex = 7, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> ATotalRes { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 7, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<int> APhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 7, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<int> AWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 7, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<int> AAccuracy { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 7, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<int> ACritMult { get; set; }

        [Menu("Critical Strike Chance", parentIndex = 7, Tooltip = "Set the minimum value to filter for critical strike chance value")]
        public RangeNode<int> ACritChance { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 7, Tooltip = "Set the minimum value to filter for total elemental spell Damage value")]
        public RangeNode<int> ATotalElemSpellDmg { get; set; }

        [Menu("Strength", parentIndex = 7, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<int> AStrength { get; set; }

        [Menu("Intelligence", parentIndex = 7, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<int> AIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 7, Tooltip = "Set the minimum value to filter Dexterity value")]
        public RangeNode<int> ADexterity { get; set; }

        [Menu("Mana", parentIndex = 7, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<int> AMana { get; set; }

        [Menu("Affix Count", parentIndex = 7, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> AAffixes { get; set; }
        #endregion
        #region Shield
        [Menu("Shields", 8, Tooltip = "Shield Filter Settings")]
        public ToggleNode Shield { get; set; }

        [Menu("Life", parentIndex = 8, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> SLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 8, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> SEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 8, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> STotalRes { get; set; }

        [Menu("Strength", parentIndex = 8, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<int> SStrength { get; set; }

        [Menu("Intelligence", parentIndex = 8, Tooltip = "Set the minimum value to filter for intelligence value")]
        public RangeNode<int> SIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 8, Tooltip = "Set the minimum value to filter for Dexterity value")]
        public RangeNode<int> SDexterity { get; set; }

        [Menu("Spell Damage", parentIndex = 8, Tooltip = "Set the minimum value to filter for spell damage value")]
        public RangeNode<int> SSpellDamage { get; set; }

        [Menu("Spell Critical Strike Chance", parentIndex = 8, Tooltip = "Set the minimum value to filter for spell critical strike chance value")]
        public RangeNode<int> SSpellCritChance { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 8, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> SLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 8, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> SAffixes { get; set; }
        #endregion
        #region Caster Dagger/Wand/Sceptre
        [Menu("Cast Weapons", 9, Tooltip = "Cast Weapon Settings")]
        public ToggleNode WeaponCaster { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 9, Tooltip = "Set the minimum value to filter for total elemental spell damage value")]
        public RangeNode<int> WcTotalElemSpellDmg { get; set; }

        [Menu("Fire/Cold/Lightning Dmg to Spells", parentIndex = 9, Tooltip = "Set the minimum value to filter for % added elemental damage to spells value")]
        public RangeNode<int> WcToElemDamageSpell { get; set; }

        [Menu("Spell Critical Chance", parentIndex = 9, Tooltip = "Set the minimum value to filter for spell critical chance value")]
        public RangeNode<int> WcSpellCritChance { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 9, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<int> WcCritMult { get; set; }

        [Menu("Affix Count", parentIndex = 9, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> WcAffixes { get; set; }
        #endregion
        #region Attack Thrusting/Dagger/Wand
        [Menu("Attack Weapons", 10, Tooltip = "Attack Weapon Settings")]
        public ToggleNode WeaponAttack { get; set; }

        [Menu("Physical Damage", parentIndex = 10, Tooltip = "Set the minimum value to filter for total physical damage value")]
        public RangeNode<int> WaPhysDmg { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 10, Tooltip = "Set the minimum value to filter for total elemental spell damage value")]
        public RangeNode<int> WaElemDmg { get; set; }

        [Menu("Affix Count", parentIndex = 10, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> WaAffixes { get; set; }
        #endregion
        #region Quiver
        [Menu("Quiver", 20, Tooltip = "Quiver Settings")]
        public ToggleNode Quiver { get; set; }

        [Menu("Life", parentIndex = 20, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<int> QLife { get; set; }

        [Menu("Total Resitance", parentIndex = 20, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<int> QTotalRes { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 20, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<int> QPhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 20, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<int> QWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 20, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<int> QAccuracy { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 20, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<int> QCritMult { get; set; }

        [Menu("Critical Strike Chance", parentIndex = 20, Tooltip = "Set the minimum value to filter for critical strike chance value")]
        public RangeNode<int> QCritChance { get; set; }

        [Menu("Affix Count", parentIndex = 20, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<int> QAffixes { get; set; }
        #endregion

        #region PoeNinjaUnique

        [Menu("Parsing Poe Ninja Setting", 99)] 
        public EmptyNode PoeNinjaUnique { get; set; }

        [Menu("Enable", parentIndex = 99, Tooltip = "Parsing poe ninja for autovendor trash uniques items")]
        public ToggleNode Update { get; set; }
        
        [Menu("League", parentIndex = 99, Tooltip = "Choose league")]
        public ListNode League { get; set; }
        
        [Menu("Chaos", "Set min chaos value for autovendor")]
        public RangeNode<int> ChaosValue { get; set; }
        
        #endregion
        
        #region Additional Menu Settings
        [Menu("Additional Menu Settings", 100)]
        public EmptyNode Settings { get; set; }

        [Menu("Hide under Mouse", parentIndex = 100, Tooltip = "Hide a star on mouse over")]
        public ToggleNode HideUnderMouse { get; set; }

        [Menu("Text or Border", parentIndex = 100, Tooltip = "Display a text or border around the filtered item")]
        public ToggleNode StarOrBorder { get; set; }

        [Menu("Text Color", parentIndex = 100, Tooltip = "Display color of star on or border around the Syndicate item/Good item")]
        public ColorNode Color { get; set; }

        [Menu("Frame Color", parentIndex = 100, Tooltip = "Display color of star on or border around the Shaper/Elder/ItemLevel")]
        public ColorNode ColorAll { get; set; }

        [Menu("Text instead of image", parentIndex = 100, Tooltip = "Replaces an image with text")]
        public ToggleNode Text { get; set; }

        [Menu("Debug Mode", parentIndex = 100, Tooltip = "Enable debug mode to report collected information about items")]
        public ToggleNode DebugMode { get; set; }

        [Menu("Hotkey")]
        public HotkeyNode HotKey { get; set; }

        [Menu("Extra Delay", "Additional delay, plugin should work without extra delay, this is merely optional.")]
        public RangeNode<int> ExtraDelay { get; set; }

        [Menu("ItemLevel", "Set min ItemLevel for highlighted Elder or Shaper items")]
        public RangeNode<int> ItemLevel_ElderOrShaper { get; set; }

        [Menu("ItemLevel", "Set min ItemLevet for highlighted (ItemBase)")]
        public RangeNode<int> ItemLevel_BaseType { get; set; }
        
        #endregion
    } 
}