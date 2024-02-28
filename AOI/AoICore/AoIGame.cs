using AoICore.Map;

namespace AoICore
{
	public class AoIGame {
		public AoIGame() {

		}

		internal StateMachine.StateMachine StateMachine { get; } = new();
		public SmallMap Map { get; } = new SmallMap();
	}
}
