using AoICore.Map;

namespace AoICore.Effects
{
	public static class PowerGain
	{
		public static void InvokeFromBuild(IMap map, TerrainHex buildLocation) {
			var buildingPlayer = buildLocation.Controller ?? throw new InvalidOperationException();

			var adjacentPlayerHexes = map.GetAdjacentHexes(buildLocation)
				.Where(hex => hex.Controller != null && hex.Controller != buildingPlayer)
				.GroupBy(hex => hex.Controller);

			foreach(var group in adjacentPlayerHexes) {
				var player = group.Key;
				var totalPowerValue = group.Sum(hex => (int)hex.Building!.Type.PowerValue);
			}

		}
	}
}
