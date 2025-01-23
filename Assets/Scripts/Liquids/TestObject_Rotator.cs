using UnityEngine;

public class TestObject_Rotator : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float rotationSpeed = 100f;
    
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
