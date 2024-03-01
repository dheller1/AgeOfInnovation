using AoICore.Map;

namespace AoICore.Players
{
	public interface IPlayer
	{
		string Name { get; }
		Terrain AssociatedTerrain { get; }
	}
}
