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
        // 各 i について Getinfo を呼び出し
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int info = serialComm.Getinfo(i);

            // ① info == 1 && まだキューブがない → 生成＆飛ばす
            if (info == 1)//&& spawnedCubes[i] == null
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

        if(gameObject.transform.position.y == -60)
        {
            Debug.Log("オブジェクトの削除" + gameObject.name);
            Destroy(gameObject);
        }
    }

    // キューブを生成して上向きにインパルスを与える
    private void SpawnAndLaunchCube(int index)
    {
        Vector3 pos = spawnPositions[index];
        GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity);
        spawnedCubes[index] = cube;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        else
            Debug.LogWarning("cubePrefab に Rigidbody がありません！");
    }
}
