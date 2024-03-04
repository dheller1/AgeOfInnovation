using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;

namespace AoICore.Commands
{
	public sealed class PlaceInitialWorkshopCommand : ICommand
	{
		public PlaceInitialWorkshopCommand(IPlayer player, TerrainHex position) {
			Player = player ?? throw new ArgumentNullException(nameof(player));
			Position = position ?? throw new ArgumentNullException(nameof(position));
		}

		public IPlayer Player { get; }
		public TerrainHex Position { get; }

		public bool CanExecute => Player.AssociatedTerrain == Position.Terrain && Position.Building == null;

		public void Execute() {
			if(Position.Building != null) { throw new InvalidOperationException("The hex already contains a building."); }
			Position.Building = new Building(Player, BuildingTypes.Workshop);
		}
		public void Undo() {
			Position.Building = null;
		}

		public override string ToString() => $"{nameof(PlaceInitialWorkshopCommand)}";

	}
}
