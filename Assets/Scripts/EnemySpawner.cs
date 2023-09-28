using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [Header("Spawn Properties")]
    public LayerMask layerMask;
    public float spawnRate = 5.0f;

    [Header("Radius")]
    public float minDistance = 10f;
    public float maxDistance = 50f;

    [Header("Enemies")]
    public GameObject[] enemies;
    public float minAmount = 1;
    public float maxAmount = 3;

    private float _spawnTime = 5.0f;
    private Player _player;

    private void Start() {
        _player = Player.instance;
    }

    private void Update() {
        if (GameManager.currentGameState != GameManager.GameState.Ingame) return;

        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        var randomIndex = Random.Range(0, enemies.Length);
        var enemyAmount = Random.Range(minAmount, maxAmount);

        for (int enemyCount = 0; enemyCount < enemyAmount; enemyCount++) {
            var randomSpawnPosition = getRandomPosition();
            if (Physics.Raycast(randomSpawnPosition, Vector3.down, out var hit, Mathf.Infinity, layerMask)) {
                Instantiate(enemies[randomIndex], hit.point, Quaternion.identity);
            }
        }

        if (spawnRate > 1f) spawnRate -= 0.1f;
        _spawnTime = spawnRate;
    }

    Vector3 getRandomPosition() {
        var position = new Vector3(
            Random.Range(_player.player.transform.position.x + minDistance, _player.player.transform.position.x + maxDistance),
            100,
            Random.Range(_player.player.transform.position.z + minDistance, _player.player.transform.position.z + maxDistance)
        );


        return position;
    }
}
