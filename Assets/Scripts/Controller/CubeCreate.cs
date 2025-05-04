using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("SerialCommunication �R���|�[�l���g")]
    public SerialCommunication serialComm;   // ��� Inspector �ŕR�Â��܂�

    [Header("�L���[�u�����ݒ�")]
    public GameObject cubePrefab;          // ������L���[�u�v���n�u
    public Vector3[] spawnPositions;       // �o�������ꏊ��z���
    public float launchForce = 5f;         // ��ɔ�΂���

    private GameObject[] spawnedCubes;     // �����ς݂̃L���[�u���o����z��

    void Start()
    {
        // spawnPositions �̒��������z������A�ŏ��͑S�� null
        spawnedCubes = new GameObject[spawnPositions.Length];
        for (int i = 0; i < spawnedCubes.Length; i++)
            spawnedCubes[i] = null;

        // serialComm ���Z�b�g����Ă��Ȃ��Ɠ����Ȃ��̂Ń`�F�b�N
        if (serialComm == null)
        {
            Debug.LogError("CubeSpawner �� serialComm �� SerialCommunication ���Z�b�g���Ă��������I");
            enabled = false;
        }
    }

    void Update()
    {
        // �e i �ɂ��� Getinfo ���Ăяo��
        for (int i = 0; i < spawnPositions.Length; i++)
        {
            int info = serialComm.Getinfo(i);

            // �@ info == 1 && �܂��L���[�u���Ȃ� �� ��������΂�
            if (info == 1)//&& spawnedCubes[i] == null
            {
                SpawnAndLaunchCube(i);
            }
            // �A info == 0 && �L���[�u������ �� ����
            else if (info == 0 && spawnedCubes[i] != null)
            {
                Destroy(spawnedCubes[i]);
                spawnedCubes[i] = null;
            }
        }

        if(gameObject.transform.position.y == -60)
        {
            Debug.Log("�I�u�W�F�N�g�̍폜" + gameObject.name);
            Destroy(gameObject);
        }
    }

    // �L���[�u�𐶐����ď�����ɃC���p���X��^����
    private void SpawnAndLaunchCube(int index)
    {
        Vector3 pos = spawnPositions[index];
        GameObject cube = Instantiate(cubePrefab, pos, Quaternion.identity);
        spawnedCubes[index] = cube;

        Rigidbody rb = cube.GetComponent<Rigidbody>();
        if (rb != null)
            rb.AddForce(Vector3.up * launchForce, ForceMode.Impulse);
        else
            Debug.LogWarning("cubePrefab �� Rigidbody ������܂���I");
    }
}
