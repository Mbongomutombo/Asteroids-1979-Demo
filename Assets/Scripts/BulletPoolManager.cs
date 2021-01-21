using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPoolManager : MonoSingleton<BulletPoolManager>
{
    
    [Header("Bullet Pool Container")]
    [SerializeField]
    private GameObject bulletPoolContainer;

    [Space]
    [Header("Bullet Prefab")]
    [SerializeField]
    private GameObject bulletPrefab;


    [Space]
    [Header("Pool of Bullets")]
    [SerializeField]
    private List<GameObject> bulletPool;

    
    private void Start()
    {
        bulletPool = GenerateBullets(10);
    }


    //pregenerate list of bullets from the bullet prefab
    List<GameObject> GenerateBullets(int amountOfBullets)
    {
        for (int i = 0; i<amountOfBullets; i++)
        {
            
            CreateBullet();
        }
        return bulletPool;
    }

    public GameObject RequestBullet()
    {
        //loop through the bullet list, cheking for inactive bullet, if found one, set it active and return to player
        //if no bullets aviable, generate new bullet
        foreach (var bullet in bulletPool)
        {
            if(bullet.activeInHierarchy == false)
            {
                //bullet is aviable
                bullet.SetActive(true);
                return bullet;
            }
        }
        //if we made it ti this point, we need to generate more bullets
        GameObject newBullet = CreateBullet(); 
        return newBullet;
    }

    GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab); //create bullet from prefab
        bullet.transform.parent = bulletPoolContainer.transform; //set bullet to child of BulletContainer
        bullet.SetActive(false); //off the bullet in hierarhy
        bulletPool.Add(bullet); //add to bullet list
        return bullet;
    }
}
