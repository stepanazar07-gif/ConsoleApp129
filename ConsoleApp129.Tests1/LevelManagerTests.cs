using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp129;

namespace ConsoleApp129.Tests
{
    [TestClass]
    public class LevelManagerTests
    {
        [TestMethod]
        public void LevelManager_Constructor_DefaultLevel()
        {
            // Arrange
            var levelManager = new LevelManager();

            // Act

            // Assert
            Assert.AreEqual(1, levelManager.CurrentLevel);
            Assert.IsFalse(levelManager.DoorOpen);
        }

        [TestMethod]
        public void LevelManager_ResetLevel_DoorClosed()
        {
            // Arrange
            var levelManager = new LevelManager();

            // Act
            levelManager.ResetLevel();

            // Assert
            Assert.IsFalse(levelManager.DoorOpen);
        }
    }
}