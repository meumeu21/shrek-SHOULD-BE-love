using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManager : MonoBehaviour
{
    [SerializeField] private UIManipulation UIManipulation;

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