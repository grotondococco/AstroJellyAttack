using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Astro : MonoBehaviour,IKillable
{
    //Health + Oxygen
    [SerializeField] public float Health = 100f;
    [SerializeField] TMPro.TextMeshProUGUI healthLevelText = default;
    [SerializeField] public float OxygenLevel = 100f;
    [SerializeField] Image OgyxgenLevelImageHUD = default;
    [SerializeField] TMPro.TextMeshProUGUI oxygenLevelText = default;
    Color HUDOK;
    Color HUDNOTOK;

    [SerializeField] Transform m_Transform = default;
    [SerializeField] SpriteRenderer m_spriteRenderer = default;
    [SerializeField] Transform AstroSpawnPoint = default;
    [SerializeField] LevelManager m_LevelManager = default;
    [SerializeField] AstroControls m_AstroControls = default;
    [SerializeField] StartTimer m_StartTimer = default;
    [SerializeField] Timer m_Timer = default;
    [SerializeField] Rescuer m_Rescuer = default;
    [SerializeField] PauseMenuManager m_PauseMenuManager = default;

    AstroAnimationManager m_AstroAnimationManager = default;
    AudioManager m_AudioManaster = default;

    public bool needToBreathe=true;
    public bool vulnerable=true;
    private float invul_time = 1f;
    public float oxygen_loose_deltatime = 1.5f;
    public int oxygen_rarefied_level = 1;
    private int oxygenPowerUpLevel = default;

    //  Attivare/Disattivare normal/debug Start
    void Start()
    {
        m_AudioManaster = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_AstroAnimationManager = gameObject.GetComponent<AstroAnimationManager>();
        HUDOK = new Color(0.7f, 1f, 0.5f, 1);
        HUDNOTOK= new Color(1f, 0.3f, 0.3f, 1);
        UpdateHealthHUD();
        UpdateOxygenHUD();
        oxygenPowerUpLevel = SaveManager.GetPowerupSettings().oxygenLevel;
        //Debug.Log("oxygen power up level: " + oxygenPowerUpLevel);
        //DebugStart();
        NormalStart();
    }

    void DebugStart() {
        m_Transform.position = AstroSpawnPoint.transform.position;
        m_AstroAnimationManager.SetAnimationIdle();
        m_AstroControls.StartControls();
        m_Timer.StartTimer(true);
        //StartCoroutine(Breath());
    }
    void NormalStart()
    {
        StartCoroutine(Spawn(AstroSpawnPoint));
    }

    IEnumerator Spawn(Transform SpawnPoint) 
    {
        m_Transform.position = Vector3.zero;
        m_AstroAnimationManager.SetAnimationIdle();
        m_AstroAnimationManager.SetDirectionDown();
        m_Rescuer.CallDrop(SpawnPoint.position);
        while(m_Rescuer.isDropping)
            yield return new WaitForSeconds(0.125f);
        m_StartTimer.StartTheTimer();
        while (m_StartTimer.timerEnd == false)
            yield return new WaitForSeconds(0.125f);
        m_AstroControls.StartControls();
        StartCoroutine(Breath());
        m_Timer.StartTimer();
        m_PauseMenuManager.canPause = true;
        yield return 0;
    }

    public void TakeDamage(float damage) 
    {
        if (Health - damage > 0)
        {
            m_AudioManaster.PlayAstroDamage();
            Health -= damage;
            UpdateHealthHUD();
            StartCoroutine(Invulnerability(invul_time));
        }
        else
        {
            Health = 0f;
            UpdateHealthHUD();
            Die();
        }
    }

    public void AddOxygen(float quantity)
    {
        if (OxygenLevel + quantity >= 100f) OxygenLevel = 100f;
        else
        {
            OxygenLevel += quantity;
        }
        UpdateOxygenHUD();
    }
    public void LoseOxygen(float quantity)
    {
        if (oxygenPowerUpLevel == 2) quantity /= 1.1f;
        if (oxygenPowerUpLevel == 3) quantity /= 1.2f;
        if (m_AstroControls.isRunning) quantity *= 1.2f;
        //Debug.Log("Lost oxygen:" + quantity);
        if (OxygenLevel - quantity > 0f)
        {
            OxygenLevel -= quantity;
            UpdateOxygenHUD();
        }
        else
        {
            OxygenLevel = 0f;
            UpdateOxygenHUD();
            Die();
        }
    }

    public void UpdateOxygenHUD() 
    {
        oxygenLevelText.text = (int)OxygenLevel + "%";
        OgyxgenLevelImageHUD.fillAmount = OxygenLevel / 100;
        if (OxygenLevel < 30) oxygenLevelText.color = HUDNOTOK;
        else oxygenLevelText.color = HUDOK;
    }

    public void UpdateHealthHUD()
    {
        healthLevelText.text = (int)Health + "%";
        if (Health < 30) healthLevelText.color = HUDNOTOK;
        else healthLevelText.color = HUDOK;
    }

    public void Rescued()
    {
        m_LevelManager.checkLevelEsit();
    }

    public void Die()
    {
        m_AstroControls.Die();
        m_LevelManager.checkLevelEsit(true);
        m_AstroAnimationManager.SetAnimationDead();
    }

    public void PlayerDisappear()
    {
        m_Transform.gameObject.SetActive(false);
    }

    public void PlayerAppear()
    {
        m_Transform.gameObject.SetActive(true);
    }

   IEnumerator Breath()
    {
        while (true)
        {
            yield return new WaitForSeconds(oxygen_loose_deltatime);
            if (needToBreathe) LoseOxygen(oxygen_rarefied_level*1.5f);
        }
    }
    IEnumerator Invulnerability(float seconds)
    {
        vulnerable = false;
        StartCoroutine(SpriteBlink());
        yield return new WaitForSeconds(seconds);
        vulnerable = true;
        yield return 0;
    }
    IEnumerator SpriteBlink() {
        float t;
        Color standardColor = new Color(m_spriteRenderer.color.r, m_spriteRenderer.color.g, m_spriteRenderer.color.b, 1);
        Color trasparentColor = new Color(standardColor.r, standardColor.g, standardColor.b, 0);
        while (vulnerable==false)
        {
            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime*10;
                m_spriteRenderer.color = Color.Lerp(standardColor, trasparentColor, t);
                yield return 0;
            }
            if (vulnerable==true) break;
            t = 1;
            while (t > 0)
            {
                t -= Time.deltaTime*10;
                m_spriteRenderer.color = Color.Lerp(standardColor, trasparentColor, t);
                yield return 0;
            }
            yield return 0;
        }
        m_spriteRenderer.color = standardColor;
        yield return 0;
    }
}
