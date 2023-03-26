using UnityEngine;

public class OneselfBehaviour : MonoBehaviour
{
    private static OneselfBehaviour _instance;
    public static OneselfBehaviour Instance
    {
        get
        {
            if (_instance == null)
            {
                var gameObject = new GameObject("Oneself");
                DontDestroyOnLoad(gameObject);

                var instance = gameObject.AddComponent<OneselfBehaviour>();
                _instance = instance;
            }

            lock (_instance)
                return _instance;
        }
    }

    [Header("Properties")]
    public ulong Uid;
    public string Username;
}