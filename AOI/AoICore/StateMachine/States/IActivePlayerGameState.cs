using AoICore.Players;

namespace AoICore.StateMachine.States
{
	/// <summary>
	/// Represents a game state in which one of the players is required to act.
	/// </summary>
	internal interface IActivePlayerGameState : IGameState
	{
		IPlayer ActivePlayer { get; }
	}
}
