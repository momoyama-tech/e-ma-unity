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
    [SerializeField] private float _flapDuration = 0.5f;// 羽ばたく時間
    [SerializeField] private float _flapNumPerSecond = 1;// 1秒間に羽ばたく回数
    [SerializeField] private float _speed = 0.1f;// 移動速度

    private Vector3 _bornPos;
    private Vector3 _targetPos;

    async Task Start()
    {
        _bornPos = new Vector3(_bornPosX, _bornPosY, _bornPosZ);
        _targetPos = new Vector3(_targetPosX, _targetPosY, _targetPosZ);
        transform.position = _bornPos;
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        transform.DOScale(_butterFlySize, 1f).SetEase(Ease.Linear).WaitForCompletion();
        // Move();
        // await Flap();
    }

    private void Move()
    {
        transform.DOMove(_targetPos, _speed).SetEase(Ease.Linear).OnComplete(() =>
        {
            Destroy(gameObject);
        });
    }
    private async UniTask Flap()
    {
        for (int i = 0; i < _flapNumPerSecond; i++)
        {
            // ランダムで羽ばたく回数を決定
            int randFlapNum = Random.Range(1, 3);
            for (int j = 0; j < randFlapNum; j++)
            {
                transform.DOMoveY(_flapHeight, _flapDuration / 2).SetEase(Ease.Linear).OnComplete(() =>
                {
                transform.DOMoveY(0, _flapDuration / 2).SetEase(Ease.Linear);
                });
            }
            await UniTask.Delay((int)(_flapDuration * 1000 / _flapNumPerSecond));
        }
    }
}