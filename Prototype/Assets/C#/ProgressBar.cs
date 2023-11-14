using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    public GameObject progressbar;
    public float progression = 0f;
    public float maxWidth = 5f; // Adjust this value to set the maximum width of the progression bar.

    // Update is called once per frame
    void Update()
    {
        // Make sure progression is within the valid range (0 to 1).
        progression = Mathf.Clamp01(progression);

        // Calculate the new scale for the progression bar based on the progression.
        Vector3 newScale = progressbar.transform.localScale;
        newScale.x = progression * maxWidth;

        // Apply the new scale to the progression bar.
        progressbar.transform.localScale = newScale;
    }
}

