using System;
using System.Collections.Generic;
using System.Linq;
using DataPersistence.Data;
using Interfaces;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace DataPersistence
{
    public class DataPersistenceManager : IInitializable, IDisposable
    {
        // FILE NAME - Data.game
        // USE ENCRYPTION - True
        private string _fileName;
        private bool _useEncryption;
        
        private List<IDataPersistence> _dataPersistenceObjects;
        private IFileDataHandler _fileDataHandler;
        private GameData _gameData;

        public DataPersistenceManager(string fileName, bool useEncryption)
        {
            _fileName = fileName;
            _useEncryption = useEncryption;
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, _fileName, _useEncryption);
            _dataPersistenceObjects = FindAllDataPersistenceObjects();
        }

        public void Initialize()
        {
            NewGameStaticEvent.OnNewGame += NewGameStaticEvent_OnNewGame;
            LoadStaticEvent.OnLoad += LoadStaticEvent_OnLoad;
            SaveStaticEvent.OnSave += SaveStaticEvent_OnSave;
        }

        public void Dispose()
        {
            NewGameStaticEvent.OnNewGame -= NewGameStaticEvent_OnNewGame;
            LoadStaticEvent.OnLoad -= LoadStaticEvent_OnLoad;
            SaveStaticEvent.OnSave -= SaveStaticEvent_OnSave;
        }
        
        private void NewGame()
        {
            _fileDataHandler.DeleteSaveFile();
            _gameData = new GameData();
        }

        private void LoadGame()
        {
            _gameData = _fileDataHandler.Load();
            
            // If no data found - start a new game
            if (_gameData == null)
            {
                Debug.Log("No data was found. Starting a new game...");
                NewGame();
                return;
            }

            foreach (IDataPersistence dataPersistenceObject in _dataPersistenceObjects)
            {
                dataPersistenceObject.LoadData(_gameData);
            }
        }

        private void SaveGame()
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
                Object.FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();

            return new List<IDataPersistence>(dataPersistenceObjects);
        }
        
        #region Events
        
        private void NewGameStaticEvent_OnNewGame(object sender, EventArgs e)
        {
            NewGame();
        }

        private void SaveStaticEvent_OnSave(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void LoadStaticEvent_OnLoad(object sender, EventArgs e)
        {
            LoadGame();
        }
        
        #endregion
    }
}
