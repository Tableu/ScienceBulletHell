using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindUIElement : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;
    void Start()
    {
        
    }
    
    void Update()
    {
        if (target != null)
        {
            transform.position = Camera.main.WorldToScreenPoint(target.transform.position + offset);
        }
    }
}
