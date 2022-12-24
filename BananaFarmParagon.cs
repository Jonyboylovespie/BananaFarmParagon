using Assets.Scripts.Models.Towers;
using Assets.Scripts.Models.Towers.Behaviors;
using Assets.Scripts.Models.Towers.Projectiles.Behaviors;
using Assets.Scripts.Models.Towers.Weapons.Behaviors;
using Assets.Scripts.Unity;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api;
using BTD_Mod_Helper.Api.Display;
using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;
using MelonLoader;
using ModHelperData = BananaFarmParagon.ModHelperData;
using BTD_Mod_Helper.Api.ModOptions;
using Assets.Scripts.Unity.UI_New.Popups;
using BananaFarmParagon.bananafarmfake;
using Assets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Assets.Scripts.Utils;
using Assets.Scripts.Models.Towers.Behaviors.Attack;
using weapondisplays;
using BTD_Mod_Helper.Api.Data;
using Assets.Scripts.Models;
using Assets.Scripts.Models.Towers.Mods;
using BTD_Mod_Helper.Api.Helpers;
using Assets.Scripts.Models.TowerSets;

[assembly: MelonInfo(typeof(BananaFarmParagon.Main), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace weapondisplays
{
    public class BananaDisplay : ModDisplay
    {
        public override string BaseDisplay => Generic2dDisplay;

        public override void ModifyDisplayNode(UnityDisplayNode node)
        {
            Set2DTexture(node, "BananaDisplay");
        }
    }
}

namespace BananaFarmParagon
{
    public class Main : BloonsTD6Mod
    {
        public override void OnNewGameModel(GameModel gameModel, Il2CppSystem.Collections.Generic.List<ModModel> mods)
        {

            gameModel.GetParagonUpgradeForTowerId("BananaFarm").cost = CostHelper.CostForDifficulty(Settings.ParagonCost, mods);

            foreach (var towerModel in gameModel.towers)
            {
                if (towerModel.appliedUpgrades.Contains(ModContent.UpgradeID<Paragon.BananaFarmParagonUpgrade>()))
                {
                    if (Settings.ParagonTowerOp == true)
                    {
                        var attackModel = towerModel.GetAttackModel();
                        var projectile = attackModel.weapons[0].projectile;
                        var weapon = attackModel.weapons[0];
                        projectile.GetBehavior<CashModel>().minimum = 18446744073709551615;
                        projectile.GetBehavior<CashModel>().maximum = 18446744073709551615;
                        projectile.GetBehavior<CashModel>().salvage = 18446744073709551615;
                        weapon.GetBehavior<EmissionsPerRoundFilterModel>().count = 100000;
                    }
                    else
                    {
                        var attackModel = towerModel.GetAttackModel();
                        var projectile = attackModel.weapons[0].projectile;
                        var weapon = attackModel.weapons[0];
                        projectile.GetBehavior<CashModel>().minimum = 100000;
                        projectile.GetBehavior<CashModel>().maximum = 100000;
                        projectile.GetBehavior<CashModel>().salvage = 1;
                        weapon.GetBehavior<EmissionsPerRoundFilterModel>().count = 10;
                    }
                }
            }
        }
        public override void OnMainMenu()
        {
            base.OnMainMenu();

            //Op melon msg

            
                if (Settings.ParagonTowerOp == false)
                {
                    MelonLogger.Msg(System.ConsoleColor.Yellow, "The balanced version of the banana farm paragon has been loaded.");
                    if (Settings.TogglePopup == true)
                    {
                        PopupScreen.instance.ShowOkPopup("The balanced version of the banana farm paragon has been loaded.");
                    }
                }
                else
                {
                    MelonLogger.Msg(System.ConsoleColor.Yellow, "The OP version of the banana farm paragon has been loaded.");
                    if (Settings.TogglePopup == true)
                    {
                        PopupScreen.instance.ShowOkPopup("The OP version of the banana farm paragon has been loaded.");
                    }
                }
            
        }
        public override void OnApplicationStart()
        {
            MelonLogger.Msg(System.ConsoleColor.Yellow, "The great one... Is ready...");
        }
    }
    public class Settings : ModSettings
    {
        public static readonly ModSettingBool ParagonTowerOp = new(false)
        {
            displayName = "OP Mode of Banana Farm Paragon",
            description = "Toggles the OP mode of the banana farm paragon",
            button = true,
        };
        public static readonly ModSettingBool TogglePopup = new(true)
        {
            displayName = "Toggle Popup",
            description = "Toggles the popup on main menu that tells you what version of the banana farm paragon you are on",
            button = true,
        };
        public static readonly ModSettingInt ParagonCost = new(1000000)
        {
            displayName = "Banana Farm Paragon Cost",
            min = 0
        };
    }
    public class Paragon
    {
        public class BananaFarmParagon : ModVanillaParagon
        {
            public override string BaseTower => "BananaFarm-500";
            public override string Name => "BananaFarm";
        }
        public class BananaFarmParagonUpgrade : ModParagonUpgrade<BananaFarmParagon>
        {
            public override int Cost => 1000000;
            public override string Description => "The banana's are too strong...not even the monkeys can control the power...";
            public override string DisplayName => "The Pinacle Of Banana Production";

            public override string Icon => "BananaFarmParagon-Icon";
            public override string Portrait => "BananaFarmParagon-Portrait";
            public override void ApplyUpgrade(TowerModel towerModel)
            {
                towerModel.range += 99999999999999;
                var attackModel = towerModel.GetAttackModel();
                attackModel.range += 99999999999999;
                var projectile = attackModel.weapons[0].projectile;
                var weapon = attackModel.weapons[0];
                towerModel.RemoveBehavior<BananaCentralBuffModel>();
                projectile.GetBehavior<CashModel>().minimum = 100000;
                projectile.GetBehavior<CashModel>().maximum = 100000;
                projectile.GetBehavior<CashModel>().salvage = 1;
                weapon.GetBehavior<EmissionsPerRoundFilterModel>().count = 10;
                towerModel.AddBehavior(Game.instance.model.GetTowerFromId("MonkeyVillage-004").GetBehavior<MonkeyCityIncomeSupportModel>().Duplicate());
                towerModel.GetBehavior<MonkeyCityIncomeSupportModel>().incomeModifier = 10;
                towerModel.GetBehavior<MonkeyCityIncomeSupportModel>().buffIconName = "";
                towerModel.GetBehavior<MonkeyCityIncomeSupportModel>().onlyShowBuffIfMutated = false;
                towerModel.GetBehavior<MonkeyCityIncomeSupportModel>().showBuffIcon = true;
                towerModel.GetBehavior<MonkeyCityIncomeSupportModel>().isGlobal = true;
                projectile.GetBehavior<AgeModel>().Lifespan = 0;
                projectile.GetBehavior<AgeModel>().rounds = 9999999;
                var farm = Game.instance.model.GetTowerFromId("EngineerMonkey-200").GetAttackModel().Duplicate();
                farm.range = towerModel.range;
                farm.name = "Farm_Weapon";
                farm.weapons[0].Rate = 100f;
                farm.weapons[0].projectile.RemoveBehavior<CreateTowerModel>();
                farm.weapons[0].projectile.ApplyDisplay<BananaDisplay>();
                farm.weapons[0].projectile.AddBehavior(new CreateTowerModel("BananaFarm000place", GetTowerModel<BananaFarmer>().Duplicate(), 0f, true, true, false, true, true));
                towerModel.AddBehavior(farm);
                farm.RemoveBehavior<RotateToTargetModel>();
            }
            public class BananaFarmParagonDisplay : ModTowerDisplay<BananaFarmParagon>
            {
                public override string BaseDisplay => GetDisplay(TowerType.BananaFarm, 4, 0, 0);
                public override bool UseForTower(int[] tiers)
                { return IsParagon(tiers); }
                public override void ModifyDisplayNode(UnityDisplayNode node)
                {
                    SetMeshTexture(node, "texture1");
                }
            }
        }
    }

    namespace bananafarmfake
    {
        public class BananaFarmer : ModTower
        {
            public override string Portrait => "BananaCostumeFarmerPortrait";
            public override string Name => "Banan";
            public override TowerSet TowerSet => TowerSet.Support;
            public override string BaseTower => TowerType.DartMonkey;
            public override bool DontAddToShop => true;
            public override int Cost => 0;
            public override int TopPathUpgrades => 0;
            public override int MiddlePathUpgrades => 0;
            public override int BottomPathUpgrades => 0;


            public override string DisplayName => "Banan";
            public override string Description => "";

            public override void ModifyBaseTowerModel(TowerModel towerModel)
            {
                towerModel.RemoveBehaviors<AttackModel>();
                towerModel.isSubTower = true;
                towerModel.AddBehavior(new TowerExpireModel("ExpireModel", 45f, 3, false, false));
                towerModel.display = new PrefabReference() { guidRef = "cd01e5de10343944ea24e6a6b3690b3a" };
                towerModel.range += 0;
                towerModel.AddBehavior(Game.instance.model.GetTowerFromId("BananaFarm-005").GetBehavior<CollectCashZoneModel>().Duplicate());
                towerModel.GetBehavior<CollectCashZoneModel>().collectRange = 1;
                towerModel.GetBehavior<CollectCashZoneModel>().attractRange = 9999999;
                towerModel.GetBehavior<CollectCashZoneModel>().speed = 1;
            }
        }
    }
}