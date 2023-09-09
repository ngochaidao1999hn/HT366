namespace HT366.Application.Dtos.Category
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}