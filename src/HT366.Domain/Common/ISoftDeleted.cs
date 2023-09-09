namespace HT366.Domain.Common
{
    public interface ISoftDeleted
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeleteTime { get; set; }
    }
}