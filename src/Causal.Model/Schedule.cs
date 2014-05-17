using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using NodaTime;

namespace Causal.Model
{
    public class Schedule
    {
        public LocalTime Time { get; set; }
        public SchedulePeriod Period { get; set; }

        public override string ToString()
        {
            return "Time = " + Time.ToString() + ", Period = " + Period.ToString();
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum SchedulePeriod
        {
            None = 0,
            Daily = 1,
            Weekly = 2,
        }
    }
}
