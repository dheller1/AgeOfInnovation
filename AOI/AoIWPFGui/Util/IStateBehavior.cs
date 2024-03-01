namespace AoIWPFGui.Util
{
	public interface IHaveState<TState> {
		TState State { get; }
	}

	/// <summary>
	/// Defines a behavior attachable to an object.
	/// </summary>
	/// <typeparam name="TAssociated">Type of the associated object</typeparam>
	internal interface IBehavior<TAssociated>
		where TAssociated : class
	{
		TAssociated AssociatedObject { get; }
		bool IsActive { get; }
	}

	/// <summary>
	/// Defines a behavior which can be attached to a <typeparamref name="TAssociated"/> object and observes
	/// a state <typeparamref name="TState"/>. The behavior defined in the functionality is only active while
	/// the observed state fulfills a given predicate.
	/// </summary>
	/// <typeparam name="TAssociated">Type of the associated object</typeparam>
	/// <typeparam name="TState">Type of the observed state</typeparam>
	internal abstract class StateBehavior<TAssociated, TState> : IBehavior<TAssociated>, IObserver<TState>
		where TAssociated : class
	{
		private bool _isActive = false;

		public StateBehavior(TAssociated associatedObject, Func<TState, bool> condition) {
			AssociatedObject = associatedObject;
			_stateCondition = condition;
		}
		
		public TAssociated AssociatedObject { get; }
		private Func<TState, bool> _stateCondition { get; }
		protected TState? _currentState;

		public bool IsActive {
			get => _isActive;
			private set {
				if(value != _isActive) {
					_isActive = value;
				}
			}
		}

		protected virtual void OnNextState(bool wasActive, bool isActive) { }

		public void OnNext(TState state) {
			_currentState = state;
			bool wasActive = _isActive;
			IsActive = _stateCondition(state) == true;
			OnNextState(wasActive, _isActive);
		}

		public void OnCompleted() { }
		public void OnError(Exception error) { throw error; }
	}
}
