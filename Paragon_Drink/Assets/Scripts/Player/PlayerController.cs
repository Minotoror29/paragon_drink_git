using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;
using FMODUnity;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerStateMachine playerStateMachine;
    public Animator animator;
    public Rigidbody2D rb;

    public PlayerControls _playerControls;

    private Vector2 _direction;
    public float dehydratedMovementSpeed = 1f;
    public float hydratedMovementSpeed = 1f;

    private float _gravityForce = -9.8f;
    [SerializeField] private float gravityMultiplier = 1f;
    public float dehydratedJumpHeight = 1f;
    public float hydratedJumpHeight = 1f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    public List<Transform> _grounds;
    public Transform currentGround;
    public LayerMask groundLayer;
    public bool requireNewJumpPress = false;
    public bool canJump;

    public bool inWater;
    public GameObject currentWater;

    [SerializeField] private float dashSpeed = 1f;
    public float dashTime = 1f;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private Bubble bubble;
    [SerializeField] private GameObject dashImpact;

    public GameObject feet;

    [Header("Animations")]
    [HideInInspector] public string idle;
    [HideInInspector] public string run;
    [HideInInspector] public string jump;
    [HideInInspector] public string fall;
    [HideInInspector] public string land;
    public string absorption = "Absorption";

    private EventInstance _footstepSound;
    private EventInstance _landWaterSound;
    [HideInInspector] public int size = 0;

    [Header("FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject jumpDiagonalFX;
    [SerializeField] private GameObject airJumpFX;
    [SerializeField] private GameObject airJumpDiagonalFX;

    [SerializeField] private GameObject landFX1;
    [SerializeField] private GameObject landFX2;

    public void Initialize()
    {
        _playerControls = new PlayerControls();
        _playerControls.Movement.Enable();
        _playerControls.Movement.Action.performed += ctx => playerStateMachine.Action();
        _playerControls.Menu.Enable();
        _playerControls.Menu.Options.performed += ctx => MenuManager.Instance.DisplayOptionsMenu();

        _grounds = new List<Transform>();

        playerStateMachine.Initialize(new DehydratedState(playerStateMachine, this, animator, new IdleState(playerStateMachine, this, animator)));

        //Initialize footstep sound
        _footstepSound = RuntimeManager.CreateInstance("event:/Player/juan_dehydrated_footsteps");
        _landWaterSound = RuntimeManager.CreateInstance("event:/Player/juan_enter_water");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube((Vector2)transform.position + (Vector2.up * (0.9f * (transform.localScale.x - 0.075f) / 2)), transform.localScale * new Vector2(0.5f, 0.9f));
    }

    public void Idle()
    {
        _direction = Vector2.zero;
        rb.velocity = Vector2.zero;
    }

    public void Grounded()
    {
        _direction.y = 0f;
    }

    public void Move(Vector2 direction, float movementSpeed)
    {
        _direction.x = direction.x * movementSpeed;

        if (direction.magnitude > 0.1f)
        {
            if (direction.x < 0f)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void Jump(float jumpHeight)
    {
        _direction.y = Mathf.Sqrt(-2f * _gravityForce * (jumpHeight));
        CreateJumpFX();
    }

    public void Fall()
    {
        _direction.y += _gravityForce * gravityMultiplier;

        //Debug.Log(_direction.y);

        //_direction.y = Mathf.Clamp(_direction.y, -18f, 50f);
    }

    public void LowFall()
    {
        _direction.y += _gravityForce * lowJumpMultiplier;
    }

    public void Dash()
    {
        _direction.y = 0;
        _direction.x = -transform.right.x * dashSpeed;
    }

    public void CreateBubble()
    {
        Bubble newBubble = Instantiate(bubble, shootPoint.position, transform.rotation);
        newBubble.Initialize();
        GameObject newDashImpact = Instantiate(dashImpact, shootPoint.position, transform.rotation);
        Destroy(newDashImpact, 0.286f);
    }

    public void AddGround(Transform ground)
    {
        _grounds.Add(ground);
    }

    public void RemoveGround(Transform ground)
    {
        if (_grounds.Contains(ground))
        {
            _grounds.Remove(ground);
        }
    }

    public void UpdateLogic()
    {
        playerStateMachine.UpdateLogic();
    }

    public void UpdatePhysics()
    {
        playerStateMachine.UpdatePhysics();

        rb.velocity = _direction;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerStateMachine.EnterCollision(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        playerStateMachine.ExitCollision(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water") && _direction.y < 0)
        {
            PlayEnterWaterSound();
        }
    }

    private void PlayEnterWaterSound()
    {
        _landWaterSound.setParameterByName("Size", size);
        _landWaterSound.start();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = true;
            currentWater = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            inWater = false;
            currentWater = null;
        }
    }

    public void PlayFootStepSound()
    {
        _footstepSound.setParameterByNameWithLabel("Surface", GetFloorSurfaceType());
        _footstepSound.setParameterByName("Size", size);
        _footstepSound.start();
    }

    public string GetFloorSurfaceType()
    {
        if (currentGround == null)
        {
            return "Floor";
        }

        string surfaceType = "Floor";

        if (currentGround.gameObject.CompareTag("Breakable Platform"))
        {
            surfaceType = "Fence";
        }
        else if (inWater)
        {
            surfaceType = "Water";
        }

        return surfaceType;
    }

    private void CreateJumpFX()
    {
        GameObject newFX;
        GameObject fxPrefab;

        if (currentGround != null)
        {
            if (_direction.x == 0)
            {
                fxPrefab = jumpFX;
            }
            else
            {
                fxPrefab = jumpDiagonalFX;
            }
        } else
        {
            if (_direction.x == 0)
            {
                fxPrefab = airJumpFX;
            }
            else
            {
                fxPrefab = airJumpDiagonalFX;
            }
        }

        float y = 0f;
        if (_direction.x < 0)
        {
            y = 180f;
        }

        newFX = Instantiate(fxPrefab, transform.position, Quaternion.Euler(0, y, 0));
        Destroy(newFX, 0.5f);
    }

    public void CreateLandFX()
    {
        GameObject newFX;
        GameObject fxPrefab;

        int i = Random.Range(1, 3);

        if (i == 1)
        {
            fxPrefab = landFX1;
        } else
        {
            fxPrefab = landFX2;
        }

        newFX = Instantiate(fxPrefab, transform.position, transform.rotation);
        Destroy(newFX, 0.5f);
    }
}
