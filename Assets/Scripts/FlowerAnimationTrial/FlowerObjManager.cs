using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class FlowerObjManager : MonoBehaviour
{
    [SerializeField] private GameObject flowerObj;
    private GameObject flowerObjParent;
    private GameObject createdFlowerObj;

    void Start()
    {
        flowerObjParent = this.gameObject;
        CreateFlowerObj();
    }

    void Update()
    {
        if(flowerObj.GetComponent<FlowerObj>().GetIsCreated())
        {
            Move();
        }
    }

    private void CreateFlowerObj()
    {
        createdFlowerObj = Instantiate(flowerObj, flowerObjParent.transform);
    }

    private void Move()
    {
        this.gameObject.transform.DOMove(new Vector3(5, 0, 0), 3f);
    }
}