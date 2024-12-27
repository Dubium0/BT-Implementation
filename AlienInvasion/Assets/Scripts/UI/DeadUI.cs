using UnityEngine;

public class DeadUI : MonoBehaviour
{
    [SerializeField]
    private GameObject panel;
    public void OpenUpThePanel() { 
        panel.SetActive(true);
    }
    
}
