using AoICore.AoIResources;
using AoICore.Buildings;
using AoICore.Players;

namespace AoICore.Map
{
	public static class Terraform
	{
		private static readonly Terrain[] _terrainWheel = [
			Terrain.Mountain,
			Terrain.Wasteland,
			Terrain.Desert,
			Terrain.Plains,
			Terrain.Swamp,
			Terrain.Lake,
			Terrain.Forest
		];

		/// <summary>
		/// Returns the amount of 'steps' on the terrain wheel from <paramref name="source"/> to
		/// <paramref name="target"/> terrain. The sign of the result indicates clockwise (positive)
		/// or anti-clockwise (negative) steps.
		/// </summary>
		public static int GetEffectiveTerrainOffset(Terrain source,  Terrain target) {
			if(source == target) { return 0; }

			var i1 = Array.IndexOf(_terrainWheel, source);
			var i2 = Array.IndexOf(_terrainWheel, target);
			if(i2 < i1) { return -GetEffectiveTerrainOffset(target, source); }

			// i1 < i2: Thus we could count from i1 upwards to i2 (alternative 1)
			// or from i2 upwards to i1 (continuing from the first element after the last).
			// In the second case, we go "counter-clockwise" on the wheel, thus a negative
			// sign is the result.
			var delta1 = i2 - i1;
			var delta2 = i2 - (_terrainWheel.Length + i1);

			return Math.Abs(delta1) < Math.Abs(delta2) ? delta1 : delta2;
		}

		/// <summary>
		/// Returns the cost in tools required to terraform from <paramref name="source"/> to
		/// <paramref name="target"/> terrain, given the <paramref name="terraformingLevel"/>.
		/// </summary>
		public static Tools GetTerraformCost(int terraformingLevel, Terrain source, Terrain target) {
			var shovelCost = CostPerShovel(terraformingLevel);
			return Math.Abs(GetEffectiveTerrainOffset(source, target)) * shovelCost;
		}

		/// <summary>
		/// Returns the total cost (coins and tools) for <paramref name="player"/> to terraform
		/// and build a workshop on <paramref name="location"/>.
		/// </summary>
		public static Cost GetTerraformAndBuildCost(IPlayer player, TerrainHex location) {
			var sourceTerrain = player.AssociatedTerrain;
			var targetTerrain = location.Terrain;

			var cost = BuildingTypes.Workshop.Cost;
			cost.Add(GetTerraformCost(player.TerraformingLevel, sourceTerrain, targetTerrain));
			return cost;
		}

		private static Tools CostPerShovel(int terraformingLevel) {
			return terraformingLevel switch {
				0 => (Tools)3,
				1 => (Tools)2,
				2 => (Tools)1,
				_ => throw new ArgumentOutOfRangeException(nameof(terraformingLevel)),
			};
		}
	}
}
