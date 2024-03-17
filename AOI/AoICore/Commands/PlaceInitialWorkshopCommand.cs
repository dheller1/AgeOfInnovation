using AoICore.Buildings;
using AoICore.Map;
using AoICore.Players;

namespace AoICore.Commands
{
	public sealed class PlaceInitialWorkshopCommand : IPlayerCommand
	{
		public PlaceInitialWorkshopCommand(IPlayer player, TerrainHex position) {
			Player = player ?? throw new ArgumentNullException(nameof(player));
			Position = position ?? throw new ArgumentNullException(nameof(position));
		}

		public IPlayer Player { get; }
		public TerrainHex Position { get; }

		public bool CanExecute => Player.AssociatedTerrain == Position.Terrain && Position.Building == null;

		public void Execute() {
			if(!CanExecute) { throw new InvalidOperationException(); }
			Position.Building = new Building(Player, BuildingTypes.Workshop);
		}

		public void Undo() {
			Position.Building = null;
		}

		public string AsText => ToString();
		public override string ToString() => $"{Player}: Place initial Workshop";
	}
}
