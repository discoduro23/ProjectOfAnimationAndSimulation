using QuantumTek.QuantumTravel;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FLock : MonoBehaviour
{
    public ArmIKBehaviour armIK;
    private GameObject fishParent;
    private GameObject visibleParent;
    public QT_Minimap Minimap;
    [Header("Spawn Setup")]
    [SerializeField] private FlockUnit flockUnitPrefab;
    [SerializeField] private GameObject visiblePrefab;
    [SerializeField] private int flockSize;
    [SerializeField] private Vector3 spawnBounds;

    [Header("Target Setup")]
    [SerializeField] private Transform _target;
    public Transform target { get { return _target; } }


    [Header("Speed Setup")]
    [Range(0, 10)]
    [SerializeField] public float _minSpeed;
    public float minSpeed { get { return _minSpeed; } }
    [Range(0, 10)]
    [SerializeField] public float _maxSpeed;
    public float maxSpeed { get { return _maxSpeed; } }


    [Header("Detection Distances")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionDistance;
    public float cohesionDistance { get { return _cohesionDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceDistance;
    public float avoidanceDistance { get { return _avoidanceDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _aligementDistance;
    public float aligementDistance { get { return _aligementDistance; } }

    [Range(0, 10)]
    [SerializeField] private float _obstacleDistance;
    public float obstacleDistance { get { return _obstacleDistance; } }

    [Range(0, 1000)]
    [SerializeField] private float _boundsDistance;
    public float boundsDistance { get { return _boundsDistance; } }

    [Range(0, 100)]
    [SerializeField] private float _targetDistance;
    public float targetDistance { get { return _targetDistance; } }

    [Header("Behaviours Weights")]
    [Range(0, 10)]
    [SerializeField] private float _cohesionWeight;
    public float cohesionWeight { get { return _cohesionWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _avoidanceWeight;
    public float avoidanceWeight { get { return _avoidanceWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _aligementWeight;
    public float aligementWeight { get { return _aligementWeight; } }

    [Range(0, 100)]
    [SerializeField] private float _obstacleWeight;
    public float obstacleWeight { get { return _obstacleWeight; } }
    
    [Range(0, 10)]
    [SerializeField] private float _boundsWeight;
    public float boundsWeight { get { return _boundsWeight; } }

    [Range(0, 10)]
    [SerializeField] private float _targetWeight;
    public float targetWeight { get { return _targetWeight; } }

    public FlockUnit[] allUnits { get; set; }
    // Start is called before the first frame update

    private GameObject ShootPoll;
    private List<GameObject> ShootList;
    public int shootPollCount = 100;
    public GameObject shootPrefab;

    public void Awake()
    {
        fishParent = new GameObject("fish Parent");
        visibleParent = new GameObject("visible Parent");
        GenerateUnits();

        ShootPoll = new GameObject("Shoot Pool");
        ShootList = new List<GameObject>();
        for (int i = 0; i < shootPollCount; i++)
        {
            var shoot = Instantiate(shootPrefab);
            shoot.gameObject.SetActive(false);
            ShootList.Add(shoot.gameObject);
            shoot.transform.parent = ShootPoll.transform;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < allUnits.Length; i++)
        {
            allUnits[i].MoveUnit();
        }
    }

    private void GenerateUnits()
    {
        allUnits = new FlockUnit[flockSize];
        for(int i = 0; i < flockSize; i++)
        {
            var randomVector = UnityEngine.Random.insideUnitSphere;
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z); 
            var spawnPosition = transform.position + randomVector;
            var rotation = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            allUnits[i] = Instantiate(flockUnitPrefab, spawnPosition, rotation);
            var visiblePref = Instantiate(visiblePrefab, spawnPosition, Quaternion.identity);
            visiblePref.GetComponent<DroneConstraption>().BoidControl = allUnits[i].gameObject.transform;
            visiblePref.transform.parent = visibleParent.transform;
            visiblePref.GetComponent<DroneConstraption>().target = target;
            Minimap.AddMarker(visiblePref.GetComponent<QT_MapObject>(), false);
            allUnits[i].armIK = armIK;
            allUnits[i].transform.parent = fishParent.transform;
            allUnits[i].AssignFlock(this);
            allUnits[i].InitializeSpeed(UnityEngine.Random.Range(minSpeed, maxSpeed));
        }
    }

    public GameObject Shoot()
    {
        //get an inactive bullet from the shootList
        //get a bullet from the shootList
        for (int i = 0; i < ShootList.Count; i++)
        {
            if (!ShootList[i].gameObject.activeInHierarchy)
            {
                ShootList[i].gameObject.SetActive(true);
                return ShootList[i];
            }
        }
        return null;
    }
}
