using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IoT_Simulation
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
		// Property för ConnectionString
		private string _connectionString;
		public string ConnectionString
		{
			get => _connectionString;
			set
			{
				_connectionString = value;
				OnPropertyChanged();
			}
		}

		// Property för DeviceId
		private string _deviceId;
		public string DeviceId
		{
			get => _deviceId;
			set
			{
				_deviceId = value;
				OnPropertyChanged();
			}
		}

		// Command för att spara inställningar
		public ICommand SaveCommand { get; }

		public SettingsViewModel()
		{
			// Ladda befintliga inställningar
			ConnectionString = Properties.Settings.Default.ConnectionString;
			DeviceId = Properties.Settings.Default.DeviceId;

			// Skapa kommando för att spara inställningarna
			SaveCommand = new RelayCommand(param => SaveSettings());
		}

		// Spara inställningar
		private void SaveSettings()
		{
			Properties.Settings.Default.ConnectionString = ConnectionString;
			Properties.Settings.Default.DeviceId = DeviceId;
			Properties.Settings.Default.Save();
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}
	}

}
