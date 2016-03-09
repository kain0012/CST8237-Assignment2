using UnityEngine;


public class GameplayHUD : MonoBehaviour
{
   
    public GUIText Level;
    public GUIText Bonus;

    public GUITexture InfoBackground;

    public GUIText Lives;
    public GUIText Score;


    public void HideInfo()
    {
        Level.enabled = false;
        Bonus.enabled = false;
        InfoBackground.enabled = false;
    }

    private void Start()
    {
        Level.enabled = false;
        Bonus.enabled = false;
        InfoBackground.enabled = false;
    }

    private void Update()
    {
    }

    public void UpdateInfo(string line1, string line2)
    {
        Level.text = line1;
        Bonus.text = line2;
        Level.enabled = true;
        Bonus.enabled = true;
        InfoBackground.enabled = true;
    }

    public void UpdateLives(string message)
    {
        Lives.text = string.Format("Lives: {0}", message);
    }

    public void UpdateScore(string message)
    {
        Score.text = string.Format("Score: {0}", message);
    }
}