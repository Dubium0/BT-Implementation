using TMPro;
using UnityEngine;

public class ScoreUpdater : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI text;

    private int score = 0;

    private void Start()
    {
        UpdateView();
    }
    public void IncreaseScore()
    {
        score+=10;
        UpdateView();


    }
    private void UpdateView()
    {
        text.text = "Score: " + score;  
    }
}
