using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApp129.Save;
using System.IO;

namespace ConsoleApp129.Tests
{
    [TestClass]
    public class SaveManagerTests
    {
        private string testSavePath = "test_save.json";

        [TestCleanup]
        public void Cleanup()
        {
            if (File.Exists(testSavePath))
            {
                File.Delete(testSavePath);
            }
        }

        [TestMethod]
        public void SaveManager_Save_FileCreated()
        {
            // Arrange
            var data = new GameData
            {
                MapLevel = 1,
                HeroHP = 40,
                HeroMaxHP = 40
            };

            // Act
            SaveManager.Save(data);

            // Assert
            Assert.IsTrue(File.Exists(testSavePath));
        }

        [TestMethod]
        public void SaveManager_Load_FileNotFound()
        {
            // Arrange
            // Файл не создаём

            // Act
            var result = SaveManager.Load();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SaveManager_SaveAndLoad_DataIntegrity()
        {
            // Arrange
            var originalData = new GameData
            {
                MapLevel = 2,
                HeroHP = 35,
                HeroMaxHP = 40,
                HeroArmor = 10,
                HasAmulet = true,
                FreezeTurns = 0
            };

            // Act
            SaveManager.Save(originalData);
            var loadedData = SaveManager.Load();

            // Assert
            Assert.IsNotNull(loadedData);
            Assert.AreEqual(originalData.MapLevel, loadedData.MapLevel);
            Assert.AreEqual(originalData.HeroHP, loadedData.HeroHP);
            Assert.AreEqual(originalData.HasAmulet, loadedData.HasAmulet);
        }
    }
}