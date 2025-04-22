using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class MoveManager : MonoBehaviour
{
    private float _bornPosX = 0;// 出現位置X
    private float _bornPosY = 0;// 出現位置Y
    private float _bornPosZ = 0;// 出現位置Z
    [SerializeField] private float _targetPosX = 5;// 目的地X
    [SerializeField] private float _targetPosY = 0;// 目的地Y
    [SerializeField] private float _targetPosZ = 0;// 目的地Z
    // [SerializeField] private float _initRotateX = 0;// 初期回転X
    // [SerializeField] private float _initRotateY = 0;// 初期回転Y
    // [SerializeField] private float _initRotateZ = 0;// 初期回転Z

    [SerializeField] private float _moveSpeed = 1;// 移動速度
    [SerializeField] private GameObject _disappearPrefab;// 消失するPrefab
    private Vector3 _bornPos;// 出現座標
    private Vector3 _targetPos;// 目標座標
    private Vector3 _initRotate;// 初期回転
    private float _lifeTime;// 移動時間

    async Task Start()
    {
        _bornPosX = transform.position.x;
        _bornPosY = transform.position.y;
        _bornPosZ = transform.position.z;

        _targetPosX = _bornPosX + _targetPosX;
        _targetPosY = _bornPosY + _targetPosY;
        _targetPosZ = _bornPosZ + _targetPosZ;

        _bornPos = new Vector3(_bornPosX, _bornPosY, _bornPosZ);
        _targetPos = new Vector3(_targetPosX, _targetPosY, _targetPosZ);
        // _initRotate = new Vector3(_initRotateX, _initRotateY, _initRotateZ);
        transform.position = _bornPos;
        // transform.rotation = Quaternion.Euler(_initRotate);

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
        if(_disappearPrefab != null)
        {
            // 消失するPrefabを生成
            GameObject disappear = Instantiate(_disappearPrefab, transform.position, Quaternion.identity);
            disappear.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            disappear.transform.DOScale(new Vector3(1, 1, 1), 0.5f).SetEase(Ease.OutBack);

            // 消失するPrefabのアニメーションが終わるまで待機
            await disappear.transform.DOScale(new Vector3(0, 0, 0), 0.5f).SetEase(Ease.InBack).AsyncWaitForCompletion();
        }
        
        await gameObject.transform.DOScale(new Vector3(0, 0, 0), 1.5f).SetEase(Ease.InBack).AsyncWaitForCompletion();
        this.gameObject.SetActive(false);
    }
}