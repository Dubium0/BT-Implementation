using TMPro;
using UnityEngine;

public class PlayerHpTracker : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;


    private void Update()
    {
        text.text = "HP: " + SingletonPlayer.Instance.Health;
    }


}
