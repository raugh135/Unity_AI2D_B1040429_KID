using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour
{
    public void Replay()
    {
        SceneManager.LoadScene("遊戲");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
