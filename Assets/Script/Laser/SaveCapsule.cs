using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveCapsule : MonoBehaviour
{
    [SerializeField] SpriteRenderer m_SpriteRenderer = default;
    [SerializeField] SpriteRenderer jellySpriteRenderer = default;
    float travelTime = 3f;

    AudioManager m_AudioManager = default;

    public void setJellySpriteAndDeploy(Jelly jelly)
    {
        jellySpriteRenderer.sprite = jelly.GetComponent<SpriteRenderer>().sprite;
        Spawn(jelly.transform.position);
        StartCoroutine(Travel(travelTime));
    }

    public void Spawn(Vector3 jellyPosition)
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        gameObject.transform.position = jellyPosition;
        gameObject.SetActive(true);
    }

    public void Despawn()
    {
        gameObject.SetActive(false);
    }

    IEnumerator Travel(float travelTime)
    {
        Color standardcolor = m_SpriteRenderer.color;
        Color.RGBToHSV(standardcolor, out float standardHSVColorH, out float standardHSVColorU, out float standardHSVColorE);
        float startingTime = Time.time;
        float stoppingTime = startingTime + travelTime;
        m_AudioManager.PlayJellySavedSuccess();
        while (Time.time <= stoppingTime)
        {
            m_SpriteRenderer.color = Random.ColorHSV(standardHSVColorH, 1f, standardHSVColorU, standardHSVColorU, standardHSVColorE, standardHSVColorE);
            gameObject.transform.Translate(Vector3.up * 0.02f, Space.World);
            yield return 0;
        }
        m_AudioManager.StopJellySavedSuccess();
        m_SpriteRenderer.color = standardcolor;
        Despawn();
    }
}
