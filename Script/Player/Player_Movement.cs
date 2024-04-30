using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class Player_Movement : MonoBehaviour
{
    public float Movement_Speed, Max_Speed;
    public float Smooth_Rotaion_Speed, JumpFource;
    public float local_Running_Speed_multiplier;
    public float movementTransitionTime;
    public GameObject PlayerCine, shoulder, player_Camera;
    public Vector3 offsetTarget;
    public float Distance;

    //public FloatingJoystick MovementFJ;

    [HideInInspector] public Animator PlayerAnimator;
    [HideInInspector] public CapsuleCollider playerCollider;
    [HideInInspector] public Rigidbody rg;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public Player_Animation_Controller PAC;
    [HideInInspector] public Player_Rotation PR;
    [HideInInspector] public Player_Weapon PW;
    [HideInInspector] public Player_Status PS;
    [HideInInspector] public Player_Getter PG;
    [HideInInspector] public Player_Vahical_Handler PVH;
    [HideInInspector] public Player_Surrounding_Check PSC;
    [HideInInspector] public GroundChecker groundChecker;
    [HideInInspector] public float Input_Horizontal, Input_Vertical;
    [HideInInspector] public NavMeshAgent Player_As_Agent;


    public bool isJumped, _Jump, isGunAvailable, isCrouching, isGrounded,
                isHoldingWeapon, isAiming, isRunning, isBow, isFighting;

    public float MinDistaceEntery;

    public TwoBoneIKConstraint rightArm, leftArm;

    public GameObject[] Children;
    public Player_Fighting_trigger rightFT, leftFT;




    private float local_Running_Speed;
    private Vector3 movement;


    /// <summary>
    /// Input controle handler
    /// </summary>
    private void player_Input()
    {

        Input_Horizontal = Input.GetAxis("Horizontal");
        Input_Vertical = Input.GetAxis("Vertical");

       
        isRunning = Input.GetKey(KeyCode.LeftShift);
        _Jump = Input.GetKeyDown(KeyCode.Space);
        isGrounded = groundChecker.countTrigger;

        // Wepaon
        if (Input.GetKeyDown(KeyCode.LeftControl)) isCrouching = !isCrouching;
        if (Input.GetKeyDown(KeyCode.Mouse1)) isAiming = !isAiming;

        isFighting = PW.isFireing;

        AnimationInput();
    }

    public void AnimationInput()
    {
        if (!PlayerAnimator) return;


        PlayerAnimator.SetFloat("moveRight", Input_Horizontal);
        PlayerAnimator.SetFloat("moveForward", Input_Vertical);
        PlayerAnimator.SetBool("isRunning", isRunning);
        PlayerAnimator.SetBool("isJumping", isJumped);
        if (!PVH.enterCalled)
        {
            PlayerAnimator.SetBool("isMoved", (Input_Horizontal != 0 || Input_Vertical != 0));
            PlayerAnimator.SetFloat("moveForward", Input_Vertical);
        }
        else
        {
            PlayerAnimator.SetBool("isMoved", true);
            PlayerAnimator.SetFloat("moveForward", 1);
        }
        PlayerAnimator.SetBool("isCrouching", isCrouching);
        PlayerAnimator.SetBool("isGunAvailable", PW.Holding_Weapon_Details.Weapon_Prefab);
        PlayerAnimator.SetBool("isAiming", isAiming);
        PlayerAnimator.SetBool("isBow", isBow);
        PlayerAnimator.SetBool("isFighting", isFighting);


        if (PW.Holding_Weapon_Details.Weapon_Prefab)
        {
            rightArm.weight = 1;
            leftArm.weight = 1;
        }
        else
        {
            isAiming = false;
            rightArm.weight = 0;
            leftArm.weight = 0;
        }
    }


    /// <summary>
    /// movement Veriable controle
    /// </summary>
    private void player_Movement_Variable_Controle()
    {
        local_Running_Speed = !isRunning ? 1 : 2;
        Vector3 move_Forward = transform.forward * Input_Vertical * local_Running_Speed;
        Vector3 move_Right = transform.right * Input_Horizontal * local_Running_Speed;

        movement = (move_Forward + move_Right).normalized * Movement_Speed;

    }


    /// <summary>
    /// Player Jump 
    /// </summary>
    private void Jump()
    {
        isJumped = _Jump;
        if (_Jump && isGrounded)
        {
            rg.AddForce(Vector3.up * JumpFource, ForceMode.VelocityChange);
            _Jump = false;
        }
    }


    private void Start()
    {
        Application.targetFrameRate = 60;
        PAC = GetComponent<Player_Animation_Controller>();
        PR = GetComponent<Player_Rotation>();
        PS = GetComponent<Player_Status>();
        PW = GetComponent<Player_Weapon>();
        PG = GetComponent<Player_Getter>();
        PVH = GetComponent<Player_Vahical_Handler>();
        PSC = GetComponentInChildren<Player_Surrounding_Check>();


        Player_As_Agent = GetComponent<NavMeshAgent>();
        rg = GetComponent<Rigidbody>();
        playerCollider = GetComponent<CapsuleCollider>();
        PlayerAnimator = GetComponentInChildren<Animator>();
        lineRenderer = GetComponent<LineRenderer>();

        PAC.PM = this;
        PSC.PM = this;
        PVH.PM = this;
        PW.PM = this;
        PVH.PM = this;
        PG.PW = PW;


        _Jump = false;


        groundChecker = GetComponentInChildren<GroundChecker>();


        updateCanvus();

        PW.bullet_Pointer = -1;
        PW.tempBurstCount = -1;
        PR.Rotation = PR.Player_Shoulder.transform.localEulerAngles;
        PW.point = 0;

    }
    private void Update()
    {
        player_Input();
        player_Movement_Variable_Controle();
        Jump();


        PR.player_MouseInput();
        PR.player_Rotaion_and_CamRotation();


        PW.Buttons();
        PW.Shoot_Call();


        PVH.VahicalControle();
    }
    private void FixedUpdate()
    {
        if (!Player_As_Agent.enabled) player_movement();
        PVH.Vahical_Physics();
    }



    /// <summary>
    /// Player Movement is Exist here and it is working with Rigidbody
    /// </summary>
    private void player_movement()
    {
        if (movement != Vector3.zero)// && timer > movementTransitionTime)
        {
            float X;
            if (!isCrouching)
            {
                if (isRunning) X = Max_Speed;
                else X = 1;
            }
            else
            {
                if (isRunning) X = 1;
                else X = 1f;
            }
            rg.MovePosition(rg.position + movement * X * Time.fixedDeltaTime);
        }
    }
    
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Vahical")
        {
            Debug.Log("called");
            if (Vector3.Distance(PVH.Destination.transform.position, transform.position) < MinDistaceEntery 
                && PVH.enterCalled)
            {
                Debug.Log("calleing");
                PVH.Enter();
            }
        }
    }
    
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.tag == "Vahical")
        {
            if (Vector3.Distance(PVH.Destination.transform.position, transform.position) < MinDistaceEntery
                && PVH.enterCalled)
            {
                Debug.Log("calleing");
                PVH.Enter();
            }
        }
    }


    private void updateCanvus()
    {
        PS.Health_Bar.maxValue = 100;
        PS.Streanth_Bar.maxValue = 100;
        PS.Streanth_Bar.value = 100;
    }


}
