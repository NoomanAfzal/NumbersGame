using UnityEngine;

public class Exitpopup : MonoBehaviour
{
    public GameObject exitPopup;

    void Update()
    {
        // Check for exit conditions (e.g., pressing the back button on mobile or Esc key on PC).
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowExitPopup();
        }
    }

    public void ShowExitPopup()
    {
        exitPopup.SetActive(true);
        // You may want to freeze the game while the popup is active.
        Time.timeScale = 0f;
    }

    public void CloseExitPopup()
    {
        exitPopup.SetActive(false);
        // Unfreeze the game when the popup is closed.
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        // Add any additional cleanup or save functionality before quitting the game.
        Application.Quit();
    }
}
