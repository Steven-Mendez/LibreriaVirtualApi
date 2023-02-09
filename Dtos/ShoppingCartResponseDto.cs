using LibreriaVirtualApi.Models;

namespace LibreriaVirtualApi.Dtos
{
    public class ShoppingCartResponseDto
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int Quantity { get; set; }

        public virtual Book Book { get; set; } = null!;
    }
}
