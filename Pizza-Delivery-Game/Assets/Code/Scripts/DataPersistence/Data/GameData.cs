using System;
using System.Collections.Generic;

namespace DataPersistence.Data
{
    [Serializable]
    public class GameData
    {
        public List<string> SavedInventoryItems;

        /// <summary>
        /// The values defined in this constructor will be the default values
        /// the game starts with when there's no data to load
        /// </summary>
        public GameData()
        {
            SavedInventoryItems = new List<string>();
        }
    }
}
