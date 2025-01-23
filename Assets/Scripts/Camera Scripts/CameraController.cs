using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Vector2 movePositions = Vector2.zero;

    [SerializeField] private Transform playerTransform;

    public static CameraController instance;

    private Vector3 newCamLocation;

    void Start()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            //Destroy(gameObject);
        }


        //DontDestroyOnLoad(this);

        //playerTransform = PlayerMovement.instance.transform;

        transform.position = new Vector3
        {
            x = playerTransform.position.x,
            y = playerTransform.position.y,
            z = transform.position.z
        };
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        newCamLocation = new Vector3
        {
            x = playerTransform.position.x,
            y = playerTransform.position.y,
            z = transform.position.z
        };


        if (movePositions.x == 1 || movePositions.x == -1)
        {
            newCamLocation.x = transform.position.x;
        }

        if (movePositions.y == 1 || movePositions.y == -1)
        {
            newCamLocation.y = transform.position.y;
        }


        transform.position = newCamLocation;
    }

    public void GetBorderDirection(Vector2 direction)
    {
        movePositions = direction;
    }

    public void GetPlayerTransform(Transform transform)
    {
        playerTransform = transform;
    }
}
