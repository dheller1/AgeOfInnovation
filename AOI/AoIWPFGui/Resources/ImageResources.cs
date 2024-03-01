using AoICore.Buildings;
using AoICore.Map;
using System.Windows.Media.Imaging;

namespace AoIWPFGui.Resources
{
	internal static class ImageResources
	{
		public static BitmapImage GetBuilding(BuildingType typ, Terrain color) => AppResources.Get<BitmapImage>("WorkshopYellow");
	}
}
