namespace CloudFormationLambda.Models
{
    public class TempratureLog
    {
        public string DeviceId { get; set; }
        public double TempratureF { get; set; }
        public DateTime ObservedTimestamp { get; set; }

        public TempratureLog(string deviceId, double tempF, DateTime observedTimestamp)
        {
            DeviceId = deviceId;
            TempratureF = tempF;
            ObservedTimestamp = observedTimestamp;

        }
    }
}
