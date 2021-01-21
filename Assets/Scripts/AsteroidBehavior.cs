using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour, IDamageable
{
    

    float distanceToCenterX2;

    public int Health { get ; set ; }
    public int Power { get ; set ; }

    public int Size { get; set; } //1 - small, 2 - medium, 3 - large

    
    // Update is called once per frame
    void Update()
    {

        
        //if the asteroid too far from the screen center, he will deactivate self
        distanceToCenterX2 = (GameManager.Instance.ScreenCenter - gameObject.transform.position).sqrMagnitude;

        if (distanceToCenterX2 > GameManager.Instance.ScreenDimension* GameManager.Instance.ScreenDimension*1.2)
        {
            gameObject.SetActive(false);
        }
        
    }


    

    
    public void Damage(int damage)
    {
        Health -= damage;
        if (Health <= 0) DestroyAsteroid();
    }

    void DestroyAsteroid()
    {
        //  animate of destruction
        // and call smaller asteroids

        
            WhenDestroyAsteroid(); //animations and effects
        gameObject.SetActive(false);
        GameManager.Instance.TripleSpawn(gameObject.transform, Size);
            
        
    }

    void WhenDestroyAsteroid()
    {
        //sound of destruction
        AsteroidPoolManager.Instance.asterCrash.Play();
    }
    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<IDamageable>().Damage(Size);
    }

    
}
