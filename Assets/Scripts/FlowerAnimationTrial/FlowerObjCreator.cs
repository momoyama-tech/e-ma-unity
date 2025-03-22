using UnityEngine;

public class FlowerObjCreator : MonoBehaviour
{
    [SerializeField] private GameObject flowerObj;
    [SerializeField] private GameObject flowerObjParent;
    private GameObject createdFlowerObj;

    void Start()
    {
        CreateFlowerObj();
    }

    public void CreateFlowerObj()
    {
        createdFlowerObj = Instantiate(flowerObj, flowerObjParent.transform);
    }
}