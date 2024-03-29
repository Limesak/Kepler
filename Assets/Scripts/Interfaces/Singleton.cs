using UnityEngine;

namespace AsteroidBelt.Interfaces
{
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance{ 
            get{
                if(_instance == null){
                    GameObject obj = new GameObject();
                    obj.name = typeof(Main).Name;
                    //obj.hideFlags = HideFlags.DontSave;
                    _instance = obj.AddComponent<T>();
                }
                return _instance;
            }
        }

        private void OnDestroy(){
            if(_instance == this){
                _instance = null;
            }
        }
    }

    public class SingletonPersistent<T> : MonoBehaviour where T : Component
    {
        public static T _instance;
        public static T Instance{ 
            get{
                /*if(_instance == null){
                GameObject obj = new GameObject();
                obj.name = typeof(Main).Name;
                obj.hideFlags = HideFlags.DontSave;
                _instance = obj.AddComponent<T>();
            }*/
                return _instance;
            }
        }

        private void OnDestroy(){
            if(_instance == this){
                _instance = null;
            }
        }

        public virtual void Awake(){
            if(_instance == null){
                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
            else{
                Destroy(this.gameObject);
            }
        }
    }
}