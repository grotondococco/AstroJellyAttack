using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    protected override void SetParameters()
    {
        base.SetParameters();
        Health = 50f;
        damage = 15f;
        walkSpeed = 4f;
        randomWalkCooldown = 2f;
        Explosion = (GameObject)Resources.Load("Explosion/FlyingMonsterExplosion");
    }

    protected override void PlayDieSound() { m_AudioManager.PlayFlyingMonsterDefeat(); }

    protected override void PlayChasingSound() { m_AudioManager.PlayFlyngMonster(); }
}
