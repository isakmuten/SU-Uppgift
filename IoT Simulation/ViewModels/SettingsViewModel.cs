using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IoT_Simulation
{
	public class SettingsViewModel : INotifyPropertyChanged
	{
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

		public Action CloseAction { get; set; }

		public SettingsViewModel()
		{
			ConnectionString = Properties.Settings.Default.ConnectionString;
			DeviceId = Properties.Settings.Default.DeviceId;
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private ICommand _saveCommand;
		public ICommand SaveCommand
		{
			get
			{
				if (_saveCommand == null)
				{
					_saveCommand = new RelayCommand<object>(SaveSettings);
				}
				return _saveCommand;
			}
		}

		private void SaveSettings(object parameter)
		{
			Properties.Settings.Default.ConnectionString = ConnectionString;
			Properties.Settings.Default.DeviceId = DeviceId;
			Properties.Settings.Default.Save();

			CloseAction?.Invoke();
		}
	}
}
