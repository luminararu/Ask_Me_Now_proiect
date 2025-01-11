namespace Ask_Me_Now.Models
{
    public class UtilizatorInteractiune
    {
        public int Id { get; set; }  
        public string? UserId { get; set; }

        public Utilizator? Utilizator { get; set; }
        public int RaspunsId { get; set; } 

        public Raspuns? Raspuns { get; set; }
        public bool Liked { get; set; }
    }
}
