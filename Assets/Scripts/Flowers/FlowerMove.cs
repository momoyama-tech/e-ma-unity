using UnityEngine;
using System;
using DG.Tweening;
using Cysharp.Threading.Tasks;

public class FlowerMove : MonoBehaviour
{
    [SerializeField] private float _speed; // 移動速度
    private float _endPosX = 400.0f; // 端の座標
    private float _centerPosX = 50.0f; // 中央の座標
    private bool _isOddNumber = false;
    private int _rotateDirection = 1; // 回転方向
    private float _time = 0f; // 横方向の移動時間
    private Vector3 _pos; // 開始位置
    private bool _isRotation = false; // 回転フラグ
    private Vector3 _center; // 中心座標

    public async UniTask Initialize()
    {
        Debugger.Log(_speed.ToString());
        // 移動時間を計算
        _time = Math.Abs(_endPosX / _speed);
        // 親コンポーネントを取得
        var parent = this.gameObject.transform.parent;
        
        if(gameObject.GetComponent<SampleFlowerObj>().GetId() % 2 == 0)
        {
            _isOddNumber = true;
        }
        else
        {
            _isOddNumber = false;
        }

        if(_isOddNumber)
        {
            _center = new Vector3(-200, 0, 0);
            _endPosX *= -1;
            _rotateDirection = -1;
        }
        else
        {
            _center = new Vector3(200, 0, 0);
            _rotateDirection = 1;
        }

        Debugger.Log("初期化終了");

        await Move();
        Debugger.Log("移動完了03");
    }

    public void ManualUpdate()
    {
        Debugger.Log("ManualUpdate");
        if(_isRotation)
        {
            Debugger.Log("回転開始");
            Rotation();
        }
    }

    /// <summary>
    /// 1周目の単純横方向移動
    /// </summary>
    private async UniTask Move()
    {
        Debugger.Log("移動開始");
        // 右に移動
        Debugger.Log(_speed.ToString());
        Debugger.Log((_endPosX / _speed).ToString());
        await gameObject.transform.DOMoveX(_endPosX, _time).SetEase(Ease.Linear).AsyncWaitForCompletion();

        _speed = _speed * 0.01f;
        _isRotation = true;
    }

    private void Rotation()
    {
        Debug.Log(gameObject.GetComponent<SampleFlowerObj>().GetId().ToString());
        _pos = _center; // 中心を基準に計算
        _pos.x += _rotateDirection * Mathf.Sin((Time.time * _speed) - (gameObject.GetComponent<SampleFlowerObj>().GetId() / 26f) * 6.3f) * 150f; // x軸方向の楕円運動
        _pos.y += Mathf.Cos((Time.time * _speed) - (gameObject.GetComponent<SampleFlowerObj>().GetId() / 26f) * 6.3f) * 50f; // y軸方向の楕円運動
        transform.position = _pos;
    }
}