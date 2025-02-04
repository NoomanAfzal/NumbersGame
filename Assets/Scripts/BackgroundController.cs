using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundController : MonoBehaviour
{
    public Sprite[] backgroundImages; // Array to store background images
    public Image backgroundImage; // Reference to the Image component on your menu background
    public Text[] themeTags; // Array of Text components for theme tags (e.g., "In Use", "Select")

    private const string SelectedBackgroundKey = "SelectedBackground";

    // Set the background during the awake phase of the script's lifecycle
    void Awake()
    {
        LoadSelectedBackground(); // Load the selected background during awake
        UpdateThemeTags(PlayerPrefs.GetInt(SelectedBackgroundKey));
        // Load the selected background when the game starts
        //LoadSelectedBackground();
        Debug.Log("Index" + PlayerPrefs.GetInt(SelectedBackgroundKey));
        backgroundImage.sprite = backgroundImages[PlayerPrefs.GetInt(SelectedBackgroundKey)];
    }

    // Function to set the background based on button click
    public void SetBackground(int index)
    {
        if (index >= 0 && index < backgroundImages.Length)
        {
            backgroundImage.sprite = backgroundImages[index];

            PlayerPrefs.SetInt(SelectedBackgroundKey, index);
            Debug.Log(PlayerPrefs.GetInt(SelectedBackgroundKey) + "Indexx");
            PlayerPrefs.Save();

            UpdateThemeTags(index);
        }
    }

    // Load the selected background when the game starts
    private void LoadSelectedBackground()
    {
        int selectedBackgroundIndex = PlayerPrefs.GetInt(SelectedBackgroundKey);
        SetBackground(selectedBackgroundIndex);
      /*  // Ensure the selected index is within the valid range
        if (selectedBackgroundIndex >= 0 && selectedBackgroundIndex < backgroundImages.Length)
        {
            // Set the background
            SetBackground(selectedBackgroundIndex);
        }
        else
        {
            Debug.LogError("Invalid saved background index: " + selectedBackgroundIndex);
        }*/
    }

    // Update the theme tags to reflect the selection status
    private void UpdateThemeTags(int selectedIndex)
    {
        for (int i = 0; i < themeTags.Length; i++)
        {
            if (i == selectedIndex)
            {
                themeTags[i].text = "In Use";
            }
            else
            {
                themeTags[i].text = "Select";
            }
        }
    }
}
