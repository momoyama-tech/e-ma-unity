using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("SerialCommunication コンポーネント")]
    public SerialCommunication serialComm;   // 後で Inspector で紐づけます

    [Header("キューブ生成設定")]
    public GameObject cubePrefab;          // 作ったキューブプレハブ
    public Vector3[] spawnPositions;       // 出したい場所を配列で
    public float launchForce = 5f;         // 上に飛ばす力

    private GameObject[] spawnedCubes;     // 生成済みのキューブを覚える配列

    public GameObject[] cubePrefabs;  // 複数のプレハブを登録可能に

    private long n = 0;

    void Start()
    {
        // spawnPositions の長さだけ配列を作り、最初は全部 null
        spawnedCubes = new GameObject[spawnPositions.Length];
        for (int i = 0; i < spawnedCubes.Length; i++)
            spawnedCubes[i] = null;

        // serialComm がセットされていないと動かないのでチェック
        if (serialComm == null)
        {
            Debug.LogError("CubeSpawner の serialComm に SerialCommunication をセットしてください！");
            enabled = false;
        }
    }

    void Update()
    {
       //各 i について Getinfo を呼び出し
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int info = serialComm.Getinfo(i);

            // ① info == 1 && まだキューブがない → 生成＆飛ばす
            if (info == 1 && n % 5 == 0)//&& spawnedCubes[i] == null
            {
                SpawnAndLaunchCube(i);
            }
            // ② info == 0 && キューブがある → 消す
            else if (info == 0 && spawnedCubes[i] != null)
            {
                Destroy(spawnedCubes[i]);
                spawnedCubes[i] = null;
            }
        }

        //if (Input.GetKey(KeyCode.Space) )
        //{
        //    SpawnAndLaunchCube(14);
        //}
        n = n + 1;
        if (n > 1000) n = 0;
    }

    // キューブを生成して上向きにインパルスを与える
    private void SpawnAndLaunchCube(int index)
    {
        Vector3 basePos = spawnPositions[index];
        float offsetX = Random.Range(-10f, 10f);
        Vector3 pos = new Vector3(basePos.x + offsetX, basePos.y - 60, basePos.z - 50);

        // 放射方向を計算
        Vector3 direction = (Vector3.up + new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-0.5f, 0.5f))).normalized;

        // 回転の設定（方向 + 任意のオフセット）
        Vector3 baseEuler = Quaternion.LookRotation(direction).eulerAngles;
        // 条件に応じて回転オフセットを変更
        Vector3 offsetEuler;
        if (direction.x < 0f)
            offsetEuler = new Vector3(0f, 0f, 90f);  // 左に飛ぶとき
        else
            offsetEuler = new Vector3(0f, 0f, -90f);   // 右に飛ぶとき
        Quaternion rotation = Quaternion.Euler(baseEuler + offsetEuler);

        // 🔽 プレハブをランダム選択
        if (cubePrefabs == null || cubePrefabs.Length == 0)
        {
            Debug.LogWarning("プレハブ配列が空です！");
            return;
        }

        GameObject selectedPrefab = cubePrefabs[Random.Range(0, cubePrefabs.Length)];
        GameObject cube = Instantiate(selectedPrefab, pos, rotation);
        spawnedCubes[index] = cube;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(direction * launchForce, ForceMode.Impulse);
        else
            Debug.LogWarning("プレハブに Rigidbody がありません！");

        Destroy(cube, 10f);
    }
}
