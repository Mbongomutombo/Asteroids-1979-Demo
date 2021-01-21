using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour, IDamageable
{

    

    public int Health { get ; set ; }
    public int Power { get ; set ; }

    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0) DestroyEnemy();
    }

    
    void DestroyEnemy()
    {
        
        //call ten explosions
        for (int i = 0; i<10; i++)
        {
          GameObject explosion =  ExplosionPoolManager.Instance.RequestExplosion();
            explosion.transform.position = gameObject.transform.position + new Vector3(Random.Range(-i*0.5f, i*0.5f), Random.Range(-i*0.5f, i*0.5f));
            explosion.SetActive(true);
            explosion.GetComponent<ParticleSystem>().Play();
            ExplosionPoolManager.Instance.explosionSound.Play();

        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<IDamageable>().Damage(5);
    }
}
