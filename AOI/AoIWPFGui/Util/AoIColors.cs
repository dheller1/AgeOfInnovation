using AoICore.Map;
using System.Windows.Media;

namespace AoIWPFGui.Util
{
	internal static class AoIColors
	{
		internal static Color TerrainColor(Terrain terrain) {
			return terrain switch {
				Terrain.Plains => Colors.SaddleBrown,
				Terrain.Wasteland => Colors.Orange,
				Terrain.Mountain => Colors.Gray,
				Terrain.Swamp => Colors.DarkSlateGray,
				Terrain.Lake => Colors.Blue,
				Terrain.Forest => Colors.DarkGreen,
				Terrain.Desert => Colors.Yellow,
				Terrain.River => Colors.Cyan,
				_ => throw new ArgumentException(nameof(terrain)),
			};
		}
	}
}
