namespace FoodDeliveryAPI.Dtos
{
    public class CoordinateDto
    {
        public CoordinateDto() { }

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

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
