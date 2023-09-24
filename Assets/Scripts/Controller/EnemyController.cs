using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    public AttackController attackController;

    private Enemy _stats;
    private NavMeshAgent _agent;

    public Transform centrePoint;

    private void Awake() {
        _stats = GetComponent<Enemy>();
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _stats.movementSpeed;
    }

    private void Update() {
        #region TargetDetection
        if (attackController.currentTarget != null) {
            var distance = Vector3.Distance(transform.position, attackController.currentTarget.transform.position);

            if (distance <= _stats.detectionRange) {
                _agent.SetDestination(attackController.currentTarget.transform.position);

                if (distance <= _agent.stoppingDistance) {
                    FaceTarget();
                }
            }
        }

        #endregion
        
        #region Patrolling
        
        if (_agent.remainingDistance <= _agent.stoppingDistance) {
            if (RandomPoint(centrePoint.position, _stats.patrolRange, out var point)) {
                _agent.SetDestination(point);
            }
        }
        
        #endregion
    }

    private void FaceTarget() {
        var direction = (attackController.currentTarget.transform.position - transform.position).normalized;
        var lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result) {
        var randomPoint = center + Random.insideUnitSphere * range;
        if (NavMesh.SamplePosition(randomPoint, out var hit, 1.0f, NavMesh.AllAreas)) {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
