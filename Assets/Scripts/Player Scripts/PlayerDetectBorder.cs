using UnityEngine;

public class PlayerDetectBorder : MonoBehaviour
{
    public LayerMask borderLayer;

    [SerializeField] private float distance;
    [SerializeField] private CameraController cam;

    private Vector2[] directions = new Vector2[4] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        cam.GetPlayerTransform(transform);
    }

    // Update is called once per frame
    void LateUpdate()
    {

        Vector2 direction = Vector2.zero;

        for (int i = 0; i < directions.Length; i++)
        {
            Vector2 pointDirection = BorderCheck(directions[i], distance);

            direction += pointDirection;

        }
        cam.GetBorderDirection(direction);

    }

    private Vector2 BorderCheck(Vector2 direction, float distance)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, borderLayer);
        if (hit.collider != null)
        {
            return direction;
        }

        return Vector2.zero;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.up * distance));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.down * distance));

        Gizmos.DrawLine(transform.position, transform.position + (Vector3.left * distance));
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * distance));
    }
}
