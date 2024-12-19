using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRegistration.TimeTracker.Domain.TimeTracker
{
    public class ArduinoModel : BaseModel
    {
        public ArduinoModel(Guid id, DateTime createdUtc, 
            DateTime modifiedUtc, 
            string arduinoId) : 
            base(id, createdUtc, modifiedUtc)
        {
            ArduinoId = arduinoId;
        }

        public string ArduinoId { get; set; }
    }
}
