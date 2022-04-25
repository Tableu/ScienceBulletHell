using Cinemachine;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private Area _area;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_area != null)
        {
            _area.LeaveArea();
        }

        _area = other.GetComponent<Area>();
        if (_area != null)
        {
            _area.EnterArea(gameObject);
        }
    }
}
