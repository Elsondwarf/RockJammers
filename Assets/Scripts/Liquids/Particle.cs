using System;
using UnityEngine;

public class Particle : MonoBehaviour
{
    public float maxLifeTime;
    public bool isDesawnable;
    private float _lifeTime;
    
    public ObjectPooling objectPooling;
    
    private void OnEnable()
    {
        _lifeTime = 0f;
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
    }
}
