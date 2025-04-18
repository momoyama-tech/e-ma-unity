using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject _creatorController;
    private GameObject _lightController;

    void Start()
    {
        _lightController = gameObject.GetComponent<LightController>().gameObject;
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
        // LightControllerのManualUpdateメソッドを呼び出す
        if (_lightController != null)
        {
            _lightController.GetComponent<LightController>().ManualUpdate();
        }
        else
        {
            Debug.LogError("LightController is not assigned.");
        }
    }
}