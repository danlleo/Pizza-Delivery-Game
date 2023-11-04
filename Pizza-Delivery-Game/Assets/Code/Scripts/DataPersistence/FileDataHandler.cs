using System;
using System.IO;
using DataPersistence.Data;
using UnityEngine;

namespace DataPersistence
{
    public class FileDataHandler
    {
        private string _dataDirectionPath;
        private string _dataFileName;

        public FileDataHandler(string dataDirectionPath, string dataFileName)
        {
            _dataDirectionPath = dataDirectionPath;
            _dataFileName = dataFileName;
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(_dataDirectionPath, _dataFileName);

            GameData loadedData = null;

            if (!File.Exists(fullPath)) return loadedData;
            
            try
            {
                string dataToLoad;

                using (var fileStream = new FileStream(fullPath, FileMode.Open))
                {
                    using (var streamReader = new StreamReader(fileStream))
                    {
                        dataToLoad = streamReader.ReadToEnd();
                    }
                }

                loadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to load data from file: {e}");
            }

            return loadedData;
        }

        public void Save(GameData gameData)
        {
            string fullPath = Path.Combine(_dataDirectionPath, _dataFileName);

            try
            {
                // Create the directory the file will be written to if it doesn't already exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath) ?? string.Empty);

                string dataToStore = JsonUtility.ToJson(gameData, true);

                // Write the serialized data to the file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    using (var streamWriter = new StreamWriter(fileStream))
                    {
                        streamWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Error occured when trying to save data to file: {e}");
            }
        }
    }
}
