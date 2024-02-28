using AoICore.Commands;
using AoICore.StateMachine;
using AoICore.StateMachine.States;

namespace AoICoreTest
{
	[TestClass]
	public class StateMachineTest
	{
		[TestMethod]
		public void TestPlaceInitialWorkshops() {
			var sm = new StateMachine();

			Assert.IsInstanceOfType(sm.CurrentState, typeof(PlaceInitialWorkshopState));
			sm.ApplyCommand(new PlaceInitialWorkshopCommand());
			Assert.IsInstanceOfType(sm.CurrentState, typeof(PlaceInitialWorkshopState));
			sm.ApplyCommand(new PlaceInitialWorkshopCommand());
			sm.ApplyCommand(new PlaceInitialWorkshopCommand());
			sm.ApplyCommand(new PlaceInitialWorkshopCommand());
			sm.ApplyCommand(new PlaceInitialWorkshopCommand());
			Assert.IsInstanceOfType(sm.CurrentState, typeof(PlaceInitialWorkshopState));
			sm.ApplyCommand(new PlaceInitialWorkshopCommand());
			Assert.IsInstanceOfType(sm.CurrentState, typeof(FinishedState));
			Assert.ThrowsException<InvalidOperationException>(() => sm.ApplyCommand(new PlaceInitialWorkshopCommand()));
		}
	}
}