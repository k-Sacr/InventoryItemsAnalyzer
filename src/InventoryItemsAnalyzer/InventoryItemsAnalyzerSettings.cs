using PoeHUD.Hud.Settings;
using SharpDX;

namespace InventoryItemsAnalyzer
{
    public class InventoryItemsAnalyzerSettings : SettingsBase
    {
        public InventoryItemsAnalyzerSettings()
        {
            //Defualts
            #region Enable/Additional Settings
            Enable = false;
            HideUnderMouse = true;
            StarOrBorder = true;
            AmountAffixes = new RangeNode<float>(2, 0, 5);
            Color = new ColorBGRA(255, 215, 0, 255);
            #endregion
            #region BodyArmour
            BodyArmour = true;
            BaLife = new RangeNode<float>(85, 60, 120);
            BaTotalRes = new RangeNode<float>(80, 60, 100);
            BaEnergyShield = new RangeNode<int>(2, 0, 5);
            BaStrength = new RangeNode<float>(40, 30, 55);
            BaIntelligence = new RangeNode<float>(40, 30, 55);
            #endregion
            #region Helmet
            Helmet = true;
            HLife = new RangeNode<float>(65, 40, 100);
            HEnergyShield = new RangeNode<int>(2, 0, 5);
            HTotalRes = new RangeNode<float>(80, 60, 100);
            HAccuracy = new RangeNode<float>(300, 200, 400);
            HIntelligence = new RangeNode<float>(40, 30, 55);
            #endregion
            #region Gloves
            Gloves = true;
            GLife = new RangeNode<float>(65, 40, 100);
            GTotalRes = new RangeNode<float>(80, 40, 130);
            GEnergyShield = new RangeNode<int>(2, 0, 5);
            GAccuracy = new RangeNode<float>(300, 200, 400);
            GAttackSpeed = new RangeNode<float>(10, 7, 16);
            GDexterity = new RangeNode<float>(40, 30, 55);
            #endregion
            #region Boots
            Boots = true;
            BLife = new RangeNode<float>(65, 40, 100);
            BTotalRes = new RangeNode<float>(70, 40, 130);
            BEnergyShield = new RangeNode<int>(2, 0, 5);
            BStrength = new RangeNode<float>(40, 30, 55);
            BIntelligence = new RangeNode<float>(40, 30, 55);
            #endregion
            #region Belts
            Belt = true;
            BeLife = new RangeNode<float>(70, 45, 100);
            BeEnergyShield = new RangeNode<int>(2, 0, 5);
            BeTotalRes = new RangeNode<float>(70, 40, 130);
            BeStrength = new RangeNode<float>(35, 25, 55);
            BeArmour = new RangeNode<int>(280, 200, 400);
            BeWeaponElemDamage = new RangeNode<float>(30, 20, 40);
            #endregion
            #region Rings
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
            RAttackSpeed = new RangeNode<float>(5, 0, 20);
            RCastSpped = new RangeNode<float>(5, 0, 20);
            #endregion
            #region Amulets
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
            #endregion
            #region Sheilds
            Shield = true;
            SLife = new RangeNode<float>(80, 60, 110);
            SEnergyShield = new RangeNode<int>(2, 0, 5);
            STotalRes = new RangeNode<float>(100, 70, 130);
            SStrength = new RangeNode<float>(35, 25, 55);
            SIntelligence = new RangeNode<float>(35, 25, 55);
            SSpellDamage = new RangeNode<float>(55, 40, 80);
            SSpellCritChance = new RangeNode<float>(80, 60, 110);
            #endregion
            #region Weapons Caster
            WeaponCaster = true;
            WcTotalElemSpellDmg = new RangeNode<float>(90, 70, 130);
            WcSpellCritChance = new RangeNode<float>(130, 100, 150);
            WcToElemDamageSpell = new RangeNode<float>(50, 30, 90);
            WcCritMult = new RangeNode<float>(30, 20, 40);
            #endregion
            #region Weapons Attack
            WeaponAttack = true;
            WaPhysDmg = new RangeNode<float>(200, 100, 500);
            WaCritChance = new RangeNode<float>(30, 20, 40);
            WaCritMulti = new RangeNode<float>(30, 20, 40);
            WaElemDmg = new RangeNode<float>(130, 100, 300);
            WaDps = new RangeNode<float>(300, 200, 500); 
            #endregion
        }
        //Get Set
        #region Body Armour
        public ToggleNode BodyArmour { get; set; }
        public RangeNode<float> BaLife { get; set; }
        public RangeNode<int> BaEnergyShield { get; set; }
        public RangeNode<float> BaTotalRes { get; set; }
        public RangeNode<float> BaStrength { get; set; }
        public RangeNode<float> BaIntelligence { get; set; }
        #endregion
        #region Helmet
        public ToggleNode Helmet { get; set; }
        public RangeNode<float> HLife { get; set; }
        public RangeNode<int> HEnergyShield { get; set; }
        public RangeNode<float> HTotalRes { get; set; }
        public RangeNode<float> HAccuracy { get; set; }
        public RangeNode<float> HIntelligence { get; set; }
        #endregion
        #region Gloves
       public ToggleNode Gloves { get; set; }
        public RangeNode<float> GLife { get; set; }
        public RangeNode<int> GEnergyShield { get; set; }
        public RangeNode<float> GTotalRes { get; set; }
        public RangeNode<float> GAccuracy { get; set; }
        public RangeNode<float> GAttackSpeed { get; set; }
        public RangeNode<float> GDexterity { get; set; }
        #endregion
        #region Boots
       public ToggleNode Boots { get; set; }
        public RangeNode<float> BLife { get; set; }
        public RangeNode<int> BEnergyShield { get; set; }
        public RangeNode<float> BTotalRes { get; set; }
        public RangeNode<float> BStrength { get; set; }
        public RangeNode<float> BIntelligence { get; set; }
        #endregion  
        #region Belt
        public ToggleNode Belt { get; set; }
        public RangeNode<float> BeLife { get; set; }
        public RangeNode<int> BeEnergyShield { get; set; }
        public RangeNode<float> BeTotalRes { get; set; }
        public RangeNode<float> BeStrength { get; set; }
        public RangeNode<int> BeArmour { get; set; }
        public RangeNode<float> BeWeaponElemDamage { get; set; }
        //        [Menu("Other Flask Suffix", 67, 60)]
        //        public RangeNode<int> BeFlaskSuffix { get; set; }
        #endregion
        #region Ring
        public ToggleNode Ring { get; set; }
        public RangeNode<float> RLife { get; set; }
        public RangeNode<int> REnergyShield { get; set; }
        public RangeNode<float> RTotalRes { get; set; }
        public RangeNode<float> RTotalAttrib { get; set; }
        public RangeNode<float> RPhysDamage { get; set; }
        public RangeNode<float> RWeaponElemDamage { get; set; }
        public RangeNode<float> RAccuracy { get; set; }
        public RangeNode<float> RManaRegen { get; set; }
        public RangeNode<float> RIncRarity { get; set; }
        public RangeNode<float> RAttackSpeed { get; set; }
        public RangeNode<float> RCastSpped { get; set; }

        #endregion
        #region Amulet
        public ToggleNode Amulet { get; set; }
        public RangeNode<float> ALife { get; set; }
        public RangeNode<int> AEnergyShield { get; set; }
        public RangeNode<float> ATotalRes { get; set; }
        public RangeNode<float> ATotalAttrib { get; set; }
        public RangeNode<float> APhysDamage { get; set; }
        public RangeNode<float> AWeaponElemDamage { get; set; }
        public RangeNode<float> AAccuracy { get; set; }
        public RangeNode<float> AManaRegen { get; set; }
        public RangeNode<float> AIncRarity { get; set; }
        public RangeNode<float> ACritMult { get; set; }
        public RangeNode<float> ACritChance { get; set; }
        public RangeNode<float> ATotalElemSpellDmg { get; set; }
        #endregion
        #region Shield
        public ToggleNode Shield { get; set; }
        public RangeNode<float> SLife { get; set; }
        public RangeNode<int> SEnergyShield { get; set; }
        public RangeNode<float> STotalRes { get; set; }
        public RangeNode<float> SStrength { get; set; }
        public RangeNode<float> SIntelligence { get; set; }
        public RangeNode<float> SSpellDamage { get; set; }
        public RangeNode<float> SSpellCritChance { get; set; }
        #endregion
        #region Caster Dagger/Wand/Sceptre
        public ToggleNode WeaponCaster { get; set; }
        public RangeNode<float> WcTotalElemSpellDmg { get; set; }
        public RangeNode<float> WcToElemDamageSpell { get; set; }
       public RangeNode<float> WcSpellCritChance { get; set; }
       public RangeNode<float> WcCritMult { get; set; }
        #endregion
        #region Attack Thrusting/Dagger/Wand
        public ToggleNode WeaponAttack { get; set; }
        public RangeNode<float> WaPhysDmg { get; set; }
        public RangeNode<float> WaElemDmg { get; set; }
        public RangeNode<float> WaDps { get; set; }
        public RangeNode<float> WaCritChance { get; set; }
        public RangeNode<float> WaCritMulti { get; set; }
        #endregion
        #region Additional Menu Settings
       public RangeNode<float> AmountAffixes { get; set; }
        public ToggleNode HideUnderMouse { get; set; }
        public ToggleNode StarOrBorder { get; set; }
        public ColorNode Color { get; set; }
    } 
    #endregion
}