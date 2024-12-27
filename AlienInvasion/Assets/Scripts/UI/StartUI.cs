using UnityEngine;

public class StartUI : MonoBehaviour
{
    [SerializeField]
    private GameObject startUI;
    [SerializeField]
    private GameObject inGameUI;


    public void StartGame()
    {
        startUI.SetActive(false);
        inGameUI.SetActive(true);
    }
}
