using UnityEngine;
using Utility;

public class StartGame : MonoBehaviour
{
    [SerializeField]
    private GameEvent startGameEvent;


    public void StartGameEvent()
    {
        startGameEvent.Raise();
    }


}
