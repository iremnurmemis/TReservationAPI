
using ReservationAPI.Entities.RequestModel;
using ReservationAPI.Entities.ResponseModel;

namespace ReservatıonAPI
{
    public class ReservationService
    {
        private double CalculateOccupancy(Carriage carriage)
        {
            if (carriage.Capacity == 0)
                throw new ArgumentException("Capacity cannot be zero");

            return (double)carriage.OccupiedSeats / carriage.Capacity * 100;
        }

        public ReservationResponseModel MakeReservation(ReservationRequestModel request)
        {
            var residentialDetails= new List<ResidentialDetails>();
            var response = new ReservationResponseModel { CanReserve = false,ResidentialDetails=residentialDetails };
            

            //requesteki valueları kontrol et
            if (request.Train == null || request.Train.Carriages == null || request.Train.Carriages.Count == 0)
            {
               return response;
            }


            //%70 in altındaki vagonları bulma
            List<Carriage> availableCarriages = new List<Carriage>();            
            foreach (var carriage in request.Train.Carriages)
            {
                double occupancyRate = CalculateOccupancy(carriage);

                if (occupancyRate < 70)
                {
                    availableCarriages.Add(carriage);
                }
            }

            if (availableCarriages.Count == 0)
            {
                return response;
            }

            //nasıl yolculuk yapmak istiyorlar?
            int passengerCount = request.NumberOfPeople;

            if (request.AllowDifferentCarriages)
            {

                foreach (var carriage in availableCarriages)
                {
                    int availableSeats = carriage.Capacity - carriage.OccupiedSeats;
                    int seatsToReserve = Math.Min(passengerCount, availableSeats);

                    if (seatsToReserve > 0)
                    {
                        residentialDetails.Add(new ResidentialDetails
                        {
                            CarriageName = carriage.Name,
                            CountOfPeople = seatsToReserve,
                        });

                        passengerCount -= seatsToReserve;

                        if (passengerCount == 0)
                            break;
                    }
                }
            }
            else
            {
                foreach (var carriage in availableCarriages)
                {
                    int availableSeats = carriage.Capacity - carriage.OccupiedSeats;

                    if (availableSeats >=passengerCount)
                    {
                        residentialDetails.Add(new ResidentialDetails
                        {
                            CarriageName = carriage.Name,
                            CountOfPeople = passengerCount,
                        });

                        passengerCount = 0;
                        break;
                    }
                }
            }

            return new ReservationResponseModel
            {
                CanReserve = passengerCount == 0,
                ResidentialDetails = passengerCount == 0 ? residentialDetails : new List<ResidentialDetails>(),
            };


        }
    }


}
