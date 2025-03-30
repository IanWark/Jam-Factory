using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Go()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
