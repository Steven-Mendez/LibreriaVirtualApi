namespace LibreriaVirtualApi.Dtos
{
    public class BookResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public string Edition { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int? UserId { get; set; }
    }
}
