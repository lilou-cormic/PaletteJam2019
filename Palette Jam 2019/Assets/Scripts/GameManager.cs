using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject FloorPrefab = null;

    [SerializeField]
    private GameObject SecondLevelPrefab = null;

    [SerializeField]
    private Collectable CollectablePrefab = null;

    public static GameManager Instance { get; private set; }

    public bool IsGameOver { get; set; }

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
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0))
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
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

            if (!slice.IsHole)
            {
                var floor = Instantiate(FloorPrefab, new Vector3(_currentSliceLocation, 0, 0), Quaternion.identity, transform);
            }

            if (!slice.FloorCollectable.IsNone())
            {
                var floorCollectable = Instantiate(CollectablePrefab, new Vector3(_currentSliceLocation, 1, 0), Quaternion.identity, transform);
                floorCollectable.CollectableData = slice.FloorCollectable;

            }

            if (slice.HasSecondLevel)
            {
                var floor = Instantiate(SecondLevelPrefab, new Vector3(_currentSliceLocation, 2, 0), Quaternion.identity, transform);
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
