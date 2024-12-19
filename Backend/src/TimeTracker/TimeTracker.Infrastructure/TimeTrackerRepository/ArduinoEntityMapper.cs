using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeRegistration.TimeTracker.Domain.TimeTracker;

namespace TimeRegistration.TimeTracker.Infrastructure.TimeTrackerRepository
{
    public static class ArduinoEntityMapper
    {
        public static ArduinoEntity Map(ArduinoModel model)
        {
            return new ArduinoEntity(
                model.Id,
                model.CreatedUtc,
                model.ModifiedUtc,
                model.ArduinoId
            );
        }

        public static ArduinoModel Map(ArduinoEntity entity)
        {
            return new ArduinoModel(
                entity.Id,
                entity.CreatedUtc,
                entity.ModifiedUtc,
                entity.ArduinoId
            );
        }
    }
}
