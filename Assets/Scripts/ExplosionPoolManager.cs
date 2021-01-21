using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionPoolManager : MonoSingleton<ExplosionPoolManager>
{
    

    [Header("Explosion Pool Container")]
    [SerializeField]
    private GameObject explosionPoolContainer;

    [Space]
    [Header("Explosion Prefab")]
    [SerializeField]
    private GameObject explosionPrefab;


    [Space]
    [Header("Pool of Explosions")]
    [SerializeField]
    private List<GameObject> explosionPool;

    public AudioSource explosionSound;

    
    private void Start()
    {
        explosionPool = GenerateExplosions(10);
    }


    //pregenerate list of explosions from explosion prefab
    List<GameObject> GenerateExplosions(int amountOfExplosions)
    {
        for (int i = 0; i < amountOfExplosions; i++)
        {

            CreateExplosion();
        }
        return explosionPool;
    }

    public GameObject RequestExplosion()
    {
        //loop through the explosion list, cheking for inactive explosion, if found one, set it active and return to player
        //if no explosions aviable, generate new explosion
        foreach (var explosion in explosionPool)
        {
            if (explosion.activeInHierarchy == false)
            {
                //explosion is aviable
                explosion.SetActive(true);
                explosionSound.Play();
                return explosion;
            }
        }
        //if we made it ti this point, we need to generate more explosions
        GameObject newExplosion = CreateExplosion();
        explosionSound.Play();
        return newExplosion;
    }

    GameObject CreateExplosion()
    {
        GameObject explosion = Instantiate(explosionPrefab); //create explosion from prefab
        explosion.transform.parent = explosionPoolContainer.transform; //set explosion to child of ExplosionContainer
        explosion.SetActive(false); //off the explosion in hierarhy
        explosionPool.Add(explosion); //add to explosion list
       return explosion;
    }
}
