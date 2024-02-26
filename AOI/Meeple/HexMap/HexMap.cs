using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
				}
				else { _fields.Remove((q, r)); }
			}
		}

		public IEnumerator<T> GetEnumerator() => _fields.Values.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _fields.Values.GetEnumerator();

		private readonly Dictionary<(int, int), T> _fields = [];
	}
}
