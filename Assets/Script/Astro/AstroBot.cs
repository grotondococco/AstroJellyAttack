using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroBot : RandomWalker
{
    [SerializeField] Rigidbody2D m_Rigidbody2D = default;
    AstroAnimationManager m_AstroAnimationManager = default;
    bool isFiring = false;
    [SerializeField] Cartridge m_Cartridge = default;
    float fireCooldown = 0.125f;
    float firingDuration = 3f;
    AstroControls.Direction directionForAnimator = default;

    protected override void SetParameters()
    {
        upBound = MenuBoundaries.menuUpBound;
        downBound = MenuBoundaries.menuDownBound;
        leftBound = MenuBoundaries.menuLeftBound;
        rightBound = MenuBoundaries.menuRightBound;
        walkSpeed = 2f;
        randomWalkCooldown = 5f;
        directionForAnimator = AstroControls.Direction.down;
    }

    public void Spawn()
    {
        m_AstroAnimationManager = gameObject.GetComponent<AstroAnimationManager>();
        SetParameters();
        gameObject.SetActive(true);
        StartCoroutine(WalkRandom(randomWalkCooldown));
    }

    private void Start()
    {
        Spawn();
    }


    protected override IEnumerator WalkRandom(float randomWalkCooldownSeconds)
    {
        float timeStart = Time.time;
        float timeStop = timeStart + randomWalkCooldownSeconds;
        while (true)
        {
            if (!isFiring && !isWalking && Time.time >= timeStop)
            {
                switch ((int)Random.Range(1, 10))
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                        {
                            timeStart = Time.time;
                            timeStop = timeStart + randomWalkCooldown;
                            StartCoroutine(Walk(walkSpeed, GenerateRandomVersor() * 5));
                            break;
                        }
                    default:
                        {
                            timeStart = Time.time;
                            timeStop = timeStart + randomWalkCooldown;
                            isFiring = true;
                            StartCoroutine(Fire(Time.time));
                            break;
                        }
                }
                yield return 0;
            }
            yield return new WaitForSeconds(randomWalkCooldown);
        }
    }

    protected override IEnumerator Walk(float speed, Vector3 direction)
    {
        bool triggerForBounds = false;
        Vector3 destination = gameObject.transform.position + direction;
        isWalking = true;
        m_AstroAnimationManager.SetAnimationWalking();
        if (direction.x >= 0)
        {
            directionForAnimator = AstroControls.Direction.right;
            m_AstroAnimationManager.SetDirectionRight();
        }
        else
        {
            directionForAnimator = AstroControls.Direction.left;
            m_AstroAnimationManager.SetDirectionLeft();
        }
        while (Vector2.Distance(destination, gameObject.transform.position) > 1&& triggerForBounds==false)
        {
            m_Rigidbody2D.velocity = (destination - transform.position) / speed;
            yield return new WaitForSeconds(0.125f);
            if (IsAtTheBounds()!=-1)
            {
                triggerForBounds = true;
            }
            yield return 0;
        }
        m_Rigidbody2D.velocity = Vector2.zero;
        m_AstroAnimationManager.SetAnimationIdle();
        isWalking = false;
        if (triggerForBounds) StartCoroutine(Walk(speed, -direction));
        yield return 0;
    }

    IEnumerator Fire(float startingTime)
    {
        float stoppingTime = startingTime + firingDuration;
        m_AstroAnimationManager.SetAnimationFiring(directionForAnimator);
        while (isFiring)
        {
            m_Cartridge.FireBullet(directionForAnimator.ToString());
            yield return new WaitForSeconds(fireCooldown);
            if (Time.time >= stoppingTime) isFiring = false;
        }
        yield return new WaitForSeconds(0.05f);
        m_AstroAnimationManager.SetAnimationIdle();
        yield return 0;
    }

}
