using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class FlowerMove : MonoBehaviour
{
    [SerializeField] private float _speed; // 移動速度
    private float _endPosX = 400.0f;
    private bool _isOddNumber = false;

    public async UniTask Initialize()
    {
        Debugger.Log(_speed.ToString());
        // 親コンポーネントを取得
        // 子コンポーネントの数が奇数なら_isOddNumber = trueにする
        var parent = this.gameObject.transform.parent;
        if(parent != null)
        {
            int childCount = parent.childCount;
            if (childCount % 2 == 1)
            {
                _isOddNumber = true;
            }
        }

        if(_isOddNumber)
        {
            _endPosX *= -1f;
        }

        Debugger.Log("初期化終了");

        await Move();
    }

    private async UniTask Move()
    {
        Debugger.Log("移動開始");
        // 右に移動
         Debugger.Log(_speed.ToString());
        await transform.DOMoveX(_endPosX, _endPosX/_speed).SetEase(Ease.Linear).OnComplete(OnEndPos).AsyncWaitForCompletion();
    }

    private void OnEndPos()
    {
        Debugger.Log("移動完了");
    }
}