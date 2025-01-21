using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleSpawner : MonoBehaviour
{
    [FormerlySerializedAs("spawnForce")] [Header("Spawn Settings")]
    public Vector2 spawnDirection;
    public float spawnForce;
    public float forceMagnitudeClamp;
    [SerializeField] private GameObject particlePrefab;
    
    [Header("Test Settings")]
    [SerializeField] private bool IsTest;
    [SerializeField] private int maxParticles;
    [SerializeField] private int batchSize;
    [SerializeField] private int currentParticles;

    /// <summary>
    /// SpawnParticle will spawn a game object, and launch it with AddForce in a direction (spawnVector) at a speed (spawnVelocity).
    /// The force is clamped by vectorClampForce as a security measure.
    /// </summary>
    /// <param name="particle">The particle which you will spawn</param>
    /// <param name="spawnVector">The direction in which the particle will travel</param>
    /// <param name="spawnVelocity">The force which the direction is multiplied by</param>
    /// <param name="vectorClampForce">The maximum force that will act on the particle (recommended to set this the same as spawnVelocity)</param>
    public void SpawnParticle(GameObject particle, Vector2 spawnVector, float spawnVelocity, float vectorClampForce)
    {
        GameObject go = Instantiate(particle);
        go.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + Vector2.ClampMagnitude(spawnVector * spawnVelocity, vectorClampForce));
    }

    private void Update()
    {
        if (currentParticles < maxParticles && IsTest)
        {
            for (int i = 0; i < batchSize; i++)
            {
                SpawnParticle(particlePrefab, spawnDirection, spawnForce, forceMagnitudeClamp);
                currentParticles++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position,   (Vector2)transform.position + Vector2.ClampMagnitude(spawnDirection * spawnForce, forceMagnitudeClamp / 100));
        }
    }
}
