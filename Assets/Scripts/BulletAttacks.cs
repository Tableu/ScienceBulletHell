using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public static class BulletAttacks
{
    public static void SpawnBullets(GameObject user, GameObject bullet, GameObject pattern, Transform parent, int layer)
    {
        Vector2 dir = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - user.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x)*Mathf.Rad2Deg;
        foreach (Transform transform in pattern.transform)
        {
            GameObject go = GameObject.Instantiate(bullet,
                user.transform.position + Quaternion.Euler(0, 0, angle) * transform.localPosition,
                transform.rotation, parent);
            go.layer = layer;
            Bullet bScript = go.GetComponent<Bullet>();
            if (bScript != null)
            {
                bScript.Direction = dir.normalized;
            }
        }
    }
}