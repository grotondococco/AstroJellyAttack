using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstroLobbyControls : MonoBehaviour
{
    public AstroAnimationManager m_AstroAnimationManager = default;
    [SerializeField] AstroMovements m_AstroMovements = default;
    [SerializeField] Rigidbody2D m_Rigidbody2D = default;
    public bool isPaused = false;
    public bool IsWalking = false;
    public bool isRunning = false;
    public string direction = default;
    AudioManager m_AudioManager = default;
    [SerializeField] PauseMenuManager m_PauseMenuManager = default;
    [SerializeField] LobbyManager m_LobbyManager = default;
    [SerializeField] LobbyLevelAreaManager m_LobbyLevelAreaManager = default;

    private string focus = null;

    private void Start()
    {
        m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_AudioManager.PlayLobbyTrack();
        m_AstroAnimationManager = gameObject.GetComponent<AstroAnimationManager>();
        StartControls();
    }
    public void StartControls()
    {
        m_PauseMenuManager.canPause = true;
        direction = "up";
        StartCoroutine(Controls());
    }
    IEnumerator Controls()
    {
        while (true)
        {
            if (!isPaused)
            {
                m_AstroMovements.Move(isRunning);
                if (IsMovingLeft())
                {
                    IsWalking = true;
                    m_AstroAnimationManager.SetDirectionLeft();
                }
                else if (IsMovingUp())
                {
                    IsWalking = true;
                    m_AstroAnimationManager.SetDirectionUp();
                }
                else if (IsMovingRight())
                {
                    IsWalking = true;
                    m_AstroAnimationManager.SetDirectionRight();
                }
                else if (IsMovingDown())
                {
                    IsWalking = true;
                    m_AstroAnimationManager.SetDirectionDown();
                }
                else
                {
                    m_Rigidbody2D.velocity = Vector2.zero;
                    IsWalking = false;
                }

                if (isRunning) m_AstroAnimationManager.SetAnimationRunning();
                else if (IsWalking) m_AstroAnimationManager.SetAnimationWalking();
                else m_AstroAnimationManager.SetAnimationIdle();



                //Running
                if (Input.GetMouseButtonDown(1))
                {
                    isRunning = true;
                }
                if (Input.GetMouseButtonUp(1))
                {
                    isRunning = false;
                }

                yield return 0;

                //LevelActions
                if (focus != null)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (focus.Equals("JellyMerchant")) m_LobbyManager.ShowPowerUpMenu();
                        if (focus.Equals("JellyBoss")) m_LobbyManager.ShowInstructions();
                        if (focus.Contains("Level"))
                        {
                            if (focus.Equals("Level1") && m_LobbyLevelAreaManager.level1)   //loadlevel1
                                Gamemanager.LoadLevel(focus);
                            else if (focus.Equals("Level2") && m_LobbyLevelAreaManager.level2) //loadlevel2
                                Gamemanager.Credits();
                            else if (focus.Equals("Level3") && m_LobbyLevelAreaManager.level3)  //loadlevel3
                                Gamemanager.Credits();
                            else m_AudioManager.PlayLevelCantEnter();
                        }
                    }
                }
            }
            else
            {
                m_Rigidbody2D.velocity = Vector2.zero;
                isRunning = false;
                IsWalking = false;
                yield return new WaitForSeconds(0.125f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12)
        {
            focus = collision.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 12)
        {
            focus = null;
        }
    }


    #region Axis

    bool IsMovingLeft()
    {
        if (Input.GetAxisRaw("Horizontal") < 0)
        {
            direction = "left";
            return true;
        }
        return false;
    }

    bool IsMovingUp()
    {
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            direction = "up";
            return true;
        }
        return false;
    }

    bool IsMovingRight()
    {
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            direction = "right";
            return true;
        }
        return false;
    }

    bool IsMovingDown()
    {
        if (Input.GetAxisRaw("Vertical") < 0)
        {
            direction = "down";
            return true;
        }
        return false;
    }

    #endregion

}

