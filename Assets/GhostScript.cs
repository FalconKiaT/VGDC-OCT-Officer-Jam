using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GhostScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float targetScale;
    [SerializeField] private float startingScale;
    [SerializeField] private Color flashColor;
    [SerializeField] private float timeToStartFlashing;
    [SerializeField] private float timeToKillPlayer;
    [SerializeField] private float flashInterval;

    [Header("References")]
    public Image sr;

    // Local Variables
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GhostRoutine());
    }

    private void OnDestroy()
    {
        // Ended, kill routines
        StopAllCoroutines();
    }

    private IEnumerator GhostRoutine()
    {
        // Variables
        Coroutine flashRoutine = null;
        
        // Set the starting variables
        this.transform.localScale = new Vector3(startingScale, startingScale, startingScale);

        // Linear Equation
        float timePassed = 0;
        float rateOfChange = (targetScale - startingScale) / (timeToKillPlayer - 0);
        float currentScale = startingScale;

        while (timePassed < timeToKillPlayer) 
        {
            // Calculate current scale
            currentScale = rateOfChange * timePassed - startingScale;
            // Apply scale
            transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            // Add time
            timePassed += Time.deltaTime;
            // Check if we have passed the flashing time
            if (timePassed >= timeToStartFlashing && flashRoutine == null)
            {
                flashRoutine = StartCoroutine(FlashRoutine());
            }
            // Wait a frame
            yield return null;
        }

        KillGhost();
    }

    /// <summary>
    /// Starts flashing ghost before it kills player
    /// </summary>
    /// <returns></returns>
    private IEnumerator FlashRoutine()
    {
        Color originalColor = sr.color;
        WaitForSeconds flashWait = new WaitForSeconds(flashInterval);

        while (true)
        {
            sr.color = flashColor;
            yield return flashWait;
            sr.color = originalColor;
            yield return flashWait;
        }
    }

    /// <summary>
    /// Executes on click
    /// </summary>
    public void PlayerClickedGhost()
    {
        // Add score
        ScoreManager.instance.score += 1;
        // Kill ghost
        KillGhost();
    }

    private void KillGhost()
    {
        // Ended, kill routines
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
