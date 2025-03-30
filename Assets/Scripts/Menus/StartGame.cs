using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Go()
    {
        MoneyUI.ResetScore();
        SceneManager.LoadScene("SampleScene");
    }
}
