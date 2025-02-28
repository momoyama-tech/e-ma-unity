using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private GameObject playerCameraRight;
    [SerializeField] private GameObject playerCameraLeft;
    [Range(-180, 180)] private float rotationX;
    [Range(-180, 180)] private float rotationY;
    [Range(0, 100)] private float height;

    void Start()
    {
        
    }

    private void Initialize()
    {
        
    }

    private void InitializeCameraPos()
    {
    }

    private void InitializeCameraRotate()
    {
        
    }
}