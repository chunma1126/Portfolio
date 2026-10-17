using System.Collections;
using Swift_Blade.Combat.Health;
using Unity.Behavior;
using UnityEngine.AI;
using UnityEngine;
using System.Linq;

namespace Swift_Blade.Enemy
{
    public class BaseEnemy : MonoBehaviour,IGetMoveSpeedAble
    {
        public Transform target;
        
        [Header("Movement Info")]
        [Range(0,30)][SerializeField] protected float moveSpeed;
        [Range(0,20)][SerializeField] protected float rotateSpeed;
        [Range(-1, 5)] [SerializeField] protected float stopDistance;
        [SerializeField] private LayerMask whatIsGround;
        
        [Header("Detect Forward Info")]
        public Transform checkForward;
        public LayerMask whatIsWall;
        public float maxDistance;
        public bool showGizmo;
        
        [Header("Weapon info")]
        public GameObject weapon;
        
        [Header("Knockback info")]
        public bool isKnockback = false;
        
        protected BehaviorGraphAgent btAgent;
        protected NavMeshAgent NavmeshAgent;
        protected Collider enemyCollider;
        protected Vector3 attackDestination;
        protected Rigidbody enemyRigidbody;

        
        protected BaseEnemyAnimationController baseAnimationController;
        protected BaseEnemyHealth baseHealth;
        private EnemyEffectController effectController;
        
        private Vector3 nextPathPoint;
        public float StopDistance { get => stopDistance; set => stopDistance = value; }
        
        private readonly WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();
        
        private void Awake()
        {
            btAgent = GetComponent<BehaviorGraphAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
            NavmeshAgent = GetComponent<NavMeshAgent>();
            baseHealth = GetComponent<BaseEnemyHealth>();
            enemyCollider = GetComponent<Collider>();
            effectController = GetComponent<EnemyEffectController>();
            
            baseAnimationController = GetComponentInChildren<BaseEnemyAnimationController>();
        }
        
        public virtual BaseEnemyHealth GetHealth() => baseHealth;
        public EnemyEffectController GetEffectController() => effectController;
        
        protected virtual void Start()
        {
            InitBtAgent();
        }
        
        private void InitBtAgent()
        {
            btAgent.enabled = false;
            
            if (target == null)
            {
                var player = FindObjectsByType<PlayerMovement>(FindObjectsSortMode.None).FirstOrDefault();
                if (player != null)
                    target = player.transform;
            }
            
            if (target == null) return;
            
            btAgent.SetVariableValue("Target", target);
            btAgent.SetVariableValue("MoveSpeed", moveSpeed);
                            
            btAgent.enabled = true;
        }
                
        protected virtual void Update()
        {
            if (baseHealth.isDead)
                return;
            
            if (baseAnimationController.isManualRotate) 
                FactToTarget(target.position);
            
            if (baseAnimationController.isManualMove && !DetectForwardObstacle())
            {
                bool isGround = Physics.Raycast(transform.position + Vector3.up * 1.2f, Vector3.down , out RaycastHit RaycastHit,  10, whatIsGround);
                if (isGround == false)return;
                
                var distance = Vector3.Distance(transform.position, target.position);
                
                if (distance > stopDistance)
                {
                    attackDestination = transform.position + transform.forward * 1;
                    attackDestination.y = RaycastHit.point.y;
                    
                    transform.position = Vector3.MoveTowards(transform.position, attackDestination,
                        baseAnimationController.AttackMoveSpeed * Time.deltaTime);
                }
            }
            
        }
        
        public void FactToTarget(Vector3 target)
        {
            if(isKnockback)return;
            
            var targetRot = Quaternion.LookRotation(target - transform.position);
            var currentEulerAngle = transform.rotation.eulerAngles;
            
            var yRotation = Mathf.LerpAngle(currentEulerAngle.y, targetRot.eulerAngles.y, rotateSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(currentEulerAngle.x, yRotation, currentEulerAngle.z);
        }
        
        private void StopImmediately()
        {
            if (NavmeshAgent == null || NavmeshAgent.enabled == false) return;
            
            NavmeshAgent.isStopped = true;
            NavmeshAgent.velocity = Vector3.zero;
        }
        
        public Vector3 GetNextPathPoint()
        {
            var path = NavmeshAgent.path;
            
            if (path.corners.Length < 2) return NavmeshAgent.destination;
            
            for (var i = 0; i < path.corners.Length; i++)
            {
                var distance = Vector3.Distance(NavmeshAgent.transform.position, path.corners[i]);

                if (distance < 1 && i < path.corners.Length - 1)
                {
                    nextPathPoint = path.corners[i + 1];
                    return nextPathPoint;
                }
            }

            return nextPathPoint;
        }

        public virtual void DeadEvent()
        {
            enemyCollider.enabled = false;
            enemyRigidbody.constraints = RigidbodyConstraints.FreezeAll;
            
            StopImmediately();
            AddEnemyWeaponCollision();
                
            Destroy(NavmeshAgent);
        }
        protected virtual void AddEnemyWeaponCollision()
        {
            if(weapon != null)
                weapon.AddComponent<EnemyWeapon>();
        }
        
        private bool DetectForwardObstacle()
        {
            var ray = new Ray(checkForward.position, checkForward.forward);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, whatIsWall)) return true;
            return false;
        }
                
        protected virtual void OnDrawGizmos()
        {
            if (showGizmo == false) return;
            
            Gizmos.DrawRay(checkForward.position, checkForward.forward * maxDistance);
        }
        
        public virtual void StartKnockback(ActionData actionData)
        {
            if(actionData.knockbackDirection == default || actionData.knockbackForce == 0 || isKnockback)return;
            
            StartCoroutine(
                Knockback(actionData.knockbackDirection , actionData.knockbackForce));
        }
        
        private IEnumerator Knockback(Vector3 knockbackDirection, float knockbackForce)
        {
            if(NavmeshAgent == null)yield break;
            
            isKnockback = true;
            
            NavmeshAgent.enabled = false;
            enemyRigidbody.useGravity = true;
            enemyRigidbody.isKinematic = false;
            enemyRigidbody.freezeRotation = true;
    
            enemyRigidbody.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
            
            yield return waitForFixedUpdate;
            
            if(NavmeshAgent == null)yield break;
            
            float timeout = 0.5f; 
            float timer = 0f;
    
            while (enemyRigidbody.linearVelocity.sqrMagnitude > 0.01f && timer < timeout) 
            {
                timer += Time.deltaTime;
                yield return null;
            }
            
            if(NavmeshAgent == null)yield break;
            
            enemyRigidbody.linearVelocity = Vector3.zero;
            enemyRigidbody.angularVelocity = Vector3.zero;
            
            yield return new WaitForFixedUpdate();
            
            if(NavmeshAgent == null)yield break;
            
            transform.position = new Vector3(transform.position.x, NavmeshAgent.nextPosition.y, transform.position.z);
            
            NavmeshAgent.Warp(transform.position);
            
            enemyRigidbody.freezeRotation = false;
            NavmeshAgent.enabled = true;
            enemyRigidbody.useGravity = false;
            enemyRigidbody.isKinematic = true;
            
            if (NavmeshAgent.hasPath)
            {
                NavmeshAgent.ResetPath();
            }

            isKnockback = false;
        }
        
        
        public float GetMoveSpeed()
        {
            return moveSpeed;
        }
        
    }
}