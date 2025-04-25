using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SampleFlowerCreator : MonoBehaviour
{
    [SerializeField] private GameObject flowerObj;
    private GameObject flowerObjParent;
    private GameObject createdFlowerObj;

    public async UniTask ManualStart()
    {
        flowerObjParent = this.gameObject;
    }

    public async UniTask ManualUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            await CreateFlowerObj();
        }
    }

    private async UniTask CreateFlowerObj()
    {
        createdFlowerObj = Instantiate(flowerObj, flowerObjParent.transform);
        await createdFlowerObj.GetComponent<SampleFlowerObj>().Initialize();
    }
}