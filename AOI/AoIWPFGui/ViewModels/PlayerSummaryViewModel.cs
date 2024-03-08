﻿using AoICore.Players;

namespace AoIWPFGui.ViewModels
{
	public class PlayerSummaryViewModel : ReactiveObject, IObserver<IPlayer?>
	{
		private double _borderOpacity = 0;

		public PlayerSummaryViewModel(IPlayer player, IObservable<IPlayer?> activePlayerObservable) {
			Player = player;
			activePlayerObservable.Subscribe(this);
		}

		public IPlayer Player { get; }

		public double BorderOpacity { get => _borderOpacity; set => this.RaiseAndSetIfChanged(ref _borderOpacity, value); }

		public void OnCompleted() {
			throw new NotImplementedException();
		}

		public void OnError(Exception error) {
			throw new NotImplementedException();
		}

		public void OnNext(IPlayer? activePlayer) {
			BorderOpacity = activePlayer == Player ? 0.9 : 0;
		}
	}
}
