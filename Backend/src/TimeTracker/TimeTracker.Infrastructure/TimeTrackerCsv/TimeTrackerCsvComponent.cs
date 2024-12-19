using System.Reflection.Metadata.Ecma335;
using TimeRegistration.TimeTracker.ApplicationServices.Component;
using TimeRegistration.TimeTracker.Domain.ArduinoLogs;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerCsv;
public sealed class TimeTrackerCsvComponent : ITimeTrackerCsvComponent
{
    const string FILEPATH = "c:/temp/arduinoLogs.csv";
    
    public bool UpsertArduinoLogsToCsv(IEnumerable<ArduinoLog> logs, string arduinoId)
    {
        // Save the list to CSV
        SaveListToCsv(logs, FILEPATH, arduinoId);

        Console.WriteLine($"List saved to {FILEPATH}");
        return true;
    }

    public IEnumerable<ArduinoLog> GetArduinoLogsFromCsv(string arduinoId)
    {
        var logs = new List<ArduinoLog>();

        if (!File.Exists(FILEPATH))
        {
            return logs;
        }

        using (var reader = new StreamReader(FILEPATH))
        {
            string? headerLine = reader.ReadLine();
            if (headerLine == null)
            {
                return logs;
            }

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                if (line == null)
                {
                    continue;
                }

                var values = line.Split(',');
                if (values[0] == arduinoId)
                {
                    logs.Add(new ArduinoLog
                    {
                        Status = values[1],
                        Timestamp = DateTime.Parse(values[2])
                    });
                }
            }
        }

        return logs;
    }

    private static void SaveListToCsv<T>(IEnumerable<T> list, string filePath, string arduinoId)
    {
        bool fileExists = File.Exists(filePath);

        using (var writer = new StreamWriter(filePath, append: true))
        {
            // Get properties of the object
            var properties = typeof(T).GetProperties();

            // Write header if the file does not exist
            if (!fileExists)
            {
                writer.WriteLine("ArduinoId," + string.Join(",", properties.Select(p => p.Name)));
            }

            // Write data
            foreach (var item in list)
            {
                var values = properties.Select(p => p.GetValue(item, null)?.ToString() ?? string.Empty);
                writer.WriteLine(arduinoId + "," + string.Join(",", values));
            }
        }
    }
}
