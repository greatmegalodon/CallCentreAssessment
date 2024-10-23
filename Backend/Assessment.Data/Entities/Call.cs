using Assessment.Data.Interfaces;

namespace Assessment.Data.Entities;

public class Call : IId, ISoftDelete
{
    public Guid Id { get; set; }
    public Guid CallingUserId { get; set; }
    
    public DateTimeOffset DateCallStarted { get; set; }
    
    public DateTime? DateDeleted { get; set; }
    
    public virtual User CallingUser { get; set; }
}