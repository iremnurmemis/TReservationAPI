using ReservatıonAPI;

namespace ReservationAPI.Entities.RequestModel
{
    public class ReservationRequestModel
    {
        public Train Train { get; set; }
        public int NumberOfPeople { get; set; }
        public bool AllowDifferentCarriages { get; set; }
    }
}
