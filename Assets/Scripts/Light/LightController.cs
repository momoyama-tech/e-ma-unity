using UnityEngine;
using System.Collections.Generic;
public class LightController : MonoBehaviour
{
    [SerializeField] private GameObject _downLights;
    [SerializeField] private GameObject _upLights;
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
    }
    private void DownLightUp()
    {
        // ライトの子オブジェクトを取得
        List<GameObject> lights = new List<GameObject>();
        // 全てのライトをオンにする
        foreach (Transform child in _downLights.transform)
        {
            if (child.gameObject.activeSelf)
            {
                lights.Add(child.gameObject);
            }
        }
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
    }

    private void DownLightDown()
    {
        // ライトの子オブジェクトを取得
        List<GameObject> lights = new List<GameObject>();
        // 全てのライトをオフにする
        foreach (Transform child in _downLights.transform)
        {
            if (child.gameObject.activeSelf)
            {
                lights.Add(child.gameObject);
            }
        }
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

    private void UpLightUp()
    {
        // ライトの子オブジェクトを取得
        List<GameObject> lights = new List<GameObject>();
        // 全てのライトをオンにする
        foreach (Transform child in _upLights.transform)
        {
            if (child.gameObject.activeSelf)
            {
                lights.Add(child.gameObject);
            }
        }
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }
    }

    private void UpLightDown()
    {
        // ライトの子オブジェクトを取得
        List<GameObject> lights = new List<GameObject>();
        // 全てのライトをオフにする
        foreach (Transform child in _upLights.transform)
        {
            if (child.gameObject.activeSelf)
            {
                lights.Add(child.gameObject);
            }
        }
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }
}