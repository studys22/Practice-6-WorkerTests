using WorkersLib;

namespace WorkerTests
{
    public class WorkerTests
    {
        [Test]
        public void DefaultConstructor_InitializesDefaults()
        {
            var w = new WORKER();
            Assert.That(w.FullName, Is.Not.Null);
            Assert.That(w.Position, Is.Not.Null);
            Assert.That(w.Salary, Is.EqualTo(0m));
            Assert.That(w.YearHired, Is.EqualTo(DateTime.Now.Year));
        }

        [Test]
        public void FullConstructor_SetsAllFields()
        {
            var w = new WORKER("Иванов И.И.", "Инженер", 50000m, 2015);
            Assert.That(w.FullName, Is.EqualTo("Иванов И.И."));
            Assert.That(w.Position, Is.EqualTo("Инженер"));
            Assert.That(w.Salary, Is.EqualTo(50000m));
            Assert.That(w.YearHired, Is.EqualTo(2015));
        }

        [Test]
        public void PartialConstructors_Work()
        {
            var w1 = new WORKER("Петров П.П.", "Менеджер");
            Assert.That(w1.FullName, Is.EqualTo("Петров П.П."));
            Assert.That(w1.Position, Is.EqualTo("Менеджер"));

            var w2 = new WORKER("Сидоров С.С.", 30000m);
            Assert.That(w2.FullName, Is.EqualTo("Сидоров С.С."));
            Assert.That(w2.Salary, Is.EqualTo(30000m));
        }

        [Test]
        public void Salary_NegativeBecomesZero()
        {
            var w = new WORKER("Иванов И.И.", "Преподаватель", -100m, 2020);
            Assert.That(w.Salary, Is.EqualTo(0m));

            w.Salary = -5;
            Assert.That(w.Salary, Is.EqualTo(0m));
        }

        [Test]
        public void YearHired_InvalidValuesSetToCurrentYear()
        {
            int current = DateTime.Now.Year;
            var w = new WORKER("Иванов И.И.", "Преподаватель", 100m, 0);
            Assert.That(w.YearHired, Is.EqualTo(current));

            w = new WORKER("Иванов И.И.", "Преподаватель", 100m, current + 5);
            Assert.That(w.YearHired, Is.EqualTo(current));
        }

        [Test]
        public void GetYearsOfService_CalculatesCorrectly()
        {
            int current = DateTime.Now.Year;
            var w = new WORKER("Иванов И.И.", "Преподаватель", 100m, current - 5);
            Assert.That(w.GetYearsOfService(), Is.EqualTo(5));
        }

        [Test]
        public void HasMoreThanYears_Behaviour()
        {
            int current = DateTime.Now.Year;
            var w = new WORKER("X", "Y", 100m, current - 10);
            Assert.That(w.HasMoreThanYears(9), Is.True);
            Assert.That(w.HasMoreThanYears(10), Is.False);
            Assert.That(w.HasMoreThanYears(11), Is.False);
        }

        [Test]
        public void Update_Method_ChangesFields()
        {
            var w = new WORKER("Иванов И.И.", "Преподаватель", 100m, 2018);
            w.Update(newFullName: "Петров П.П.", newPosition: "Декан", newSalary: 200m, newYearHired: 2016);

            Assert.That(w.FullName, Is.EqualTo("Петров П.П."));
            Assert.That(w.Position, Is.EqualTo("Декан"));
            Assert.That(w.Salary, Is.EqualTo(200m));
            Assert.That(w.YearHired, Is.EqualTo(2016));
        }

        [Test]
        public void DisplayNameYears_IncludesNameAndYears()
        {
            int current = DateTime.Now.Year;
            var w = new WORKER("Иванов И.И.", "Преподаватель", 100m, current - 3);
            string s = w.DisplayNameYears();
            Assert.That(s, Does.Contain("Иванов И.И."));
            Assert.That(s, Does.Contain("3"));
            Assert.That(s, Does.Contain("года"));
        }

        [Test]
        public void ToString_IncludesAllFields()
        {
            var w = new WORKER("Иванов И.И.", "Преподаватель", 1000m, 2019);
            var s = w.ToString();
            Assert.That(s, Does.Contain("Иванов И.И."));
            Assert.That(s, Does.Contain("Преподаватель"));
            Assert.That(s, Does.Contain("1000").Or.Contains("1 000"));
            Assert.That(s, Does.Contain("2019"));
        }
    }
}