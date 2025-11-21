using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services
{
    public class AgeData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { 55, 10 };
            yield return new object[] { 80, 0 };
            yield return new object[] { 15, 50 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    public class AgeService
    {
        private int AdultAge = 18;
        private int RetireAge = 65;
        public bool IsAdult(int age)
        {
            if (age < 0)
                throw new ArgumentException("Age cannot be negative");
            return age >= AdultAge;
        }

        public int YearsToRetirement(int age)
        {
            if (age < 0)
                throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative");

            return age >= RetireAge ? 0 : RetireAge - age;
        }

        
    }
}
