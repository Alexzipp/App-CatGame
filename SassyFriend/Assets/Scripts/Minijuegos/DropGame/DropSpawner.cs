using System.Collections;
using UnityEngine;

public class DropSpawner : MonoBehaviour
{
    private Coroutine spawnRoutine;

    [Header("Prefabs")]
    public GameObject cleanDropPrefab;
    public GameObject dirtyDropPrefab;

    [Header("Spawn Settings")]
    public float initialSpawnInterval = 1.5f; // tiempo entre gotas al inicio
    public float minSpawnInterval = 0.5f;     // límite de rapidez
    private float spawnY = 6f;                 // altura de spawn
    private float[] zonePercentages = { 0.2f, 0.5f, 0.8f }; // zonas horizontales (% ancho pantalla)

    [Header("Oleadas")]
    public float waveDuration = 15f; // cada 15s sube dificultad
    public float spawnIntervalDecrease = 0.2f;
    public float dirtyChanceIncrease = 0.1f;  // +10% prob de gotas sucias por oleada
    private float dirtyChance = 0.2f; // 20% de inicio
    private float currentSpawnInterval;

    private float elapsedTime;

    private void Start()
    {
        currentSpawnInterval = initialSpawnInterval;
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Oleadas: cada X segundos se ajusta dificultad
        if (elapsedTime >= waveDuration)
        {
            elapsedTime = 0;
            currentSpawnInterval = Mathf.Max(minSpawnInterval, currentSpawnInterval - spawnIntervalDecrease);
            dirtyChance = Mathf.Clamp01(dirtyChance + dirtyChanceIncrease);
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnDrop();
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    private void SpawnDrop()
    {
        // Seleccionar zona (izq, centro, der)
        int zone = Random.Range(0, zonePercentages.Length);
        float screenX = zonePercentages[zone] * Screen.width;
        float worldX = Camera.main.ScreenToWorldPoint(new Vector3(screenX, 0, 10)).x;

        // Variación interna dentro de la zona
        float variation = Random.Range(-0.5f, 0.5f);
        Vector3 spawnPos = new Vector3(worldX + variation, spawnY, 0);

        // Decidir si es limpia o sucia
        GameObject prefab = (Random.value < dirtyChance) ? dirtyDropPrefab : cleanDropPrefab;
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    public void StopSpawning()
    {
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }
    }
}
