using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jelly : RandomWalker,IRescuable
{
    [SerializeField] Animator m_animator = default;
    [SerializeField] SpriteRenderer m_SpriteRederer = default;
    [SerializeField] Rigidbody2D m_Rigidbody2D = default;
    private int originalSortingOrder = default;
    public bool saved = false;
    public bool gettingSaved = false;
    private bool isEscaping = false;
    float savingSpeed=0.6f;
    int points=1;
    string laserDirection = null;
    LevelManager m_LevelManager = default;

    AudioManager m_AudioManager = default;

    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_LevelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }
    public void Spawn(bool walksrandom = true)
    {
        if (walksrandom)
        {
            SetParameters();
            StartCoroutine(WalkRandom(randomWalkCooldown));
        }
    }

    protected override void SetParameters()
    {
        walkSpeed = 1f;
        randomWalkCooldown = 3f;
        isWalking = false;
    }
    public void GetSaved() 
    {
        if (transform.parent.parent.name == "Fixed") m_LevelManager.AddPointsSpecialJelly(points);
        else m_LevelManager.AddPoints(points);
        gameObject.SetActive(false);
    }

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //collisione con il Laser
        if (collision.gameObject.layer == 9)
        {
            if (collision.gameObject.GetComponent<Laser>().target == null)
            {
                laserDirection = collision.gameObject.GetComponent<Laser>().laserDirection;
                if (isWalking) Intercepted();
                collision.gameObject.GetComponent<Laser>().SetTarget(this);
                gettingSaved = true;
                m_animator.SetBool("beingSaved", true);
                StartCoroutine(Save(savingSpeed));
            }
            else if (!isEscaping)
            {
                Escape();
            }
        }
    }


    private void Escape()
    {
        isEscaping = true;
        StartCoroutine(Walk(walkSpeed * 2, GenerateRandomVersor()));
    }


    private void Intercepted()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        isWalking = false;
        isEscaping = false;
    }

    IEnumerator Save(float savingSpeed) {
        Vector3 startingPosition = gameObject.transform.position;
        originalSortingOrder = m_SpriteRederer.sortingOrder;
        Intercepted();
        if (laserDirection == "down")   m_SpriteRederer.sortingOrder += 10;
        ModifyCollidersForSave();
        m_AudioManager.PlayJellySave();

        while (gettingSaved)
        {
            gameObject.transform.Translate(Vector3.up * Time.deltaTime* savingSpeed);
            yield return 0;
        }

        m_AudioManager.StopJellySave();

        m_SpriteRederer.sortingOrder = originalSortingOrder;
        
        if (saved)  GetSaved();
        
        else {
            gameObject.transform.position = startingPosition;
            ResetOriginalColliders();
            m_animator.SetBool("beingSaved", false);
            Escape();
        }
        yield return 0;
    }


    protected override IEnumerator WalkRandom(float randomWalkCooldownSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(randomWalkCooldownSeconds);
            if (!isEscaping && !isWalking && !gettingSaved)
                StartCoroutine(Walk(walkSpeed, GenerateRandomVersor()));
        }
    }
    protected override IEnumerator Walk(float speed, Vector3 direction)
    {

        Vector3 destination = gameObject.transform.position + direction;
        isWalking = true;
        m_animator.SetBool("isWalk", true);
        m_Rigidbody2D.velocity = (destination - transform.position);
        yield return new WaitForSeconds(2f);
        m_animator.SetBool("isWalk", false);
        isWalking = false;
        yield return 0;
    }

    public void ModifyCollidersForSave()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    }
    public void ResetOriginalColliders()
    {
        gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
    }
}
