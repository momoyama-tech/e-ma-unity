using UnityEngine;
using DG.Tweening;

public class DOTweenInitializer : MonoBehaviour
{
    void Awake()
    {
        // DOTweenの容量を設定（アクティブなTween数: 2000、シーケンス数: 100）
        DOTween.SetTweensCapacity(2000, 100);
    }
}