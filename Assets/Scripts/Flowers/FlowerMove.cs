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
    private float _time = 0f; // 横方向の移動時間
    private Vector3 _pos; // 開始位置
    private bool _isRotation = false; // 回転フラグ

    public async UniTask Initialize()
    {
        Debugger.Log(_speed.ToString());
        // 移動時間を計算
        _time = Math.Abs(_endPosX / _speed);
        // 親コンポーネントを取得
        var parent = this.gameObject.transform.parent;
        // 子コンポーネントの数が奇数なら_isOddNumber = trueにする
        if (parent != null)
        {
            int childCount = parent.childCount;
            if (childCount % 2 == 1)
            {
                _isOddNumber = true;
            }
        }

        // 奇数番目なら座標を反転
        if (_isOddNumber)
        {
            _endPosX *= -1f;
            _centerPosX *= -1f;
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
        // 現在地を端として楕円に回転移動
        _pos = gameObject.transform.position;
        _pos.x = Mathf.Sin(Time.time * _speed) * 150f;
        _pos.y = Mathf.Cos(Time.time * _speed) * 100f;
        transform.position = _pos;
    }
}