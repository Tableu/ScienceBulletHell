using Cinemachine;
using UnityEngine;

public class Area : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    public Collider2D Collider;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnterArea(GameObject Player)
    {
        VirtualCamera.enabled = true;
        Collider.enabled = false;
        VirtualCamera.Follow = Player.transform;
    }

    public void LeaveArea()
    {
        VirtualCamera.enabled = false;
        Collider.enabled = true;
        VirtualCamera.Follow = null;
    }
}
