# VitalsLogger.iOS

**VitalsLogger** is a .NET for iOS library that reads and logs patient vitals from Apple HealthKit. Designed for healthcare and remote patient monitoring apps, this library provides real-time access to data such as heart rate, oxygen saturation, respiratory rate, and step count.

## Features

- 📊 Reads latest vitals from HealthKit
- 🧪 Supports Heart Rate, SpO2, Respiratory Rate, and Step Count
- 🔐 HealthKit authorization handling
- 📦 Xamarin.iOS / .NET 8 iOS compatible

## Example Usage

```csharp
var service = new VitalsService();
bool granted = await service.RequestAuthorizationAsync();

if (granted)
{
    var vitals = await service.ReadLatestVitalsAsync();
    foreach (var item in vitals)
    {
        Console.WriteLine($"{item.Key}: {item.Value}");
    }
}
```

## License

MIT License © 2025 Sarath Reddy Konda
This library is part to demonstrate technical contributions in U.S. public health infrastructure.
