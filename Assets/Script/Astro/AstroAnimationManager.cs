using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroAnimationManager : MonoBehaviour
{
    [SerializeField] Animator m_Animator = default;
    [SerializeField] SpriteRenderer m_SpriteRenderer = default;

    [SerializeField] SpriteRenderer rifle_sprite_side = default;
    [SerializeField] SpriteRenderer rifle_sprite_up = default;
    [SerializeField] SpriteRenderer rifle_sprite_down = default;
    [SerializeField] SpriteRenderer rifle_arm_sprite = default;

    #region AnimationLayers: IDLE-RUNNING-WALKING-SAVING-DEAD-FIRING
    void SetAllLayerWeightToZero()
    {
        DespawnRifle();
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

    public void SetAnimationWalking()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(1, 1);
    }

    public void SetAnimationRunning()
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(2, 1);
    }

    public void SetAnimationSaving()
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

    public void SetAnimationFiring(AstroControls.Direction AstroDirection)
    {
        SetAllLayerWeightToZero();
        m_Animator.SetLayerWeight(5, 1);
        if (AstroDirection== AstroControls.Direction.right)
        {
            rifle_sprite_side.flipX = true;
            rifle_arm_sprite.flipX = true;
        }
        else
        {
            rifle_sprite_side.flipX = false;
            rifle_arm_sprite.flipX = false;
        }
        SpawnRifle(AstroDirection);
    }

    private void SpawnRifle(AstroControls.Direction AstroDirection)
    {
        if (AstroDirection== AstroControls.Direction.left|| AstroDirection == AstroControls.Direction.right)
        {
            rifle_arm_sprite.color = new Color(rifle_arm_sprite.color.r, rifle_arm_sprite.color.g, rifle_arm_sprite.color.b, 1f);
            rifle_sprite_side.color = new Color(rifle_sprite_side.color.r, rifle_sprite_side.color.g, rifle_sprite_side.color.b, 1f);
        }
        if (AstroDirection == AstroControls.Direction.down)
        {
            rifle_sprite_down.color = new Color(rifle_sprite_side.color.r, rifle_sprite_side.color.g, rifle_sprite_side.color.b, 1f);
        }
        if (AstroDirection == AstroControls.Direction.up)
        {
            rifle_sprite_up.color = new Color(rifle_sprite_side.color.r, rifle_sprite_side.color.g, rifle_sprite_side.color.b, 1f);
        }
    }

    private void DespawnRifle()
    {
        rifle_sprite_side.color = new Color(rifle_sprite_side.color.r, rifle_sprite_side.color.g, rifle_sprite_side.color.b, 0f);
        rifle_sprite_up.color= new Color(rifle_sprite_up.color.r, rifle_sprite_up.color.g, rifle_sprite_up.color.b, 0f);
        rifle_sprite_down.color= new Color(rifle_sprite_down.color.r, rifle_sprite_down.color.g, rifle_sprite_down.color.b, 0f);
        rifle_arm_sprite.color = new Color(rifle_arm_sprite.color.r, rifle_arm_sprite.color.g, rifle_arm_sprite.color.b, 0f);
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
        SetDirection(1);
        m_SpriteRenderer.flipX = false;
    }
    public void SetDirectionRight()
    {
        SetDirection(1);
        m_SpriteRenderer.flipX = true;
    }
    public void SetDirectionUp()
    {
        SetDirection(2);
    }

    #endregion

}