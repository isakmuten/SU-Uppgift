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
using System.Windows.Shapes;
using Microsoft.Azure.Devices.Client;


namespace IoT_Simulation
{
	/// <summary>
	/// Interaction logic for SettingsWindow.xaml
	/// </summary>
	public partial class SettingsWindow : Window
	{
		
		public SettingsWindow()
		{
			InitializeComponent();

			ConnectionStringTextBox.Text = Properties.Settings.Default.ConnectionString;
			DeviceIdTextBox.Text = Properties.Settings.Default.DeviceId;
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			Properties.Settings.Default.ConnectionString = ConnectionStringTextBox.Text;
			Properties.Settings.Default.DeviceId = DeviceIdTextBox.Text;

			Properties.Settings.Default.Save();

			IoTDevice.InitializeClient();

			this.Close();
		}
	}
}
