namespace Application.Payloads
{
    public interface IDPOMapper<in PayloadObject, out DomainObject> where PayloadObject : IPayloadObject
    {
        DomainObject Map(PayloadObject payloadObject);
    }
}