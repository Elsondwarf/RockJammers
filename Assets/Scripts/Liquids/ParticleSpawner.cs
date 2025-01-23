using System;
using UnityEngine;
using UnityEngine.Serialization;

public class ParticleSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public Vector2 spawnDirection;
    public float spawnForce;
    public float forceMagnitudeClamp;
    [SerializeField] private GameObject _particlePrefab;
    
    [Header("Pooling Library")]
    [SerializeField] private ObjectPooling _objectPooling; 
    
    [Header("Test Settings")]
    [SerializeField] private bool _IsTest;
    [SerializeField] private int _maxParticles;
    [Range(1,10)][SerializeField] private int _batchSize;
    [SerializeField] private float _timeBetweenSpawns;
    private float _spawnTimer;

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
        //GameObject go = Instantiate(particle);
        GameObject go = _objectPooling.GetPooledObject();
        go.SetActive(true);
        _objectPooling.activeObjects.Add(go);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + Vector2.ClampMagnitude(spawnVector * spawnVelocity, vectorClampForce));
    }
    
    /// <summary>
    /// Overload of SpawnParticle, that requires no arguments and instead instanciates default values as set in the script object. 
    /// </summary>
    public void SpawnParticle()
    {
        //GameObject go = Instantiate(_particlePrefab);
        GameObject go = _objectPooling.GetPooledObject();
        go.SetActive(true);
        go.transform.position = transform.position;
        go.GetComponent<Rigidbody2D>().AddForce((Vector2)transform.position + Vector2.ClampMagnitude(spawnDirection * spawnForce, forceMagnitudeClamp));
    }
    
    private void Update()
    {
        if (_objectPooling.activeObjects.Count < _maxParticles && _IsTest && _spawnTimer > _timeBetweenSpawns)
        {
            for (int i = 0; i < _batchSize; i++)
            {
                SpawnParticle(_particlePrefab, spawnDirection, spawnForce, forceMagnitudeClamp);
                _spawnTimer = 0f;
            }
        }
        _spawnTimer += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            Gizmos.DrawLine(transform.position,   (Vector2)transform.position + Vector2.ClampMagnitude(spawnDirection * spawnForce, forceMagnitudeClamp / 100));
        }
    }
}
