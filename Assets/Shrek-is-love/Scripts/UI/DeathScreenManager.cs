using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private UIManipulation UIManipulation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void Restart()
    {
        UIManipulation.RestartSequence();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        DataPersistenceManager.instance.ResetGame();
    }

    public void ExitToMainMenu()
    {
        DataPersistenceManager.instance.ResetGame();
        SceneManager.LoadSceneAsync("MainMenu");
    }
}