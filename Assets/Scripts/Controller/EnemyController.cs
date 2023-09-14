using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {
    private float _attackTime = 0f;

    private Enemy _stats;
    
    private Player _target;
    private NavMeshAgent _agent;

    public Transform centrePoint;

    private void Awake() {
        _stats = GetComponent<Enemy>();
        _target = Player.instance;
        _agent = GetComponent<NavMeshAgent>();
        _agent.speed = _stats.movementSpeed;
    }

    private void Update() {
        #region PlayerDetection

        var distance = Vector3.Distance(transform.position, _target.transform.position);
        
        if (distance <= _stats.detectionRange) {
            _agent.SetDestination(_target.transform.position);

            if (distance <= _agent.stoppingDistance) {
                FaceTarget();
            }
        }

        #endregion
        
        #region Patrolling
        
        if (_agent.remainingDistance <= _agent.stoppingDistance) {
            if (RandomPoint(centrePoint.position, _stats.patrolRange, out var point)) {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                _agent.SetDestination(point);
            }
        }
        
        #endregion

        #region Attack

        _attackTime -= Time.deltaTime;
        if (!(_attackTime <= 0.0f)) return;
        if (!(distance <= _stats.attackRange)) return;
        
        Player.instance.currentHealth -= 1;
        _attackTime = 1 / _stats.attackSpeed;

        #endregion
    }

    private void FaceTarget() {
        var direction = (_target.transform.position - transform.position).normalized;
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
