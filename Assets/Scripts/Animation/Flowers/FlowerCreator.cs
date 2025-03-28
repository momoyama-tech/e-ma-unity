using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class FlowerCreator : MonoBehaviour
{
    [SerializeField] private GameObject _flowerPrefab;// 生成するflowerのPrefab
    [SerializeField] private Transform _flowerParent;// flowerの親オブジェクト
    private Sprite _flowerSprite;// 画像を設定するSprite
    private string _url = "";// 画像のURL

    [SerializeField] private GameObject _actionCableClientObject;// ActionCableClientを持つオブジェクト
    private ActionCableClient _actionCableClient;

    void Start()
    {
        _actionCableClient = _actionCableClientObject.GetComponent<ActionCableClient>();
        if (_actionCableClient == null)
        {
            Debug.LogError("ActionCableClient is null");
        }
        else
        {
            Debug.Log("ActionCableClient is not null");
        }
    }

    /// <summary>
    /// スペースキーが押された時、花のURLをサーバーから取得できていればflowerを生成するメソッドをよ出す
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("FlowerURL: " + _actionCableClient.GetFlowerUrl());
            if (_actionCableClient.GetFlowerUrl() != null)
            {
                _url = _actionCableClient.GetFlowerUrl();
                CreateFlower();
            }
            else
            {
                Debug.LogError("FlowerURL is null");
            }
        }
    }

    /// <summary>
    /// flowerを生成する
    /// </summary>
    void CreateFlower()
    {
        GameObject flower = Instantiate(_flowerPrefab, _flowerParent);
        flower.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        // flowerの画像を_urlから取得して設定する
        StartCoroutine(SetFlowerImage(flower));
    }

    /// <summary>
    /// flowerの画像を_urlから取得して設定する
    /// </summary>
    /// <param name="flower"></param>
    /// <returns></returns>
    private IEnumerator SetFlowerImage(GameObject flower)
    {
        // flowerの画像を_urlから取得して設定する
        Debug.Log("ImageLoadStart");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageLoadERROR:" + www.error);
        }
        else
        {
            // _urlから取得した画像をSpriteに変換
            Debug.Log("www: " + www);
            Debug.Log("www.downloadHandler: " + www.downloadHandler);
            Debug.Log("www.downloadHandler.texture: " + ((DownloadHandlerTexture)www.downloadHandler).texture);

            Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            // flowerのSpriteRendererに設定する
            flower.GetComponent<SpriteRenderer>().sprite = _flowerSprite;

            Debug.Log("Success");
        }
    }
}