using System;
using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using Misc;
using UnityEngine;
using Interfaces;

namespace DataPersistence
{
    public class DataPersistenceManager : Singleton<DataPersistenceManager>
    {
        [Header("File Storage Config")] 
        [SerializeField] private string _fileName;
        
        private List<IDataPersistence> _dataPersistenceObjects;
        private FileDataHandler _fileDataHandler;
        private GameData _gameData;
        
        private void Start()
        {
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName);
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
            
            LoadGame();
        }

        private void OnApplicationQuit()
        {
            SaveGame();
        }

        public void NewGame()
        {
            _gameData = new GameData();
        }

        public void LoadGame()
        {
            _gameData = _fileDataHandler.Load();
            
            // If no data found - start a new game
            if (_gameData == null)
            {
                Debug.Log("No data was found. Starting a new game...");
                NewGame();
            }

            foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
            {
                dataPersistenceObject.LoadData(_gameData);
            }
        }

        public void SaveGame()
        {
            foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
            {
                dataPersistenceObject.SaveData(_gameData);
            }
            
            // Write saved data to a file
            _fileDataHandler.Save(_gameData);
        }

        private List<IDataPersistence> FindAllDataPersistenceObjects()
        {
            IEnumerable<IDataPersistence> dataPersistenceObjects =
                FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
    }
}
