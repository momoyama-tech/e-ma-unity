using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SampleFlowerCreator : MonoBehaviour
{
    private GameObject[] _flowerObjList;
    [SerializeField] private GameObject _flowerObj;
    [SerializeField] private int _flowerObjNum = 26;
    public async UniTask ManualStart()
    {
        if (_flowerObj == null)
        {
            Debug.LogError("Error: _flowerObj is not assigned in the inspector.");
            return;
        }
        else
        {
            Debug.Log("FlowerObj is assigned in the inspector.");
        }

        _flowerObjList = new GameObject[_flowerObjNum];
        for (int i = 0; i < _flowerObjNum; i++)
        {
            Debugger.Log("FlowerObjNumber " + i + " is created");
            _flowerObjList[i] = Instantiate(_flowerObj, this.gameObject.transform);
            _flowerObjList[i].transform.localPosition = new Vector3(0, 0, 0);
            _flowerObjList[i].transform.localScale = new Vector3(1f, 1f, 1f);
            // createdFlowerObj.GetComponent<SampleFlowerObj>().Initialize();
            _flowerObjList[i].GetComponent<FlowerMove>().Initialize();
            await UniTask.Delay(2000);
        }
    }

    public void ManualUpdate()
    {
        foreach (var flowerObj in _flowerObjList)
        {
            flowerObj.GetComponent<FlowerMove>().ManualUpdate();
        }
    }
}