using UnityEngine;
using System;
using DG.Tweening;

public class FlowerMove : MonoBehaviour
{
    [SerializeField] private float _speed; // 移動速度
    private float _endPosX = 400.0f; // 端の座標
    private float _centerPosX = 50.0f;// 中央の座標
    private bool _isOddNumber = false;
    private float _time = 0f;// 横方向の移動時間

    public void Initialize()
    {
        Debugger.Log(_speed.ToString());
        // 移動時間を計算
        _time = Math.Abs(_endPosX/_speed);
        // 親コンポーネントを取得
        var parent = this.gameObject.transform.parent;
        // 子コンポーネントの数が奇数なら_isOddNumber = trueにする
        if(parent != null)
        {
            int childCount = parent.childCount;
            if (childCount % 2 == 1)
            {
                _isOddNumber = true;
            }
        }

        // 奇数番目なら座標を反転
        if(_isOddNumber)
        {
            _endPosX *= -1f;
            _centerPosX *= -1f;
        }

        Debugger.Log("初期化終了");

        Move();
    }

    /// <summary>
    /// 1周目の単純横方向移動
    /// </summary>
    private void Move()
    {
        Debugger.Log("移動開始");
        // 右に移動
        Debugger.Log(_speed.ToString());
        Debugger.Log((_endPosX/_speed).ToString());
        transform.DOMoveX(_endPosX, _time).SetEase(Ease.Linear).OnComplete(OnEndPos);
    }

    /// <summary>
    /// 2週目以降、回転移動アニメーションの下半分
    /// </summary>
    private void OnEndPos()
    {
        Debugger.Log("移動完了01");
        gameObject.SetActive(false);
        // 少し下に移動
        var sequance = DOTween.Sequence();
        sequance.Append(transform.DOMoveY(-100f, 1f).SetEase(Ease.Linear));

        // オブジェクトを小さくして非表示
        // 完了するまで待つ
        sequance.Append(transform.DOScale(new Vector3(0.01f, 0.01f, 0.01f), 1f))
        .OnComplete(() => gameObject.SetActive(false));

        // 同時に端まで移動
        sequance.Join(transform.DOMoveX(_centerPosX, _time).SetEase(Ease.Linear));
        sequance.OnComplete(OnCenterPos);
        sequance.Play();
    }

    /// <summary>
    /// 3週目以降、回転移動アニメーションの上半分
    /// </summary>
    private void OnCenterPos()
    {
        Debugger.Log("移動完了02");

        var sequance = DOTween.Sequence();

        // オブジェクトを大きくして表示
        // 完了するまで待つ
        gameObject.SetActive(true);
        gameObject.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);

        // 少し上に移動
        sequance.Append(transform.DOMoveY(100f, 1f).SetEase(Ease.Linear));

        // 同時に端まで移動
        sequance.Join(transform.DOMoveX(_endPosX, _time).SetEase(Ease.Linear));
        sequance.OnComplete(OnEndPos);
        sequance.Play();
    }
}