using AoICore.Map;

namespace AoICore.Commands
{
	// a command which builds or upgrades a building, triggering power gain
	internal interface ITriggerPowerGain : IPlayerCommand
	{
		TerrainHex Position { get; }
		IMap Map { get; }
	}
}
