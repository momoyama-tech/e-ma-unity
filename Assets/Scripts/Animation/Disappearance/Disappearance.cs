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
        Activate();
    }

    /// <summary>
    /// _disappearScaleの大きさまで小さくなる
    /// </summary>
    async UniTask Activate()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

        await transform.DOScale(_disappearScale, _disappearTime).SetEase(Ease.Linear).AsyncWaitForCompletion();

        await transform.DOScale(0, 0.1f).SetEase(Ease.Linear).AsyncWaitForCompletion();

        gameObject.SetActive(false);
    }
}