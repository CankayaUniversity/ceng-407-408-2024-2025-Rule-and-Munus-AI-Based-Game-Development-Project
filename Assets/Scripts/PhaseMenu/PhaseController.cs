using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseController : MonoBehaviour
{
    public GameManager gameManager;
    private void Awake()
    {
        gameManager.phase += 1;
    }
    public void ArenaButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
    }

    public void GatherButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FinishPhaseButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
