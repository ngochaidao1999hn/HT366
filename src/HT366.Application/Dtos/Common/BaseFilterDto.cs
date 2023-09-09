namespace HT366.Application.Dtos.Common
{
    public class BaseFilterDto
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
    }
}