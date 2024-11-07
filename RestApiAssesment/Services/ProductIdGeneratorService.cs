namespace RestApiAssesment.Services
{
    public class ProductIdGeneratorService
    {
        private static readonly Random _random = new Random();

        public int GenerateUniqueProductId()
        {
            return _random.Next(100000, 999999); 
        }

    }
}
