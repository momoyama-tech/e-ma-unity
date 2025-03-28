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
    [SerializeField] private float _lifeTime = 5;// 羽ばたく時間
    [SerializeField] private float _flapNumPerSecond = 1;// 1秒間に羽ばたく回数

    private Vector3 _bornPos;
    private Vector3 _targetPos;

    async Task Start()
    {
        _bornPos = new Vector3(_bornPosX, _bornPosY, _bornPosZ);
        _targetPos = new Vector3(_targetPosX, _targetPosY, _targetPosZ);
        transform.position = _bornPos;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.DOScale(_butterFlySize, 1f).SetEase(Ease.Linear).WaitForCompletion();
        Active();
    }

    private async Task Active()
    {
        Move();
        SizeUp();
        // Flap();
    }

    private void Move()
    {
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
        float distance = Vector3.Distance(_bornPos, _targetPos);
        float sizeUpTime = distance / _lifeTime;
        transform.DOScale(_butterFlySize, sizeUpTime).SetEase(Ease.Linear);
    }
    private async UniTask Flap()
    {
        for (int i = 0; i < _flapNumPerSecond; i++)
        {
            // ランダムで羽ばたく回数を決定
            int randFlapNum = Random.Range(1, 3);
            for (int j = 0; j < randFlapNum; j++)
            {
                transform.DOMoveY(_flapHeight, _lifeTime / 2).SetEase(Ease.Linear).OnComplete(() =>
                {
                transform.DOMoveY(0, _lifeTime / 2).SetEase(Ease.Linear);
                });
            }
            await UniTask.Delay((int)(_lifeTime * 1000 / _flapNumPerSecond));
        }
    }
}