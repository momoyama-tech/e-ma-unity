using UnityEngine;

public class CreateController : MonoBehaviour
{
    private GameObject[] _creators;
    void Start()
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
            ShowPrefab(0);
        if(Input.GetKeyDown(KeyCode.S))
            ShowPrefab(1);
        if(Input.GetKeyDown(KeyCode.D))
            ShowPrefab(2);
        if(Input.GetKeyDown(KeyCode.F))
            ShowPrefab(3);
    }

    private void ShowPrefab(int index)
    {
        Debug.Log(_creators[index].name);
        _creators[index].GetComponent<Creator>().Create();
    }
}