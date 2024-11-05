using ReservatıonAPI;

namespace ReservationAPI.Entities.ResponseModel
{
    public class ReservationResponseModel
    {
        public bool CanReserve { get; set; }
        public List<ResidentialDetails> ResidentialDetails { get; set; }
    }
}
