using System.Reflection.Metadata.Ecma335;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerCsv;
public sealed class TimeTrackerCsvComponent : ITimeTrackerCsvComponent
{
    public IEnumerable<ArduinoLogsModel> UpsertArduinoLogsToCsv(IEnumerable<ArduinoLogsModel> arudinoLogsModel)
    {
        string filePath = "arduinoLogs.csv";

        // Save the list to CSV
        SaveListToCsv(arudinoLogsModel, filePath);

        Console.WriteLine($"List saved to {filePath}");
        return arudinoLogsModel;
    }

    private static void SaveListToCsv<T>(IEnumerable<T> list, string filePath)
    {
        using (var writer = new StreamWriter(filePath))
        {
            // Get properties of the object
            var properties = typeof(T).GetProperties();

            // Write header
            writer.WriteLine(string.Join(",", properties.Select(p => p.Name)));

            // Write data
            foreach (var item in list)
            {
                var values = properties.Select(p => p.GetValue(item, null)?.ToString() ?? string.Empty);
                writer.WriteLine(string.Join(",", values));
            }
        }
    }
}


