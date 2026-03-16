using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp129;

namespace ConsoleApp129.Tests
{
    [TestClass]
    public class HeroTests
    {
        [TestMethod]
        public void Hero_Constructor_Coordinates()
        {
            // Arrange
            int startX = 12;
            int startY = 12;

            // Act
            var hero = new Hero(startX, startY);

            // Assert
            Assert.AreEqual(startX, hero.X);
            Assert.AreEqual(startY, hero.Y);
        }

        [TestMethod]
        public void Hero_Constructor_StatusEffects()
        {
            // Arrange
            var hero = new Hero(12, 12);

            // Act

            // Assert
            Assert.AreEqual(0, hero.FreezeTurns);
            Assert.AreEqual(0, hero.SandTurns);
            Assert.AreEqual(0, hero.PoisonTurns);
        }

        [TestMethod]
        public void Hero_Constructor_Artifacts()
        {
            // Arrange
            var hero = new Hero(12, 12);

            // Act

            // Assert
            Assert.IsFalse(hero.HasAmulet);
            Assert.IsFalse(hero.HasCrown);
            Assert.IsFalse(hero.HasScepter);
        }

        [TestMethod]
        public void Hero_PickAmulet_HasAmulet()
        {
            // Arrange
            var hero = new Hero(12, 12);

            // Act
            hero.HasAmulet = true;

            // Assert
            Assert.IsTrue(hero.HasAmulet);
        }
    }
}