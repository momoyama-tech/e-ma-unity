using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EmaCreator : MonoBehaviour
{
    private int _id;// EmaのID、全てのEmaにユニークなIDを付与するために使用
    private SpriteChanger _spriteChanger;
    [SerializeField] private GameObject _emaPrefab;

    public void ManualStart()
    {
        _id = 0;
        _spriteChanger = GetComponent<SpriteChanger>();
        if (_spriteChanger == null)
        {
            Debugger.RefactLog("SpriteChanger component is missing on this GameObject.");
        }
    }

    public void ManualUpdate()
    {
        foreach (var ema in GetComponentsInChildren<EmaMove>())
        {
            ema.GetComponent<EmaMove>().ManualUpdate();
        }
    }

    /// <summary>
    /// Emaを生成するメソッド
    /// flowerUrl, nameUrl, wishUrlの3つのURLを受け取り、EmaPrefabをインスタンス化し、スプライトを設定
    /// EmaPrefabの子オブジェクトにスプライトを設定する
    /// EmaPrefabのスプライトはSpriteChangerを使用して取得
    /// </summary>
    /// <param name="flowerUrl"></param>
    /// <param name="nameUrl"></param>
    /// <param name="wishUrl"></param>
    /// <returns></returns>
    public async UniTask CreateEma(string flowerUrl, string nameUrl, string wishUrl)
    {
        if (_emaPrefab == null)
        {
            Debugger.RefactLog("EmaPrefab is not assigned in the inspector.");
            return;
        }

        // EmaPrefabをインスタンス化
        var emaObj = Instantiate(_emaPrefab, gameObject.transform);

        // SpriteChanger のインスタンスを複製して初期化
        var spriteChangerInstance = Instantiate(_spriteChanger);
        await spriteChangerInstance.Initialize(flowerUrl, nameUrl, wishUrl);

        // スプライトを設定
        emaObj.GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetFlowerSprite();
        emaObj.transform.Find("Name").GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetNameSprite();
        emaObj.transform.Find("Wish").GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetWishSprite();

        // Ema の初期化
        emaObj.GetComponent<Ema>().Initialize(_id, spriteChangerInstance.GetFlowerSprite(), spriteChangerInstance.GetNameSprite(), spriteChangerInstance.GetWishSprite());

        // EmaMove の初期化
        await emaObj.GetComponent<EmaMove>().Initialize();
        Debugger.RefactLog($"EmaMoveの初期化完了 for ID: {_id}");

        _id++;
    }
}