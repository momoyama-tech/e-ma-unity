using UnityEngine;

public class EffectCreateController : MonoBehaviour
{
    private GameObject[] _creators;
    public void ManualStart()
    {
        // このオブジェクトの子オブジェクトを_creatorsに格納
        _creators = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _creators[i] = transform.GetChild(i).gameObject;
        }

        foreach (var creator in _creators)
        {
            creator.GetComponent<Creator>().ManualStart();
        }
    }

    public void ManualUpdate()
    {
        // 数字の1キーから9キーまでの入力を受け付けて、対応するPrefabを生成
        if(Input.GetKeyDown(KeyCode.Q))
            ShowPrefab(0);  
        if(Input.GetKeyDown(KeyCode.W))
            ShowPrefab(1);
        if(Input.GetKeyDown(KeyCode.E))
            ShowPrefab(2);
        if(Input.GetKeyDown(KeyCode.R))
            ShowPrefab(3);
        if(Input.GetKeyDown(KeyCode.T))
            ShowPrefab(4);
        if(Input.GetKeyDown(KeyCode.Y))
            ShowPrefab(5);
        if(Input.GetKeyDown(KeyCode.U))
            ShowPrefab(6);
        if(Input.GetKeyDown(KeyCode.Alpha8))
            ShowPrefab(7);
        if(Input.GetKeyDown(KeyCode.Alpha9))
            ShowPrefab(8);
    }

    private void ShowPrefab(int index)
    {
        Debug.Log(_creators[index].name);
        try
        {
            _creators[index].GetComponent<Creator>().Create();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error creating prefab: {e.Message}");
        }
    }
}