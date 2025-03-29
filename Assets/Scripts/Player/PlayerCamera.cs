using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isRight;
    private int rotateDirection;

    void Start()
    {
        if(isRight)
        {
            rotateDirection = 1;
        }
        else
        {
            rotateDirection = -1;
        }
    }

    void Update()
    {
        UpdateRotate();
    }

    private void UpdateRotate()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            transform.Rotate(Vector3.right * speed * rotateDirection);
        }
        if(Input.GetKey(KeyCode.DownArrow))
        {
            transform.Rotate(Vector3.left * speed * rotateDirection);
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * speed * rotateDirection);
        }
        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.down * speed * rotateDirection);
        }
    }
}