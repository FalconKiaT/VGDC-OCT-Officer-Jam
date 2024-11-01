using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class GhostSpawner : MonoBehaviour
{
    public static GhostSpawner ghostSpawner;

    [Header("Objects")]
    [SerializeField] private RectTransform localTransform;
    [SerializeField] private List<Sprite> ghostImageVariants;
    [SerializeField] private GameObject ghostPrefab;
    [SerializeField] private RectTransform canvasRect;

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

        StartCoroutine(SpawnRoutine());
    }


    private void InitCameraBounds()
    {
        screenBotLeft = camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane));
        screenTopRight = camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane));
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds timer = new WaitForSeconds(defaultSpawnFrequency);
        while (true)
        {
            SpawnGhostFromRandomPos();
            ChangeDifficulty();
            yield return timer;
        }
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

    private void SpawnGhostFromRandomPos()
    {
        //Vector2 position = RandomPositionOnScreen();

        // Get the canvas width and height
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        // Generate a random position within the canvas boundaries
        float randomX = Random.Range(-canvasWidth / 2, canvasWidth / 2);
        float randomY = Random.Range(-canvasHeight / 2, canvasHeight / 2);
        Vector2 randomPosition = new Vector2(randomX, randomY);



        // Instantiate ghost
        // GameObject ghost = Instantiate(ghostPrefab, position, Quaternion.identity, this.gameObject.transform);
        

        // Instantiate the object and set its position relative to the canvas
        GameObject spawnedObject = Instantiate(ghostPrefab, localTransform);
        spawnedObject.GetComponent<RectTransform>().anchoredPosition = randomPosition;

        Image spriteRenderer = spawnedObject.GetComponent<Image>();

        // Get a random sprite
        if (ghostImageVariants.Count > 0)
        {
            Sprite randomImage = ghostImageVariants[Random.Range(0, ghostImageVariants.Count)];
            // Set the sprite
            spriteRenderer.sprite = randomImage;
        }

        currentEnemyNumberSpawned++;

        if (debug) Debug.Log("Ghost Spawned!");
    }
    
    private Vector2 RandomPositionOnScreen()
    {
        float spawnPosX = Random.Range(canvasRect.rect.x + spawnPaddingX, canvasRect.anchoredPosition.x + canvasRect.rect.width - spawnPaddingX);
        float spawnPosY = Random.Range(canvasRect.rect.y + spawnPaddingY, canvasRect.anchoredPosition.y + canvasRect.rect.height - spawnPaddingY);

        return new Vector2(spawnPosX, spawnPosY);
    }
}
