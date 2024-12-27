using UnityEngine;

public class RestartGame : MonoBehaviour
{
    
    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }


}
