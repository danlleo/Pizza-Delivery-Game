using System;
using System.Collections.Generic;
using DataPersistence.Data;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace DataPersistence
{
    public class DataPersistenceManager : IInitializable, IDisposable
    {
        public static Action<DataPersistenceManager> OnAnyDataLoadRequest;
        
        private readonly List<PersistentData> _dataPersistenceObjects;
        private readonly IFileDataHandler _fileDataHandler;
        
        private GameData _gameData;

        public DataPersistenceManager(string fileName, bool useEncryption)
        {
            _fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, useEncryption);
            _dataPersistenceObjects = new List<PersistentData>();
        }

        public void Initialize()
        {
            NewGameStaticEvent.OnNewGame += NewGameStaticEvent_OnNewGame;
            LoadStaticEvent.OnLoad += LoadStaticEvent_OnLoad;
            SaveStaticEvent.OnSave += SaveStaticEvent_OnSave;
            SceneManager.activeSceneChanged += SceneManager_OnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_OnSceneLoaded;
        }

        public void Dispose()
        {
            NewGameStaticEvent.OnNewGame -= NewGameStaticEvent_OnNewGame;
            LoadStaticEvent.OnLoad -= LoadStaticEvent_OnLoad;
            SaveStaticEvent.OnSave -= SaveStaticEvent_OnSave;
            SceneManager.activeSceneChanged -= SceneManager_OnActiveSceneChanged;
            SceneManager.sceneLoaded -= SceneManager_OnSceneLoaded;
            
            ClearDataPersistenceObjectsSubscriptions();
        }

        public void AddDataPersistenceObject(PersistentData persistentData)
        {
            _dataPersistenceObjects.Add(persistentData);
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

            foreach (PersistentData dataPersistenceObject in _dataPersistenceObjects)
            {
                dataPersistenceObject.LoadData(_gameData);
            }
        }

        private void SaveGame()
        {
            if (_dataPersistenceObjects != null)
            {
                foreach (PersistentData dataPersistenceObject in _dataPersistenceObjects)
                {
                    dataPersistenceObject.SaveData(_gameData);
                }
            }
            
            // Write saved data to a file
            _fileDataHandler.Save(_gameData);
        }

        private void ClearDataPersistenceObjectsSubscriptions()
        {
            foreach (PersistentData dataPersistence in _dataPersistenceObjects)
            {
                dataPersistence.Dispose();
            }
        }
        
        private void ClearDataPersistenceObjects()
        {
            _dataPersistenceObjects.Clear();
        }
        
        #region Events
        
        private void NewGameStaticEvent_OnNewGame(object sender, EventArgs e)
        {
            NewGame();
            OnAnyDataLoadRequest?.Invoke(this);
        }

        private void SaveStaticEvent_OnSave(object sender, EventArgs e)
        {
            SaveGame();
        }

        private void LoadStaticEvent_OnLoad(object sender, EventArgs e)
        {
            LoadGame();
        }
        
        private void SceneManager_OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name == nameof(Enums.Scenes.Scene.LoadingScene))
                return;
            
            ClearDataPersistenceObjects();
            OnAnyDataLoadRequest?.Invoke(this);
        }
        
        private void SceneManager_OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            if (arg0.name == nameof(Enums.Scenes.Scene.LoadingScene))
                return;
            
            LoadGame();
        }
        
        #endregion
    }
}
