using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TwoButtonNavigation : MonoBehaviour
{
    public Button firstButton;
    public Button secondButton;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                firstButton.Select();
            }
            else if (EventSystem.current.currentSelectedGameObject == firstButton.gameObject)
            {
                secondButton.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                secondButton.Select();
            }
            else if (EventSystem.current.currentSelectedGameObject == secondButton.gameObject)
            {
                firstButton.Select();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            GameObject selected = EventSystem.current.currentSelectedGameObject;
            
            if (selected != null)
            {
                Button selectedButton = selected.GetComponent<Button>();
                if (selectedButton != null)
                {
                    selectedButton.onClick.Invoke();
                }
            }
        }
    }
    
    private void OnEnable()
    {
        firstButton.Select();
    }
}