using UnityEngine;

public class CreateController : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.A))
            ShowPrefab(0);
        if (Input.GetKeyDown(KeyCode.S))
            ShowPrefab(1);
        if (Input.GetKeyDown(KeyCode.D))
            ShowPrefab(2);
        if (Input.GetKeyDown(KeyCode.F))
            ShowPrefab(3);
        if (Input.GetKeyDown(KeyCode.G))
            ShowPrefab(4);
        if (Input.GetKeyDown(KeyCode.H))
        {
            for (int i = 0; i < 5; i++)
            {
                ShowPrefab(1);
                ShowPrefab(2);
                ShowPrefab(3);
                ShowPrefab(4);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
            ShowPrefab(6);
        if (Input.GetKeyDown(KeyCode.K))
            ShowPrefab(7);
        if (Input.GetKeyDown(KeyCode.L))
            ShowPrefab(8);
    }

    private void ShowPrefab(int index)
    {
        try
        {
            Debug.Log(_creators[index].name);
            _creators[index].GetComponent<Creator>().Create();
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error creating prefab: {e.Message}");
        }
    }
}