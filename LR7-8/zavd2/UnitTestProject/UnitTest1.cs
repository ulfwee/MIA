using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;

using Project.Repositories;
using Project.Services;
using Xunit;

public class MasterServiceTests
{
    private readonly Mock<IMasterRepository> _mockRepo;
    private readonly MasterService _service;

    public MasterServiceTests()
    {
        _mockRepo = new Mock<IMasterRepository>();
        _service = new MasterService(_mockRepo.Object);
    }

    private List<Master> GetSampleMasters() => new List<Master>
    {
        new Master { Id = 1, Name = "Іван Петренко", Category = Category.Plumbing, Ranking = 4.8 },
        new Master { Id = 2, Name = "Олег Сидоренко", Category = Category.Electrical, Ranking = 4.9 },
        new Master { Id = 3, Name = "Микола Коваль", Category = Category.Assembly, Ranking = 4.5 }
    };

    [Fact]
    public async Task GetTopRankedMastersAsync_ReturnsMasterWithHighestRanking()
    {
        var masters = GetSampleMasters();
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(masters);

        var topMasters = (await _service.GetTopRankedMastersAsync()).ToList();

        Assert.Single(topMasters); 
        Assert.Equal("Олег Сидоренко", topMasters[0].Name);
        Assert.Equal(4.9, topMasters[0].Ranking);
    }

    [Fact]
    public async Task GetAverageRankingAsync_ReturnsCorrectAverage()
    {
        var masters = GetSampleMasters();
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(masters);

        var avg = await _service.GetAverageRankingAsync();

        Assert.InRange(avg, 4.733, 4.734);
    }

    [Fact]
    public async Task CountLowRankedMastersAsync_ReturnsCorrectCount()
    {
        var masters = GetSampleMasters();
        _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(masters);

        var count = await _service.CountLowRankedMastersAsync(4.8);

        Assert.Equal(1, count);
    }

    [Fact]
    public async Task CountLowRankedMastersAsync_ThrowsForInvalidThreshold()
    {
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CountLowRankedMastersAsync(-1));
        await Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => _service.CountLowRankedMastersAsync(6));
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectMaster()
    {
        var masters = GetSampleMasters();
        _mockRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(masters[1]);

        var master = await _service.GetByIdAsync(2);

        Assert.NotNull(master);
        Assert.Equal("Олег Сидоренко", master.Name);
        Assert.Equal(Category.Electrical, master.Category);
        Assert.Equal(4.9, master.Ranking);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsIfMasterNotFound()
    {
        _mockRepo.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Master?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() => _service.GetByIdAsync(99));
    }
}
