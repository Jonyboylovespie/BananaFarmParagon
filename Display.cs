using Assets.Scripts.Models.Towers;
using Assets.Scripts.Unity.Display;
using BTD_Mod_Helper.Api.Display;

namespace Displays
{
public class Display : ModTowerDisplay<BananaFarmParagon.Paragon>
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
