using UnityEngine;

public class DisplayManager : MonoBehaviour
{
    [SerializeField] private GameObject projectorCameras; // ProjectorCamerasオブジェクトをアサイン

    void Start()
    {
        // 接続されているディスプレイの数を取得
        int displayCount = Display.displays.Length;

        // すべてのディスプレイを有効化
        for (int i = 1; i < displayCount; i++)
        {
            Display.displays[i].Activate();
        }

        Debug.Log($"Displays Activated: {displayCount}");

        // ProjectorCameras内のカメラを処理
        if (projectorCameras != null)
        {
            Camera[] cameras = projectorCameras.GetComponentsInChildren<Camera>();

            // ディスプレイ数とカメラ数を比較
            if (cameras.Length > displayCount)
            {
                Debug.LogWarning("There are more cameras than displays. Some cameras will not be assigned to a display.");
            }

            // 各カメラを対応するディスプレイに割り当て
            for (int i = 0; i < Mathf.Min(cameras.Length, displayCount); i++)
            {
                cameras[i].targetDisplay = i; // カメラをディスプレイに割り当て
                Debug.Log($"Camera {cameras[i].name} assigned to Display {i}");
            }
        }
        else
        {
            Debug.LogWarning("ProjectorCameras object is not assigned.");
        }
    }
}