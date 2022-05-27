using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_rifle : MonoBehaviour
{
    [SerializeField] Animator m_Animator = default;
    [SerializeField] Transform cartridgeTransform;
    [SerializeField] Transform gunHoleUp;
    [SerializeField] Transform gunHoleDown;
    [SerializeField] Transform gunHoleLeft;
    [SerializeField] Transform gunHoleRight;
    [SerializeField] Rigidbody2D m_RigidBody2D = default;
    private float firingDistance = 5f;
    private float travelSpeed = 10f;
    private float damage = default;
    public bool bulletReady = true;
    private Vector2 spawnPoint;
    AudioManager m_AudioManager = default;
    private int firingLevel = default;

    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        firingLevel = SaveManager.GetPowerupSettings().firingLevel;
        if (firingLevel == 1) damage = 5f;
        if (firingLevel == 2) damage = 7f;
        if (firingLevel == 3) damage = 10f;
        //Debug.Log("Bullet damage:" + damage);
    }
    public void Spawn(string direction)
    {   
        this.gameObject.SetActive(true);
        m_RigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        m_RigidBody2D.velocity = Vector2.zero;
        m_RigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        cartridgeTransform = GameObject.FindWithTag("Astro").transform.GetChild(1).transform.GetChild(4);
        gunHoleUp = cartridgeTransform.GetChild(0).transform;
        gunHoleDown = cartridgeTransform.GetChild(1).transform;
        gunHoleLeft = cartridgeTransform.GetChild(2).transform;
        gunHoleRight = cartridgeTransform.GetChild(3).transform;
        bulletReady = false;
        gameObject.transform.position = cartridgeTransform.position;
        spawnPoint = gameObject.transform.position;
        FixDirection(direction);
        Travel(direction);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, spawnPoint) > firingDistance) Despawn();
    }

    /*
        La Funzione Despawn e' richiamata dall'animator alla fine dell'animazione di esplosione
     */
    public void Despawn()
    {
        m_Animator.SetBool("side", false);
        m_Animator.SetBool("front", false);
        gameObject.SetActive(false);
        bulletReady = true;
    }


    void Travel(Vector2 direction)
    {
        m_RigidBody2D.velocity = direction * travelSpeed;
    }


    void Travel(string direction)
    {
        switch (direction)
        {
            case "up":
                gameObject.transform.position = gunHoleUp.position;
                Travel(Vector2.up);
                break;
            case "down":
                gameObject.transform.position = gunHoleDown.position;
                Travel(Vector2.down);
                break;
            case "left":
                gameObject.transform.position = gunHoleLeft.position;
                Travel(Vector2.left); 
                break;
            case "right":
                gameObject.transform.position = gunHoleRight.position;
                Travel(Vector2.right);
                break;
            default:
                break;
        }
    }

    void FixDirection(string direction)
    {
        if (direction == "left" || direction == "right") m_Animator.SetBool("side", true);
        else if (direction == "up" || direction == "down") m_Animator.SetBool("front", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_RigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        //collisione con un nemico/boss oppure con un nemico volante
        if (collision.gameObject.layer == 13 || collision.gameObject.layer == 16)
        {
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
        m_AudioManager.PlayBulletExpl();
        m_Animator.SetTrigger("explosion");
    }

}
