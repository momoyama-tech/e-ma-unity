using UnityEngine;

public class Flower : MonoBehaviour
{
    [SerializeField] private Material[] materials;
    private int index;

    void Update()
    {
        index = 0;
    }

    void ChangeMaterial()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            
            GetComponent<Renderer>().material = materials[index];
            index++;
        }
    }
}