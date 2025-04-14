using UnityEngine;

public class RandomButterflyCreator : MonoBehaviour
{
    [SerializeField] private GameObject[] _butterflyPrefab;
    [SerializeField] private GameObject _butterflyParent;
    [SerializeField] private float _createMaxRangeX = 5f; // 生成範囲の最大値
    [SerializeField] private float _createMinRangeX = -5f; // 生成範囲の最小値
    [SerializeField] private float _createMaxRangeY = 5f; // 生成範囲の最大値
    [SerializeField] private float _createMinRangeY = 0f; // 生成範囲の最小値
    [SerializeField] private float _createMaxRangeZ = 0f; // 生成範囲の最大値
    [SerializeField] private float _createMinRangeZ = 0f; // 生成範囲の最小値


    public void ManualStart()
    {
        // CreateButterfly();
    }

    public void ManualUpdate()
    {
        CreateButterfly();
    }

    private void CreateButterfly()
    {
        // ランダムなインデックスを生成
        int randomIndex = Random.Range(0, _butterflyPrefab.Length);

        // ランダムなバタフライを生成
        GameObject butterfly = Instantiate(_butterflyPrefab[randomIndex], _butterflyParent.transform);

        // バタフライの位置をランダムに設定
        float randomX = Random.Range(_createMinRangeX, _createMaxRangeX);
        float randomY = Random.Range(_createMinRangeY, _createMaxRangeY);
        float randomZ = Random.Range(_createMinRangeZ, _createMaxRangeZ);

        butterfly.transform.position = new Vector3(randomX, randomY, randomZ);
    }
}
