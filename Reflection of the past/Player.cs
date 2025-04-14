using System;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    private PlayerStateMachine StateMachine;

    public Rigidbody Rigidbody { get; private set; }
    public Animator Animator { get; private set; }
    
    
    public bool isDead;
    [Space]
    
    [Header("MoveInfo")]
    public float MoveSpeed;
    public float JumpForce;
    public bool checkGroundBool;
    public LayerMask whatIsGround;
    public Transform checkGroundTrm;
    public float checkGroundDistance;

    [Header("Torchlight")] 
    public bool canCreateTorchlight;
    public Torch torchlight;
    [SerializeField] private int torchlightCount;
    [SerializeField] private float torchDeadTime;
    private float torchDeadTimer;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
        
        StateMachine = new PlayerStateMachine();
        StateMachine.AddState(PlayerStateEnum.Idle , new PlayerIdleState(this , StateMachine , "Idle"));
        StateMachine.AddState(PlayerStateEnum.Run , new PlayerMoveState(this , StateMachine , "Move"));
        StateMachine.AddState(PlayerStateEnum.Jump , new PlayerJumpState(this , StateMachine , "Jump"));
        StateMachine.AddState(PlayerStateEnum.Falling , new PlayerFallingState(this , StateMachine , "Falling"));
        StateMachine.AddState(PlayerStateEnum.Dead , new PlayerDeadState(this , StateMachine , "Dead"));
        
        StateMachine.Init(PlayerStateEnum.Idle);

        GameManager.OnBReached += OnCreateTorchlight;
    }

    private void OnDestroy()
    {
        GameManager.OnBReached -= OnCreateTorchlight;
    }

    private void Update()
    {
        if (GameManager.isFirstScene) return;
        
        if (transform.position.y <= -20)
        {
            Dead();
        }
        
        StateMachine.currentState.Update();
        checkGroundBool = CheckGround();
        
        //UI_DebugPlayer.Instance.GetList[0].text = StateMachine.currentState.ToString();

        if (Input.GetKeyDown(KeyCode.Mouse0) && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() == false)
        {
            if(canCreateTorchlight)
                CreateTorchlight();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void Dead()
    {
        if(isDead)return;

        GameManager.SetStateToAState();
        isDead = true;
        Rigidbody.isKinematic = true;
        GetComponent<Collider>().enabled = false;
        StateMachine.ChangeState(PlayerStateEnum.Dead);
    }

    public void OnCreateTorchlight()
    {
        canCreateTorchlight = true;
    }
    
    public void AddTorchTimer(float amount)
    {
        if(isDead)return;
        
        torchDeadTimer += amount;
        // if (torchDeadTimer >= torchDeadTime)
        // {
        //     Dead();
        //     torchDeadTimer = 0;
        // }
    }
    
    public void Flip()
    {
        Vector3 targetDir = Rigidbody.velocity;
        targetDir.y = 0;
        targetDir.z = 0;
        
        Quaternion lookDir = Quaternion.LookRotation(targetDir);
        transform.rotation = lookDir;
    }

    #region Movement
    public void Move(float speed)
    {
        Rigidbody.velocity = new Vector3(speed , Rigidbody.velocity.y , Rigidbody.velocity.z);
    }
    
    public void Jump(float jumpForce)
    {
        Rigidbody.velocity = new Vector3(Rigidbody.velocity.x , jumpForce , Rigidbody.velocity.z);
    }

    public void StopMovement()
    {
        Rigidbody.velocity = Vector3.zero;
    }
    
    #endregion
        
    public bool CheckGround() => Physics.Raycast(checkGroundTrm.position , Vector3.down , checkGroundDistance , whatIsGround);

    private void CreateTorchlight()
    {
        if(torchlightCount <=0 || !checkGroundBool)return;

        --torchlightCount;
        Torch newTorch = Instantiate(torchlight,transform.position,Quaternion.identity);
    }
        
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawRay(checkGroundTrm.position ,  Vector3.down * checkGroundDistance);
    }

}
