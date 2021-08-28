using UnityEngine.AI;
using UnityEngine;

public class VilagerMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform player;

    private Vector3 startingPosition;
    private Vector3 roamPosition;
    private VilagerAI vilagerAI;
    private bool walking = false;

    private void Awake()
    {
        vilagerAI = gameObject.GetComponent<VilagerAI>();
    }

    private void Start()
    {
        startingPosition = transform.position;
        roamPosition = vilagerAI.GetRoamingPosition(startingPosition);
    }

    private void Update()
    {
        FindTarget();
        if (!walking)
        {
            HandleWalk();
        }
        if (Vector3.Distance(transform.position, roamPosition) < 1f)
        {
            walking = false;
        }
    }

    private void HandleWalk()
    {
        walking = true;
        roamPosition = vilagerAI.GetRoamingPosition(startingPosition);
        vilagerAI.MoveTo(roamPosition);
    }


    private void FindTarget()
    {
        if (Vector3.Distance(transform.position, player.position) < 3f)
        {
            print("Hele there!");
        }
    }
}
