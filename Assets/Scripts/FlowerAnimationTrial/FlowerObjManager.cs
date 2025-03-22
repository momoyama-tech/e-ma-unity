using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class FlowerObjManager : MonoBehaviour
{
    [SerializeField] private GameObject flowerObj;
    private GameObject flowerObjParent;
    private GameObject createdFlowerObj;

    async Task Start()
    {
        flowerObjParent = this.gameObject;
        await CreateFlowerObj();
        Move();
    }

    void Update()
    {

    }

    private async Task CreateFlowerObj()
    {
        createdFlowerObj = Instantiate(flowerObj, flowerObjParent.transform);
        await createdFlowerObj.GetComponent<FlowerObj>().Instantiate();
    }

    private void Move()
    {
        Debug.Log("Move");
        this.gameObject.transform.DOMove(new Vector3(10, 0, 0), 3f);
    }
}