using DataPersistence.Data;

namespace DataPersistence
{
    public interface IFileDataHandler
    {
        public GameData Load();
        public void Save(GameData gameData);
        public void DeleteSaveFile();
    }
}