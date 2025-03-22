using BackPack;

namespace testy
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void Test_MinItem()
        {
            Problem problem = new Problem(5, 1000);
            int pojemnosc = problem.przedmioty.Min(p => p.waga); // moze pomiescic przynajmniej jeden najmniejszy przedmiot
            Result result = problem.Solve(pojemnosc);
            
            Assert.IsTrue(result.objects.Length > 0); // brak assert.isBigger??
        }

        [TestMethod]
        public void Test_NoItem()
        {
            // Żaden przedmiot nie mieści się w plecaku 
            Problem problem = new Problem(5, 1000);
            int pojemnosc = 0;
            Result result = problem.Solve(pojemnosc);

            Assert.AreEqual(0, result.objects.Length);
        }

        [TestMethod]
        public void Test_SpecificInstance()
        {
            Problem problem = new Problem(3, 1);
            problem.przedmioty = new Przedmiot[]
            {
                new Przedmiot(10, 5), // Stosunek 2.0
                new Przedmiot(6, 4),  //  1.5
                new Przedmiot(5, 10)  //  0.5
            };
            Assert.AreEqual(3, problem.przedmioty.Count()); // 3 przedmioty
            int pojemnosc = 9;
            Result result = problem.Solve(pojemnosc);

            Assert.AreEqual(2, result.objects.Length, "powinno: wybrane 2 przedmioty.");
            Assert.AreEqual(16, result.sumWartosc, "wartosc = 16.");
            Assert.AreEqual(9, result.sumWaga, "waga = 9.");
        }
        [TestMethod]
        public void Test_EmptyItemList()
        {
            Problem problem = new Problem(0, 1);
            int pojemnosc = 10;
            Result result = problem.Solve(pojemnosc);
            Assert.AreEqual(0, result.objects.Length);
            Assert.AreEqual(0, result.sumWartosc);
            Assert.AreEqual(0, result.sumWaga);
        }
        [TestMethod]
        // Czy faktycznie jest 10 przedmiotow
        public void Test_Values()
        {
            List<int> sizes = new List<int>() { 10, 20, 30, 40, 50 };
            foreach (int n in sizes)
            {
                Problem problem = new(n, 19);
                Assert.AreEqual(n, problem.przedmioty.Count());
            }
        }
    }
}
