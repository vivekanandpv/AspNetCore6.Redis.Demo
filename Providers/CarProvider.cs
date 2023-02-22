using AspNetCore6.Redis.Demo.Models;

namespace AspNetCore6.Redis.Demo.Providers;

public interface ICarProvider
{
    Car Get();
}

public class CarProvider : ICarProvider
{
    public Car Get()
    {
        Thread.Sleep(3000);
        return new Car { Color = "Green", Id = 1234, Make = "Maruti Suzuki", Model = "Dzire", Year = 2020 };
    }
}