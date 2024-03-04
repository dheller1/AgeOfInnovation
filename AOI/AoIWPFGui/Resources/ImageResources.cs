using AoICore.Buildings;
using AoICore.Map;
using System.Windows.Media.Imaging;
using static AoICore.Buildings.BuildingTypes;

namespace AoIWPFGui.Resources
{
	internal static class ImageResources
	{
		public static BitmapImage GetBuilding(BuildingType typ, Terrain color) {
			if(typ == BuildingTypes.Guild) {
				return AppResources.Get<BitmapImage>("GuildYellow");
			}
			else if(typ == BuildingTypes.Workshop) {
				return AppResources.Get<BitmapImage>("WorkshopYellow");
			}
			throw new ArgumentException("Unexpected buildingt type", nameof(typ));
		}
	}
}
