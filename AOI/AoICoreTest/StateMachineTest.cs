using AoICore;
using AoICore.Commands;
using AoICore.StateMachine.States;

namespace AoICoreTest
{
	[TestClass]
	public class StateMachineTest
	{
		[TestMethod]
		public void TestPlaceInitialWorkshops() {
			var game = new AoIGame();
			var firstPlayer = game.Players.First();
			
			Assert.IsTrue(game.CurrentState is PlaceInitialWorkshopState);
			Assert.AreEqual(firstPlayer, game.ActivePlayer);

			// wrong terrain type
			Assert.ThrowsException<InvalidOperationException>(() =>
				game.InvokeCommand(new PlaceInitialWorkshopCommand(firstPlayer, game.Map[5, 7]!)));
			
			game.InvokeCommand(new PlaceInitialWorkshopCommand(firstPlayer, game.Map[5, 4]!));
			Assert.IsNotNull(game.Map[5, 4]?.Building);
			Assert.AreEqual(firstPlayer, game.Map[5, 4]?.Controller);

			Assert.IsTrue(game.CurrentState is PlaceInitialWorkshopState);
			Assert.AreEqual(game.Players.Skip(1).First(), game.ActivePlayer);
		}

		[TestMethod]
		public void TestUndo() {
			var game = new AoIGame();
			var firstPlayer = game.Players.First();
			
			game.InvokeCommand(new PlaceInitialWorkshopCommand(firstPlayer, game.Map[5, 4]!));
			Assert.IsNotNull(game.Map[5, 4]?.Building);
			Assert.AreEqual(game.Players.Skip(1).First(), game.ActivePlayer);

			game.UndoCommand();
			Assert.IsNull(game.Map[5, 4]!.Building);
			Assert.AreEqual(firstPlayer, game.ActivePlayer);
		}
	}
}