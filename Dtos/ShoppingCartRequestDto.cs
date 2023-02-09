namespace LibreriaVirtualApi.Dtos
{
    public class ShoppingCartRequestDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }
    }
}
