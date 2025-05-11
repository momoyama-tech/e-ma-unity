using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class EmaCreator : MonoBehaviour
{
    private int _newEmaID;// EmaのID、全てのEmaにユニークなIDを付与するために使用
    private int _rebornEmaID;// EmaのID、再生時に使用
    private SpriteChanger _spriteChanger;
    [SerializeField] private int _maxEmaCount = 22; // Emaの最大数
    [SerializeField] private GameObject _emaPrefab;
    private GameObject[] _emaObjects;

    public void ManualStart()
    {
        _newEmaID = 0;
        _rebornEmaID = 0;
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
        var spriteChangerInstance = emaObj.AddComponent<SpriteChanger>();
        await spriteChangerInstance.Initialize(flowerUrl, nameUrl, wishUrl);

        // スプライトを設定
        emaObj.GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetFlowerSprite();
        emaObj.transform.Find("Name").GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetNameSprite();
        emaObj.transform.Find("Wish").GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetWishSprite();

        // Ema の初期化
        emaObj.GetComponent<Ema>().Initialize(_newEmaID, spriteChangerInstance.GetFlowerSprite(), spriteChangerInstance.GetNameSprite(), spriteChangerInstance.GetWishSprite());

        // EmaMove の初期化
        await emaObj.GetComponent<EmaMove>().Initialize(false);
        Debugger.RefactLog($"EmaMoveの初期化完了 for ID: {_newEmaID}");

        _newEmaID++;
    }

    public async UniTask RebornEma(string flowerUrl, string nameUrl, string wishUrl)
    {
        if (_emaPrefab == null)
        {
            Debugger.RefactLog("EmaPrefab is not assigned in the inspector.");
            return;
        }

        DecideOverrideID();

        // EmaPrefabをインスタンス化
        var emaObj = Instantiate(_emaPrefab, gameObject.transform);

        // SpriteChanger のインスタンスを複製して初期化
        var spriteChangerInstance = emaObj.AddComponent<SpriteChanger>();
        await spriteChangerInstance.Initialize(flowerUrl, nameUrl, wishUrl);

        // スプライトを設定
        emaObj.GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetFlowerSprite();
        emaObj.transform.Find("Name").GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetNameSprite();
        emaObj.transform.Find("Wish").GetComponent<SpriteRenderer>().sprite = spriteChangerInstance.GetWishSprite();

        // Ema の初期化
        emaObj.GetComponent<Ema>().Initialize(_rebornEmaID, spriteChangerInstance.GetFlowerSprite(), spriteChangerInstance.GetNameSprite(), spriteChangerInstance.GetWishSprite());

        // EmaMove の初期化
        await emaObj.GetComponent<EmaMove>().Initialize(true);
        Debugger.RefactLog($"EmaMoveの初期化完了 for ID: {_rebornEmaID}");

        _rebornEmaID++;
    }

    public async UniTask RebornEma(Sprite flowerSprite, Sprite nameSprite, Sprite wishSprite)
    {
        if (_emaPrefab == null)
        {
            Debugger.RefactLog("EmaPrefab is not assigned in the inspector.");
            return;
        }

        DecideOverrideID();

        // EmaPrefabをインスタンス化
        var emaObj = Instantiate(_emaPrefab, gameObject.transform);

        // スプライトを設定
        emaObj.GetComponent<SpriteRenderer>().sprite = flowerSprite;
        emaObj.transform.Find("Name").GetComponent<SpriteRenderer>().sprite = nameSprite;
        emaObj.transform.Find("Wish").GetComponent<SpriteRenderer>().sprite = wishSprite;

        // Ema の初期化
        emaObj.GetComponent<Ema>().Initialize(_rebornEmaID, flowerSprite, nameSprite, wishSprite);

        // EmaMove の初期化
        await emaObj.GetComponent<EmaMove>().Initialize(true);
        Debugger.RefactLog($"EmaMoveの初期化完了 for ID: {_rebornEmaID}");

        _rebornEmaID++;
    }

    /// <summary>
    /// 上書きする絵馬のIDを決定するメソッド
    /// _rebornEmaIDの値を確認して、上書きするIDを決定
    /// _rebornEmaIDが上限を超えた場合、0に戻す
    /// 上限を超えていなければ、_currentEmaIDに_rebornEmaIDを代入
    /// 上限を超えた場合、_currentEmaIDに0を代入
    /// このオブジェクトの子コンポーネントであるEmaオブジェクトを全探索し、_currentEmaIDと一致するIDを持つEmaオブジェクトを探す
    /// 一致するEmaオブジェクトが見つかった場合、そのEmaオブジェクトのSpriteを上書きする
    /// </summary>
    private void DecideOverrideID()
    {
        if (_rebornEmaID >= _maxEmaCount)
        {
            Debugger.RefactLog($"上書きする絵馬のIDが上限を超えました: {_rebornEmaID}");
            _rebornEmaID = 0;
        }

        foreach (var ema in GetComponentsInChildren<Ema>())
        {
            if (ema.GetId() == _rebornEmaID)
            {
                Debugger.RefactLog($"上書きする絵馬のIDが見つかりました: {_rebornEmaID}");
                return;
            }
        }
        Debugger.RefactLog($"上書きする絵馬のIDが見つかりませんでした: {_rebornEmaID}");
    }
}