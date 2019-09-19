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
            ItemLevel = new RangeNode<int>(84, 0, 100);
            DebugMode = new ToggleNode(false);
            #endregion
            #region BodyArmour
            BodyArmour = new ToggleNode(true);
            BaLife = new RangeNode<float>(100, 60, 120);
            BaTotalRes = new RangeNode<float>(100, 60, 100);
            BaEnergyShield = new RangeNode<int>(2, 1, 5);
            BaStrength = new RangeNode<float>(40, 30, 55);
            BaIntelligence = new RangeNode<float>(40, 30, 55);
            BaDexterity = new RangeNode<float>(40, 30, 55);
            BaLifeCombo = new RangeNode<int>(4, 1, 4);
            BaAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Helmet
            Helmet = new ToggleNode(true);
            HLife = new RangeNode<float>(65, 60, 100);
            HEnergyShield = new RangeNode<int>(2, 1, 5);
            HTotalRes = new RangeNode<float>(65, 60, 100);
            HAccuracy = new RangeNode<float>(350, 200, 500);
            HStrength = new RangeNode<float>(30, 30, 55);
            HIntelligence = new RangeNode<float>(30, 30, 55);
            HDexterity = new RangeNode<float>(30, 30, 55);
            HMana = new RangeNode<float>(60, 50, 80);
            HLifeCombo = new RangeNode<int>(3, 1, 3);
            HAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Gloves
            Gloves = new ToggleNode(true);
            GLife = new RangeNode<float>(60, 60, 100);
            GTotalRes = new RangeNode<float>(65, 40, 130);
            GEnergyShield = new RangeNode<int>(2, 1, 5);
            GAccuracy = new RangeNode<float>(350, 200, 500);
            GAttackSpeed = new RangeNode<float>(11, 8, 16);
            GPhysDamage = new RangeNode<float>(7, 2, 10);
            GStrength = new RangeNode<float>(30, 30, 55);
            GIntelligence = new RangeNode<float>(30, 30, 55);
            GDexterity = new RangeNode<float>(30, 30, 55);
            GMana = new RangeNode<float>(60, 50, 80);
            GLifeCombo = new RangeNode<int>(2, 1, 2);
            GAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Boots
            Boots = new ToggleNode(true);
            BLife = new RangeNode<float>(60, 40, 100);
            BTotalRes = new RangeNode<float>(65, 40, 130);
            BEnergyShield = new RangeNode<int>(2, 1, 5);
            BStrength = new RangeNode<float>(30, 25, 55);
            BIntelligence = new RangeNode<float>(30, 25, 55);
            BDexterity = new RangeNode<float>(30, 25, 55);
            BMoveSpeed = new RangeNode<float>(20, 15, 30);
            BMana = new RangeNode<float>(60, 50, 80);
            BLifeCombo = new RangeNode<int>(2, 1, 2);
            BAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Belts
            Belt = new ToggleNode(true);
            BeLife = new RangeNode<float>(60, 45, 100);
            BeEnergyShield = new RangeNode<int>(2, 1, 5);
            BeTotalRes = new RangeNode<float>(65, 40, 130);
            BeStrength = new RangeNode<float>(30, 25, 55);
            BeWeaponElemDamage = new RangeNode<float>(25, 20, 40);
            BeFlaskReduced = new RangeNode<float>(-20, -20, -10);
            BeFlaskDuration = new RangeNode<float>(10, 10, 20);
            BeAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Rings
            Ring = new ToggleNode(true);
            RLife = new RangeNode<float>(55, 35, 80);
            REnergyShield = new RangeNode<int>(2, 1, 5);
            RTotalRes = new RangeNode<float>(65, 50, 100);
            RPhysDamage = new RangeNode<float>(10, 6, 12);
            RWeaponElemDamage = new RangeNode<float>(25, 20, 40);
            RAccuracy = new RangeNode<float>(350, 200, 400);
            RAttackSpeed = new RangeNode<float>(5, 5, 20);
            RCastSpped = new RangeNode<float>(5, 5, 20);
            RStrength = new RangeNode<float>(30, 25, 55);
            RIntelligence = new RangeNode<float>(30, 25, 55);
            RDexterity = new RangeNode<float>(30, 25, 55);
            RMana = new RangeNode<float>(60, 50, 80);
            RAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Amulets
            Amulet = new ToggleNode(true);
            ALife = new RangeNode<float>(55, 40, 80);
            AEnergyShield = new RangeNode<int>(2, 1, 5);
            ATotalRes = new RangeNode<float>(65, 40, 130);
            APhysDamage = new RangeNode<float>(10, 6, 12);
            AWeaponElemDamage = new RangeNode<float>(25, 20, 40);
            AAccuracy = new RangeNode<float>(350, 200, 400);
            ACritMult = new RangeNode<float>(25, 20, 40);
            ACritChance = new RangeNode<float>(25, 20, 40);
            ATotalElemSpellDmg = new RangeNode<float>(13, 10, 30);
            AStrength = new RangeNode<float>(30, 25, 55);
            AIntelligence = new RangeNode<float>(30, 25, 55);
            ADexterity = new RangeNode<float>(30, 25, 55);
            AMana = new RangeNode<float>(60, 50, 80);
            AAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Shields
            Shield = new ToggleNode(true);
            SLife = new RangeNode<float>(65, 60, 110);
            SEnergyShield = new RangeNode<int>(2, 1, 5);
            STotalRes = new RangeNode<float>(65, 70, 130);
            SStrength = new RangeNode<float>(30, 25, 55);
            SIntelligence = new RangeNode<float>(30, 25, 55);
            SDexterity = new RangeNode<float>(30, 25, 55);
            SSpellDamage = new RangeNode<float>(25, 25, 80);
            SSpellCritChance = new RangeNode<float>(75, 60, 110);
            SLifeCombo = new RangeNode<int>(3, 1, 3);
            SAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Weapons Caster
            WeaponCaster = new ToggleNode(true);
            WcTotalElemSpellDmg = new RangeNode<float>(80, 70, 130);
            WcSpellCritChance = new RangeNode<float>(80, 70, 150);
            WcToElemDamageSpell = new RangeNode<float>(30, 20, 90);
            WcCritMult = new RangeNode<float>(25, 20, 40);
            WcAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Weapons Attack
            WeaponAttack = new ToggleNode(true);
            WaPhysDmg = new RangeNode<float>(300, 250, 500);
            WaElemDmg = new RangeNode<float>(250, 200, 300);
            WaAffixes = new RangeNode<float>(1, 0, 2);
            #endregion
            #region Quivers

            Quiver = new ToggleNode(true);
            QLife = new RangeNode<float>(55, 40, 80);
            QTotalRes = new RangeNode<float>(65, 40, 130);
            QPhysDamage = new RangeNode<float>(10, 6, 12);
            QWeaponElemDamage = new RangeNode<float>(25, 20, 40);
            QAccuracy = new RangeNode<float>(350, 200, 400);
            QCritMult = new RangeNode<float>(25, 20, 40);
            QCritChance = new RangeNode<float>(25, 20, 40);
            QAffixes = new RangeNode<float>(2, 0, 5);

            #endregion
        }
        //Get Set

        public ToggleNode Enable { get; set; }

        #region Body Armour
        [Menu("Body Armour", 1, Tooltip = "Body Armour Filter Settings")]
        public ToggleNode BodyArmour { get; set; }

        [Menu("Life", parentIndex = 1, Tooltip = "Set the minimum value to filter life value")]
        public RangeNode<float> BaLife { get; set; }

        [Menu("Energy Shield (tier)", parentIndex = 1, Tooltip = "Set the minimum value to filter defence tier value")]
        public RangeNode<int> BaEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 1, Tooltip = "Set the minimum value to filter total resistance value")]
        public RangeNode<float> BaTotalRes { get; set; }

        [Menu("Strength", parentIndex = 1, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<float> BaStrength { get; set; }

        [Menu("Intelligence", parentIndex = 1, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> BaIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 1, Tooltip = "Set the minimum value to filter dexterity value")]
        public RangeNode<float> BaDexterity { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 1, Tooltip = "Set the minimum value to filter lifecombo tier value")]
        public RangeNode<int> BaLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 1, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> BaAffixes { get; set; }
        #endregion
        #region Helmet
        [Menu("Helmets", 2, Tooltip = "Helmet Filter Settings")]
        public ToggleNode Helmet { get; set; }

        [Menu("Life", parentIndex = 2, Tooltip = "Set the minimum value to filter Life value")]
        public RangeNode<float> HLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 2, Tooltip = "Set the minimum value to filter defence tier value")]
        public RangeNode<int> HEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 2, Tooltip = "Set the minimum value to filter total resistance value")]
        public RangeNode<float> HTotalRes { get; set; }

        [Menu("Accuracy", parentIndex = 2, Tooltip = "Set the minimum value to filter total accuracy value")]
        public RangeNode<float> HAccuracy { get; set; }

        [Menu("Strength", parentIndex = 2, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<float> HStrength { get; set; }

        [Menu("Intelligence", parentIndex = 2, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> HIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 2, Tooltip = "Set the minimum value to filter Dexterity value")]
        public RangeNode<float> HDexterity { get; set; }

        [Menu("Mana", parentIndex = 2, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<float> HMana { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 2, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> HLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 2, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> HAffixes { get; set; }
        #endregion
        #region Gloves
        [Menu("Gloves", 3, Tooltip = "Glove Filter Settings")]
        public ToggleNode Gloves { get; set; }

        [Menu("Life", parentIndex = 3, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> GLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 3, Tooltip = "Set the minimum value to filter for defence tier value")]
        public RangeNode<int> GEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 3, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> GTotalRes { get; set; }

        [Menu("Strength", parentIndex = 3, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<float> GStrength { get; set; }

        [Menu("Intelligence", parentIndex = 3, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> GIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 3, Tooltip = "Set the minimum value to filter Dexterity value")]
        public RangeNode<float> GDexterity { get; set; }

        [Menu("Accuracy", parentIndex = 3, Tooltip = "Set the minimum value to filter for accuracy value")]
        public RangeNode<float> GAccuracy { get; set; }

        [Menu("Attack Speed", parentIndex = 3, Tooltip = "Set the minimum value to filter for attack speed value")]
        public RangeNode<float> GAttackSpeed { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 3, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> GPhysDamage { get; set; }

        [Menu("Mana", parentIndex = 3, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<float> GMana { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 3, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> GLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 3, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> GAffixes { get; set; }
        #endregion
        #region Boots
        [Menu("Boots", 4, Tooltip = "Boot Filter Settings")]
        public ToggleNode Boots { get; set; }

        [Menu("Life", parentIndex = 4, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> BLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 4, Tooltip = "Set the minimum value to filter for defence tier value")]
        public RangeNode<int> BEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 4, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> BTotalRes { get; set; }

        [Menu("Strength", parentIndex = 4, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<float> BStrength { get; set; }

        [Menu("Intelligence", parentIndex = 4, Tooltip = "Set the minimum value to filter for intelligence value")]
        public RangeNode<float> BIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 4, Tooltip = "Set the minimum value to filter for Dexterity value")]
        public RangeNode<float> BDexterity { get; set; }

        [Menu("Movement Speed", parentIndex = 4, Tooltip = "Set the minimum value to filter for movement speed value")]
        public RangeNode<float> BMoveSpeed { get; set; }

        [Menu("Mana", parentIndex = 4, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<float> BMana { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 4, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> BLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 4, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> BAffixes { get; set; }
        #endregion  
        #region Belt
        [Menu("Belts", 5, Tooltip = "Belt Settings")]
        public ToggleNode Belt { get; set; }

        [Menu("Life", parentIndex = 5, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> BeLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 5, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> BeEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 5, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> BeTotalRes { get; set; }

        [Menu("Strength", parentIndex = 5, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<float> BeStrength { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 5, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> BeWeaponElemDamage { get; set; }

        [Menu("Flask Reduced", parentIndex = 5, Tooltip = "Set the minimum value to filter for Flask Reduced value")]
        public RangeNode<float> BeFlaskReduced { get; set; }

        [Menu("Flask Duration", parentIndex = 5, Tooltip = "Set the minimum value to filter for Flask Duration value")]
        public RangeNode<float> BeFlaskDuration { get; set; }

        [Menu("Affix Count", parentIndex = 5, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> BeAffixes { get; set; }
        //        [Menu("Other Flask Suffix", 67, 60)]
        //        public RangeNode<int> BeFlaskSuffix { get; set; }
        #endregion
        #region Ring
        [Menu("Rings", 6, Tooltip = "Ring Settings")]
        public ToggleNode Ring { get; set; }

        [Menu("Life", parentIndex = 6, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> RLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 6, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> REnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 6, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> RTotalRes { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 6, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> RPhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 6, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> RWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 6, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<float> RAccuracy { get; set; }

        [Menu("Attack Speed", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased attack speed value")]
        public RangeNode<float> RAttackSpeed { get; set; }

        [Menu("Cast Speed", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased cast speed value")]
        public RangeNode<float> RCastSpped { get; set; }

        [Menu("Strength", parentIndex = 6, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<float> RStrength { get; set; }

        [Menu("Intelligence", parentIndex = 6, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> RIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 6, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> RDexterity { get; set; }

        [Menu("Mana", parentIndex = 6, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<float> RMana { get; set; }

        [Menu("Affix Count", parentIndex = 6, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> RAffixes { get; set; }
        #endregion
        #region Amulet
        [Menu("Amulets", 7, Tooltip = "Amulet Settings")]
        public ToggleNode Amulet { get; set; }

        [Menu("Life", parentIndex = 7, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> ALife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 7, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> AEnergyShield { get; set; }

        [Menu("Total Resitance", parentIndex = 7, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> ATotalRes { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 7, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> APhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 7, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> AWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 7, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<float> AAccuracy { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 7, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<float> ACritMult { get; set; }

        [Menu("Critical Strike Chance", parentIndex = 7, Tooltip = "Set the minimum value to filter for critical strike chance value")]
        public RangeNode<float> ACritChance { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 7, Tooltip = "Set the minimum value to filter for total elemental spell Damage value")]
        public RangeNode<float> ATotalElemSpellDmg { get; set; }

        [Menu("Strength", parentIndex = 7, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<float> AStrength { get; set; }

        [Menu("Intelligence", parentIndex = 7, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> AIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 7, Tooltip = "Set the minimum value to filter Dexterity value")]
        public RangeNode<float> ADexterity { get; set; }

        [Menu("Mana", parentIndex = 7, Tooltip = "Set the minimum value to filter Mana value")]
        public RangeNode<float> AMana { get; set; }

        [Menu("Affix Count", parentIndex = 7, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> AAffixes { get; set; }
        #endregion
        #region Shield
        [Menu("Shields", 8, Tooltip = "Shield Filter Settings")]
        public ToggleNode Shield { get; set; }

        [Menu("Life", parentIndex = 8, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> SLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 8, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> SEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 8, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> STotalRes { get; set; }

        [Menu("Strength", parentIndex = 8, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<float> SStrength { get; set; }

        [Menu("Intelligence", parentIndex = 8, Tooltip = "Set the minimum value to filter for intelligence value")]
        public RangeNode<float> SIntelligence { get; set; }

        [Menu("Dexterity", parentIndex = 8, Tooltip = "Set the minimum value to filter for Dexterity value")]
        public RangeNode<float> SDexterity { get; set; }

        [Menu("Spell Damage", parentIndex = 8, Tooltip = "Set the minimum value to filter for spell damage value")]
        public RangeNode<float> SSpellDamage { get; set; }

        [Menu("Spell Critical Strike Chance", parentIndex = 8, Tooltip = "Set the minimum value to filter for spell critical strike chance value")]
        public RangeNode<float> SSpellCritChance { get; set; }

        [Menu("LifeCombo ((Tier)", parentIndex = 8, Tooltip = "Set the minimum value to filter life combo tier value")]
        public RangeNode<int> SLifeCombo { get; set; }

        [Menu("Affix Count", parentIndex = 8, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> SAffixes { get; set; }
        #endregion
        #region Caster Dagger/Wand/Sceptre
        [Menu("Cast Weapons", 9, Tooltip = "Cast Weapon Settings")]
        public ToggleNode WeaponCaster { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 9, Tooltip = "Set the minimum value to filter for total elemental spell damage value")]
        public RangeNode<float> WcTotalElemSpellDmg { get; set; }

        [Menu("Fire/Cold/Lightning Dmg to Spells", parentIndex = 9, Tooltip = "Set the minimum value to filter for % added elemental damage to spells value")]
        public RangeNode<float> WcToElemDamageSpell { get; set; }

        [Menu("Spell Critical Chance", parentIndex = 9, Tooltip = "Set the minimum value to filter for spell critical chance value")]
        public RangeNode<float> WcSpellCritChance { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 9, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<float> WcCritMult { get; set; }

        [Menu("Affix Count", parentIndex = 9, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> WcAffixes { get; set; }
        #endregion
        #region Attack Thrusting/Dagger/Wand
        [Menu("Attack Weapons", 10, Tooltip = "Attack Weapon Settings")]
        public ToggleNode WeaponAttack { get; set; }

        [Menu("Physical Damage", parentIndex = 10, Tooltip = "Set the minimum value to filter for total physical damage value")]
        public RangeNode<float> WaPhysDmg { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 10, Tooltip = "Set the minimum value to filter for total elemental spell damage value")]
        public RangeNode<float> WaElemDmg { get; set; }

        [Menu("Affix Count", parentIndex = 10, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> WaAffixes { get; set; }
        #endregion
        #region Quiver
        [Menu("Quiver", 20, Tooltip = "Quiver Settings")]
        public ToggleNode Quiver { get; set; }

        [Menu("Life", parentIndex = 20, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> QLife { get; set; }

        [Menu("Total Resitance", parentIndex = 20, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> QTotalRes { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 20, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> QPhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 20, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> QWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 20, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<float> QAccuracy { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 20, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<float> QCritMult { get; set; }

        [Menu("Critical Strike Chance", parentIndex = 20, Tooltip = "Set the minimum value to filter for critical strike chance value")]
        public RangeNode<float> QCritChance { get; set; }

        [Menu("Affix Count", parentIndex = 20, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> QAffixes { get; set; }
        #endregion

        #region Additional Menu Settings
        [Menu("Additional Menu Settings", 100)]
        public EmptyNode Settings { get; set; }

        [Menu("Hide under Mouse", parentIndex = 100, Tooltip = "Hide a star on mouse over")]
        public ToggleNode HideUnderMouse { get; set; }

        [Menu("Text or Border", parentIndex = 100, Tooltip = "Display a text or border around the filtered item")]
        public ToggleNode StarOrBorder { get; set; }

        [Menu("Text Color", parentIndex = 100, Tooltip = "Display color of star on or border around the Syndicate item")]
        public ColorNode Color { get; set; }

        [Menu("Frame Color", parentIndex = 100, Tooltip = "Display color of star on or border around the Shaper/Elder/ItemLevel")]
        public ColorNode ColorAll { get; set; }

        [Menu("Debug Mode", parentIndex = 100, Tooltip = "Enable debug mode to report collected information about items")]
        public ToggleNode DebugMode { get; set; }

        [Menu("Hotkey")]
        public HotkeyNode HotKey { get; set; }

        [Menu("Extra Delay", "Additional delay, plugin should work without extra delay, this is merely optional.")]
        public RangeNode<int> ExtraDelay { get; set; }

        [Menu("ItemLevel", "Set min ItemLevet for highlighted")]
        public RangeNode<int> ItemLevel { get; set; }
    } 
    #endregion
}