using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Azure.Devices.Client;

namespace IoT_Simulation
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private bool isFanOn = false;

		public MainWindow()
		{
			InitializeComponent();

			//IoTDevice.InitializeClient();
		}
		private async void ToggleButton_Click(object sender, RoutedEventArgs e)
		{

			isFanOn = !isFanOn;
			string statusMessage;

			if (isFanOn)
			{
				ToggleButton.Content = "Turn Off";
				statusMessage = $"Fan is On at speed {SpeedSlider.Value}";
			}
			else
			{
				ToggleButton.Content = "Turn On";
				statusMessage = "Fan is Off";
			}

			await IoTDevice.SendDeviceToCloudMessagesAsync(statusMessage);
		}
		private async void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (isFanOn)
			{
				string statusMessage = $"fan is On at speed {SpeedSlider.Value}";
				StatusTextBlock.Text = statusMessage;

				await IoTDevice.SendDeviceToCloudMessagesAsync(statusMessage);
			}
		}
		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			SettingsWindow settingsWindow = new SettingsWindow();
			settingsWindow.ShowDialog();
		}

	}
}
