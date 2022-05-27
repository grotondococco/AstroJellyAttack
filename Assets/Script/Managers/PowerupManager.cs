using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{

    [SerializeField] LobbyManager m_LobbyManager = default;
    [Header("PopUpWindow")]
    //LevelText
    [SerializeField]TMPro.TextMeshProUGUI MovSpeedLevelText = default;
    [SerializeField] TMPro.TextMeshProUGUI OxygenTankText = default;
    [SerializeField] TMPro.TextMeshProUGUI FirePowerText = default;
    [SerializeField] TMPro.TextMeshProUGUI CameraPeakText = default;
    //CostText
    [SerializeField] TMPro.TextMeshProUGUI MovSpeedCostText = default;
    [SerializeField] TMPro.TextMeshProUGUI OxygenTankCostText = default;
    [SerializeField] TMPro.TextMeshProUGUI FirePowerCostText = default;
    [SerializeField] TMPro.TextMeshProUGUI CameraCostText = default;

    [Header("FixedWindow")]
    [SerializeField] TMPro.TextMeshProUGUI MovSpeedLevelTextFixed = default;
    [SerializeField] TMPro.TextMeshProUGUI OxygenTankTextFixed = default;
    [SerializeField] TMPro.TextMeshProUGUI FirePowerTextFixed = default;
    [SerializeField] TMPro.TextMeshProUGUI CameraPeakTextFixed = default;


    private PowerupSettings powerups;
    PointsManager m_PointsManager = default;
    private AudioManager m_AudioManager;

    private int[] speedCost = {0,0, 200, 500 };
    private int[] oxygenCost = {0,0, 100, 300 };
    private int[] fireCost = {0,0, 150, 400 };
    private int camerapeakcost = 100;

    int speedlevel, oxygenlevel, firelevel;
    bool camerapeak;
    int totalpoints;
    private void Start()
    {
        m_AudioManager=GameObject.Find("AudioManager").GetComponent<AudioManager>();
        m_PointsManager = GameObject.Find("PointsManager").GetComponent<PointsManager>();
        totalpoints = m_PointsManager.getTotalPoints();
        powerups = SaveManager.GetPowerupSettings();
        speedlevel = powerups.speedLevel;
        oxygenlevel = powerups.oxygenLevel;
        firelevel = powerups.firingLevel;
        camerapeak = powerups.cameraPeak;
        UpdateAllGuis();
    }

    private void UpdateAllGuis()
    {
        //Debug.Log("Lodaded: speed:" + speedlevel + " oxy:" + oxygenlevel + " fire:" + firelevel + " cam" + camerapeak);
        UpdatePopUpGUI();
        UpdateFixedGUI();
    }

    private void UpdatePopUpGUI()
    {
        if (speedlevel < 3)
        {
            MovSpeedLevelText.text = "Lv." + speedlevel;
            MovSpeedCostText.text = speedCost[speedlevel + 1].ToString()+" JP";
        }
        else { 
            MovSpeedLevelText.text = "Max";
            MovSpeedCostText.text = "";
        }
        if (oxygenlevel < 3)
        {
            OxygenTankText.text = "Lv." + oxygenlevel;
            OxygenTankCostText.text = oxygenCost[oxygenlevel + 1].ToString() + " JP";
        }
        else { 
            OxygenTankText.text = "Max";
            OxygenTankCostText.text = "";
        }
        if (firelevel < 3) { 
            FirePowerText.text = "Lv." + firelevel;
            FirePowerCostText.text = fireCost[firelevel + 1].ToString() + " JP";
        }
        else { 
            FirePowerText.text = "Max";
            FirePowerCostText.text = "";
        }
        if (!camerapeak) { 
            CameraPeakText.text = "Lv.0";
            CameraCostText.text = camerapeakcost.ToString() + " JP";
        }
        else { 
            CameraPeakText.text = "Max";
            CameraCostText.text = "";
        }
    }

    private void UpdateFixedGUI()
    {
        if (speedlevel < 3) MovSpeedLevelTextFixed.text = "Lv." + speedlevel;
        else MovSpeedLevelTextFixed.text = "Max";
        if (oxygenlevel < 3) OxygenTankTextFixed.text = "Lv." + oxygenlevel;
        else OxygenTankTextFixed.text = "Max";
        if (firelevel < 3) FirePowerTextFixed.text = "Lv." + firelevel;
        else FirePowerTextFixed.text = "Max";
        if (!camerapeak) CameraPeakTextFixed.text = "Lv.0";
        else CameraPeakTextFixed.text = "Max";
    }

    public void CheckPowerUpPrice(int type)
    {
        int cost=0;
        if (type == 0 && (speedlevel< speedCost.Length-1)) cost = speedCost[speedlevel + 1];
        if (type == 1 && (oxygenlevel < oxygenCost.Length - 1)) cost = oxygenCost[oxygenlevel + 1];
        if (type==2 && (firelevel < fireCost.Length - 1)) cost = fireCost[firelevel + 1];
        if (type == 3 && !camerapeak) cost = camerapeakcost;
        if (cost == 0) {
            m_AudioManager.PlayLevelCantEnter();
            return;
        }
        if (cost <= totalpoints) BuyPowerUp(type, cost);
        else m_AudioManager.PlayLevelCantEnter();
    }

    private void BuyPowerUp(int type, int cost) {
        m_AudioManager.PlayMerchantSell();
        totalpoints -= cost;
        Debug.Log(totalpoints);
        if (type == 0) speedlevel += 1;
        if (type == 1) oxygenlevel += 1;
        if (type == 2) firelevel += 1;
        if (type == 3) camerapeak = true;
        m_PointsManager.UsePoints(cost);
        SaveManager.SavePowerups(new PowerupSettings(speedlevel,oxygenlevel,firelevel,camerapeak));
        m_LobbyManager.UpdateGUI();
        UpdateAllGuis();
    }

}
