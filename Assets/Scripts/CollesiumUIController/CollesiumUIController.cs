using UnityEngine;
using UnityEngine.SceneManagement;

public class CollesiumUIController : MonoBehaviour
{
    public void BackToVillageButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void BackToPhaseButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

}
