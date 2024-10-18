using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class GhostSpawner : MonoBehaviour
{
    public static GhostSpawner ghostSpawner;

    [Header("Objects")]
    [SerializeField] private GameObject ghostPrefab;

    [Header("Spawn")]
    [SerializeField] private float defaultSpawnFrequency = 5f;
    [SerializeField] private float spawnPaddingX;
    [SerializeField] private float spawnPaddingY;

    [Header("Difficulty")]
    [SerializeField] private float changeInSpawnFrequency;
    [SerializeField] private float minimumSpawnFrequency;
    [SerializeField] private int changeDiffilcultyAfterSpawn;

    [Header("Debug")]
    [SerializeField] private bool debug;

    private Camera camera;
    private Vector2 screenBotLeft, screenTopRight;
    private float currentTime = 0f;

    private float spawnFrequency;
    private int currentEnemyNumberSpawned = 0;

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

        spawnFrequency = defaultSpawnFrequency;

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
            ChangeDifficulty();
            ResetTimer(defaultSpawnFrequency);
        }
    }

    private void ResetTimer(float newTime)
    {
        currentTime = newTime;
    }

    private void ChangeDifficulty()
    {
        if (currentEnemyNumberSpawned % changeDiffilcultyAfterSpawn == 0)
        {
            defaultSpawnFrequency -= changeInSpawnFrequency;

            if (defaultSpawnFrequency < minimumSpawnFrequency)
                defaultSpawnFrequency = minimumSpawnFrequency;

            if (debug) Debug.Log("Difficulty Changed!");
        }
    }

    private GameObject SpawnGhostFromRandomPos()
    {
        Vector2 position = RandomPositionOnScreen();

        GameObject ghost = Instantiate(ghostPrefab, position, Quaternion.identity, this.gameObject.transform);

        currentEnemyNumberSpawned++;

        if (debug) Debug.Log("Ghost Spawned!");

        return ghost;
    }
    
    private Vector2 RandomPositionOnScreen()
    {
        float spawnPosX = Random.Range(screenBotLeft.x + spawnPaddingX, screenTopRight.x - spawnPaddingX);
        float spawnPosY = Random.Range(screenBotLeft.y + spawnPaddingY, screenTopRight.y - spawnPaddingY);

        return new Vector2(spawnPosX, spawnPosY);
    }
}
