using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class FlapManager : MonoBehaviour
{
    [SerializeField] private float _flapHeight = 1;// 羽ばたく高さ
    [SerializeField] private float _flapSpeed = 1;// 羽ばたく速度
    [SerializeField] private float _flapNumPerSecond = 1;// 1秒間に羽ばたく回数

    async void Start()
    {
        await Flap(); // 非同期処理を待機
    }

    /// <summary>
    /// _flapHeightの高さまで上昇し、_flapHeightの高さまで下降する
    /// ランダムで上昇、下降する回数を決定
    /// 回数だけ繰り返す
    /// 一秒間に羽ばたく回数からawaitの時間を計測
    /// </summary>
    /// <returns></returns>
    private async UniTask Flap()
    {
        // 上昇、下降する回数を決定
        int flapCount = Random.Range(1, 5);
        float flapTime = 1f / _flapNumPerSecond;

        for (int i = 0; i < flapCount; i++)
        {
            // 上昇
            await transform.DOLocalMoveY(_flapHeight, _flapSpeed).SetEase(Ease.Linear).AsyncWaitForCompletion();
            // 下降
            await transform.DOLocalMoveY(0, _flapSpeed).SetEase(Ease.Linear).AsyncWaitForCompletion();
        }
        
        await UniTask.Delay((int)(flapTime * 1000) * flapCount);
    }
}