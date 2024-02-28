using AoICore.Players;

namespace AoICore.StateMachine.States
{
	/// <summary>
	/// State which expects a player to place one of their two initial workshops.
	/// </summary>
	public class PlaceInitialWorkshopState : IGameState
	{
		public PlaceInitialWorkshopState(int workshopIndex, IPlayer player) {
			_workshopIndex = workshopIndex;
			Player = player;
		}

		public IPlayer Player { get; }
		private readonly int _workshopIndex;

		public override bool Equals(object? obj) {
			var other = obj as PlaceInitialWorkshopState;
			return other != null && Player == other.Player && _workshopIndex == other._workshopIndex;
		}
		public override string ToString() => $"{nameof(PlaceInitialWorkshopState)}({Player}, {_workshopIndex + 1}. workshop)";
		public override int GetHashCode() => (Player, _workshopIndex).GetHashCode();
	}
}
