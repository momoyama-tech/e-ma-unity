using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class Disappearance : MonoBehaviour
{
    [SerializeField] private float _disappearTime = 1f; // 消えるまでの時間
    [SerializeField] private float _disappearScale = 0.01f; // 大きさ

    void Start()
    {
        gameObject.SetActive(true);
        Activate();
    }

    /// <summary>
    /// _disappearScaleの大きさまで小さくなる
    /// </summary>
    async UniTask Activate()
    {
        transform.localScale = new Vector3(0.5f, 0.5f, 1f);

        // Z軸は1fで固定
        // X軸とY軸を_disappearScaleの大きさまで小さくする
        await transform.DOScale(new Vector3(_disappearScale, _disappearScale, 1f), _disappearTime)
            .SetEase(Ease.Linear).AsyncWaitForCompletion();

        await transform.DOScale(0, 0.1f).SetEase(Ease.Linear).AsyncWaitForCompletion();

        gameObject.SetActive(false);
    }
}