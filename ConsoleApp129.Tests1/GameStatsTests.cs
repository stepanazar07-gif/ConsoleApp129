using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp129;

namespace ConsoleApp129.Tests
{
    [TestClass]
    public class GameStatsTests
    {
        [TestMethod]
        public void GameStats_Constructor_DefaultValues()
        {
            // Arrange
            var stats = new GameStats();

            // Act
            // (создание объекта в Arrange)

            // Assert
            Assert.AreEqual(40, stats.HP);
            Assert.AreEqual(40, stats.MaxHP);
            Assert.AreEqual(10, stats.Armor);
            Assert.AreEqual(1, stats.Level);
        }

        [TestMethod]
        public void GameStats_TakeDamage_WithArmor()
        {
            // Arrange
            var stats = new GameStats();
            int damage = 15;
            int expectedHP = 35;

            // Act
            stats.TakeDamage(damage);

            // Assert
            Assert.AreEqual(expectedHP, stats.HP);
        }

        [TestMethod]
        public void GameStats_TakeDamage_MinimumDamage()
        {
            // Arrange
            var stats = new GameStats();
            int damage = 5;
            int expectedHP = 39;

            // Act
            stats.TakeDamage(damage);

            // Assert
            Assert.AreEqual(expectedHP, stats.HP);
        }

        [TestMethod]
        public void GameStats_TakeDamage_HPNotNegative()
        {
            // Arrange
            var stats = new GameStats();
            stats.HP = 5;
            int damage = 100;

            // Act
            stats.TakeDamage(damage);

            // Assert
            Assert.IsTrue(stats.HP >= 0);
        }
    }
}