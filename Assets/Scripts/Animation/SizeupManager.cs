using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class SizeUpManager : MonoBehaviour
{
    [SerializeField] private float _butterFlySize = 1;// 蝶の大きさ
    [SerializeField] private float _finishTime = 1;// 大きくなるまでの時間
    
    async Task Start()
    {
        await SizeUp();
    }

    /// <summary>
    /// 蝶の大きさを大きくする
    /// 生成座標と目的座標の距離を計算
    /// その距離と_lifeTimeを使って蝶の大きさを変更の終了時間を決定
    /// </summary>
    private async UniTask SizeUp()
    {
        transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);

        // 次のスケール変更を待機
        await transform.DOScale(_butterFlySize, _finishTime).SetEase(Ease.Linear).AsyncWaitForCompletion();
    }
}