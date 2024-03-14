namespace AoICore.Map
{
	public interface IMap : IEnumerable<TerrainHex>
	{
		TerrainHex? this[int q, int r] { get; set; }
		IEnumerable<TerrainHex> GetAdjacentHexes(TerrainHex hex);
	}
}
