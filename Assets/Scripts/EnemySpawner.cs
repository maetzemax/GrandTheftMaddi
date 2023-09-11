using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public GameObject[] enemies;

    public float spawnRate = 5.0f;
    private float spawnTime = 5.0f;

    MeshCollider meshCollider;

    private void Start() {
        meshCollider = gameObject.GetComponent<MeshCollider>();
    }

    void Update() {
        spawnTime -= Time.deltaTime;

        if (spawnTime <= 0.0f) {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy() {
        int randomIndex = Random.Range(0, enemies.Length);
        Vector3 randomSpawnposition = new Vector3(
            Random.Range(meshCollider.bounds.min.x, meshCollider.bounds.max.x),
            meshCollider.bounds.max.y,
            Random.Range(meshCollider.bounds.min.z, meshCollider.bounds.max.z)
        );

        Instantiate(enemies[randomIndex], randomSpawnposition, Quaternion.identity);

        if (spawnRate > 1f) spawnRate *= 0.975f; 
        spawnTime = spawnRate;
    }
}
