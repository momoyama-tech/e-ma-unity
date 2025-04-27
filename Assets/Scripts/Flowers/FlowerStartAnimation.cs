using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;

public class FlowerStartAnimation : MonoBehaviour
{

    public void ManualStart()
    {
        gameObject.SetActive(false);
    }
    public async UniTask Initialize()
    {
        Debug.Log("スタートアニメーション");
        gameObject.SetActive(true);
        gameObject.transform.position = new Vector3(0, -70, -1);
        await BubbleAimation();
    }

    /// <summary>
    /// 泡の苗をアニメーションさせる
    /// </summary>
    /// <returns></returns>
    private async UniTask BubbleAimation()
    {
        gameObject.transform.DOMoveY(0, 2).SetEase(Ease.OutBack).AsyncWaitForCompletion();
    }
}