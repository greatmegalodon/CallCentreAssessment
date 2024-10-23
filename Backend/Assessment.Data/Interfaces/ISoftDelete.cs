namespace Assessment.Data.Interfaces;

public interface ISoftDelete
{
    public DateTime? DateDeleted { get; set; }
}