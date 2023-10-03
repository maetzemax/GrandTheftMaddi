using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Spawn Properties")]
    public LayerMask layerMask;
    public float spawnRate = 5.0f;
    private float initialSpawnRate;

    [Header("Radius")]
    public float minDistance = 10f;
    public float maxDistance = 50f;

    [Header("Enemies")]
    public GameObject[] firstWaveEnemies;
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
        if (spawnRate > (initialSpawnRate / 2)) {
            var enemyAmount = Random.Range(minAmount, maxAmount);
            for (int enemyCount = 0; enemyCount < enemyAmount; enemyCount++) {
                var randomSpawnPosition = getRandomPosition();
                var randomIndex = Random.Range(0, firstWaveEnemies.Length);

                if (Physics.Raycast(randomSpawnPosition, Vector3.down, out var hit, Mathf.Infinity, layerMask) && Vector3.Distance(randomSpawnPosition, _player.transform.position) > minDistance) {
                    Instantiate(firstWaveEnemies[randomIndex], hit.point, Quaternion.identity);
                }
            }
        }

        if (spawnRate < (initialSpawnRate / 2)) {
            var enemyAmount = Random.Range(minAmount, maxAmount);
            for (int enemyCount = 0; enemyCount < enemyAmount; enemyCount++) {
                var randomSpawnPosition = getRandomPosition();
                var randomIndex = Random.Range(0, secondWaveEnemies.Length);

                if (Physics.Raycast(randomSpawnPosition, Vector3.down, out var hit, Mathf.Infinity, layerMask) && Vector3.Distance(randomSpawnPosition, _player.transform.position) > minDistance) {
                    Instantiate(secondWaveEnemies[randomIndex], hit.point, Quaternion.identity);
                }
            }
        }

        if (spawnRate > 1f) spawnRate -= 0.1f;
        _spawnTime = spawnRate;
    }

    Vector3 getRandomPosition() {
        var position = new Vector3(
            Random.Range(_player.transform.position.x - maxDistance, _player.transform.position.x + maxDistance),
            100,
            Random.Range(_player.transform.position.z - maxDistance, _player.transform.position.z + maxDistance)
        );

        return position;
    }
}
