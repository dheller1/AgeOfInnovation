namespace AoICore.Commands
{
	internal sealed class PlaceInitialWorkshopCommand : ICommand
	{
		public void Execute() {
			;
		}

		public override string ToString() => $"{nameof(PlaceInitialWorkshopCommand)}";
	}
}
