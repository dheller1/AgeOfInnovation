using Meeple.Util;
using System;
using System.Collections;

namespace Meeple.HexMap
{
	public class HexMap<T> : IEnumerable<T> where T : IHexField {
		public HexMap() { }

		public HexMap(IEnumerable<T> fields) : this() {
			foreach(var f in fields) {
				this[f.Q, f.R] = f;
			}
		}

		public T? this[int q, int r] {
			get => _fields.GetValueOrDefault((q, r));
			set {
				if(value != null) {
					value.Q = q;
					value.R = r;
					_fields[(q, r)] = value;
					_fieldCoordinates[value] = (q, r);
				}
				else {
					if(_fields.TryGetValue((q, r), out var oldValue)) {
						_fieldCoordinates.Remove(oldValue);
					};
					_fields.Remove((q, r));
				}
			}
		}

		public IEnumerator<T> GetEnumerator() => _fields.Values.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _fields.Values.GetEnumerator();

		public IEnumerable<T> AdjacentTo(T field) {
			return _adjacencies
				.Select(adj => this[field.Q + adj.Item1, field.R + adj.Item2])
				.WhereNotNull();
		}

		private readonly Dictionary<(int, int), T> _fields = [];
		private readonly Dictionary<T, (int, int)> _fieldCoordinates = [];

		private static readonly (int, int)[] _adjacencies = [
			(-1,  0),
			(+1,  0),
			( 0, -1),
			(+1, -1),
			(-1, +1),
			( 0, +1)
		];
	}
}
