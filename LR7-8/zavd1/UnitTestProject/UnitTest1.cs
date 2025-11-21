using Project.Services;

namespace UnitTestProject
{
    public class UnitTest1
    {
        private readonly AgeService _service;

        public UnitTest1()
        {
            _service = new AgeService();
        }

        public static IEnumerable<object[]> AdditionAgeData()
        {
            yield return new object[] { 13, false };
            yield return new object[] { 27, true };
        }

        [Theory]
        [InlineData(17, false)]
        [InlineData(18, true)]
        [InlineData(25, true)]
        public void IsAdult_ReturnsCorrectResult(int age, bool expected)
        {
            var result = _service.IsAdult(age);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(30, 35)]
        [InlineData(65, 0)]
        [InlineData(70, 0)]
        public void YearsToRetirement_ReturnsCorrectYears(int age, int expected)
        {
            var result = _service.YearsToRetirement(age);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void IsAdult_ThrowsForNegativeAge()
        {
            Assert.Throws<ArgumentException>(() => _service.IsAdult(-1));
        }

        [Fact]
        public void YearsToRetirement_ThrowsForNegativeAge()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => _service.YearsToRetirement(-5));
        }

        [Fact]
        public void YearsToRetirement_IsEquel()
        {
            var ageService = new AgeService();
            int age1 = 45;

            int result = ageService.YearsToRetirement(age1);

            Assert.Equal(20, result);
        }

        [Fact]
        public void IsAdult()
        {
            var ageService = new AgeService();
            int age1 = 17;

            bool result = ageService.IsAdult(age1);

            Assert.False(result);
        }

        [Theory]
        [ClassData(typeof(AgeData))]
        public void YearsToRetirement_withAgeData_ReturnsCorrectYears(int age, int expected)
        {
            var ageService = new AgeService();
            int result = ageService.YearsToRetirement(age);
            Assert.Equal(expected, result);
        }

        [Theory]
        [MemberData(nameof(AdditionAgeData))]
        public void IsAdult_wAdditionAgeData_ReturnsCorrectResult(int age, bool expected)
        {
            var ageservice = new AgeService();
            bool result = ageservice.IsAdult(age);
            Assert.Equal(expected, result);

        }

        [Fact]
        public void AreTheSame()
        {
            var ageservice1 = new AgeService();
            var ageservice2 = new AgeService();
            Assert.NotSame(ageservice1, ageservice2);
        }

        [Fact]
        public void StringStartsWith()
        {
            string str1 = "Age isnt correct";
            Assert.StartsWith("Age", str1);
        }

        [Fact]
        public void IsInRange()
        {
            var ageService = new AgeService();
            int age1 = 23;

            int result = ageService.YearsToRetirement(age1);
            Assert.InRange(result, 40, 50);
        }
    }
}
