namespace AoICore.AoIResources
{
	public struct Tools(int amount)
	{
		private int _amount = amount;

		public override readonly string ToString() => $"Tools: {_amount}";

		public static explicit operator int(Tools Tools) => Tools._amount;
		public static explicit operator Tools(int i) => new Tools(i);

		public static Tools operator +(Tools a, Tools b) => new(a._amount + b._amount);
		public static Tools operator -(Tools a, Tools b) => new(a._amount - b._amount);
		public static Tools operator ++(Tools c) { c._amount += 1; return c; }
		public static Tools operator --(Tools c) { c._amount -= 1; return c; }

		public static bool operator <(Tools c, int i) => c._amount < i;
		public static bool operator >(Tools c, int i) => c._amount > i;
		public static bool operator ==(Tools c, int i) => c._amount == i;
		public static bool operator !=(Tools c, int i) => c._amount != i;
		public static bool operator <=(Tools c, int i) => c._amount <= i;
		public static bool operator >=(Tools c, int i) => c._amount >= i;

		public static bool operator <(Tools a, Tools b) => a._amount < b._amount;
		public static bool operator >(Tools a, Tools b) => a._amount > b._amount;
		public static bool operator ==(Tools a, Tools b) => a._amount == b._amount;
		public static bool operator !=(Tools a, Tools b) => a._amount != b._amount;
		public static bool operator <=(Tools a, Tools b) => a._amount <= b._amount;
		public static bool operator >=(Tools a, Tools b) => a._amount >= b._amount;

		public override readonly bool Equals(object? obj) => obj is Tools oc && oc._amount == this._amount;
		public override readonly int GetHashCode() => _amount.GetHashCode();
	}
}
