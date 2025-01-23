using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum ParticleType
{
    [InspectorName("TestFluid")] TestFluid,
    [InspectorName("Oxygen")] Oxygen,
    [InspectorName("Water")] Water,
}

public class Particle : MonoBehaviour
{
    public ParticleType tag;
    
    [Header("Particle Attraction Variables")]
    public float attenuationRadius;
    public float attractionForce;
    public float cohesionForce;
    public float foreignRepulsionForce;
    [SerializeField] private CircleCollider2D _triggerCollider;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private List<Particle> _particles = new List<Particle>();
    [SerializeField] private List<Particle> _foreignParticles = new List<Particle>();
    [SerializeField] private List<Rigidbody2D> _rigidbodies = new List<Rigidbody2D>();
    
    [Header("LifeTime Variables")]
    public float maxLifeTime;
    public bool isDesawnable;
    private float _lifeTime;
    
    public ObjectPooling objectPooling;
    
    private void OnEnable()
    {
        _lifeTime = 0f;
        _particles.Add(this);
        _rigidbodies.Add(GetComponent<Rigidbody2D>());
    }

    private void OnDisable()
    {
        _particles.Remove(this);
        _rigidbodies.Remove(GetComponent<Rigidbody2D>());
    }

    private void Update()
    {
        if (isActiveAndEnabled && isDesawnable)
        {
            if (_lifeTime < maxLifeTime && maxLifeTime != 0f)
            {
                _lifeTime += Time.deltaTime;
            }

            if (_lifeTime > maxLifeTime && maxLifeTime != 0f)
            {
                objectPooling.activeObjects.Remove(this.gameObject);
                gameObject.SetActive(false);
            }
        }
        
        _triggerCollider.radius = attenuationRadius;
    }

    private void FixedUpdate()
    {
        Vector2 averageVector = new Vector2();
        foreach (Particle particle in _particles)
        {
            averageVector += (Vector2)particle.transform.position;
        }
        averageVector /= _particles.Count;
        _rigidbody.AddForce(averageVector * (attractionForce * Time.fixedDeltaTime));

        Vector2 averageMovementVector = new Vector2();
        foreach (Rigidbody2D rb in _rigidbodies)
        {
            averageMovementVector += rb.linearVelocity;
        }
        averageMovementVector /= _rigidbodies.Count;
        _rigidbody.AddForce(averageMovementVector * (cohesionForce * Time.fixedDeltaTime));

        if (_foreignParticles.Count > 0)
        {
            Vector2 averageForeignVector = new Vector2();
            foreach (Particle foreignParticle in _foreignParticles)
            {
                averageForeignVector += (Vector2)foreignParticle.transform.position;
            }
            averageForeignVector /= _foreignParticles.Count;
            _rigidbody.AddForce(averageForeignVector * (-foreignRepulsionForce * Time.fixedDeltaTime));
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Particle particle) && !_particles.Contains(particle) && particle.tag == this.tag)
        {
            _particles.Add(particle);
            _rigidbodies.Add(particle.gameObject.GetComponent<Rigidbody2D>());
        }
        else if (other.TryGetComponent(out Particle otherParticle) && !_foreignParticles.Contains(otherParticle) &&
                 otherParticle.tag != this.tag)
        {
            _foreignParticles.Add(otherParticle);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Particle particle) && _particles.Contains(particle) && particle.tag == this.tag)
        {
            _particles.Remove(particle);
            _rigidbodies.Remove(particle.gameObject.GetComponent<Rigidbody2D>());
        }
        else if (other.TryGetComponent(out Particle otherParticle) && !_foreignParticles.Contains(otherParticle) &&
                 otherParticle.tag != this.tag)
        {
            _foreignParticles.Remove(otherParticle);
        }
    }
}
