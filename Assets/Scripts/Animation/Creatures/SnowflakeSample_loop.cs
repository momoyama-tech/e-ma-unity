using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class SnowflakeSample_loop : MonoBehaviour
{
    [Header("出現位置の設定")]
    [SerializeField] private float _bornPosXMin = -50f; // 出現位置Xの最小値（横方向の最小位置）
    [SerializeField] private float _bornPosXMax = 50f; // 出現位置Xの最大値（横方向の最大位置）
    [SerializeField] private float _bornPosYMin = 50f; // 出現位置Yの最小値（高さ方向の最小位置）
    [SerializeField] private float _bornPosYMax = 100f; // 出現位置Yの最大値（高さ方向の最大位置）
    [SerializeField] private float _bornPosZMin = -50f; // 出現位置Zの最小値（奥行き方向の最小位置）
    [SerializeField] private float _bornPosZMax = 50f; // 出現位置Zの最大値（奥行き方向の最大位置）

    [Header("雪の設定")]
    [SerializeField] private float _snowflakeSizeMin = 0.1f; // 雪の大きさ最小値
    [SerializeField] private float _snowflakeSizeMax = 0.3f; // 雪の大きさ最大値
    [SerializeField] private bool _isRotate = true; // 回転するかどうか
    [SerializeField] private bool _isFall = true; // 落ちるかどうか
    [SerializeField] private bool _isSway = true; // 左右にフラフラ動くかどうか

    [Header("動きの設定")]
    [SerializeField] private float _fallSpeedMin = 2f; // 落下速度最小値（秒単位）
    [SerializeField] private float _fallSpeedMax = 10f; // 落下速度最大値（秒単位）
    [SerializeField] private float _swayAmountMin = 0f; // 左右に揺れる最小距離
    [SerializeField] private float _swayAmountMax = 2f; // 左右に揺れる最大距離

    private Vector3 _bornPos; // 出現座標
    private Tween _rotationTween; // 回転Tween
    private Tween _fallTween; // 落下Tween
    private Tween _swayTween; // 横移動Tween

    async Task Start()
    {
        // 出現位置をランダムに設定
        _bornPos = new Vector3(
            Random.Range(_bornPosXMin, _bornPosXMax),  // Xのランダム範囲
            Random.Range(_bornPosYMin, _bornPosYMax),  // Yのランダム範囲
            Random.Range(_bornPosZMin, _bornPosZMax)   // Zのランダム範囲
        );

        transform.position = _bornPos;

        // 雪の大きさをランダムに設定
        float snowflakeSize = Random.Range(_snowflakeSizeMin, _snowflakeSizeMax);
        transform.localScale = new Vector3(snowflakeSize, snowflakeSize, snowflakeSize);

        // 初期アクションを開始
        Active();
    }

    private void Active()
    {
        Rotation();  // 回転を追加
        Fall();      // 落下を追加
    }

    /// <summary>
    /// 回転アニメーション
    /// Y軸で回転させる
    /// </summary>
    private void Rotation()
    {
        if (!_isRotate)
        {
            return;
        }

        // 回転速度をランダムに設定（例えば1秒〜5秒間で1回転）
        float rotationDuration = Random.Range(1f, 5f);  // 回転にかかる時間をランダムに設定
        _rotationTween = transform.DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);  // 永久に回転させる
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

        // 出現した高さを基準に落下距離と時間を設定
        float fallDistance = _bornPos.y; // 出現位置から0になるまで落ちる

        // 落下時間をランダムに設定（例えば、1秒〜3秒の範囲で落ちる）
        float fallDuration = Random.Range(_fallSpeedMin, _fallSpeedMax) * (fallDistance / 100f); // 距離に応じて時間を調整

        // 落ちるアニメーション
        _fallTween = transform.DOMoveY(0f, fallDuration) // 0まで落ちる
            .SetEase(Ease.Linear) // 落ちる速さを一定にするためにLinearに設定
            .OnKill(() =>
            {
                // 高さが0以下になったら初期位置に戻す
                transform.position = _bornPos;
                Fall(); // 再度落下アニメーションを開始
            });

        // 左右にフラフラ動かすアニメーション（_isSwayがtrueの場合のみ）
        if (_isSway)
        {
            float swayDirection = Random.Range(0f, 1f) < 0.5f ? 1f : -1f; // 揺れの方向（ランダムに左右）
            float swayAmount = Random.Range(_swayAmountMin, _swayAmountMax); // 揺れ幅のランダム設定
            _swayTween = transform.DOBlendableMoveBy(new Vector3(swayDirection * swayAmount, 0, 0), Random.Range(0.3f, 0.7f))
                .SetLoops(-1, LoopType.Yoyo) // 左右に往復運動
                .SetEase(Ease.InOutSine);  // なめらかに動かす
        }
    }
}
