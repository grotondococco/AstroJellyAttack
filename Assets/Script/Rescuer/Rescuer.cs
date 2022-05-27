using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rescuer : MonoBehaviour
{
    [SerializeField] GameObject astro = default;
    [SerializeField] CameraFollow cameraFollow = default;
    [SerializeField] GameObject spaceship = default;
    [SerializeField] GameObject cone = default;
    Vector3 astropos;
    Vector3 astroInitialpos;
    Vector3 spawnPos;
    Vector3 savePos;
    float yOffsetSpawn = 5f;
    float yOffsetSave = 8f;
    [SerializeField] Animator m_Animator = default;
    private bool isArriving = false;
    private bool isRescuing = false;
    public bool isDropping = false;
    private bool isReturningBack = false;
    private bool success=false;

    float rescuingSpeed = 0.01f;

    AudioManager m_AudioManager = default;

    private void updatePositions()
    {
        astropos = astro.transform.position;
        spawnPos = new Vector3(astropos.x, astropos.y+ yOffsetSpawn, astropos.z);
        savePos= new Vector3(astropos.x, astropos.y+ yOffsetSave, astropos.z);
    }

    public void CallDrop(Vector3 position)
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameObject.transform.position = position;
        spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + yOffsetSave, gameObject.transform.position.z);
        gameObject.SetActive(true);
        astropos = spawnPos;
        astro.transform.position = spawnPos;
        savePos = new Vector3(astropos.x, astropos.y - yOffsetSave, astropos.z);
        Drop();
    }
    private void Drop()
    {
        cone.SetActive(true);
        astro.GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(GoDrop());
    }
    public void callRescue()
    {
        updatePositions();
        gameObject.SetActive(true);
        astroInitialpos = astropos;
        StopAllCoroutines();
        Spawn();
    }

    public void stopRescue()
    {
        StopAllCoroutines();
        StartCoroutine(rescueInterrupted());
    }

    private void Spawn()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        updatePositions();
        gameObject.transform.position = spawnPos;
        gameObject.SetActive(true);
        cone.SetActive(true);
        StartCoroutine(goRescue());
    }

    IEnumerator goRescue()
    {
        isArriving = true;
        float t = 0;
        while (isArriving)
        {
            t += Time.deltaTime;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, astropos, t);
            if (gameObject.transform.position == astropos) isArriving = false;
            yield return 0;
        }
        m_Animator.SetBool("isRescuing", true);
        StartCoroutine(Rescue());
        yield return 0;
    }

    IEnumerator rescueInterrupted()
    {
        m_AudioManager.StopRescueWarp();
        if (success)
        {
            cameraFollow.changeFocus(spaceship.transform);
            cone.SetActive(false);
            astro.SetActive(false);
        }
        else
        {
            astro.transform.position = astroInitialpos;
        }
        cone.SetActive(false);
        isReturningBack = true;
        float t = 0;
        m_Animator.SetBool("isRescuing", false);
        while (isReturningBack)
        {
            t += Time.deltaTime*rescuingSpeed*3;
            gameObject.transform.position= Vector3.MoveTowards(gameObject.transform.position, spawnPos, t);
            if (gameObject.transform.position == spawnPos) isReturningBack = false;
            yield return 0;
        }
        if (success)
        {
            astro.GetComponent<Astro>().Rescued();
        }
        gameObject.SetActive(false);
        yield return 0;
    }

    IEnumerator Rescue()
    {
        m_AudioManager.PlayRescueWarp();
        isRescuing = true;
        float t = 0;
        m_Animator.SetBool("isRescuing", true);
        while (isRescuing)
        {
            t += Time.deltaTime *rescuingSpeed;
            astro.transform.position = Vector3.MoveTowards(astro.transform.position,savePos, t);
            if (astro.transform.position.y >= savePos.y)
            {
                isRescuing = false;
                success = true;
                stopRescue();
            }
            yield return 0;
        }
        yield return 0;
    }
    IEnumerator GoDrop()
    {
        m_AudioManager.PlayRescueWarp();
        isDropping = true;
        float t = 0;
        m_Animator.SetBool("isRescuing", true);
        while (isDropping)
        {
            t += Time.deltaTime * rescuingSpeed*3;
            astro.transform.position = Vector3.MoveTowards(astro.transform.position, savePos, t);
            if (astro.transform.position.y<= savePos.y)
            {
                isDropping = false;
            }
            yield return 0;
        }
        m_AudioManager.StopRescueWarp();
        astro.GetComponent<CircleCollider2D>().enabled = true;
        StartCoroutine(GoAway());
        yield return 0;
    }

    IEnumerator GoAway()
    {
        cone.SetActive(false);
        isReturningBack = true;
        float t = 0;
        m_Animator.SetBool("isRescuing", false);
        while (isReturningBack)
        {
            t += Time.deltaTime * rescuingSpeed * 3;
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, spawnPos, t);
            if (gameObject.transform.position == spawnPos) isReturningBack = false;
            yield return 0;
        }
        gameObject.SetActive(false);
        yield return 0;
    }

}
