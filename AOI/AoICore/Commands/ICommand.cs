﻿namespace AoICore.Commands
{
	public interface ICommand
	{
		void Execute();
		void Undo();
	}
}
