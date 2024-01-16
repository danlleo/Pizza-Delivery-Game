using System;
using System.IO;
using DataPersistence.Data;
using UnityEngine;

namespace DataPersistence
{
    public class FileDataHandler : IFileDataHandler
    {
        private const string ENCRYPTION_KEYWORD = "Prototype";
        
        private string _dataDirectionPath;
        private string _dataFileName;

        private bool _useEncryption;
        
        public FileDataHandler(string dataDirectionPath, string dataFileName, bool useEncryption)
        {
            _dataDirectionPath = dataDirectionPath;
            _dataFileName = dataFileName;
            _useEncryption = useEncryption;
        }

        public GameData Load()
        {
            string fullPath = Path.Combine(_dataDirectionPath, _dataFileName);

            GameData loadedData = null;

            if (!File.Exists(fullPath))
            {
                Debug.LogError("Path not found!");
                return null;
            }
            
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
                
                if (_useEncryption)
                {
                    dataToLoad = EncryptDecrypt(dataToLoad);
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

                if (_useEncryption)
                {
                    dataToStore = EncryptDecrypt(dataToStore);
                }
                
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

        public void DeleteSaveFile()
        {
            string fullPath = Path.Combine(_dataDirectionPath, _dataFileName);
            
            if (!File.Exists(fullPath))
            {
                Debug.Log("Save file doesn't exist!");
                return;
            }
            
            File.Delete(fullPath);
        }

        private string EncryptDecrypt(string data)
        {
            string modifiedData = "";

            for (int i = 0; i < data.Length; i++)
            {
                modifiedData += (char)(data[i] ^ ENCRYPTION_KEYWORD[i % ENCRYPTION_KEYWORD.Length]);
            }

            return modifiedData;
        }
    }
}
