namespace HiddenVilla_Server.Models
{
    public class BlazorRoom
    {
        public int Id { get; set; }
        public string RoomName { get; set; } = String.Empty;
        public double Price { get; set; }
        public bool IsActive { get; set; } = false;
    }
}
