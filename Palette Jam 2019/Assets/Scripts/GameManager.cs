using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject FloorPrefab = null;

    [SerializeField]
    private GameObject SecondLevelPrefab = null;

    [SerializeField]
    private GameObject RocksPrefab = null;

    [SerializeField]
    private Collectable CollectablePrefab = null;

    [SerializeField]
    private GameObject PauseMenu = null;

    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; set; } = false;

    public bool IsPaused { get; set; } = false;

    private Camera _camera;

    private int _currentSliceLocation = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void Start()
    {
        _camera = Camera.main;

        InstantiateMap();

        IsGameOver = false;

        UnPause();
    }

    public void Update()
    {
        if (!IsPaused)
        {
            //if (Input.GetKeyDown(KeyCode.Escape))
            if (Input.GetButtonDown("Pause") || Input.GetButtonDown("Fire2"))
            {
                Time.timeScale = 0;
                IsPaused = true;
                PauseMenu.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.R))
                UnityEngine.SceneManagement.SceneManager.LoadScene("Main");
        }
    }

    public void UnPause()
    {
        PauseMenu.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1;
    }

    public void FixedUpdate()
    {
        if (IsGameOver)
            return;

        if (_currentSliceLocation < _camera.transform.position.x + _camera.orthographicSize * _camera.aspect + 5)
        {
            InstantiateMap();
        }
    }

    public void InstantiateMap()
    {
        var mapPortion = MapGenerator.GenerateMapPortion();

        for (int i = 0; i < mapPortion.Slices.Length; i++)
        {
            var slice = mapPortion.Slices[i];

            if (slice.IsHole)
            {
                if (Random.Range(0f, 1f) > 0.85f)
                {
                    Instantiate(FloorPrefab, new Vector3(_currentSliceLocation, 0, 0), Quaternion.identity, transform);
                    Instantiate(RocksPrefab, new Vector3(_currentSliceLocation, 1, 0), Quaternion.identity, transform);
                }
            }
            else
            {
                Instantiate(FloorPrefab, new Vector3(_currentSliceLocation, 0, 0), Quaternion.identity, transform);
            }

            if (!slice.FloorCollectable.IsNone())
            {
                var floorCollectable = Instantiate(CollectablePrefab, new Vector3(_currentSliceLocation, 1, 0), Quaternion.identity, transform);
                floorCollectable.CollectableData = slice.FloorCollectable;

            }

            if (slice.HasSecondLevel)
            {
                Instantiate(SecondLevelPrefab, new Vector3(_currentSliceLocation, 2, 0), Quaternion.identity, transform);
            }

            if (!slice.SecondLevelCollectable.IsNone())
            {
                var floorCollectable = Instantiate(CollectablePrefab, new Vector3(_currentSliceLocation, 3, 0), Quaternion.identity, transform);
                floorCollectable.CollectableData = slice.SecondLevelCollectable;
            }

            _currentSliceLocation++;
        }
    }
}
