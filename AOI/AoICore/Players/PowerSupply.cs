using Meeple.Util;
using System.Diagnostics;

namespace AoICore.Players
{
	public class PowerSupply : NotificationBase
	{
		public PowerSupply() : this((PowerTokens)7, (PowerTokens)5) { }

		public PowerSupply(PowerTokens bowl_I, PowerTokens bowl_II) {
			Bowl_I = bowl_I;
			Bowl_II = bowl_II;
		}

		public PowerTokens Bowl_I { get => _bowl_I; private set => SetProperty(ref _bowl_I, value); }
		public PowerTokens Bowl_II { get => _bowl_II; private set => SetProperty(ref _bowl_II, value); }
		public PowerTokens Bowl_III { get => _bowl_III; private set => SetProperty(ref _bowl_III, value); }

		public PowerTokens MaxGain => 2 * Bowl_I + 1 * Bowl_II;
		public PowerTokens AvailablePower => Bowl_III;


		internal struct GainDetails {
			public PowerTokens I_to_II;
			public PowerTokens II_to_III;
		}

		internal GainDetails Gain(int amount) => Gain((PowerTokens)amount);
		internal GainDetails Gain(PowerTokens tokens) {
			if(tokens < 0 || tokens > MaxGain) { throw new ArgumentOutOfRangeException(nameof(tokens)); }

			var details = new GainDetails();

			var tokensLeft = tokens;
			var ItoII = (PowerTokens)Math.Min((int)tokensLeft, (int)Bowl_I);
			if(ItoII > 0) {
				tokensLeft -= ItoII;
				Bowl_I -= ItoII;
				Bowl_II += ItoII;
				details.I_to_II = ItoII;
			}

			if(tokensLeft > 0) {
				var IItoIII = (PowerTokens)Math.Min((int)tokensLeft, (int)Bowl_II);
				if(IItoIII > 0) {
					tokensLeft -= IItoIII;
					Bowl_II -= IItoIII;
					Bowl_III += IItoIII;
					details.II_to_III = IItoIII;
				}
			}
			Debug.Assert(tokensLeft == 0);
			return details;
		}

		internal void UndoGain(GainDetails details) {
			if(details.II_to_III > 0) {
				Bowl_III -= details.II_to_III;
				Bowl_II += details.II_to_III;
			}
			Bowl_II -= details.I_to_II;
			Bowl_I += details.I_to_II;
		}

		internal void Sacrifice(int amount) => Sacrifice((PowerTokens)amount);
		internal void Sacrifice(PowerTokens tokens) {
			if(tokens < 0 || 2 * tokens > Bowl_II) { throw new ArgumentOutOfRangeException(nameof(tokens)); }

			Bowl_II -= 2 * tokens;
			Bowl_III += tokens;
		}

		private PowerTokens _bowl_I;
		private PowerTokens _bowl_II;
		private PowerTokens _bowl_III = (PowerTokens)0;
	}
}
