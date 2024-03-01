using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Meeple.Util
{
	public class NotificationBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null) {
			if(EqualityComparer<T>.Default.Equals(storage, value)) {
				return false;
			}

			storage = value;
			NotifyPropertyChanged(propertyName);
			return true;
		}

		public void NotifyPropertyChanged([CallerMemberName] string? propertyName = null) {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
