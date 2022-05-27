using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Laser : MonoBehaviour
{
    [SerializeField] AstroControls m_astroControls = default;
    [SerializeField] SaveCapsule m_SaveCapsule = default;
    [SerializeField] SpriteRenderer laserCone = default;
    [SerializeField] SpriteRenderer ring1 = default;
    [SerializeField] SpriteRenderer ring2 = default;
    [SerializeField] SpriteRenderer ring3 = default;
    [SerializeField] SpriteRenderer cage = default;
    [SerializeField] SpriteRenderer hitArea = default;

    AudioManager m_AudioManager = default;

    private int[] originalSortingOrder;


    float offsetYUp, offsetYDown, offsetXLeft, offsetXRight;
    public Jelly target = null;
    Vector2 savingPoint;
    public string laserDirection = null;

    private void Start()
    {
        originalSortingOrder = new int[6];
        originalSortingOrder[0] = laserCone.sortingOrder;
        originalSortingOrder[1] = ring1.sortingOrder;
        originalSortingOrder[2] = ring2.sortingOrder;
        originalSortingOrder[3] = ring3.sortingOrder;
        originalSortingOrder[4] = cage.sortingOrder;
        originalSortingOrder[5] = hitArea.sortingOrder;
        offsetYUp = 1f;
        offsetYDown = -0.5f;
        offsetXLeft = -1.5f;
        offsetXRight = 1.52f;
        gameObject.SetActive(false);
    }

    public void SpawnLeft()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x+offsetXLeft, 
                                                    gameObject.transform.position.y, 
                                                    gameObject.transform.position.z);
        gameObject.SetActive(true);
    }

    public void SpawnRight()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x+offsetXRight,
                                                    gameObject.transform.position.y,
                                                    gameObject.transform.position.z);
        gameObject.SetActive(true);
    }
    public void SpawnUp()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    gameObject.transform.position.y + offsetYUp,
                                                    gameObject.transform.position.z);
        gameObject.SetActive(true);
    }
    public void SpawnDown()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                                                    gameObject.transform.position.y + offsetYDown,
                                                    gameObject.transform.position.z);
        fixLayerOrderDown();
        gameObject.SetActive(true);
    }

    public void fixLayerOrderDown() {
        laserCone.sortingOrder += 10;
        ring1.sortingOrder += 10;
        ring2.sortingOrder += 10;
        ring3.sortingOrder += 10;
        cage.sortingOrder += 10;
        hitArea.sortingOrder += 10;
    }

    public void ResetLayerOrder() 
    {
        laserCone.sortingOrder = originalSortingOrder[0];
        ring1.sortingOrder = originalSortingOrder[1];
        ring2.sortingOrder = originalSortingOrder[2];
        ring3.sortingOrder = originalSortingOrder[3];
        cage.sortingOrder = originalSortingOrder[4];
        hitArea.sortingOrder = originalSortingOrder[5];
    }

    public void Spawn(Vector3 AstroPos,AstroControls.Direction AstroDirection) {
        if (!gameObject.activeSelf)
        {
            m_AudioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
            m_AudioManager.PlayLaserWarp();
            gameObject.transform.position = AstroPos;
            switch (AstroDirection)
            {
                case AstroControls.Direction.up:
                    laserDirection = "up";
                    SpawnUp();
                    break;
                case AstroControls.Direction.down:
                    laserDirection = "down";
                    SpawnDown();
                    break;
                case AstroControls.Direction.left:
                    laserDirection = "left";
                    SpawnLeft();
                    break;
                case AstroControls.Direction.right:
                    laserDirection = "right";
                    SpawnRight();
                    break;
                default:
                    Despawn();
                    break;
            }
            savingPoint = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1.5f);
        }
        if (target != null)
        {
            if (target.transform.position.y >= savingPoint.y) {
                m_astroControls.jellySaved();
                m_SaveCapsule.setJellySpriteAndDeploy(target);
                target.saved = true;
                target.gettingSaved = false;
                Despawn();
            }
        }
    }

    public void Despawn() 
    {
        if (gameObject.activeSelf) {
            m_AudioManager.StopLaserWarp();
            if (target != null)
            {
                AlertJelly(target);
            }
            ResetTarget();
            ResetLayerOrder();
            gameObject.SetActive(false);
        }
    }



    public void SetTarget(Jelly target) 
    {
        this.target = target;
    }

    public void ResetTarget() {
        target = null;
    }

    public void AlertJelly(Jelly jelly)
    {
        jelly.gettingSaved = false;
    }
}
