using UnityEngine;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    public string[] sceneName;

    public void OnStartLoad()
    {
        SceneManager.LoadScene(sceneName[0], LoadSceneMode.Single);
    }

    public void OnSettingsLoad()
    {
        SceneManager.LoadScene(sceneName[1], LoadSceneMode.Single);
        GameObject.FindGameObjectWithTag("Music").GetComponent<Music>().StopMusic();
    }

    public void OnGameWonLoad()
    {
        SceneManager.LoadScene(sceneName[2], LoadSceneMode.Single);
    }

    public void OnGameLostLoad()
    {
        SceneManager.LoadScene(sceneName[3], LoadSceneMode.Single);
    }

    public void OnQuit()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("Level");
    }
}
