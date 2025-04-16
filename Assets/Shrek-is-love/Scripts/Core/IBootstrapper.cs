using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBootstrapper
{
    void InstantiateAudioManager();
    void InstantiatedataPersistenceManager();
    void ConfigureDependencies();
}
