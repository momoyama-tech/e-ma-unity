using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private GameObject _targetPrefab;
    private GameObject _targetParent;
    [SerializeField] private float _createMaxRangeX = 400f; // 生成範囲の最大値
    [SerializeField] private float _createMinRangeX = -400f; // 生成範囲の最小値
    [SerializeField] private float _createMaxRangeY = 60f; // 生成範囲の最大値
    [SerializeField] private float _createMinRangeY = -60f; // 生成範囲の最小値
    [SerializeField] private float _createMaxRangeZ = -20f; // 生成範囲の最大値
    [SerializeField] private float _createMinRangeZ = -300f; // 生成範囲の最小値

    public void ManualStart()
    {
        _targetParent = this.gameObject;
    }

    public void Create()
    {
        Debug.Log($"Create {_targetPrefab.name}");
        // Prefabを生成
        GameObject prefab = Instantiate(_targetPrefab, _targetParent.transform);

        float randomX = Random.Range(_createMinRangeX, _createMaxRangeX);
        float randomY = Random.Range(_createMinRangeY, _createMaxRangeY);
        float randomZ = Random.Range(_createMinRangeZ, _createMaxRangeZ);

        prefab.transform.position = new Vector3(randomX, randomY, randomZ);
    }
}