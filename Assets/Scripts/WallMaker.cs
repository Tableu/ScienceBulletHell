using System;
using System.Collections.Generic;
using UnityEngine;

public class WallMaker : MonoBehaviour
{
    [SerializeField] private PlayerMovement PlayerMovement;
    [SerializeField] private LineRenderer lr;
    public float WallLifetime;
    public float WallWidth;
    private bool _wallActive = false;
    private float _startTime;
    private Vector2 _startPos;
    private Vector2 _endPos;

    private void Start()
    {
        PlayerMovement.OnDashEnd += SpawnWall;
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
                float angle = Mathf.Atan2(_endPos.y - _startPos.y, _endPos.x - _startPos.x)*Mathf.Rad2Deg;
                Vector2 size;
                float xDiff = Mathf.Abs(_endPos.x - _startPos.x);
                float yDiff = Mathf.Abs(_endPos.y - _startPos.y);
                if (xDiff < WallWidth || yDiff < WallWidth)
                {
                    size = new Vector2(Mathf.Max(xDiff, yDiff),WallWidth);
                }else
                {
                    size = new Vector2(Mathf.Sqrt((xDiff*xDiff) + (yDiff*yDiff)),WallWidth);
                }
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
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube((_startPos + _endPos) / 2, new Vector3(Mathf.Abs(_endPos.x - _startPos.x), 2, 1f));
    }
#endif
}
