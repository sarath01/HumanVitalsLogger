# VitalsLogger.iOS

**VitalsLogger** is a .NET iOS library that reads and logs patient vitals using Apple HealthKit. It is designed for use in healthcare, fitness, and remote monitoring applications.

## 📋 Features

- Reads latest vitals from Apple HealthKit
- Supports:
  - ❤️ Heart Rate
  - 💨 Respiratory Rate
  - 🩸 Oxygen Saturation (SpO2)
  - 👣 Step Count
- Proper unit conversion (percent, per minute, count)
- Built using Xamarin.iOS / .NET 8 iOS

## 🚀 Quick Start

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

## 📜 License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for details.

---

© 2025 Sarath Reddy Konda. Created in support of a U.S. EB2-NIW petition to advance healthcare technology and public interest software.
