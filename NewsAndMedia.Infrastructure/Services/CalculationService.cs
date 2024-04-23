using NewsAndMedia.Core.Interfaces;

namespace NewsAndMedia.Infrastructure.Services
{
    public class CalculationService : ICalculationService
    {        
        public decimal Calculate(decimal input)
        {            

            return (decimal) Math.Pow(Math.Log((double)input), 1.0/3.0);
        }
    }
}
