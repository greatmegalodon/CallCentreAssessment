using Assessment.Data.Interfaces;

namespace Assessment.Data.Entities;

public class User : IId, ISoftDelete
{
    public Guid Id { get; set; }
    
    public string Username { get; set; }
    
    public DateTime? DateDeleted { get; set; }
    
    public virtual ICollection<Call> Calls { get; set; }
}