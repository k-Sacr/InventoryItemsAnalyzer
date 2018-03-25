using PoeHUD.Hud.Settings;
using PoeHUD.Plugins;
using SharpDX;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzerSettings : SettingsBase
    {
        public InventoryItemsAnalyzerSettings()
        {
            //Defaults
            #region Enable/Additional Settings
            Enable = false;
            HideUnderMouse = true;
            StarOrBorder = true;
            Color = new ColorBGRA(255, 215, 0, 255);
            DebugMode = false;
            #endregion
            #region BodyArmour
            BodyArmour = true;
            BaLife = new RangeNode<float>(85, 60, 120);
            BaTotalRes = new RangeNode<float>(80, 60, 100);
            BaEnergyShield = new RangeNode<int>(2, 1, 5);
            BaStrength = new RangeNode<float>(40, 30, 55);
            BaIntelligence = new RangeNode<float>(40, 30, 55);
            BaAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Helmet
            Helmet = true;
            HLife = new RangeNode<float>(65, 40, 100);
            HEnergyShield = new RangeNode<int>(2, 1, 5);
            HTotalRes = new RangeNode<float>(80, 60, 100);
            HAccuracy = new RangeNode<float>(300, 200, 400);
            HIntelligence = new RangeNode<float>(40, 30, 55);
            HAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Gloves
            Gloves = true;
            GLife = new RangeNode<float>(65, 40, 100);
            GTotalRes = new RangeNode<float>(80, 40, 130);
            GEnergyShield = new RangeNode<int>(2, 1, 5);
            GAccuracy = new RangeNode<float>(300, 200, 400);
            GAttackSpeed = new RangeNode<float>(10, 7, 16);
            GPhysDamage = new RangeNode<float>(4, 2, 10);
            GDexterity = new RangeNode<float>(40, 30, 55);
            GAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Boots
            Boots = true;
            BLife = new RangeNode<float>(65, 40, 100);
            BTotalRes = new RangeNode<float>(70, 40, 130);
            BEnergyShield = new RangeNode<int>(2, 1, 5);
            BStrength = new RangeNode<float>(40, 30, 55);
            BIntelligence = new RangeNode<float>(40, 30, 55);
            BMoveSpeed = new RangeNode<float>(20, 0, 50);
            BAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Belts
            Belt = true;
            BeLife = new RangeNode<float>(70, 45, 100);
            BeEnergyShield = new RangeNode<int>(2, 1, 5);
            BeTotalRes = new RangeNode<float>(70, 40, 130);
            BeStrength = new RangeNode<float>(35, 25, 55);
            BeArmour = new RangeNode<int>(280, 200, 400);
            BeWeaponElemDamage = new RangeNode<float>(30, 20, 40);
            BeAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Rings
            Ring = true;
            RLife = new RangeNode<float>(55, 40, 80);
            REnergyShield = new RangeNode<int>(2, 1, 5);
            RTotalRes = new RangeNode<float>(80, 40, 130);
            RTotalAttrib = new RangeNode<float>(75, 50, 100);
            RPhysDamage = new RangeNode<float>(8, 6, 12);
            RWeaponElemDamage = new RangeNode<float>(30, 20, 40);
            RAccuracy = new RangeNode<float>(250, 200, 400);
            RManaRegen = new RangeNode<float>(50, 40, 70);
            RIncRarity = new RangeNode<float>(25, 20, 30);
            RAttackSpeed = new RangeNode<float>(5, 0, 20);
            RCastSpped = new RangeNode<float>(5, 0, 20);
            RAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Amulets
            Amulet = true;
            ALife = new RangeNode<float>(55, 40, 80);
            AEnergyShield = new RangeNode<int>(2, 1, 5);
            ATotalRes = new RangeNode<float>(80, 40, 130);
            ATotalAttrib = new RangeNode<float>(75, 50, 100);
            APhysDamage = new RangeNode<float>(8, 6, 12);
            AWeaponElemDamage = new RangeNode<float>(30, 20, 40);
            AAccuracy = new RangeNode<float>(250, 200, 400);
            AManaRegen = new RangeNode<float>(50, 40, 70);
            AIncRarity = new RangeNode<float>(25, 20, 30);
            ACritMult = new RangeNode<float>(30, 20, 40);
            ACritChance = new RangeNode<float>(30, 20, 40);
            ATotalElemSpellDmg = new RangeNode<float>(30, 20, 40);
            AAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Shields
            Shield = true;
            SLife = new RangeNode<float>(80, 60, 110);
            SEnergyShield = new RangeNode<int>(2, 1, 5);
            STotalRes = new RangeNode<float>(100, 70, 130);
            SStrength = new RangeNode<float>(35, 25, 55);
            SIntelligence = new RangeNode<float>(35, 25, 55);
            SSpellDamage = new RangeNode<float>(55, 40, 80);
            SSpellCritChance = new RangeNode<float>(80, 60, 110);
            SAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Weapons Caster
            WeaponCaster = true;
            WcTotalElemSpellDmg = new RangeNode<float>(90, 70, 130);
            WcSpellCritChance = new RangeNode<float>(130, 100, 150);
            WcToElemDamageSpell = new RangeNode<float>(50, 30, 90);
            WcCritMult = new RangeNode<float>(30, 20, 40);
            WcAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
            #region Weapons Attack
            WeaponAttack = true;
            WaPhysDmg = new RangeNode<float>(200, 100, 500);
            WaCritChance = new RangeNode<float>(30, 20, 40);
            WaCritMulti = new RangeNode<float>(30, 20, 40);
            WaElemDmg = new RangeNode<float>(130, 100, 300);
            WaDps = new RangeNode<float>(300, 200, 500);
            WaAffixes = new RangeNode<float>(2, 0, 5);
            #endregion
        }
        //Get Set
        #region Body Armour
        [Menu("Body Armour", 1, Tooltip = "Body Armour Filter Settings")]
        public ToggleNode BodyArmour { get; set; }

        [Menu("Life", parentIndex = 1, Tooltip = "Set the minimum value to filter life value")]
        public RangeNode<float> BaLife { get; set; }

        [Menu("Energy Shield (tier)", parentIndex = 1, Tooltip = "Set the minimum value to filter energy shield tier value")]
        public RangeNode<int> BaEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 1, Tooltip = "Set the minimum value to filter total resistance value")]
        public RangeNode<float> BaTotalRes { get; set; }

        [Menu("Strength", parentIndex = 1, Tooltip = "Set the minimum value to filter Strength value")]
        public RangeNode<float> BaStrength { get; set; }

        [Menu("Intelligence", parentIndex = 1, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> BaIntelligence { get; set; }

        [Menu("Affix Count", parentIndex = 1, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> BaAffixes { get; set; }
        #endregion
        #region Helmet
        [Menu("Helmets", 2, Tooltip = "Helmet Filter Settings")]
        public ToggleNode Helmet { get; set; }

        [Menu("Life", parentIndex = 2, Tooltip = "Set the minimum value to filter Life value")]
        public RangeNode<float> HLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 2, Tooltip = "Set the minimum value to filter energy shield tier value")]
        public RangeNode<int> HEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 2, Tooltip = "Set the minimum value to filter total tesistance value")]
        public RangeNode<float> HTotalRes { get; set; }

        [Menu("Accuracy", parentIndex = 2, Tooltip = "Set the minimum value to filter total accuracy value")]
        public RangeNode<float> HAccuracy { get; set; }

        [Menu("Intelligence", parentIndex = 2, Tooltip = "Set the minimum value to filter Intelligence value")]
        public RangeNode<float> HIntelligence { get; set; }

        [Menu("Affix Count", parentIndex = 2, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> HAffixes { get; set; }
        #endregion
        #region Gloves
        [Menu("Gloves", 3, Tooltip = "Glove Filter Settings")]
        public ToggleNode Gloves { get; set; }

        [Menu("Life", parentIndex = 3, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> GLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 3, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> GEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 3, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> GTotalRes { get; set; }

        [Menu("Dexterity", parentIndex = 3, Tooltip = "Set the minimum value to filter for dexterity value")]
        public RangeNode<float> GAccuracy { get; set; }

        [Menu("Accuracy", parentIndex = 3, Tooltip = "Set the minimum value to filter for accuracy value")]
        public RangeNode<float> GAttackSpeed { get; set; }

        [Menu("Attack Speed", parentIndex = 3, Tooltip = "Set the minimum value to filter for attack speed value")]
        public RangeNode<float> GPhysDamage { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 3, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> GDexterity { get; set; }

        [Menu("Affix Count", parentIndex = 3, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> GAffixes { get; set; }
        #endregion
        #region Boots
        [Menu("Boots", 4, Tooltip = "Boot Filter Settings")]
        public ToggleNode Boots { get; set; }

        [Menu("Life", parentIndex = 4, Tooltip = "Set the minimum value to filter for life value")]
        public RangeNode<float> BLife { get; set; }

        [Menu("Energy Shield (Tier)", parentIndex = 4, Tooltip = "Set the minimum value to filter for energy shield tier value")]
        public RangeNode<int> BEnergyShield { get; set; }

        [Menu("Total Resistance", parentIndex = 4, Tooltip = "Set the minimum value to filter for total resistance value")]
        public RangeNode<float> BTotalRes { get; set; }

        [Menu("Strength", parentIndex = 4, Tooltip = "Set the minimum value to filter for strength value")]
        public RangeNode<float> BStrength { get; set; }

        [Menu("Intelligence", parentIndex = 4, Tooltip = "Set the minimum value to filter for intelligence value")]
        public RangeNode<float> BMoveSpeed { get; set; }

        [Menu("Movement Speed", parentIndex = 4, Tooltip = "Set the minimum value to filter for movement speed value")]
        public RangeNode<float> BIntelligence { get; set; }

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

        [Menu("Armour", parentIndex = 5, Tooltip = "Set the minimum value to filter for armour value")]
        public RangeNode<int> BeArmour { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 5, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> BeWeaponElemDamage { get; set; }

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

        [Menu("Total Attributes", parentIndex = 6, Tooltip = "Set the minimum value to filter for total attributes value")]
        public RangeNode<float> RTotalAttrib { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 6, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> RPhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 6, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> RWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 6, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<float> RAccuracy { get; set; }

        [Menu("Mana Regeneration", parentIndex = 6, Tooltip = "Set the minimum value to filter for mana regeneration value")]
        public RangeNode<float> RManaRegen { get; set; }

        [Menu("Increased Rarity", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased rarity value")]
        public RangeNode<float> RIncRarity { get; set; }

        [Menu("Attack Speed", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased attack speed value")]
        public RangeNode<float> RAttackSpeed { get; set; }

        [Menu("Cast Speed", parentIndex = 6, Tooltip = "Set the minimum value to filter for increased cast speed value")]
        public RangeNode<float> RCastSpped { get; set; }

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

        [Menu("Total Attributes", parentIndex = 7, Tooltip = "Set the minimum value to filter for total attributes value")]
        public RangeNode<float> ATotalAttrib { get; set; }

        [Menu("Physical Damage to Attacks", parentIndex = 7, Tooltip = "Set the minimum value to filter for physical damage to attacks value")]
        public RangeNode<float> APhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", parentIndex = 7, Tooltip = "Set the minimum value to filter for weapon elemental damage value")]
        public RangeNode<float> AWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", parentIndex = 7, Tooltip = "Set the minimum value to filter for accuracy rating value")]
        public RangeNode<float> AAccuracy { get; set; }

        [Menu("Mana Regeneration", parentIndex = 7, Tooltip = "Set the minimum value to filter for mana regeneration value")]
        public RangeNode<float> AManaRegen { get; set; }

        [Menu("Increased Rarity", parentIndex = 7, Tooltip = "Set the minimum value to filter for increased rarity value")]
        public RangeNode<float> AIncRarity { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 7, Tooltip = "Set the minimum value to filter for critical strike multiplier value")]
        public RangeNode<float> ACritMult { get; set; }

        [Menu("Critical Strike Chance", parentIndex = 7, Tooltip = "Set the minimum value to filter for critical strike chance value")]
        public RangeNode<float> ACritChance { get; set; }

        [Menu("Total Elemental Spell Damage", parentIndex = 7, Tooltip = "Set the minimum value to filter for total elemental spell Damage value")]
        public RangeNode<float> ATotalElemSpellDmg { get; set; }

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

        [Menu("Spell Damage", parentIndex = 8, Tooltip = "Set the minimum value to filter for spell damage value")]
        public RangeNode<float> SSpellDamage { get; set; }

        [Menu("Spell Critical Strike Chance", parentIndex = 8, Tooltip = "Set the minimum value to filter for spell critical strike chance value")]
        public RangeNode<float> SSpellCritChance { get; set; }

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

        [Menu("DPS", parentIndex = 10, Tooltip = "Set the minimum value to filter for total weapon DPS value")]
        public RangeNode<float> WaDps { get; set; }

        [Menu("Spell Critical Chance", parentIndex = 10, Tooltip = "Set the minimum value to filter for attack critical chance value")]
        public RangeNode<float> WaCritChance { get; set; }

        [Menu("Critical Strike Multiplier", parentIndex = 10, Tooltip = "Set the minimum value to filter for  attack critical strike multiplier value")]
        public RangeNode<float> WaCritMulti { get; set; }

        [Menu("Affix Count", parentIndex = 10, Tooltip = "Set the minimum number of affixes for this item class")]
        public RangeNode<float> WaAffixes { get; set; }
        #endregion
        #region Additional Menu Settings
        [Menu("Additional Menu Settings", 100)]
        public EmptyNode Settings { get; set; }

        [Menu("Hide under Mouse", parentIndex = 100, Tooltip = "Hide a star on mouse over")]
        public ToggleNode HideUnderMouse { get; set; }

        [Menu("Star or Border", parentIndex = 100, Tooltip = "Display a star on or border around the filtered item")]
        public ToggleNode StarOrBorder { get; set; }

        [Menu("Star/Border Color", parentIndex = 100, Tooltip = "Display color of star on or border around the filtered item")]
        public ColorNode Color { get; set; }

        [Menu("Debug Mode", parentIndex = 100, Tooltip = "Enable debug mode to report collected information about items")]
        public ToggleNode DebugMode { get; set; }
    } 
    #endregion
}