namespace FoodDeliveryAPI.Dtos
{
    public class CoordinateDto
    {
        private double latitude;
        private double longitude;

        public CoordinateDto(string coordinateString)
        {
            string[] aux = coordinateString.Split(',');

            if (aux.Length != 2)
            {
                throw new ArgumentException("Wrong format of the coordinate");
            }

            try
            {
                Latitude = double.Parse(aux[0].Trim());
                Longitude = double.Parse(aux[1].Trim());
            }
            catch (Exception)
            {
                throw new ArgumentException("Wrong format of the coordinate");
            }
        }

        public double Latitude { get => latitude; set => latitude = value; }
        public double Longitude { get => longitude; set => longitude = value; }
    }
}
