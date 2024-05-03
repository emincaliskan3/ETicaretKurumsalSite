namespace EntityLayer
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime? CreateDate { get; set; }
    }
}
