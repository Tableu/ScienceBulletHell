using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class WallMaker : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private LineRenderer lr;
    [SerializeField] private GameObject HealthRefill;
    [SerializeField] private Slider Slider;
    public float WallLifetime;
    public float WallWidth;
    public float RedeemHealthCount;
    public Vector2 ItemSpawnMin;
    public Vector2 ItemSpawnMax;
    private bool _wallActive = false;
    private float _startTime;
    private float angle;
    private Vector2 size;
    private Vector2 _startPos;
    private Vector2 _endPos;
    private int reflectionCount;

    private void Start()
    {
        PlayerMovement.OnDashEnd += SpawnWall;
        Slider.maxValue = RedeemHealthCount;
        Slider.value = 0;
    }

    private void FixedUpdate()
    {
        if (_wallActive)
        {
            if (Time.time - _startTime >= WallLifetime)
            {
                _wallActive = false;
                lr.enabled = false;
            }
            else
            {
                List<RaycastHit2D> results = new List<RaycastHit2D>();
                
                
                Physics2D.BoxCast((_startPos + _endPos) / 2, size, 
                    angle,Vector2.zero, new ContactFilter2D()
                    {
                        layerMask = LayerMask.GetMask("EnemyProjectiles", "PlayerProjectiles"),
                        useLayerMask = true
                    }, results);
                foreach (RaycastHit2D hit in results)
                {
                    if (hit)
                    {
                        Bullet bullet = hit.collider.gameObject.GetComponent<Bullet>();
                        if (bullet != null)
                        {
                            if (bullet.RecentWall != this)
                            {
                                bullet.Direction = Vector2.Reflect(bullet.Direction, hit.normal);
                                bullet.RecentWall = this;
                                bullet.gameObject.layer = 9;
                                reflectionCount++;
                                if (reflectionCount >= RedeemHealthCount)
                                {
                                    reflectionCount = 0;
                                    SpawnHealthItem();
                                }
                                Slider.value = reflectionCount;
                            }
                        }
                        else
                        {
                            Destroy(hit.collider.gameObject);
                        }
                    }
                }
            }
        }
    }

    private void SpawnHealthItem()
    {
        Random.InitState(System.DateTime.Now.Millisecond);
        float x = Random.Range(ItemSpawnMin.x, ItemSpawnMax.x);
        float y = Random.Range(ItemSpawnMin.y, ItemSpawnMax.y);
        Vector2 boostSize = HealthRefill.GetComponent<BoxCollider2D>().size;
        RaycastHit2D hit = Physics2D.BoxCast(transform.position+new Vector3(x, y), boostSize, 0, Vector2.right, Single.PositiveInfinity, LayerMask.GetMask("Background","Player", "Enemy", "Powerups"));
        int count = 0;
        while (hit && count < 10)
        {
            count++;
            x = Random.Range(ItemSpawnMin.x, ItemSpawnMax.x);
            y = Random.Range(ItemSpawnMin.y, ItemSpawnMax.y);
            hit = Physics2D.BoxCast(transform.position + new Vector3(x, y), boostSize, 0, Vector2.right, Single.PositiveInfinity, LayerMask.GetMask("Background","Player", "Enemy", "Powerups"));
        }

        Instantiate(HealthRefill, transform.position+new Vector3(x, y), Quaternion.identity);
    }

    private void SpawnWall()
    {
        lr.enabled = true;
        lr.SetPositions(new []
        {
            (Vector3) PlayerMovement.DashStartPos,
            (Vector3) PlayerMovement.DashEndPos
        });
        _startPos = PlayerMovement.DashStartPos;
        _endPos = PlayerMovement.DashEndPos;
        _wallActive = true;
        _startTime = Time.time;
        angle = Mathf.Atan2(_endPos.y - _startPos.y, _endPos.x - _startPos.x)*Mathf.Rad2Deg;
        float xDiff = Mathf.Abs(_endPos.x - _startPos.x);
        float yDiff = Mathf.Abs(_endPos.y - _startPos.y);

        if (xDiff < WallWidth || yDiff < WallWidth)
        {
            size = new Vector2(Mathf.Max(xDiff, yDiff),WallWidth);
        }else
        {
            size = new Vector2(Mathf.Sqrt((xDiff*xDiff) + (yDiff*yDiff)),WallWidth);
        }
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((_startPos + _endPos) / 2, new Vector3(Mathf.Abs(_endPos.x - _startPos.x), 2, 1f));
    }
#endif
}
