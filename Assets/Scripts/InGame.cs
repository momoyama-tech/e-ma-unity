using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject _creatorController;

    void Start()
    {
        // CreatorControllerのManualStartメソッドを呼び出す
        if (_creatorController != null)
        {
            _creatorController.GetComponent<CreateController>().ManualStart();
        }
        else
        {
            Debug.LogError("CreatorController is not assigned.");
        }
    }

    void Update()
    {
        // CreatorControllerのManualUpdateメソッドを呼び出す
        if (_creatorController != null)
        {
            _creatorController.GetComponent<CreateController>().ManualUpdate();
        }
        else
        {
            Debug.LogError("CreatorController is not assigned.");
        }
    }
}