namespace CurioExchangeService.Entities
{
    public class Piece : Entity
    {
        public string Name { get; set; }

        public virtual Set Set { get; set; }

        public bool Rare { get; set; }
    }
}