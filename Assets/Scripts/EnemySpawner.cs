using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    private TerrainCollider _meshCollider;
    public LayerMask layerMask;
    
    public float spawnRate = 5.0f;
    private float _spawnTime = 5.0f;
    
    public GameObject[] enemies;

    private void Start() {
        _meshCollider = gameObject.GetComponent<TerrainCollider>();
    }

    private void Update() {
        _spawnTime -= Time.deltaTime;

        if (_spawnTime <= 0.0f) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        var randomIndex = Random.Range(0, enemies.Length);
        var randomSpawnPosition = new Vector3(
            Random.Range(_meshCollider.bounds.min.x, _meshCollider.bounds.max.x),
            100,
            Random.Range(_meshCollider.bounds.min.z, _meshCollider.bounds.max.z)
        );

        if (Physics.Raycast(randomSpawnPosition, Vector3.down, out var hit, Mathf.Infinity, layerMask)) {
            Instantiate(enemies[randomIndex], hit.point, Quaternion.identity);

            if (spawnRate > 1f) spawnRate *= 0.975f; 
            _spawnTime = spawnRate;
        } else {
            SpawnEnemy();
        }
    }
}
