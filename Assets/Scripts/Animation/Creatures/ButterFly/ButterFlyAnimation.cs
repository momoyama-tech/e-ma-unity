using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class ButterFlyAnimation : MonoBehaviour
{
    [SerializeField] private float _bornPosX = 0;// 出現位置X
    [SerializeField] private float _bornPosY = 0;// 出現位置Y
    [SerializeField] private float _bornPosZ = 0;// 出現位置Z
    [SerializeField] private float _targetPosX = 5;// 目的地X
    [SerializeField] private float _targetPosY = 0;// 目的地Y
    [SerializeField] private float _targetPosZ = 0;// 目的地Z
    [SerializeField] private float _butterFlySize = 1;// 蝶の大きさ
    [SerializeField] private float _flapHeight = 1;// 羽ばたく高さ
    [SerializeField] private float _flapSpeed = 1;// 羽ばたく速度
    [SerializeField] private float _lifeTime = 5;// 消滅までの時間
    [SerializeField] private float _flapNumPerSecond = 1;// 1秒間に羽ばたく回数
    [SerializeField] private bool _isMove = false;// 移動するかどうか
    [SerializeField] private bool _isSizeUp = false;// 大きくなるかどうか
    [SerializeField] private bool _isFlap = false;// 羽ばたくかどうか
    [SerializeField] private bool _isRotate = false;// 回転するかどうか

    private Vector3 _bornPos;// 出現座標
    private Vector3 _targetPos;// 目標座標
    private Vector3 _moveDistance;// 出現位置と目的地の距離

    async Task Start()
    {
        _bornPos = new Vector3(_bornPosX, _bornPosY, _bornPosZ);
        _targetPos = new Vector3(_targetPosX, _targetPosY, _targetPosZ);
        _moveDistance = _targetPos - _bornPos;
        transform.position = _bornPos;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.DOScale(_butterFlySize, 1f).SetEase(Ease.Linear).WaitForCompletion();
        Active();
    }

    private async Task Active()
    {
        Move();
        SizeUp();
        Flap();
    }

    private void Move()
    {
        if (_isMove == false)
        {
            return;
        }
        // 目的地に向かって移動
        transform.DOMove(_targetPos, _lifeTime).SetEase(Ease.Linear).OnComplete(() =>
        {
            Debug.Log("Arrived at the destination");
            // 目的地に到達したら消滅
            // Destroy(gameObject);
        });
    }

    /// <summary>
    /// 蝶の大きさを大きくする
    /// 生成座標と目的座標の距離を計算
    /// その距離と_lifeTimeを使って蝶の大きさを変更の終了時間を決定
    /// </summary>
    private void SizeUp()
    {
        if (_isSizeUp == false)
        {
            transform.localScale = new Vector3(_butterFlySize, _butterFlySize, _butterFlySize);
            return;
        }
        float distance = Vector3.Distance(_bornPos, _targetPos);
        float sizeUpTime = distance / _lifeTime;
        transform.DOScale(_butterFlySize, sizeUpTime).SetEase(Ease.Linear);
    }

    /// <summary>
    /// 羽ばたきアニメーション
    /// 移動距離と_lifeTimeを使って羽ばたく回数を決定
    /// 1回に羽ばたく回数はランダムで決定
    /// バグ：上下移動のアニメーションで上に移動したあと、y座標の更新が行われていない
    /// そのため、下方向に移動した時、上に上がる前の座標から移動してしまう
    /// そのため、上に移動した後、y座標を更新する必要がある
    /// 移動がないと正常に動く
    /// </summary>
    /// <returns></returns>.
    private async UniTask Flap()
    {
        if (_isFlap == false)
        {
            return;
        }
        // 移動距離と_lifeTimeを使って羽ばたく回数を決定
        for (int i = 0; i < (_moveDistance.magnitude / _lifeTime) * _flapNumPerSecond; i++)
        {
            // ランダムで羽ばたく回数を決定
            int randFlapNum = Random.Range(1, 3);
            for (int j = 0; j < randFlapNum; j++)
            {
                // なめらかな上下移動
                await transform.DOMoveY(this.transform.position.y + _flapHeight, 1 / _flapSpeed)
                    .SetEase(Ease.InOutSine) // なめらかな動き
                    .AsyncWaitForCompletion();

                await transform.DOMoveY(this.transform.position.y - _flapHeight, 1 / _flapSpeed)
                    .SetEase(Ease.InOutSine) // なめらかな動き
                    .AsyncWaitForCompletion();
            }
            await UniTask.Delay((int)(_lifeTime * 1000 / _flapNumPerSecond));
        }
    }

    private void Rotation()
    {

    }
}