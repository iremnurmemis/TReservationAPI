namespace ReservatıonAPI
{
    public class Train
    {
        public string Name {  get; set; }
        public List<Carriage> Carriages { get; set; } = new List<Carriage>();

    }
}
