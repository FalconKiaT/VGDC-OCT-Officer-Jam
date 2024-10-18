using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public static GhostSpawner ghostSpawner;

    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private float defaultSpawnFrequency = 5f;

    [SerializeField] private float spawnPaddingX;
    [SerializeField] private float spawnPaddingY;

    [SerializeField] private bool debug;

    private Camera camera;
    private Vector2 screenBotLeft, screenTopRight;
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
        screenBotLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
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
        float spawnPosX = Random.Range(screenBotLeft.x + spawnPaddingX, screenTopRight.x - spawnPaddingX);
        float spawnPosY = Random.Range(screenBotLeft.y + spawnPaddingY, screenTopRight.y - spawnPaddingY);

        Vector2 position = new Vector2(spawnPosX, spawnPosY);

        GameObject ghost = Instantiate(ghostPrefab, position, Quaternion.identity, this.gameObject.transform);

        if (debug) Debug.Log("Ghost Spawned!");

        return ghost;
    }
}
