using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleDeath : MonoBehaviour
{
    [Header("Screamer Object")]
    [SerializeField] private GameObject screamerObject;
    [SerializeField] private float timeToSwitch;
    [SerializeField] private bool isJumpscaring = false;
    [SerializeField] private AudioSource screamerSound;

    public void Jumpscare()
    {
        if (!isJumpscaring)
        {
            isJumpscaring = true;
            screamerObject.SetActive(true);
            StartCoroutine(WaitTimer());
            screamerSound.Play();
        }
    }

    private IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(timeToSwitch);
        SceneManager.LoadScene("YOU DEAD");
    }
}
