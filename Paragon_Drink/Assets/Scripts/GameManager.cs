using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance => instance;

    public GameParameters gameParameters;
    private StateMachine _stateMachine;
    [SerializeField] private Manager[] _managers;

    public int itemsCollected = 0;

    private PlayerControls _playerControls;
    private float _displayCursorTimer = 3f;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        gameParameters.Initialize();
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        _playerControls = new PlayerControls();
        _playerControls.Enable();
    }

    private void OnSceneChanged(Scene currentScene, Scene nextScene)
    {
        GetManagers();
        InitializeManagers();
        gameParameters.InitializeParameters();

        if (nextScene == SceneManager.GetSceneByBuildIndex(0))
        {
            _stateMachine.Initialize(new MenuState());
        }
        else if (nextScene == SceneManager.GetSceneByBuildIndex(1))
        {
            _stateMachine.Initialize(new CutsceneState());
        }
        else if (nextScene == SceneManager.GetSceneByBuildIndex(2))
        {
            _stateMachine.Initialize(new TransitionState());
        }
    }

    private void GetManagers()
    {
        _stateMachine = FindObjectOfType<StateMachine>();
        _managers = FindObjectsOfType<Manager>(true);
    }

    private void InitializeManagers()
    {
        foreach (Manager manager in _managers)
        {
            manager.Initialize(this, _stateMachine);
        }
    }

    private void Update()
    {
        _stateMachine.UpdateLogic();

        foreach (Manager manager in _managers)
        {
            manager.UpdateLogic();
        }

        DisplayCursor();
    }

    private void DisplayCursor()
    {
        if (_playerControls.Mouse.MouseDelta.ReadValue<Vector2>() != Vector2.zero)
        {
            _displayCursorTimer = 3f;
            Cursor.visible = true;
        }
        else
        {
            if (_displayCursorTimer > 0)
            {
                _displayCursorTimer -= Time.deltaTime;
            }
            else
            {
                Cursor.visible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        _stateMachine.UpdatePhysics();
    }

    public void CollectItem()
    {
        itemsCollected++;
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
}
