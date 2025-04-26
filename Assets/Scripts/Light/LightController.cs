using UnityEngine;
using System.Collections.Generic;
public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject _downLights;// 下からのライト
    [SerializeField] private GameObject _upLights;// 上からのライト
    [SerializeField] private GameObject _midiumLights;// 正面からのライト
    List<GameObject> _downLightList = new List<GameObject>();
    List<GameObject> _upLightList = new List<GameObject>();
    List<GameObject> _midiumLightList = new List<GameObject>();

    public void ManualStart()
    {
        // DownLightsの子オブジェクトを取得
        foreach (Transform child in _downLights.transform)
        {
            _downLightList.Add(child.gameObject);
        }

        // UpLightsの子オブジェクトを取得
        foreach (Transform child in _upLights.transform)
        {
            _upLightList.Add(child.gameObject);
        }

        // MidiumLightsの子オブジェクトを取得
        foreach (Transform child in _midiumLights.transform)
        {
            _midiumLightList.Add(child.gameObject);
        }
    }
    public void ManualUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            DownLightUp();
            Debug.Log("LightUp");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            DownLightDown();
            Debug.Log("LightDown");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            UpLightUp();
            Debug.Log("UpLightUp");
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            UpLightDown();
            Debug.Log("UpLightDown");
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            MidiumLightUp();
            Debug.Log("MidiumLightUp");
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            MidiumLightDown();
            Debug.Log("MidiumLightDown");
        }
    }
    private void DownLightUp()
    {
        foreach (var light in _downLightList)
        {
            light.SetActive(true);
        }
    }

    private void DownLightDown()
    {
        foreach (var light in _downLightList)
        {
            light.SetActive(false);
        }
    }

    private void UpLightUp()
    {
        foreach (var light in _upLightList)
        {
            light.SetActive(true);
        }
    }

    private void UpLightDown()
    {
        foreach (var light in _upLightList)
        {
            light.SetActive(false);
        }
    }

    private void MidiumLightUp()
    {
        foreach (var light in _midiumLightList)
        {
            light.SetActive(true);
        }
    }

    private void MidiumLightDown()
    {
        foreach (var light in _midiumLightList)
        {
            light.SetActive(false);
        }
    }
}