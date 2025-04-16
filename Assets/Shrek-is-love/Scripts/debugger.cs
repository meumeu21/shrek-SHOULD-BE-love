using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class debugger : MonoBehaviour
{
    void Update()
    {
        GameObject selected = EventSystem.current.currentSelectedGameObject;
        Debug.Log("Selected: " + (selected != null ? selected.name : "None"));
    }
}
