using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterExplosion : MonoBehaviour
{
    public void Explode()
    {
        this.gameObject.SetActive(true);
    }
    //richiamato da animator
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
