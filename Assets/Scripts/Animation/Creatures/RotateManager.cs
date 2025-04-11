using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class RotateManager : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1;// 回転速度
    [SerializeField] private float _rotationAngle = 360;// 回転角度
    [SerializeField] private float _rotationNumPerSecond = 1;// 1秒間に回転する回数

    async Task Update()
    {
        await Rotation();
    }

    /// <summary>
    /// _rotationAngleの角度まで回転する
    /// _rotationSpeedの速度で回転する
    /// ランダムで回転する回数を決定
    /// 回数だけ繰り返す
    /// 1秒間に回転する回数からawaitの時間を計測
    /// </summary>
    
    private async UniTask Rotation()
    {
        // 回転する回数を決定
        int rotationCount = Random.Range(1, 5);
        float rotationTime = 1f / _rotationNumPerSecond;

        for (int i = 0; i < rotationCount; i++)
        {
            // ワールド座標でX軸方向に回転
            Quaternion targetRotation = Quaternion.Euler(new Vector3(_rotationAngle, 0, 0));
            await transform.DORotateQuaternion(targetRotation, _rotationSpeed).SetEase(Ease.Linear).AsyncWaitForCompletion();
        }

        // 回転が完了したら元の角度に戻す
        Quaternion originalRotation = Quaternion.Euler(Vector3.zero);
        await transform.DORotateQuaternion(originalRotation, _rotationSpeed).SetEase(Ease.Linear).AsyncWaitForCompletion();

        await UniTask.Delay((int)(rotationTime * 1000) * rotationCount);
    }
}