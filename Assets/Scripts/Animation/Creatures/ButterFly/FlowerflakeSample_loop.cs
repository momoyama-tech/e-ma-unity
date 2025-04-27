using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class FlowerflakeSample_loop : MonoBehaviour
{
    [Header("出現位置の設定")]
    [SerializeField] private float _bornPosXMin = -50f;
    [SerializeField] private float _bornPosXMax = 50f;
    [SerializeField] private float _bornPosYMin = 0f;
    [SerializeField] private float _bornPosYMax = 50f;
    [SerializeField] private float _bornPosZMin = -50f;
    [SerializeField] private float _bornPosZMax = 50f;

    [Header("花の設定")]
    [SerializeField] private float _flowerflakeSizeMin = 0.1f; // 花の大きさ最小値
    [SerializeField] private float _flowerflakeSizeMax = 0.3f; // 花の大きさ最大値
    [SerializeField] private bool _isRotate = false;
    [SerializeField] private bool _isFall = true;
    [SerializeField] private bool _isSway = true;

    [Header("動きの設定")]
    [SerializeField] private float _fallSpeedMin = 1f;
    [SerializeField] private float _fallSpeedMax = 3f;
    [SerializeField] private float _swayAmountMin = 0f;
    [SerializeField] private float _swayAmountMax = 3f;

    private Vector3 _bornPos;
    private Tween _rotationTween;
    private Tween _fallTween;
    private Tween _swayTween;

    async Task Start()
    {
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

    private void Active()
    {
        Rotation();
        Fall();
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

        float rotationDuration = Random.Range(1f, 5f);
        _rotationTween = transform.DORotate(new Vector3(0, 0, 360), rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
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

        float fallDistance = _bornPos.y;

        float fallDuration = Random.Range(_fallSpeedMin, _fallSpeedMax) * (fallDistance / 100f);

        _fallTween = transform.DOMoveY(0f, fallDuration)
            .SetEase(Ease.Linear)
            .OnKill(() =>
            {
                transform.position = _bornPos;
                Fall();
            });

        if (_isSway)
        {
            float swayDirection = Random.Range(0f, 1f) < 0.5f ? 1f : -1f;
            float swayAmount = Random.Range(_swayAmountMin, _swayAmountMax);
            _swayTween = transform.DOBlendableMoveBy(new Vector3(swayDirection * swayAmount, 0, 0), Random.Range(0.3f, 0.7f))
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }
    }
}
