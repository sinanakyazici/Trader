using BuildingBlocks.Core.Extensions;
using System.Text.Json.Serialization;

namespace BuildingBlocks.Core.Event;

public class IntegrationEvent
{
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateExtensions.DateTimeNow;
    }

    [JsonConstructor]
    public IntegrationEvent(Guid id, DateTime createdDate)
    {
        Id = id;
        CreatedDate = createdDate;
    }

    [JsonInclude]
    public Guid Id { get; private init; }

    [JsonInclude]
    public DateTime CreatedDate { get; private init; }
}
