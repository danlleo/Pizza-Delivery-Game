using System;
using DataPersistence.Data;

namespace DataPersistence
{
    public abstract class PersistentData : IDisposable
    {
        protected PersistentData()
        {
            DataPersistenceManager.OnAnyDataLoadRequest += OnAnyDataLoadRequest;
        }
        
        public void Dispose()
        {
            DataPersistenceManager.OnAnyDataLoadRequest -= OnAnyDataLoadRequest;
        }
        
        public abstract void LoadData(GameData gameData);
        
        public abstract void SaveData(GameData gameData);

        private void OnAnyDataLoadRequest(DataPersistenceManager dataPersistenceManager)
        {
            dataPersistenceManager.AddDataPersistenceObject(this);
        }
    }
}