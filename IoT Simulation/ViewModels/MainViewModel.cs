using GalaSoft.MvvmLight.Command;
using IoT_Simulation.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IoT_Simulation.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private readonly IoTDeviceService _iotDeviceService;
		private FanModel _fan;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		private ICommand _toggleFanCommand;
		private ICommand _openSettingsCommand;
		
		private bool _isFanOn;
		private double _fanSpeed;

		public MainViewModel()
		{
			_iotDeviceService = new IoTDeviceService(Properties.Settings.Default.ConnectionString);
			Fan = new FanModel
			{
				IsOn = false,
				Speed = 1  
			};
		}

		public FanModel Fan
		{
			get => _fan;
			set
			{
				_fan = value;
				OnPropertyChanged();
			}
		}

		public bool IsFanOn
		{
			get => Fan.IsOn;
			set
			{
				Fan.IsOn = value;
				OnPropertyChanged(nameof(IsFanOn));
				OnPropertyChanged(nameof(ButtonText));
				OnPropertyChanged(nameof(StatusText));
			}
		}

		public string ButtonText
		{
			get => IsFanOn ? "Turn Off" : "Turn On";
		}

		public double FanSpeed
		{
			get => Fan.Speed;
			set
			{
				Fan.Speed = value;
				OnPropertyChanged(nameof(FanSpeed));
				OnPropertyChanged(nameof(StatusText));

				Task.Run(() => _iotDeviceService.ReportDeviceStateAsync(IsFanOn, FanSpeed));
			}
		}

		public string StatusText
		{
			get => IsFanOn ? $"Fan is On at speed {FanSpeed}" : "Fan is Off";
		}
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public ICommand ToggleFanCommand
		{
			get
			{
				if (_toggleFanCommand == null)
				{
					_toggleFanCommand = new RelayCommand(
						param => this.ToggleFan()
					);
				}
				return _toggleFanCommand;
			}
		}

		private void ToggleFan()
		{
			IsFanOn = !IsFanOn;
			OnPropertyChanged(nameof(IsFanOn));
			OnPropertyChanged(nameof(ButtonText));

			Task.Run(() => _iotDeviceService.ReportDeviceStateAsync(IsFanOn, FanSpeed));
		}

		public ICommand OpenSettingsCommand
		{
			get
			{
				if (_openSettingsCommand == null)
				{
					_openSettingsCommand = new RelayCommand(param => OpenSettings());

				}
				return _openSettingsCommand;
			}
		}

		private void OpenSettings()
		{
			SettingsWindow settingsWindow = new SettingsWindow();
			settingsWindow.ShowDialog();
		}
	}
}
