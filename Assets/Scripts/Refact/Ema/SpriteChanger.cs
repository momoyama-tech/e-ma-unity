using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpriteChanger : MonoBehaviour
{
    private Sprite _flowerSprite;
    private Sprite _nameSprite;
    private Sprite _wishSprite;

    public void Initialize(string flowerUrl, string nameUrl, string wishUrl)
    {
        StartCoroutine(SetFlowerInfo(flowerUrl));
        StartCoroutine(SetNameImage(nameUrl));
        StartCoroutine(SetWishImage(wishUrl));
    }

    private IEnumerator SetFlowerInfo(string flowerUrl)
    {
        // flowerの画像を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(flowerUrl);
        yield return www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    private IEnumerator SetNameImage(string nameUrl)
    {
        // flowerの名前を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(nameUrl);
        yield return www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _nameSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    private IEnumerator SetWishImage(string wishUrl)
    {
        // flowerの願いを_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(wishUrl);
        yield return www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _wishSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    public Sprite GetFlowerSprite()
    {
        return _flowerSprite;
    }
    public Sprite GetNameSprite()
    {
        return _nameSprite;
    }
    public Sprite GetWishSprite()
    {
        return _wishSprite;
    }
}