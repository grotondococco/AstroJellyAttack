using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : RandomWalker,IKillable
{
    protected float Health;
    protected float damage;
    protected float attackRange;
    protected bool isChasing;
    protected float attackCooldown;
    protected float lastAttackTime;
    protected EnemyAnimator m_EnemyAnimator = default;
    protected Radar m_Radar = default;
    protected Rigidbody2D m_Rigidbody2D = default;
    [SerializeField]protected GameObject m_Astro = default;
    protected GameObject Explosion = default;
    protected IEnumerator randomWalkCoroutine = default;
    protected IEnumerator chaseCoroutine=default;
    protected AudioManager m_AudioManager = default;

    protected override void SetParameters()
    { 
        Health = 100f;
        damage = 10f;
        attackRange = 1.2f;
        walkSpeed = 2f;
        randomWalkCooldown = 2f;
        attackCooldown = 3f;
        Explosion = (GameObject)Resources.Load("Explosion/MonsterExplosion");
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_EnemyAnimator = gameObject.GetComponent<EnemyAnimator>();
        m_Rigidbody2D =gameObject.GetComponent<Rigidbody2D>();
        m_Astro = null;
        m_Radar = gameObject.transform.GetChild(0).GetComponent<Radar>();
        randomWalkCoroutine = WalkRandom(randomWalkCooldown);
        chaseCoroutine = ChaseTarget(walkSpeed);
        lastAttackTime = Time.time;
        isChasing = false;
        isWalking = false;
    }
    protected void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        SetParameters();
        gameObject.SetActive(true);
        Explosion.SetActive(false);
        StartCoroutine(randomWalkCoroutine);
        StartCoroutine(chaseCoroutine);
    }

    public void TakeDamage(float damage)
    {
        m_Radar.ImproveRadius();
        m_EnemyAnimator.SetAnimationBeingHIt();
        if (Health - damage > 0)
        {
            Health -= damage;
        }
        else
        {
            Health = 0f;
            StopAllCoroutines();
            GameObject mostexpl=Instantiate(Explosion, this.gameObject.transform.position,this.gameObject.transform.rotation);
            mostexpl.GetComponent<MonsterExplosion>().Explode();
            Die();
        }
    }

    //richiamato a fine animation(animation event)
    public virtual void Die()
    {
        PlayDieSound();
        gameObject.SetActive(false);
    }


    protected void SelectDirectionForAnimator(Vector2 direction)
    {
        if(direction.x==0 && direction.y==0) m_EnemyAnimator.SetDirectionDown();
        else if (Mathf.Abs(direction.x) >= Mathf.Abs(direction.y))
        {
            if (direction.x >= 0)
            {
                m_EnemyAnimator.SetDirectionRight();
            }
            else
            {
                m_EnemyAnimator.SetDirectionLeft();
            }
        }
        else
        {
            if (direction.y > 0) m_EnemyAnimator.SetDirectionUp();
            else m_EnemyAnimator.SetDirectionDown();
        }

    }

    protected virtual IEnumerator ChaseTarget(float speed)
    {

        Vector3 offset;
        m_EnemyAnimator.SetAnimationRunning();
        while (true) { 
            if (m_Astro!=null)
            {
                PlayChasingSound();
                offset =GetOffset(m_Astro.transform.position - gameObject.transform.position);
                isChasing = true;
                m_Rigidbody2D.velocity = ((m_Astro.transform.position - gameObject.transform.position) +offset)*speed/2;
                SelectDirectionForAnimator(m_Rigidbody2D.velocity);
                yield return 0;
            }
            else isChasing = false;
            yield return 0;
        }
    }

    protected virtual void PlayChasingSound() { m_AudioManager.PlayGreenMonster(); }

    protected virtual void PlayDieSound() { m_AudioManager.PlayGreenMonsterDefeat(); }

    protected Vector3 GetOffset(Vector3 distance)
    {
        Vector3 res = distance;
        if (res.x != 0)
        {
            res.x = res.x > 0 ? -0.5f : 0.5f;
        }
        if (res.y != 0)
        {
            res.y = res.y > 0 ? -0.5f : 0.5f;
        }
        return res;
    }

    protected override IEnumerator WalkRandom(float randomWalkCooldownSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(randomWalkCooldownSeconds);
            if (!isWalking && !isChasing)
            {   
                StartCoroutine(Walk(walkSpeed, GenerateRandomVersor()));
            }
        }
    }
    protected override IEnumerator Walk(float speed, Vector3 direction)
    {
        Vector3 destination = gameObject.transform.position + direction;
        isWalking = true;
        m_EnemyAnimator.SetAnimationRunning();
        m_Rigidbody2D.velocity = (destination - transform.position);
        SelectDirectionForAnimator(m_Rigidbody2D.velocity);
        yield return new WaitForSeconds(2f);
        m_EnemyAnimator.SetAnimationIdle();
        isWalking = false;
        yield return 0;
    }

    protected IEnumerator TryAttack(float damage)
    {
        while (m_Astro != null)
        {
            if (Vector3.Distance(transform.position, m_Astro.transform.position)<= attackRange)
            {
                if ((lastAttackTime + attackCooldown <= Time.time) && m_Astro.GetComponent<Astro>().vulnerable)
                {
                    Attack(damage);
                    lastAttackTime = Time.time;
                }
            }
            yield return 0;
        }
        yield return 0;

    }

    protected void Attack(float damage)
    {
        m_EnemyAnimator.SetAnimationAttacking();
        m_Astro.GetComponent<Astro>().TakeDamage(damage);
    }

    public void AstroSpotted(GameObject target)
    {
        m_Astro = target;
    }

    public void AstroLost()
    {
        m_Astro = null;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        //collisione con le mura interne/esterne
        if ((collision.gameObject.layer == 8 || collision.gameObject.layer == 15)&&(!isChasing))
        {
            m_Rigidbody2D.velocity = -m_Rigidbody2D.velocity;
        }
        //collisione con Astro
        if (collision.gameObject.layer == 10)
        {
            StartCoroutine(TryAttack(damage));
        }
    }

}
