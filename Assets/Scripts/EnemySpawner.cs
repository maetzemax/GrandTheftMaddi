using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    [Header("Spawn Properties")] public LayerMask layerMask;
    public float spawnRate = 5.0f;
    private float initialSpawnRate;

    [Header("Radius")] public float minDistance = 10f;
    public float maxDistance = 50f;

    [Header("Enemies")] public GameObject[] firstWaveEnemies;
    public GameObject[] secondWaveEnemies;
    public float minAmount = 1;
    public float maxAmount = 3;

    private float _spawnTime = 0.0f;
    private Player _player;

    private void Start() {
        _player = Player.instance;
        initialSpawnRate = spawnRate;
    }

    private void Update() {
        if (GameManager.currentGameState != GameManager.GameState.Ingame) return;

        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        var enemyAmount = Random.Range(minAmount, maxAmount);

        for (int enemyCount = 0; enemyCount < enemyAmount;) {
            var randomSpawnPosition = getRandomPosition();
            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out var hit, Mathf.Infinity, layerMask)) {
                var randomIndex = Random.Range(0, firstWaveEnemies.Length);
                Instantiate(firstWaveEnemies[randomIndex], hit.point, Quaternion.identity);
                enemyCount++;
            }
            
            randomSpawnPosition = getRandomPosition();
            if (spawnRate < initialSpawnRate / 2) {
                if (Physics.Raycast(randomSpawnPosition, Vector3.down, out var hit2, Mathf.Infinity, layerMask)) {
                    var randomIndex = Random.Range(0, secondWaveEnemies.Length);
                    Instantiate(secondWaveEnemies[randomIndex], hit2.point, Quaternion.identity);
                    enemyCount++;
                }
            }
        }

        if (spawnRate > 1f) spawnRate -= 0.1f;
        _spawnTime = spawnRate;
    }

    Vector3 getRandomPosition() {
        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(minDistance, maxDistance);

        Vector3 offset = new Vector3(Mathf.Sin(angle) * distance, 100, Mathf.Cos(angle) * distance);
        return _player.transform.position + offset;
    }
}