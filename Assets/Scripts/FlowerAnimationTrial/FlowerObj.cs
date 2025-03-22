using UnityEngine;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading.Tasks;

public class FlowerObj : MonoBehaviour
{
    [SerializeField] private int _totalStemNum;
    [SerializeField] private GameObject[] _flowerElements;
    private GameObject _parentFlowerElement;
    private GameObject _selectedFlowerElement;

    private int _cotyledonNum = 1;
    private int _stemNum = 2;
    private int _bloomingNum = 3;

    public async Task Instantiate()
    {
        _parentFlowerElement = this.gameObject;
        await CreateFlowerElements();
    }

    private void SelectFlowerElements(int num)
    {
        switch (num)
        {
            case 1:
                _selectedFlowerElement = _flowerElements[0];
                break;
            case 2:
                _selectedFlowerElement = _flowerElements[1];
                break;
            case 3:
                _selectedFlowerElement = _flowerElements[2];
                break;
        }
    }

    /// <summary>
    ///  花の要素を生成
    ///  子葉 -> 茎 -> 花
    ///  要素は小さい状態で生成して徐々に大きくしていく
    /// </summary>
    /// <returns></returns>
    private async UniTask CreateFlowerElements()
    {
        // 子葉を生成
        SelectFlowerElements(_cotyledonNum);// 生成するオブジェクトを子葉に変更
        var cotyledon = Instantiate(_selectedFlowerElement, _parentFlowerElement.transform);// 子葉を生成
        
        // cotyledonの大きさを変更
        cotyledon.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        cotyledon.transform.DOScale(new Vector3(1f, 1f, 0.7f), 3f);

        await UniTask.Delay(2000);

        // 茎が伸びる
        // 茎の数だけ繰り返す
        // 茎の数はインスペクターで設定
        SelectFlowerElements(1);
        for (int i = 0; i < _totalStemNum; i++)
        {
            SelectFlowerElements(_stemNum);// 生成するオブジェクトを茎に変更
            var stem = Instantiate(_selectedFlowerElement, _parentFlowerElement.transform);// 茎を生成

            // 生成位置を子葉の位置から少し上にずらす
            // 徐々に大きくしながらy軸方向に座標移動
            stem.transform.position = cotyledon.transform.position + new Vector3(0, 5f * i, -0.01f * (i + 1));
            stem.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            stem.transform.DOScale(new Vector3(1f, 1f, 0.7f), 2f);
            stem.transform.DOMoveY(5f * (i + 1), 2f);

            await UniTask.Delay(1000);
        }

        await UniTask.Delay(2000);

        // 花が咲く
        SelectFlowerElements(_bloomingNum);// 生成するオブジェクトを花に変更
        var blooming = Instantiate(_selectedFlowerElement, _parentFlowerElement.transform);// 花を生成

        // 生成位置を茎の位置から少し上にずらす
        // 徐々に大きくしながらy軸方向に座標移動
        blooming.transform.position = cotyledon.transform.position + new Vector3(0, 5f * _totalStemNum, -0.01f * (_totalStemNum + 1));
        blooming.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        blooming.transform.DOScale(new Vector3(1f, 1f, 1f), 5f);
        blooming.transform.DOMoveY(5f * (_totalStemNum + 1), 5f);

        await UniTask.Delay(5000);

        // 不要なTweenを削除
        DOTween.KillAll();
    }
}