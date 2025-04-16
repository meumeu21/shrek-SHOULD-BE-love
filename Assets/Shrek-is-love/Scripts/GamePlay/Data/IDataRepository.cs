using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataRepository
{
    GameData Load();
    void Save(GameData data);
}
