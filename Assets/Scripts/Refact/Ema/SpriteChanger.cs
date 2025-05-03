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

    private async UniTask SetFlowerInfo(string flowerUrl)
    {
        // flowerの画像を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(flowerUrl);
        await www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        Debugger.RefactLog("flower sprite is set");
        Debug.Log(_flowerSprite);
    }

    private async UniTask SetNameImage(string nameUrl)
    {
        // flowerの名前を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(nameUrl);
        await www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _nameSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        Debugger.RefactLog("name sprite is set");
        Debug.Log(_nameSprite);
    }

    private async UniTask SetWishImage(string wishUrl)
    {
        // flowerの願いを_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(wishUrl);
        await www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _wishSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        Debugger.RefactLog("wish sprite is set");
        Debug.Log(_wishSprite);
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