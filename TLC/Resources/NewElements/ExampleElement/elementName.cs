using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace modName.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class elementName : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.modName.hjson' file.
		public override void SetDefaults()
		{
			Item.width = 40;
			Item.height = 40;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
