//
// VitalsLogger - VitalsService.cs
// Developed by Sarath Konda, 2025
//
// Copyright (c) 2025 Sarath Konda
// Licensed under the MIT License. See LICENSE file in the project root.
//
// This service reads vital signs using iOS HealthKit for use in health monitoring apps.
//

using Foundation;
using HealthKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VitalsLogger
{
    public class VitalsService
    {
        private readonly HKHealthStore healthStore = new HKHealthStore();


        public async Task<bool> RequestAuthorizationAsync()
        {
            var readTypes = new NSSet(new[]
            {
                HKQuantityType.Create(HKQuantityTypeIdentifier.HeartRate),
                HKQuantityType.Create(HKQuantityTypeIdentifier.StepCount),
                HKQuantityType.Create(HKQuantityTypeIdentifier.RespiratoryRate),
                HKQuantityType.Create(HKQuantityTypeIdentifier.OxygenSaturation)
            });

            var granted = await healthStore.RequestAuthorizationToShareAsync(null, readTypes);
            return granted.Item1;
        }


        public async Task<Dictionary<string, double>> ReadLatestVitalsAsync()
        {
            var results = new Dictionary<string, double>();

            await ReadQuantity(HKQuantityTypeIdentifier.HeartRate, "HeartRate", results);
            await ReadQuantity(HKQuantityTypeIdentifier.OxygenSaturation, "OxygenSaturation", results);
            await ReadQuantity(HKQuantityTypeIdentifier.RespiratoryRate, "RespiratoryRate", results);
            await ReadQuantity(HKQuantityTypeIdentifier.StepCount, "StepCount", results);

            return results;
        }


        private async Task ReadQuantity(HKQuantityTypeIdentifier identifier, string key, Dictionary<string, double> output)
        {
            if (identifier == null)
            {
                return;
            }

            // Convert enum to string correctly for HKQuantityType.Create
            var quantityType = HKQuantityType.Create(identifier);
            var sortDescriptor = new NSSortDescriptor(HKSample.SortIdentifierEndDate, false);

            var query = new HKSampleQuery(quantityType, null, 1, new[] { sortDescriptor },
                (q, results, error) =>
                {
                    if (results != null && results.Length > 0)
                    {
                        var quantitySample = results[0] as HKQuantitySample;
                        if (quantitySample == null) return;

                        HKUnit unit;

                        if (identifier == HKQuantityTypeIdentifier.StepCount)
                        {
                            unit = HKUnit.Count;
                        }
                        else if (identifier == HKQuantityTypeIdentifier.OxygenSaturation)
                        {
                            unit = HKUnit.Percent;
                        }
                        else
                        {
                            unit = HKUnit.Count;
                        }

                        output[key] = quantitySample.Quantity.GetDoubleValue(unit);
                    }
                });

            healthStore.ExecuteQuery(query);
            await Task.Delay(500); // Allow async query to complete
        }


    }
}
