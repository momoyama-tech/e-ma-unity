using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SampleFlowerCreator : MonoBehaviour
{
    private GameObject[] _flowerObjList;// / 花オブジェクトのリスト
    [SerializeField] private GameObject _flowerObj;// / 花オブジェクト
    [SerializeField] private int _flowerObjNum = 26;// リストに格納する花の数
    private bool _isStarted = false;// Updateを呼ぶかどうかのフラグ
    private int _completeFlowerListNum = 0;// 完成した花の数
    public async UniTask ManualStart()
    {
        // フラワーオブジェクトがnullかどうかを判定
        if (_flowerObj == null)
        {
            Debug.LogError("Error: _flowerObj is not assigned in the inspector.");
            return;
        }
        else
        {
            Debug.Log("FlowerObj is assigned in the inspector.");
        }

        // フラワーオブジェクトのリストを初期化
        // リストにテンプレを格納
        _flowerObjList = new GameObject[_flowerObjNum];
        for (int i = 0; i < _flowerObjNum; i++)
        {
            // Debugger.Log("FlowerObjNumber " + i + " is created");
            _flowerObjList[i] = Instantiate(_flowerObj, this.gameObject.transform);
            _flowerObjList[i].transform.localPosition = new Vector3(0, 0, -0.1f);
            _flowerObjList[i].GetComponent<SampleFlowerObj>().Initialize(null, null, null, i);
            // _flowerObjList[i].GetComponent<FlowerMove>().Initialize();
            await UniTask.Delay(100);
        }
        _isStarted = true;
    }

    public void ManualUpdate()
    {
        if (_isStarted)
        {
            foreach (var flowerObj in _flowerObjList)
            {
                flowerObj.GetComponent<FlowerMove>().ManualUpdate();
            }
        }
    }

    public void SetFlowerInfo(string flowerUrl, string nameUrl, string wishUrl)
    {
        _flowerObjList[_completeFlowerListNum].GetComponent<SampleFlowerObj>().SetFlowerInfo(flowerUrl, nameUrl, wishUrl);
        _completeFlowerListNum++;
    }
}