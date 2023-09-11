using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

public class EnemyController : MonoBehaviour {
    public float lookRadius = 10f;
    private float randomPointRange = 50f;

    private float attackSpeed = 2f;
    private float attackTime = 0f;

    Transform target;
    NavMeshAgent agent;

    public Transform centrePoint;

    void Start() {
        target = Player.instance.transform;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        float distance = Vector3.Distance(transform.position, target.position);

        Vector3 point;

        if (distance <= lookRadius) {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance) {
                FaceTarget();
            }
        } else if (agent.remainingDistance <= agent.stoppingDistance) {
            if (RandomPoint(centrePoint.position, randomPointRange, out point)) {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }

        #region Attack Timer

        attackTime -= Time.deltaTime;
        if (attackTime <= 0.0f) {
            if (distance <= 1.5f) {
                Player.instance.currentHealth -= 1;
                attackTime = attackSpeed;
            }
        } else {
            return;
        }

        #endregion

    }

    private void FaceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
