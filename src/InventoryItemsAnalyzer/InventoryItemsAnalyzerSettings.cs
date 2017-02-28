using PoeHUD.Plugins;
using PoeHUD.Hud.Settings;
using SharpDX;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzerSettings : SettingsBase
    {
        public InventoryItemsAnalyzerSettings()
        {
            Enable = false;
            HideUnderMouse = true;
            StarOrBorder = true;
            AmountAffixes = new RangeNode<float>(2, 0, 5);
            Color = new ColorBGRA(255, 215, 0, 255);

            BodyArmour = true;
            BaLife = new RangeNode<float>(85, 60, 120);
            BaTotalRes = new RangeNode<float>(80, 60, 100);
            BaEnergyShield = new RangeNode<int>(2, 0, 5);
            BaStrength = new RangeNode<float>(40, 30, 55);
            BaIntelligence = new RangeNode<float>(40, 30, 55);

            Helmet = true;
            HLife = new RangeNode<float>(65, 40, 100);
            HEnergyShield = new RangeNode<int>(2, 0, 5);
            HTotalRes = new RangeNode<float>(80, 60, 100);
            HAccuracy = new RangeNode<float>(300, 200, 400);
            HIntelligence = new RangeNode<float>(40, 30, 55);

            Boots = true;
            BLife = new RangeNode<float>(65, 40, 100);
            BTotalRes = new RangeNode<float>(70, 40, 130);
            BEnergyShield = new RangeNode<int>(2, 0, 5);
            BStrength = new RangeNode<float>(40, 30, 55);
            BIntelligence = new RangeNode<float>(40, 30, 55);

            Gloves = true;
            GLife = new RangeNode<float>(65, 40, 100);
            GTotalRes = new RangeNode<float>(80, 40, 130);
            GEnergyShield = new RangeNode<int>(2, 0, 5);
            GAccuracy = new RangeNode<float>(300, 200, 400);
            GAttackSpeed = new RangeNode<float>(10, 7, 16);
            GDexterity = new RangeNode<float>(40, 30, 55);

            Shield = true;
            SLife = new RangeNode<float>(80, 60, 110);
            SEnergyShield = new RangeNode<int>(2, 0, 5);
            STotalRes = new RangeNode<float>(100, 70, 130);
            SStrength = new RangeNode<float>(35, 25, 55);
            SIntelligence = new RangeNode<float>(35, 25, 55);
            SSpellDamage = new RangeNode<float>(55, 40, 80);
            SSpellCritChance = new RangeNode<float>(80, 60, 110);

            Belt = true;
            BeLife = new RangeNode<float>(70, 45, 100);
            BeEnergyShield = new RangeNode<int>(2, 0, 5);
            BeTotalRes = new RangeNode<float>(70, 40, 130);
            BeStrength = new RangeNode<float>(35, 25, 55);
            BeArmour = new RangeNode<int>(280, 200, 400);
            BeWeaponElemDamage = new RangeNode<float>(30, 20, 40);

            Ring = true;
            RLife = new RangeNode<float>(55, 40, 80);
            REnergyShield = new RangeNode<int>(2, 0, 5);
            RTotalRes = new RangeNode<float>(80, 40, 130);
            RTotalAttrib = new RangeNode<float>(75, 50, 100);
            RPhysDamage = new RangeNode<float>(8, 6, 12);
            RWeaponElemDamage = new RangeNode<float>(30, 20, 40);
            RAccuracy = new RangeNode<float>(250, 200, 400);
            RManaRegen = new RangeNode<float>(50, 40, 70);
            RIncRarity = new RangeNode<float>(25, 20, 30);

            Amulet = true;
            ALife = new RangeNode<float>(55, 40, 80);
            AEnergyShield = new RangeNode<int>(2, 0, 5);
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
        }

        #region Body Armour

        [Menu("Body Armour", 10)]
        public ToggleNode BodyArmour { get; set; }

        [Menu("Life", 11, 10)]
        public RangeNode<float> BaLife { get; set; }

        [Menu("Energy Shield (tier)", 12, 10)]
        public RangeNode<int> BaEnergyShield { get; set; }

        [Menu("Total Resistance", 13, 10)]
        public RangeNode<float> BaTotalRes { get; set; }

        [Menu("Strength", 14, 10)]
        public RangeNode<float> BaStrength { get; set; }

        [Menu("Intelligence", 15, 10)]
        public RangeNode<float> BaIntelligence { get; set; }

        #endregion

        #region Helmet

        [Menu("Helmet", 20)]
        public ToggleNode Helmet { get; set; }

        [Menu("Life", 21, 20)]
        public RangeNode<float> HLife { get; set; }

        [Menu("Energy Shield (tier)", 22, 20)]
        public RangeNode<int> HEnergyShield { get; set; }

        [Menu("Total Resistance", 23, 20)]
        public RangeNode<float> HTotalRes { get; set; }

        [Menu("Accuracy", 24, 20)]
        public RangeNode<float> HAccuracy { get; set; }

        [Menu("Intelligence", 25, 20)]
        public RangeNode<float> HIntelligence { get; set; }


        #endregion

        #region Boots
        [Menu("Boots", 30)]
        public ToggleNode Boots { get; set; }

        [Menu("Life", 31, 30)]
        public RangeNode<float> BLife { get; set; }

        [Menu("Energy Shield (tier)", 32, 30)]
        public RangeNode<int> BEnergyShield { get; set; }

        [Menu("Total Resistance", 33, 30)]
        public RangeNode<float> BTotalRes { get; set; }

        [Menu("Strength", 34, 30)]
        public RangeNode<float> BStrength { get; set; }

        [Menu("Intelligence", 35, 30)]
        public RangeNode<float> BIntelligence { get; set; }

        #endregion

        #region Gloves
        [Menu("Gloves", 40)]
        public ToggleNode Gloves { get; set; }

        [Menu("Life", 41, 40)]
        public RangeNode<float> GLife { get; set; }

        [Menu("Energy Shield (tier)", 42, 40)]
        public RangeNode<int> GEnergyShield { get; set; }

        [Menu("Total Resistance", 43, 40)]
        public RangeNode<float> GTotalRes { get; set; }

        [Menu("Accuracy", 44, 40)]
        public RangeNode<float> GAccuracy { get; set; }

        [Menu("Attack Speed", 45, 40)]
        public RangeNode<float> GAttackSpeed { get; set; }

        [Menu("Dexterity", 46, 40)]
        public RangeNode<float> GDexterity { get; set; }
        #endregion

        #region Shield
        [Menu("Shield", 50)]
        public ToggleNode Shield { get; set; }

        [Menu("Life", 51, 50)]
        public RangeNode<float> SLife { get; set; }

        [Menu("Energy Shield (tier)", 52, 50)]
        public RangeNode<int> SEnergyShield { get; set; }

        [Menu("Total Resistance", 53, 50)]
        public RangeNode<float> STotalRes { get; set; }

        [Menu("Strength", 54, 30)]
        public RangeNode<float> SStrength { get; set; }

        [Menu("Intelligence", 55, 30)]
        public RangeNode<float> SIntelligence { get; set; }

        [Menu("Spell Damage", 56, 50)]
        public RangeNode<float> SSpellDamage { get; set; }

        [Menu("Spell Critical Strike Chance", 57, 50)]
        public RangeNode<float> SSpellCritChance { get; set; }
        #endregion

        #region Belt
        [Menu("Belt", 60)]
        public ToggleNode Belt { get; set; }

        [Menu("Life", 61, 60)]
        public RangeNode<float> BeLife { get; set; }

        [Menu("Energy Shield (tier)", 62, 60)]
        public RangeNode<int> BeEnergyShield { get; set; }

        [Menu("Total Resistance", 63, 60)]
        public RangeNode<float> BeTotalRes { get; set; }

        [Menu("Strength", 64, 60)]
        public RangeNode<float> BeStrength { get; set; }

        [Menu("Armour", 65, 60)]
        public RangeNode<int> BeArmour { get; set; }

        [Menu("Weapon Elemental Damage", 66, 60)]
        public RangeNode<float> BeWeaponElemDamage { get; set; }

        //        [Menu("Other Flask Suffix", 67, 60)]
        //        public RangeNode<int> BeFlaskSuffix { get; set; }
        #endregion


        #region Ring
        [Menu("Ring", 70)]
        public ToggleNode Ring { get; set; }

        [Menu("Life", 71, 70)]
        public RangeNode<float> RLife { get; set; }

        [Menu("Energy Shield (tier)", 72, 70)]
        public RangeNode<int> REnergyShield { get; set; }

        [Menu("Total Resistance", 73, 70)]
        public RangeNode<float> RTotalRes { get; set; }

        [Menu("Total Attributes", 74, 70)]
        public RangeNode<float> RTotalAttrib { get; set; }

        [Menu("Physical Damage to Attacks", 75, 70)]
        public RangeNode<float> RPhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", 76, 70)]
        public RangeNode<float> RWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", 77, 70)]
        public RangeNode<float> RAccuracy { get; set; }

        [Menu("Mana Regeneration", 78, 70)]
        public RangeNode<float> RManaRegen { get; set; }

        [Menu("Increased Rarity", 79, 70)]
        public RangeNode<float> RIncRarity { get; set; }

        #endregion


        #region Amulet
        [Menu("Amulet", 80)]
        public ToggleNode Amulet { get; set; }

        [Menu("Life", 81, 80)]
        public RangeNode<float> ALife { get; set; }

        [Menu("Energy Shield (tier)", 82, 80)]
        public RangeNode<int> AEnergyShield { get; set; }

        [Menu("Total Resistance", 83, 80)]
        public RangeNode<float> ATotalRes { get; set; }

        [Menu("Total Attributes", 84, 80)]
        public RangeNode<float> ATotalAttrib { get; set; }

        [Menu("Physical Damage to Attacks", 85, 80)]
        public RangeNode<float> APhysDamage { get; set; }

        [Menu("Weapon Elemental Damage", 86, 80)]
        public RangeNode<float> AWeaponElemDamage { get; set; }

        [Menu("Accuracy Rating", 87, 80)]
        public RangeNode<float> AAccuracy { get; set; }

        [Menu("Mana Regeneration", 88, 80)]
        public RangeNode<float> AManaRegen { get; set; }

        [Menu("Increased Rarity", 89, 80)]
        public RangeNode<float> AIncRarity { get; set; }

        [Menu("Critical Strike Multiplier", 90, 80)]
        public RangeNode<float> ACritMult { get; set; }

        [Menu("Critical Strike Chance", 91, 80)]
        public RangeNode<float> ACritChance { get; set; }

        [Menu("Total Elemental Spell Damage", 92, 80)]
        public RangeNode<float> ATotalElemSpellDmg { get; set; }

        #endregion

        [Menu("Right amount of affixes")]
        public RangeNode<float> AmountAffixes { get; set; }

        [Menu("Hide under mouse?")]
        public ToggleNode HideUnderMouse { get; set; }

        [Menu("Star or border")]
        public ToggleNode StarOrBorder { get; set; }

        [Menu("Color")]
        public ColorNode Color { get; set; }


        
    }
}