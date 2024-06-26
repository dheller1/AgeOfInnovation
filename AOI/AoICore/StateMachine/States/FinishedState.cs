﻿using AoICore.Commands;

namespace AoICore.StateMachine.States
{
	public class FinishedState : IGameState
	{
		public IEnumerable<IGameState> ApplyCommand(ICommand command) => throw new UnsupportedCommandException(command);
		public override string ToString() => $"Finished";
	}
}
