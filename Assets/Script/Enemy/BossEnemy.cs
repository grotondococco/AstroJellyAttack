using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy :Enemy
{
    protected override void SetParameters()
    {
        base.SetParameters();
        Health = 300f;
        damage = 25f;
        walkSpeed = 2f;
        randomWalkCooldown = 2f;
        Explosion = (GameObject)Resources.Load("Explosion/BossMonsterExplosion");
    }

    public override void Die()
    {
        m_AudioManager.PlayBossMonsterDefeat();
        gameObject.SetActive(false);
    }
    protected override void PlayChasingSound() { m_AudioManager.PlayBossMonster(); }
    protected override void PlayDieSound() { m_AudioManager.PlayBossMonsterDefeat(); }

}
