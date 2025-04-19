using UnityEngine;
using DG.Tweening;

public class Firefly : MonoBehaviour
{
    void Start()
    {
        var rand1 = Random.Range(-5, 5);
        var rand2 = Random.Range(-5, 5);
        var rand3 = Random.Range(-5, 5);
        var rand4 = Random.Range(-5, 5);
        var sequence = DOTween.Sequence();
        
        // x,y,z軸にランダムに移動
        // 不規則に動くため3軸同時に移動する
        // 素早く動く
        // ループする


        sequence.Append(transform.DOMoveX(rand1, rand2).SetRelative(true));
        sequence.Append(transform.DOMoveY(rand3, rand4).SetRelative(true));
        sequence.Append(transform.DOMoveZ(rand1, rand2).SetRelative(true));
        sequence.Append(transform.DOMoveX(rand4, rand3).SetRelative(true));
        sequence.Append(transform.DOMoveY(rand2, rand4).SetRelative(true));
        sequence.Append(transform.DOMoveZ(rand2, rand1).SetRelative(true));
        
        sequence.SetSpeedBased(true);
        sequence.Play().SetLoops(-1, LoopType.Restart);
    }
}