using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class sakura_FlowerflakeSample_loop : MonoBehaviour
{
    [Header("出現位置の設定")]
    [SerializeField] private float _bornPosXMin = -50f; // 出現位置X軸の最小値
    [SerializeField] private float _bornPosXMax = 50f;  // 出現位置X軸の最大値
    [SerializeField] private float _bornPosYMin = 50f;   // 出現位置Y軸の最小値
    [SerializeField] private float _bornPosYMax = 100f;  // 出現位置Y軸の最大値
    [SerializeField] private float _bornPosZMin = -50f; // 出現位置Z軸の最小値
    [SerializeField] private float _bornPosZMax = 50f;  // 出現位置Z軸の最大値

    [Header("花の設定")]
    [SerializeField] private float _flowerflakeSizeMin = 0.1f; // 花の大きさ最小値
    [SerializeField] private float _flowerflakeSizeMax = 0.3f; // 花の大きさ最大値
    [SerializeField] private bool _isRotate = true;           // 花が回転するか
    [SerializeField] private bool _isFall = true;              // 花が落下するか
    [SerializeField] private bool _isSway = true;              // 花が左右に揺れるか

    [Header("動きの設定")]
    [SerializeField] private float _fallSpeedMin = 7f; // 落下速度最小値（秒単位）
    [SerializeField] private float _fallSpeedMax = 10f; // 落下速度最大値（秒単位）
    [SerializeField] private float _swayAmountMin = 0.1f; // 左右に揺れる最小距離
    [SerializeField] private float _swayAmountMax = 2f; // 左右に揺れる最大距離

    // 内部変数
    private Vector3 _bornPos;            // 出現時の初期位置
    private Tween _rotationTween;        // 回転アニメーション用のTween
    private Tween _fallTween;            // 落下アニメーション用のTween
    private Tween _swayTween;             // 揺れアニメーション用のTween

    // スタート時に呼ばれる
    async Task Start()
    {
        // 出現位置をランダムに決定
        _bornPos = new Vector3(
            Random.Range(_bornPosXMin, _bornPosXMax),
            Random.Range(_bornPosYMin, _bornPosYMax),
            Random.Range(_bornPosZMin, _bornPosZMax)
        );

        transform.position = _bornPos;

        // 花の大きさをランダムに設定
        float flowerflakeSize = Random.Range(_flowerflakeSizeMin, _flowerflakeSizeMax);
        transform.localScale = new Vector3(flowerflakeSize, flowerflakeSize, flowerflakeSize);

        Active();
    }

    // 花の動作を有効化
    private void Active()
    {
        Rotation();
        Fall();
    }

    /// <summary>
    /// 回転アニメーション
    /// Y軸を中心に回転する
    /// </summary>
    private void Rotation()
    {
        if (!_isRotate)
        {
            return;
        }

        float rotationDuration = Random.Range(1f, 2f); // 回転にかかる時間をランダムで決める
        _rotationTween = transform.DORotate(new Vector3(0, 720, 0), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental); // 無限ループで回転
    }

    /// <summary>
    /// 落下アニメーション
    /// </summary>
    private void Fall()
    {
        if (!_isFall)
        {
            return;
        }

        float fallDistance = _bornPos.y; // 出現位置の高さ
        float fallDuration = Random.Range(_fallSpeedMin, _fallSpeedMax) * (fallDistance / 100f); // 落下時間を計算

        _fallTween = transform.DOMoveY(0f, fallDuration) // 地面まで移動
            .SetEase(Ease.Linear)
            .OnKill(() =>
            {
                transform.position = _bornPos; // 落下終了後、初期位置に戻して再度落下
                Fall();
            });

        if (_isSway)
        {
            float swayDirection = Random.Range(0f, 1f) < 0.5f ? 1f : -1f; // 揺れの方向をランダムで決定
            float swayAmount = Random.Range(_swayAmountMin, _swayAmountMax); // 揺れ幅をランダムで決定
            _swayTween = transform.DOBlendableMoveBy(new Vector3(swayDirection * swayAmount, 0, 0), Random.Range(0.3f, 0.7f))
                .SetLoops(-1, LoopType.Yoyo) // 左右に往復運動
                .SetEase(Ease.InOutSine); // スムーズに揺れる
        }
    }
}
