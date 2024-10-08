using GalaSoft.MvvmLight.Command;
using IoT_Simulation.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace IoT_Simulation.ViewModels
{
	public class MainViewModel : INotifyPropertyChanged
	{
		private FanModel _fan;
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
				OnPropertyChanged(nameof(StatusText));
			}
		}

		public double FanSpeed
		{
			get => Fan.Speed;
			set
			{
				Fan.Speed = value;
				OnPropertyChanged(nameof(FanSpeed));
				OnPropertyChanged(nameof(StatusText));
			}
		}

		public string StatusText
		{
			get => IsFanOn ? $"Fan is On at speed {FanSpeed}" : "Fan is Off";
		}

		public MainViewModel()
		{
			Fan = new FanModel();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		private ICommand _toggleFanCommand;
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
		}

		private ICommand _openSettingsCommand;
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
