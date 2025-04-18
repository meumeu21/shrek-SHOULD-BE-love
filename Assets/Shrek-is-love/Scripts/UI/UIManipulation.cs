using UnityEngine;

public class UIManipulation : MonoBehaviour 
{
    [SerializeField] private GameObject UICanvas;
    [SerializeField] private GameObject DeathScreen;
    [SerializeField] private GameObject PauseMenu;

    private bool isPaused;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            if(!isPaused && !DeathScreen.activeSelf)
            {
                isPaused = true;
                Cursor.lockState = CursorLockMode.Confined;
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
            }
            else if(isPaused && !DeathScreen.activeSelf)
            {
                isPaused = false;
                RestartSequence();
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    public void DeathSequence()
    {
        if (UICanvas != null && DeathScreen != null) 
        {
            UICanvas.SetActive(false);
            DeathScreen.SetActive(true);
            PauseMenu.SetActive(false);

            // Остановить время
            Time.timeScale = 0f;
        }
    }

    public void RestartSequence() 
    {
        if (UICanvas != null && DeathScreen != null) 
        {
            UICanvas.SetActive(true);
            DeathScreen.SetActive(false);
            PauseMenu.SetActive(false);

            // Восстановить время
            Time.timeScale = 1f;
        }
    }
}