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
    private bool _isStarted = false;
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
            _flowerObjList[i].GetComponent<SampleFlowerObj>().Initialize();
            // _flowerObjList[i].GetComponent<FlowerMove>().Initialize();
            await UniTask.Delay(2000);
        }
        _isStarted = true;
    }

    public void ManualUpdate()
    {
        if (_isStarted)
        {
            if (_flowerObjList == null || _flowerObjList.Length == 0)
            {
                Debug.LogError("Error: _flowerObjList is not initialized or empty.");
                return;
            }

            var i = 0;

            foreach (var flowerObj in _flowerObjList)
            {
                if (flowerObj == null)
                {
                    Debug.LogError("Error: One of the flower objects in _flowerObjList is null.");
                    Debugger.Log($"Flower object {i} is null.");
                    continue;
                }

                var flowerMove = flowerObj.GetComponent<FlowerMove>();
                if (flowerMove == null)
                {
                    Debug.LogError($"Error: FlowerMove component is missing on {flowerObj.name}.");
                    continue;
                }

                Debugger.Log("ManualUpdate Success");

                flowerMove.ManualUpdate();
                i++;
            }
        }
    }
}