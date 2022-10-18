using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Option()
    {
        return;
    }

    public void QuitGame()
    {
        // that this will only work from a built application.
        //Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("StartScene");
    }
}
