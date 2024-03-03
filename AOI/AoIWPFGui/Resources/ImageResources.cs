using AoICore.Buildings;
using AoICore.Map;
using System.Windows.Media.Imaging;

namespace AoIWPFGui.Resources
{
	internal static class ImageResources
	{
		public static BitmapImage GetBuilding(BuildingType typ, Terrain color) {
			switch(typ) {
				case BuildingType.Guild:
					return AppResources.Get<BitmapImage>("GuildYellow");
				case BuildingType.Workshop:
				default:
					return AppResources.Get<BitmapImage>("WorkshopYellow");
			}
		}
	}
}
