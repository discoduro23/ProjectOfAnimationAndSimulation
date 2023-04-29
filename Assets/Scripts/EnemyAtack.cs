using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyAtack : MonoBehaviour
{
    private Transform myTransform;
    private Transform target;
    [SerializeField] private float attackInterval;
    private float attackTime;
    private bool canAttack;
    public bool isAiming;

    [Range (0, 360)]
    [SerializeField] private int FOVAngle;

    [Range(0, 100)]
    [SerializeField] private int _AttackDistance;
    public int attackDistance { get { return _AttackDistance; } }


    // Start is called before the first frame update
    private void Awake()
    {
        
    }

    void Start()
    {
        myTransform = transform;
        target = GetComponent<FlockUnit>().assignedFlock.target;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTime > attackInterval)
            {
                canAttack = true;
            }
        attackTime += Time.deltaTime;

        if (IsInFOV(target.position) && IsInDistance(target.position) && canAttack && isAiming)
        {
            Debug.Log("attacked");
            attackTime = 0;
            canAttack = false;

        }
    }

    private bool IsInFOV(Vector3 position)
    {
        return Vector3.Angle(myTransform.forward, position - myTransform.position) <= FOVAngle;
    }

    private bool IsInDistance(Vector3 position)
    {
        return (myTransform.position - position).magnitude < _AttackDistance;
    }
}
