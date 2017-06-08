using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCtrl : MonoBehaviour {

	public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void realoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void playLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void goToMenuPage(string name)
    {
        foreach (GameObject canvas in GameObject.FindGameObjectsWithTag("MenuCanvas"))
        {
            canvas.GetComponent<Canvas>().enabled = false;
        }
        GameObject.Find(name).GetComponent<Canvas>().enabled = true;
    }
}
