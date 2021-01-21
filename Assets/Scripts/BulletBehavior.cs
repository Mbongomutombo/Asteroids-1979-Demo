using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour, IDamageable
{
    [Header("Bullet speed")]
    public float speed;

    public int Health { get ; set ; }
    public int Power { get ; set ; }

    public bool Scoreable { get; set; }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        

    }

    //when the bullet leave cameras frustrum
    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    public void Damage(int damage)
    {
        //we may to set inactive trough interface
        //Health -= damage;
        //if (Health <= 0) gameObject.SetActive(false);
    }

    private  void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<IDamageable>().Damage(1);
        // or we may make inactive when collide + increase scores
        gameObject.SetActive(false);
        if (Scoreable)  GameManager.Instance.Scores += 1;
    }
}
