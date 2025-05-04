using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SpriteChanger : MonoBehaviour
{
    private Sprite _flowerSprite;
    private Sprite _nameSprite;
    private Sprite _wishSprite;

    public async UniTask Initialize(string flowerUrl, string nameUrl, string wishUrl)
    {
        await SetFlowerInfo(flowerUrl);
        await SetNameImage(nameUrl);
        await SetWishImage(wishUrl);
    }

    /// <summary>
    /// サーバーから取得したurlを元にflowerの画像を取得し、スプライトを設定する
    /// </summary>
    /// <param name="flowerUrl"></param>
    /// <returns></returns>
    private async UniTask SetFlowerInfo(string flowerUrl)
    {
        // flowerの画像を_urlから取得して設定する
        if (string.IsNullOrEmpty(flowerUrl))
        {
            Debug.LogError("SetFlowerInfo に渡された URL が null または空です。");
            return;
        }
        Debug.Log("ダウンロード対象のURL: " + flowerUrl);
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(flowerUrl);
        await www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("画像のダウンロードに失敗しました: " + www.error);
            return;
        }

        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    /// <summary>
    /// flowerの名前をサーバーから取得したurlを元に取得し、スプライトを設定する
    /// </summary>
    /// <param name="nameUrl"></param>
    /// <returns></returns>
    private async UniTask SetNameImage(string nameUrl)
    {
        // flowerの名前を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(nameUrl);
        await www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _nameSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
    }

    /// <summary>
    /// flowerの願いをサーバーから取得したurlを元に取得し、スプライトを設定する
    /// </summary>
    /// <param name="wishUrl"></param>
    /// <returns></returns>
    private async UniTask SetWishImage(string wishUrl)
    {
        // flowerの願いを_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(wishUrl);
        await www.SendWebRequest();
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