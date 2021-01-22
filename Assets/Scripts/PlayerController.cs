using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour, IDamageable
{

    public float rotationSpeed;
    public float movementSpeed;
   
    
    [Space]
    [Tooltip("Weapon cooldown")]
    [Range(0f, 2f)]
    public float cooldown;

    [Space]
    [Header("Particle systems")]
    [Tooltip ("Particle system")]

    public GameObject exhaust;

    [Tooltip("Particle system")]

    public GameObject exhaustLeft;

    [Tooltip("Particle system")]

    public GameObject exhaustRight;

    [Space]
    [Tooltip("Bullet Spawn Point")]
    [SerializeField]
    GameObject bulletSpawnPoint;

    [SerializeField]
    Slider indicatorHP;

    private float time = 0f;

    public int Health { get ; set; }
    public int Power { get ; set ; }

    

    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip shoot;
    [SerializeField]
    AudioClip structure_warning;
    [Space]
    [SerializeField]
    Animator gameOver;

    public void Damage(int damage)
    {
        Health -= damage;
        indicatorHP.value = Health;
        if (Health < 10 )
        {
            audioSource.PlayOneShot(structure_warning);
        }
        if (Health <= 0) KillShip();

    }

    void KillShip()
    {

        GameManager.Instance.SaveHiScore();
        gameOver.SetTrigger("GameOver");
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponent<IDamageable>().Damage(1);
        
    }


    void Start()
    {
        Health = 100;
    }
    // Update is called once per frame
    void Update()
    {

        

        // add thrust to main engine
        if (Input.GetKey(KeyCode.W)) GetComponent<Rigidbody>().AddForce(transform.forward * movementSpeed * Time.deltaTime);
        // play animation main engihe exhaust when accelerator key pressed
        if (Input.GetKeyDown(KeyCode.W)) exhaust.GetComponent<ParticleSystem>().Play();

        // add thrust to right shunting engine
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(-Vector3.up * rotationSpeed * Time.deltaTime);
        // play animation of right SE
        if (Input.GetKeyDown(KeyCode.A)) exhaustRight.GetComponent<ParticleSystem>().Play();

        // add thrust to left shunting engine
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        // play animation of left SE
        if (Input.GetKeyDown(KeyCode.D)) exhaustLeft.GetComponent<ParticleSystem>().Play();

        //stop animation of engines
        if (Input.GetKeyUp(KeyCode.W)) exhaust.GetComponent<ParticleSystem>().Stop();
        if (Input.GetKeyUp(KeyCode.A)) exhaustRight.GetComponent<ParticleSystem>().Stop();
        if (Input.GetKeyUp(KeyCode.D)) exhaustLeft.GetComponent<ParticleSystem>().Stop();



        // shooting
        if (time > 0f)
        {
            time -= Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            // request bullet from bullet pool
            GameObject bullet = BulletPoolManager.Instance.RequestBullet();
            bullet.GetComponent<BulletBehavior>().Scoreable = true;
            bullet.transform.position = bulletSpawnPoint.transform.position;
            bullet.transform.rotation = bulletSpawnPoint.transform.rotation;
            time = cooldown;
            audioSource.PlayOneShot(shoot);
        }
    }
}
