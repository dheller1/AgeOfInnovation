namespace AoICore.AoIResources
{
	public struct Coins(int amount)
	{
		private int _amount = amount;

		public override readonly string ToString() => $"Coins: {_amount}";

		public static explicit operator int(Coins coins) => coins._amount;
		public static explicit operator Coins(int i) => new Coins(i);

		public static Coins operator+(Coins a, Coins b) => new(a._amount + b._amount);
		public static Coins operator-(Coins a, Coins b) => new(a._amount - b._amount);
		public static Coins operator++(Coins c) { c._amount += 1; return c; }
		public static Coins operator--(Coins c) { c._amount -= 1; return c; }

		public static bool operator <(Coins c, int i) => c._amount < i;
		public static bool operator >(Coins c, int i) => c._amount > i;
		public static bool operator ==(Coins c, int i) => c._amount == i;
		public static bool operator !=(Coins c, int i) => c._amount != i;
		public static bool operator <=(Coins c, int i) => c._amount <= i;
		public static bool operator >=(Coins c, int i) => c._amount >= i;

		public override readonly bool Equals(object? obj) => obj is Coins oc && oc._amount == this._amount;
		public override readonly int GetHashCode() => _amount.GetHashCode();
	}
}
