using UnityEngine;
using UnityEngine.UI;

public class DurablityUI : MonoBehaviour
{
    public Text livesText;

    private void Update()
    {
        livesText.text = "���������: " + PlayerStats.Lives.ToString();
    }
}
