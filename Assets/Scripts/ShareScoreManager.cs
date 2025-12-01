using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ShareScoreManager : MonoBehaviour
{
    // The player's score to be shared (get this from your game logic)
    public int playerScore = 1000;
    
    // Call this public method when the UI Share Button is clicked
    public void OnShareScoreButtonClicked()
    {
        // Start a Coroutine to handle the screenshot and sharing process
        StartCoroutine(ShareScoreWithScreenshot());
    }

    private IEnumerator ShareScoreWithScreenshot()
    {
        // 1. Wait for the end of the frame to capture the *entire* screen, 
        // including the latest score update.
        yield return new WaitForEndOfFrame();

        // 2. Capture the screen (works best on mobile)
        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        // 3. Save the captured image to a temporary file
        string filePath = Path.Combine(Application.temporaryCachePath, "shared_score_image.png");
        File.WriteAllBytes(filePath, screenshot.EncodeToPNG());

        // Clean up the Texture2D object after encoding
        Destroy(screenshot);
        
        // 4. Create the text message for sharing
        string shareText = $"I just scored **{playerScore}** in [Your Awesome Game Name]! Beat my high score! [Link to your game/website]";

        // 5. Use the NativeShare plugin to bring up the system share sheet
        new NativeShare()
            .SetText(shareText)
            .AddFile(filePath)
            .SetSubject("Check out my score!")
            .SetCallback((result, shareTarget) => Debug.Log("Share Result: " + result + ", Target: " + shareTarget))
            .Share();

        // Optional: Delete the temporary file after sharing is initiated
        // (The system may hold a reference to it until the sharing is complete)
        File.Delete(filePath);
    }
}
