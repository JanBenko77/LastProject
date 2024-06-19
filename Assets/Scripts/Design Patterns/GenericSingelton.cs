
using UnityEngine;

public class GenericSingelton<T> : MonoBehaviour where T : MonoBehaviour
{
        private static T _instance;
        public static T Instance{
        get{
            if(_instance == null){
                _instance = FindObjectOfType<T>();
            }
            if(_instance == null){
                GameObject TContainer = new GameObject(typeof(T).Name);
                _instance = TContainer.AddComponent<T>();
            }
            return _instance;
        }
    }
    // Start is called before the first frame update
    protected virtual void Awake()
    {
        if(_instance != null)
        {
            Destroy(gameObject);
        }
        else{
            _instance = gameObject.GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
    }
}
