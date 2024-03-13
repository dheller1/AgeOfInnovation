namespace Meeple.Resources
{
	public struct Resource<T>(int amount) {
		private int _amount = amount;

		public Resource() : this(0) { }

		public override readonly string ToString() => $"{_amount}";

		public static explicit operator int(Resource<T> resource) => resource._amount;
		public static explicit operator Resource<T>(int i) => new Resource<T>(i);

		public static Resource<T> operator +(Resource<T> a, Resource<T> b) => new(a._amount + b._amount);
		public static Resource<T> operator -(Resource<T> a, Resource<T> b) => new(a._amount - b._amount);
		public static Resource<T> operator ++(Resource<T> c) { c._amount += 1; return c; }
		public static Resource<T> operator --(Resource<T> c) { c._amount -= 1; return c; }

		public static Resource<T> operator *(Resource<T> a, int i) => new(a._amount * i);
		public static Resource<T> operator *(int i, Resource<T> a) => a * i;

		public static bool operator <(Resource<T> c, int i) => c._amount < i;
		public static bool operator >(Resource<T> c, int i) => c._amount > i;
		public static bool operator ==(Resource<T> c, int i) => c._amount == i;
		public static bool operator !=(Resource<T> c, int i) => c._amount != i;
		public static bool operator <=(Resource<T> c, int i) => c._amount <= i;
		public static bool operator >=(Resource<T> c, int i) => c._amount >= i;

		

		public static bool operator <(Resource<T> a, Resource<T> b) => a._amount < b._amount;
		public static bool operator >(Resource<T> a, Resource<T> b) => a._amount > b._amount;
		public static bool operator ==(Resource<T> a, Resource<T> b) => a._amount == b._amount;
		public static bool operator !=(Resource<T> a, Resource<T> b) => a._amount != b._amount;
		public static bool operator <=(Resource<T> a, Resource<T> b) => a._amount <= b._amount;
		public static bool operator >=(Resource<T> a, Resource<T> b) => a._amount >= b._amount;

		public override readonly bool Equals(object? obj) => obj is Resource<T> oc && oc._amount == this._amount;
		public override readonly int GetHashCode() => _amount.GetHashCode();
	}
}
