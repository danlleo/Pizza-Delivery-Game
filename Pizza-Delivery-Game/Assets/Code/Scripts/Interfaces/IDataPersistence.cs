using DataPersistence.Data;

namespace Interfaces
{
    public interface IDataPersistence
    {
        public void LoadData(GameData gameData);
        public void SaveData(GameData gameData);
    }
}
