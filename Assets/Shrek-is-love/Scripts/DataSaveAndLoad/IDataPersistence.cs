using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence
{
   void LoadData(GameData gameData); // только чтение данных

   void SaveData(ref GameData gameData); // референс позволяет изменять данные
}
