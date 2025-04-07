using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class FlowerSample : MonoBehaviour
{
    [Header("出現位置の設定")]
    [SerializeField] private float _bornPosXMin = -100f; // 出現位置Xの最小値（横方向の最小位置）
    [SerializeField] private float _bornPosXMax = 100f; // 出現位置Xの最大値（横方向の最大位置）
    [SerializeField] private float _bornPosYMin = 100f; // 出現位置Yの最小値（高さ方向の最小位置）
    [SerializeField] private float _bornPosYMax = 150f; // 出現位置Yの最大値（高さ方向の最大位置）
    [SerializeField] private float _bornPosZMin = -100f; // 出現位置Zの最小値（奥行き方向の最小位置）
    [SerializeField] private float _bornPosZMax = 100f; // 出現位置Zの最大値（奥行き方向の最大位置）

    [Header("花びら設定")]
    [SerializeField] private float _petalSizeMin = 0.1f; // 花びらの大きさ最小値
    [SerializeField] private float _petalSizeMax = 0.3f; // 花びらの大きさ最大値
    [SerializeField] private bool _isRotate = true; // 回転するかどうか
    [SerializeField] private bool _isFall = true; // 落ちるかどうか
    [SerializeField] private bool _isSway = true; // 左右にフラフラ動くかどうか

    [Header("動きの設定")]
    [SerializeField] private float _fallSpeedMin = 1f; // 落下速度最小値（秒単位）
    [SerializeField] private float _fallSpeedMax = 3f; // 落下速度最大値（秒単位）
    [SerializeField] private float _swayAmountMin = 0.5f; // 左右に揺れる最小距離
    [SerializeField] private float _swayAmountMax = 2.5f; // 左右に揺れる最大距離
    [SerializeField] private float _windStrengthMin = 0.5f; // 風の強さ最小値
    [SerializeField] private float _windStrengthMax = 2.5f; // 風の強さ最大値
    [SerializeField] private float _windDirectionChangeInterval = 2f; // 風向き変更のインターバル

    private Vector3 _bornPos; // 出現座標
    private Tween _rotationTween; // 回転Tween
    private Tween _fallTween; // 落下Tween
    private Tween _swayTween; // 横移動Tween
    private float _windDirection; // 風向き
    private float _windStrength; // 風の強さ

    async Task Start()
    {
        // 出現位置をランダムに設定
        _bornPos = new Vector3(
            Random.Range(_bornPosXMin, _bornPosXMax),  // Xのランダム範囲
            Random.Range(_bornPosYMin, _bornPosYMax),  // Yのランダム範囲
            Random.Range(_bornPosZMin, _bornPosZMax)   // Zのランダム範囲
        );

        transform.position = _bornPos;

        // 花びらの大きさをランダムに設定
        float petalSize = Random.Range(_petalSizeMin, _petalSizeMax);
        transform.localScale = new Vector3(petalSize, petalSize, petalSize);

        // 風の初期設定
        _windDirection = Random.Range(0f, 360f); // 風向きランダム設定
        _windStrength = Random.Range(_windStrengthMin, _windStrengthMax); // 風の強さランダム設定

        Active(); // アクション開始
    }

    private void Active()
    {
        Rotation();  // 回転を追加
        Fall();      // 落下を追加
        Sway();      // 揺れを追加
    }

    private void Rotation()
    {
        if (!_isRotate) return;

        // 回転の時間をランダム設定
        float rotationDuration = Random.Range(1f, 5f);
        _rotationTween = transform.DORotate(new Vector3(0, 0, 360f), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);  // 永久に回転
    }

    private void Fall()
    {
        if (!_isFall) return;

        // 落下時間をランダム設定
        float fallDuration = Random.Range(_fallSpeedMin, _fallSpeedMax);
        _fallTween = transform.DOMoveY(0f, fallDuration)
            .SetEase(Ease.Linear) // 一定速度で落ちる
            .OnUpdate(() =>
            {
                // 地面に着いたら停止
                if (transform.position.y <= 0f)
                {
                    _rotationTween?.Kill();
                    _swayTween?.Kill();
                }
            });
    }

    private void Sway()
    {
        if (!_isSway) return;

        // 風の影響で左右に揺れる
        _swayTween = transform
            .DOBlendableMoveBy(new Vector3(Mathf.Sin(_windDirection) * _windStrength, 0f, 0f), Random.Range(0.5f, 2f))
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);  // 左右に往復運動

        // 風の方向と強さをランダムに変更
        DOVirtual.DelayedCall(Random.Range(1f, _windDirectionChangeInterval), () =>
        {
            _windDirection = Random.Range(0f, 360f); // 風向きをランダムに変更
            _windStrength = Random.Range(_windStrengthMin, _windStrengthMax); // 風強さをランダムに変更
            Sway(); // 再度揺れを開始
        });
    }
}
