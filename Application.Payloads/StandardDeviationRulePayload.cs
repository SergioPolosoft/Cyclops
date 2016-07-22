namespace Application.Payloads
{
    public class StandardDeviationRulePayload:IPayloadObject
    {
        public string Name { get; set; }
        public bool WithinControl { get; set; }
        public string Comment { get; set; }
        public int NumberOfControls { get; set; }
        public int StandardDeviationLimit { get; set; }
    }
}