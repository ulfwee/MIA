public class ParkingPaymentService
{
    public bool ProcessPayment(string carNumber, decimal amount)
    {
        Console.WriteLine($"Processing parking payment {amount:C} for car {carNumber}");
        return true;
    }
}


public class ParkingNotificationService
{
    public void SendEmail(string email, string message)
    {
        Console.WriteLine($"Sending email to {email}: {message}");
    }
}



public class ParkingAllocationService
{
    public string AllocateSpot(List<string> zones)
    {
        var rnd = new Random();
        var zone = zones[rnd.Next(zones.Count)];
        var spot = rnd.Next(1, 50); 

        var result = $"{zone}-{spot}";
        Console.WriteLine($"Allocated parking spot: {result}");

        return result;
    }
}



public class ParkingRepository
{
    public string SaveParkingSession(string carNumber, string spot)
    {
        var id = Guid.NewGuid().ToString();
        Console.WriteLine($"Parking session saved ID: {id}, Car: {carNumber}, Spot: {spot}");
        return id;
    }
}



public class SmartParkingFacade
{
    private readonly ParkingPaymentService _payment;
    private readonly ParkingNotificationService _notification;
    private readonly ParkingAllocationService _allocation;
    private readonly ParkingRepository _repository;

    public SmartParkingFacade(
        ParkingPaymentService payment,
        ParkingNotificationService notification,
        ParkingAllocationService allocation,
        ParkingRepository repository)
    {
        _payment = payment;
        _notification = notification;
        _allocation = allocation;
        _repository = repository;
    }

    public void ParkCar(string carNumber, decimal amount, string email, List<string> zones)
    {
        Console.WriteLine("\n=== Starting smart parking workflow ===");

        var spot = _allocation.AllocateSpot(zones);

        var sessionId = _repository.SaveParkingSession(carNumber, spot);

        var paymentSuccess = _payment.ProcessPayment(carNumber, amount);
        if (!paymentSuccess)
        {
            Console.WriteLine("Payment failed.");
            return;
        }

        _notification.SendEmail(email, $"Your car has been parked at spot {spot}. Session ID: {sessionId}");

        Console.WriteLine("Smart parking process completed.\n");
    }
}



class Program
{
    static void Main()
    {
        var payment = new ParkingPaymentService();
        var notification = new ParkingNotificationService();
        var allocation = new ParkingAllocationService();
        var repository = new ParkingRepository();

        var smartParking = new SmartParkingFacade(payment, notification, allocation, repository);

        smartParking.ParkCar(
            carNumber: "AA1234BB",
            amount: 50.00m,
            email: "driver@example.com",
            zones: new List<string> { "A", "B", "C" }
        );
    }
}
