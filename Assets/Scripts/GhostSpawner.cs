using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public static GhostSpawner ghostSpawner;

    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float defaultSpawnFrequency = 5f;

    [SerializeField] private bool debug;

    private Camera camera;
    private float screenBotLeft, screenTopRight;
    private float currentTime = 0f;

    private void Awake()
    {
        if (ghostSpawner == null) 
            ghostSpawner = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        camera = Camera.main;

        InitCameraBounds();

        ResetTimer(defaultSpawnFrequency);
    }

    private void Update()
    {
        Timer();
    }

    private void InitCameraBounds()
    {
        Vector3 screenBotLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        Vector3 screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    private void Timer()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            SpawnGhostFromRandomPos();
            // TODO: adjust frequency based on difficulty
            ResetTimer(defaultSpawnFrequency);
        }
    }

    private void ResetTimer(float newTime)
    {
        currentTime = newTime;
    }

    private GameObject SpawnGhostFromRandomPos()
    {
        Vector3 position = Vector3.zero;

        GameObject ghost = Instantiate(ghostPrefab, position, Quaternion.identity, this.gameObject.transform);

        if (debug)
            Debug.Log("Ghost Spawned!");

        return ghost;
    }
}
