using Microsoft.Azure.Amqp.Framing;
using Microsoft.Azure.Devices.Client;
using System;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Simulation
{
	public class IoTDevice
	{
		private static DeviceClient deviceClient;
		private static string connectionString = Properties.Settings.Default.ConnectionString;

		// Initiera klienten
		public static void InitializeClient()
		{
			if (string.IsNullOrEmpty(connectionString))
			{
				Console.WriteLine("Connection string is null or empty. Please check the settings.");
				return;
			}

			deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
		}


		// Skicka telemetridata
		public static async Task SendDeviceToCloudMessagesAsync(string message)
		{
			if (deviceClient == null)
			{
				Console.WriteLine("DeviceClient is not initialized. Please initialize before sending messages.");
				return;
			}

			var messageBytes = Encoding.ASCII.GetBytes(message);
			var telemetryMessage = new Message(messageBytes);

			await deviceClient.SendEventAsync(telemetryMessage);
		}

	}
}

