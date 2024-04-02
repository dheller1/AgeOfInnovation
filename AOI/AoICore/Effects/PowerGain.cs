using AoICore.Map;
using AoICore.Players;
using AoICore.StateMachine.States;

namespace AoICore.Effects
{
	public static class PowerGain
	{
		/// <summary>
		/// Invokes power gain from building in an adjacent space, returning the list of resulting
		/// game states (each of which allows one of the neighbors to gain power).
		/// </summary>
		public static IEnumerable<IGameState> InvokeFromBuild(IPlayer activePlayer, IMap map, TerrainHex buildLocation) {
			var adjacentPlayerHexes = map.GetAdjacentHexes(buildLocation)
				.Where(hex => hex.Controller != null && hex.Controller != activePlayer)
				.GroupBy(hex => hex.Controller!);

			var result = new List<IGameState>();
			foreach(var group in adjacentPlayerHexes) {
				var player = group.Key;
				var totalPowerValue = group.Sum(hex => (int)hex.Building!.Type.PowerValue);

				var maxGain = new[] { totalPowerValue, (int)player.Power.MaxGain, 1 + (int)player.VictoryPoints }.Min();
				if(maxGain > 0) {
					result.Add(new AllowGainPowerState(player, (PowerTokens)maxGain));
				}
			}
			return result;
		}
	}
}
