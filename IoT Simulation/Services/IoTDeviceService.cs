using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Text;
using System.Threading.Tasks;

public class IoTDeviceService
{
	private readonly DeviceClient _deviceClient;

	public IoTDeviceService(string connectionString)
	{
		_deviceClient = DeviceClient.CreateFromConnectionString(connectionString, TransportType.Mqtt);
	}

	public async Task ReportDeviceStateAsync(bool isFanOn, double fanSpeed)
	{
		TwinCollection reportedProperties = new TwinCollection();
		reportedProperties["fanState"] = isFanOn ? "On" : "Off";
		reportedProperties["fanSpeed"] = fanSpeed;

		await _deviceClient.UpdateReportedPropertiesAsync(reportedProperties);
	}

	public async Task SendTelemetryAsync(string telemetryData)
	{
		var message = new Message(Encoding.ASCII.GetBytes(telemetryData));
		await _deviceClient.SendEventAsync(message);
	}

	public async Task<Twin> GetDeviceTwinAsync()
	{
		return await _deviceClient.GetTwinAsync();
	}
}
