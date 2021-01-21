using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidPoolManager : MonoSingleton<AsteroidPoolManager>
{
    


    [Header("Asteroid Pool Container")]
    [SerializeField]
    private GameObject asteroidPoolContainer;


    [Space]
    [Header("Asteroid Prefabs")]
    [SerializeField]
    private GameObject[] asteroidPrefabs;


    [Space]
    [Header("Pool of Asteroids")]
    [SerializeField]
    private List<GameObject> asteroidPool;

    
    public AudioSource asterCrash;

    

    private void Start()
    {
        asteroidPool = GenerateAsteroids(50);
    }


    //pregenerate list of asteroids from asteroid prefab
    List<GameObject> GenerateAsteroids(int amountOfAsteroids)
    {
        for (int i = 0; i < amountOfAsteroids; i++)
        {
            CreateAsteroid();
        }
        return asteroidPool;
    }

    public GameObject RequestAsteroid(int size)
    {
        //loop through the asteroid list, cheking for inactive asteroid, if found one, set it active and return 
        //if no asteroids aviable, generate new asteroid
        foreach (var asteroid in asteroidPool)
        {
            if (asteroid.activeInHierarchy == false)
            {
                //if the asteroid is aviable
                asteroid.SetActive(true);
                asteroid.GetComponent<AsteroidBehavior>().Health = size;
                asteroid.GetComponent<AsteroidBehavior>().Size = size;
                asteroid.transform.localScale = new Vector3(1,1,1);
                return asteroid;
            }
        }
        //if we made it to this point, we need to generate more asteroids

        GameObject newAsteroid = CreateAsteroid();
        newAsteroid.GetComponent<AsteroidBehavior>().Health = size;
        newAsteroid.GetComponent<AsteroidBehavior>().Size = size;
        return newAsteroid;
    }

    GameObject CreateAsteroid()
    {
        int r = Random.Range(0, 3);
        GameObject asteroid = Instantiate(asteroidPrefabs[r], new Vector3 (-50,-50,10), Quaternion.identity); //create asteroid from prefab
       
        asteroid.transform.parent = asteroidPoolContainer.transform; //set the asteroid to child of the AsteroidContainer
        asteroid.SetActive(false); //off the asteroid in hierarhy
        asteroidPool.Add(asteroid); //add to the asteroid list
        return asteroid;
    }
}
