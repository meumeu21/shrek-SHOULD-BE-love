using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickSoundButton : MonoBehaviour
{
    public void OnAnyButtonClicked()
    {
        FindObjectOfType<AudioManager>().Play("Click");
        Debug.Log("Click!");
    }
}
