using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private float _bornPosX = 0;// 出現位置X
    [SerializeField] private float _bornPosY = 0;// 出現位置Y
    [SerializeField] private float _bornPosZ = 0;// 出現位置Z
    [SerializeField] private float _targetPosX = 5;// 目的地X
    [SerializeField] private float _targetPosY = 0;// 目的地Y
    [SerializeField] private float _targetPosZ = 0;// 目的地Z

    [SerializeField] private float _moveSpeed = 1;// 移動速度

    [SerializeField] private GameObject _disappearPrefab;// 消失するPrefab

    private Vector3 _bornPos;// 出現座標
    private Vector3 _targetPos;// 目標座標

    private float _lifeTime;// 移動時間

    async Task Start()
    {
        _bornPos = new Vector3(_bornPosX, _bornPosY, _bornPosZ);
        _targetPos = new Vector3(_targetPosX, _targetPosY, _targetPosZ);
        transform.position = _bornPos;

        // 出現座標と目的座標の距離を計算し、_moveSpeedを使って移動時間を決定
        float distance = Vector3.Distance(_bornPos, _targetPos);
        _lifeTime = distance / _moveSpeed;

        await Move();
    }
    

    private async UniTask Move()
    {
        // 目的地に向かって移動
        await transform.DOMove(_targetPos, _lifeTime).SetEase(Ease.Linear).AsyncWaitForCompletion();

        // 目的地に到達したら消滅
        Disable();    
    }

    private async UniTask Disable()
    {
        // 目的地に到達したら消滅
        GameObject disappear = Instantiate(_disappearPrefab, transform.position, Quaternion.identity);
        disappear.transform.position = transform.position;
        await transform.DOScale(0, 0.2f).SetEase(Ease.Linear).AsyncWaitForCompletion();
        this.gameObject.SetActive(false);
    }
}