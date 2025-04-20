using UnityEngine;

public class InGame : MonoBehaviour
{
    [SerializeField] private GameObject _creatorController;
    [SerializeField] private GameObject _effectCreatorController;
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

        if (_lightController != null)
        {
            _lightController.GetComponent<LightController>().ManualStart();
        }
        else
        {
            Debug.LogError("LightController is not assigned.");
        }

        // EffectCreateControllerのManualStartメソッドを呼び出す
        if (_effectCreatorController != null)
        {
            _effectCreatorController.GetComponent<EffectCreateController>().ManualStart();
        }
        else
        {
            Debug.LogError("EffectCreateController is not assigned.");
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

        // EffectCreateControllerのManualUpdateメソッドを呼び出す
        if (_effectCreatorController != null)
        {
            _effectCreatorController.GetComponent<EffectCreateController>().ManualUpdate();
        }
        else
        {
            Debug.LogError("EffectCreateController is not assigned.");
        }
    }
}