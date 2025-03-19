using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.UI;

public class FlowerCreator : MonoBehaviour
{
    [SerializeField] private GameObject flowerPrefab;// 生成するflowerのPrefab
    [SerializeField] private Transform flowerParent;// flowerの親オブジェクト
    private Sprite flowerSprite;// 画像を設定するSprite

    private string url;

    void Start()
    {
        url = "https://appmedia.jp/wp-content/uploads/2019/11/95b3429781493584eb1a4ed0d7360c8e.jpg";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CreateFlower();
        }
    }

    /// <summary>
    /// flowerを生成する
    /// </summary>
    void CreateFlower()
    {
        GameObject flower = Instantiate(flowerPrefab, flowerParent);
        flower.transform.position = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        // flowerの画像をurlから取得して設定する
        StartCoroutine(SetFlowerImage(flower));
    }

    /// <summary>
    /// flowerの画像をurlから取得して設定する
    /// </summary>
    /// <param name="flower"></param>
    /// <returns></returns>
    private IEnumerator SetFlowerImage(GameObject flower)
    {
        // flowerの画像をurlから取得して設定する
        Debug.Log("ImageLoadStart");
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log("ImageLoadERROR:" + www.error);
        }
        else
        {
            // urlから取得した画像をSpriteに変換
            Debug.Log("www: " + www);
            Debug.Log("www.downloadHandler: " + www.downloadHandler);
            Debug.Log("www.downloadHandler.texture: " + ((DownloadHandlerTexture)www.downloadHandler).texture);

            Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
            flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            // flowerのSpriteRendererに設定する
            flower.GetComponent<SpriteRenderer>().sprite = flowerSprite;

            Debug.Log("Success");
        }
    }
}