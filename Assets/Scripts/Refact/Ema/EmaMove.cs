using UnityEngine;
using System;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class EmaMove : MonoBehaviour
{
    [SerializeField] private float _speed; // 移動速度
    private float _endPosX = 400.0f; // 端の座標
    private bool _isOddNumber = false;
    private int _rotateDirection = 1; // 回転方向
    private float _time = 0f; // 横方向の移動時間
    private Vector3 _pos; // 開始位置
    private bool _isRotation = false; // 回転フラグ
    private Vector3 _center; // 中心座標
    private Ema _ema;

    public async UniTask Initialize(bool isRotate)
    {
        _isRotation = isRotate;
        Debugger.RefactLog("EmaMoveの初期化開始");

        // 移動時間を計算
        _time = Math.Abs(_endPosX / _speed);

        // 親コンポーネントを取得
        _ema = gameObject.GetComponent<Ema>();

        _isOddNumber = UnityEngine.Random.value > 0.5f;

        // 奇数番目なら左から右にすすむ
        // 偶数番目なら右から左に進む
        if (_isOddNumber)
            _endPosX *= -1;

        Debugger.RefactLog("初期化終了");

        if(!_isRotation)
        {
            await Move();
        }

        Debugger.RefactLog("移動完了03");
    }

    /// <summary>
    /// まずは移動を行い、次に回転を行う
    /// 回転はUpdateで行う
    /// </summary>
    public void ManualUpdate()
    {
        if(_isRotation)
        {
            Rotation();
        }
    }

    /// <summary>
    /// 1周目の単純横方向移動
    /// </summary>
    private async UniTask Move()
    {
        await gameObject.transform.DOMoveX(_endPosX, _time).SetEase(Ease.Linear).AsyncWaitForCompletion();

        // 輪廻するオブジェクトに上書き設定する
        // 親コンポーネントが持つEmaCreator.csのRebornEmaメソッドを呼び出す
        gameObject.transform.parent.GetComponent<EmaCreator>().RebornEma(_ema.GetFlowerSprite(), _ema.GetNameSprite(), _ema.GetWishSprite());
        Destroy(gameObject);
    }

    /// <summary>
    /// 転生後の回転移動
    /// </summary
    private void Rotation()
    {
        CheckIsRight();
        _pos = _center; // 中心を基準に計算
        _pos.x += _rotateDirection * Mathf.Sin((Time.time * _speed * 0.01f) - (gameObject.GetComponent<Ema>().GetId() / 26f) * 59.4f) * 150f; // x軸方向の楕円運動
        _pos.y += Mathf.Cos((Time.time * _speed * 0.01f) - (gameObject.GetComponent<Ema>().GetId() / 26f) * 59.4f) * 40f; // y軸方向の楕円運動
        transform.position = _pos;
        if(_pos.y < -30)
        {
            // _emaQueue.EnQueue(_ema.GetId(), _isOddNumber); // キューから削除
            // gameObject.SetActive(false);
            Debugger.RefactLog("絵馬がy=-30以下になったので削除");
            // Destroy(gameObject);
        }
    }

    /// <summary>
    /// 右か左かを判定する
    /// 回転の中心を決定する
    /// </summary>
    private void CheckIsRight()
    {
        if(_ema.GetIsRight())
        {
            _center = new Vector3(-200, -10, 0);
            _rotateDirection = -1;
        }
        else
        {
            _center = new Vector3(200, -10, 0);
            _rotateDirection = 1;
        }
    }
}