using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AstroControls : MonoBehaviour,IRescuable
{
    AstroAnimationManager m_AstroAnimationManager = default;
    [SerializeField] AstroMovements m_AstroMovements = default;
    [SerializeField] Laser m_Laser = default;
    [SerializeField] Rescuer m_Rescuer = default;
    [SerializeField] Rigidbody2D m_Rigidbody2D = default;
    [SerializeField] GameObject ControlsHidden = default;
    [SerializeField] GameObject ControlsShown = default;
    private bool showcontrols = false;
    public bool isPaused = false;
    private bool IsWalking = false;
    public bool isRunning = false;
    private bool isSaving = false;
    private bool canMove = true;
    private bool isBeingRescued = false;
    private bool canFire = true;
    public bool canRescue = true;
    float laserCooldown = 3f;
    float fireCooldown = 0.125f;
    public bool laserReady = true;
    public string direction = default;
    public enum Direction { left, right, up, down };
    public Direction AstroDirection = default;

    [SerializeField] Cartridge m_Cartridge = default;
    private bool isFiring = false;

    #region laserHUD
    [SerializeField] Image laserHUDImage = default;
    [SerializeField] Image laserHUDCDImage = default;
    Color readyColor = new Color(0.4f, 1f, 0.5f, 1f);
    Color cooldownColor = new Color(1f, 0.4f, 0.4f, 1f);
    Color inUseColor = new Color(0.6f, 1f, 0.5f, 1f);
    #endregion

    public void StartControls() {
        m_AstroAnimationManager = gameObject.GetComponent<AstroAnimationManager>();
        AstroDirection = Direction.up;
        direction = "up";
        laserHUDImage.color = readyColor;
        StartCoroutine(Controls());
    }
    IEnumerator Controls() {
        while (true)
        {
            if (!isPaused)
            {
                if (canMove)
                {
                    m_AstroMovements.Move(isRunning);
                    if (IsMovingLeft())
                    {
                        IsWalking = true;
                        m_AstroAnimationManager.SetDirectionLeft();
                        //m_AstroMovements.Move(Vector2.left);
                    }
                    if (IsMovingUp())
                    {
                        IsWalking = true;
                        m_AstroAnimationManager.SetDirectionUp();
                        //m_AstroMovements.Move(Vector2.up);
                    }
                    if (IsMovingRight())
                    {
                        IsWalking = true;
                        m_AstroAnimationManager.SetDirectionRight();
                        //m_AstroMovements.Move(Vector2.right);
                    }
                    if (IsMovingDown())
                    {
                        IsWalking = true;
                        m_AstroAnimationManager.SetDirectionDown();
                        //m_AstroMovements.Move(Vector2.down);
                    }
                }
                else
                {
                    m_Rigidbody2D.velocity = Vector2.zero;
                }

                if (m_Rigidbody2D.velocity == Vector2.zero) IsWalking = false;

                if (isFiring) m_AstroAnimationManager.SetAnimationFiring(AstroDirection);
                else if (isSaving) m_AstroAnimationManager.SetAnimationSaving();
                else if (isRunning) m_AstroAnimationManager.SetAnimationRunning();
                else if (IsWalking) m_AstroAnimationManager.SetAnimationWalking();
                else m_AstroAnimationManager.SetAnimationIdle();



                //Running
                if (Input.GetMouseButtonDown(1) && !isBeingRescued)
                {
                    isRunning = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    isRunning = false;
                }


                //SavingCreatures-Laser
                if (Input.GetKey(KeyCode.E) && laserReady && !isBeingRescued)
                {
                    isSaving = true;
                    canMove = false;
                    laserHUDImage.color = inUseColor;
                    m_Laser.Spawn(gameObject.transform.position, AstroDirection);
                }
                if (Input.GetKeyUp(KeyCode.E))
                {
                    isSaving = false;
                    canMove = true;
                    m_Laser.Despawn();
                }

                if (!isSaving && laserReady)
                {
                    laserHUDImage.color = readyColor;
                }

                //ANIMATOR FOR RESCUE

                if (isBeingRescued)
                {
                    canMove = false;
                    m_AstroAnimationManager.SetAnimationIdle();
                }

                //CallRescue
                if (canRescue)
                {
                    if (Input.GetKeyDown(KeyCode.R))
                    {
                        canMove = false;
                        canFire = false;
                        isBeingRescued = true;
                        if (gameObject.GetComponent<CircleCollider2D>().enabled)
                            ModifyCollidersForSave();
                        m_Rescuer.callRescue();
                    }
                }


                if (Input.GetKeyUp(KeyCode.R))
                {
                    canMove = true;
                    canFire = true;
                    if (isBeingRescued)
                    {
                        if (!gameObject.GetComponent<CircleCollider2D>().enabled)
                            ResetOriginalColliders();
                        m_Rescuer.stopRescue();
                    }
                    isBeingRescued = false;
                }

                //FIRING
                if (Input.GetMouseButtonDown(0) && canFire)
                {
                    canMove = false;
                    isFiring = true;
                    StartCoroutine(Fire());
                }

                if (Input.GetMouseButtonUp(0))
                {
                    canMove = true;
                    isFiring = false;
                }

                if (Input.GetKeyUp(KeyCode.C)) {
                    showcontrols = !showcontrols;
                    ShowControls(showcontrols);
                }


                    yield return 0;
            }
            else
            {
                m_Rigidbody2D.velocity = Vector2.zero;
                isRunning = false;
                IsWalking = false;
                if (isFiring)
                {
                    isFiring = false;
                    canMove = true;
                }
                if (isSaving)
                {
                    isSaving = false;
                    canMove = true;
                    m_Laser.Despawn();
                }
                if (isBeingRescued)
                {
                    canMove = true;
                    if (!gameObject.GetComponent<CircleCollider2D>().enabled)
                        ResetOriginalColliders();
                    m_Rescuer.stopRescue();
                    isBeingRescued = false;
                }
                    yield return new WaitForSeconds(0.125f);
            }
        }
    }

    public void jellySaved() 
    {
        StartCoroutine(LaserCooldown());
    }

 
    IEnumerator Fire()
    {
        canFire = false;
        while (isFiring)
        {
            m_Cartridge.FireBullet(AstroDirection.ToString());
            yield return new WaitForSeconds(fireCooldown);
        }
        yield return new WaitForSeconds(0.05f);
        canFire = true;
        yield return 0;
    }
    IEnumerator LaserCooldown()
    {
        laserReady = false;
        laserHUDImage.color = cooldownColor;
        float startingTime = Time.time;
        float stoppingTime = startingTime + laserCooldown;
        float deltaTime;
        while (Time.time <= stoppingTime)
        {
            deltaTime = stoppingTime - Time.time;
            laserHUDCDImage.fillAmount = ((laserCooldown- deltaTime) / laserCooldown);
            yield return 0;
        }
        laserHUDImage.fillAmount = 1f;
        laserHUDImage.color = readyColor;
        laserReady = true;
        yield return 0;
    }

    public void Die()
    {
        m_Rigidbody2D.velocity = Vector2.zero;
        StopAllCoroutines();
    }

    private void ShowControls(bool showcontrols) {
        if (showcontrols)
        {
            ControlsHidden.SetActive(false);
            ControlsShown.SetActive(true);
        }
        else
        {
            ControlsHidden.SetActive(true);
            ControlsShown.SetActive(false);
        }

    }

    public void ModifyCollidersForSave()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
   public void ResetOriginalColliders()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    #region Axis

    bool IsMovingLeft()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            AstroDirection = Direction.left;
            direction = "left";
            return true;
        }
        return false;
    }

    bool IsMovingUp()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            AstroDirection = Direction.up;
            direction = "up";
            return true;
        }
        return false;
    }

    bool IsMovingRight()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            AstroDirection = Direction.right;
            direction = "right";
            return true;
        }
        return false;
    }

    bool IsMovingDown()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            AstroDirection = Direction.down;
            direction = "down";
            return true;
        }
        return false;
    }

    #endregion

}
