abstract class ParkingProcessor
{
    public void ProcessParking()
    {
        IdentifyCar();
        FindSpot();
        ParkCar();
        ProcessPayment();
        ConfirmParking();
    }

    protected virtual void IdentifyCar()
    {
        Console.WriteLine("Автомобіль ідентифіковано через камеру.");
    }

    protected abstract void FindSpot();

    protected virtual void ParkCar()
    {
        Console.WriteLine("Автомобіль припарковано.");
    }

    protected abstract void ProcessPayment();

    protected virtual void ConfirmParking()
    {
        Console.WriteLine("Підтвердження паркування відправлено водію.");
    }
}

class MallParkingProcessor : ParkingProcessor
{
    protected override void FindSpot()
    {
        Console.WriteLine("Пошук місця серед зон A, B, C...");
    }

    protected override void ProcessPayment()
    {
        Console.WriteLine("Оплата паркування через термінал ТРЦ.");
    }
}

class BusinessParkingProcessor : ParkingProcessor
{
    protected override void FindSpot()
    {
        Console.WriteLine("Виділення місця на основі бронювання співробітника.");
    }

    protected override void ProcessPayment()
    {
        Console.WriteLine("Оплата списана з корпоративного рахунку.");
    }
}

class EvParkingProcessor : ParkingProcessor
{
    protected override void FindSpot()
    {
        Console.WriteLine("Пошук вільної зарядної станції...");
    }

    protected override void ProcessPayment()
    {
        Console.WriteLine("Оплата за електроенергію та паркування через мобільний застосунок.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        ParkingProcessor mall = new MallParkingProcessor();
        mall.ProcessParking();

        Console.WriteLine();

        ParkingProcessor business = new BusinessParkingProcessor();
        business.ProcessParking();

        Console.WriteLine();

        ParkingProcessor ev = new EvParkingProcessor();
        ev.ProcessParking();
    }
}
