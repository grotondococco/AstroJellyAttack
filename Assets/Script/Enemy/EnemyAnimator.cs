using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    Animator m_Animator = default;
    SpriteRenderer m_SpriteRenderer = default;
    private void Awake()
    {
        m_Animator = gameObject.GetComponent<Animator>();
        m_SpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    #region AnimationLayers: IDLE-RUNNING-ATTACKING-BEINGHIT-DEAD
    void SetAllLayerWeightToZero()
    {

        for (int i = 0; i < m_Animator.layerCount; i++)
        {
            m_Animator.SetLayerWeight(i, 0);
        }
    }
    public void SetAnimationIdle()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(0, 1);
    }

    public void SetAnimationRunning()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(1, 1);
    }

    public void SetAnimationAttacking()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(2, 1);
    }

    public void SetAnimationBeingHIt()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(3, 1);
    }

    public void SetAnimationDead()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(4, 1);
        m_Animator.SetBool("isDead", true);
    }


    #endregion

    #region DIRECTION: DOWN-LEFT-RIGHT-UP
    private void SetDirection(int direction)
    {
        m_Animator.SetInteger("direction", direction);
    }

    public void SetDirectionDown()
    {
        SetDirection(0);
    }
    public void SetDirectionLeft()
    {
        m_SpriteRenderer.flipX = false;
        SetDirection(1);
    }
    public void SetDirectionRight()
    {
        m_SpriteRenderer.flipX = true;
        SetDirection(1);
    }
    public void SetDirectionUp()
    {
        SetDirection(2);
    }

    #endregion
}