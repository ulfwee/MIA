using Moq;
public interface IDataService
{
    int GetData();
}

public class MyService
{
    private readonly IDataService _dataService;

    public MyService(IDataService dataService)
    {
        _dataService = dataService;
    }

    public int Calculate()
    {
        return _dataService.GetData() * 2;
    }
}

public class MyServiceTests
{
    [Fact]
    public void Calculate_WithMockedDataService_ReturnsCorrectResult()
    {
        // Arrange
        var mockDataService = new Mock<IDataService>();
        mockDataService.Setup(ds => ds.GetData()).Returns(10);

        var service = new MyService(mockDataService.Object);

        // Act
        var result = service.Calculate();

        // Assert
        Assert.Equal(20, result);
    }
}