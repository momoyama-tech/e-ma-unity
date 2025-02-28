using UnityEngine;

public class Player : MonoBehaviour
{
  [SerializeField] private float speed;
  private Rigidbody rb;
  void Start()
  {
    rb = GetComponent<Rigidbody>();
    if(rb == null)
    {
      Debug.LogError("Rigidbody is not attached to the player object.");
    }
  }

  void Update()
  {
    UpdateMove();
  }

  private void UpdateMove()
  {
    if (Input.GetKey(KeyCode.W))
    {
      rb.AddForce(Vector3.forward * speed);
    }
    if (Input.GetKey(KeyCode.S))
    {
      rb.AddForce(Vector3.back * speed);
    }
    if (Input.GetKey(KeyCode.A))
    {
      rb.AddForce(Vector3.left * speed);
    }
    if (Input.GetKey(KeyCode.D))
    {
      rb.AddForce(Vector3.right * speed);
    }
  }
}