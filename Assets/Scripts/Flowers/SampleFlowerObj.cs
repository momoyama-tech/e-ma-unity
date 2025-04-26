using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.Networking;

public class SampleFlowerObj : MonoBehaviour
{
    [SerializeField] private int _totalStemNum;// 茎画像の数
    [SerializeField] private float _flowerScale = 1f;// // 花の大きさ
    [SerializeField] private GameObject[] _flowerElements;// 花の要素(子葉の方から0番目)
    private GameObject _parentFlowerElement;
    private GameObject _selectedFlowerElement;
    private Sprite _flowerSprite;// 画像を設定するSprite
    private string _flowerUrl = "";// 画像のURL
    private string _nameUrl = "";// 名前のURL
    private string _wishUrl = "";// 願いのURL
    private int _cotyledonNum = 1;
    private int _stemNum = 2;
    private int _bloomingNum = 3;
    private int _id = 0;

    public async UniTask Initialize(string flowerUrl, string nameUrl, string wishUrl, int id)
    {
        _flowerUrl = flowerUrl;
        _nameUrl = nameUrl;
        _wishUrl = wishUrl;
        _id = id;

        _parentFlowerElement = this.gameObject;
        gameObject.GetComponent<FlowerMove>().Initialize();
    }

    /// <summary>
    /// 花要素のどれを生成するか決定
    /// </summary>
    /// <param name="num"></param>
    private void SelectFlowerElements(int num)
    {
        switch (num)
        {
            case 1:
                _selectedFlowerElement = _flowerElements[0];
                break;
            case 2:
                _selectedFlowerElement = _flowerElements[1];
                break;
            case 3:
                _selectedFlowerElement = _flowerElements[2];
                break;
        }
    }

    /// <summary>
    ///  花の要素を生成
    ///  子葉 -> 茎 -> 花
    ///  要素は小さい状態で生成して徐々に大きくしていく
    /// </summary>
    /// <returns></returns>
    private async UniTask CreateFlowerElements(Sprite sprite)
    {
        // 子葉を生成
        SelectFlowerElements(_cotyledonNum);// 生成するオブジェクトを子葉に変更
        var cotyledon = Instantiate(_selectedFlowerElement, _parentFlowerElement.transform);// 子葉を生成

        // cotyledonの大きさを変更
        cotyledon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        cotyledon.transform.DOScale(new Vector3(1f * _flowerScale, 1f * _flowerScale, 0.7f * _flowerScale), 3f);

        await UniTask.Delay(2000);

        // 茎が伸びる
        // 茎の数だけ繰り返す
        // 茎の数はインスペクターで設定
        SelectFlowerElements(1);
        for (int i = 0; i < _totalStemNum; i++)
        {
            SelectFlowerElements(_stemNum);// 生成するオブジェクトを茎に変更
            var stem = Instantiate(_selectedFlowerElement, _parentFlowerElement.transform);// 茎を生成

            // 生成位置を子葉の位置から少し上にずらす
            // 徐々に大きくしながらy軸方向に座標移動
            stem.transform.position = cotyledon.transform.position + new Vector3(0, 5f * i * _flowerScale, -0.01f * (i + 1));
            stem.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            stem.transform.DOScale(new Vector3(1f * _flowerScale, 1f * _flowerScale, 0.7f * _flowerScale), 2f);
            stem.transform.DOMoveY(5f * (i + 1) * _flowerScale, 2f);

            await UniTask.Delay(1000);
        }
        await UniTask.Delay(2000);

        // 花が咲く
        SelectFlowerElements(_bloomingNum);// 生成するオブジェクトを花に変更
        var blooming = Instantiate(_selectedFlowerElement, _parentFlowerElement.transform);// 花を生成

        blooming.GetComponent<SpriteRenderer>().sprite = sprite;

        // 生成位置を茎の位置から少し上にずらす
        // 徐々に大きくしながらy軸方向に座標移動
        blooming.transform.position = cotyledon.transform.position + new Vector3(0, 5f * _totalStemNum * _flowerScale, -0.01f * (_totalStemNum + 1));
        blooming.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        blooming.transform.DOScale(new Vector3(1f * _flowerScale, 1f * _flowerScale, 1f * _flowerScale), 5f);
        blooming.transform.DOMoveY(5f * (_totalStemNum + 1) * _flowerScale, 5f);

        await UniTask.Delay(5000);

        // 不要なTweenを削除
        DOTween.KillAll();
    }

    /// <summary>
    /// flowerの画像を_urlから取得して設定する
    /// </summary>
    /// <param name="flower"></param>
    /// <returns></returns>
    private IEnumerator SetFlowerImage(GameObject flower)
    {
        // flowerの画像を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_flowerUrl);
        yield return www.SendWebRequest();
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        // flowerのSpriteRendererに設定する
        flower.GetComponent<SpriteRenderer>().sprite = _flowerSprite;
    }

    private IEnumerator SetNameImage(GameObject flower)
    {
        // flowerの名前を_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_nameUrl);
        yield return www.SendWebRequest();

        // _urlから取得した画像をSpriteに変換
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        // flowerのSpriteRendererに設定する
        flower.GetComponent<SpriteRenderer>().sprite = _flowerSprite;
    }

    private IEnumerator SetWishImage(GameObject flower)
    {
        // flowerの願いを_urlから取得して設定する
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(_wishUrl);
        yield return www.SendWebRequest();
        // _urlから取得した画像をSpriteに変換
        Texture2D tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        _flowerSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
        // flowerのSpriteRendererに設定する
        flower.GetComponent<SpriteRenderer>().sprite = _flowerSprite;
    }

    public void SetFlowerInfo(string flowerUrl, string nameUrl, string wishUrl)
    {
        Debug.Log("花情報を設定");
        _flowerUrl = flowerUrl;
        _nameUrl = nameUrl;
        _wishUrl = wishUrl;

        // 花の要素を生成
        StartCoroutine(SetFlowerImage(gameObject));
        StartCoroutine(SetNameImage(gameObject.transform.Find("Name").gameObject));
        StartCoroutine(SetWishImage(gameObject.transform.Find("Wish").gameObject));
    }

    public int GetId()
    {
        return _id;
    }
}