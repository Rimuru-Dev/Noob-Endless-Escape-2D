// using UnityEngine;
//
// public class SaveLoadHandler : MonoBehaviour
// {
//     // private List<IFuckingSaveLoad> savedInterfaces = new();
//     //
//     // private void OnEnable()
//     // {
//     //     SceneManager.sceneLoaded += OnSceneLoaded;
//     // }
//     //
//     // private void OnDisable()
//     // {
//     //     SceneManager.sceneLoaded -= OnSceneLoaded;
//     // }
//     //
//     // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
//     // {
//     //     savedInterfaces.Clear();
//     //
//     //     var f = GameObject.FindObjectsOfType<GameObject>(true);
//     //
//     //     foreach (var c in f.Where(x => x != null))
//     //     {
//     //         var s = c.GetComponent<IFuckingSaveLoad>();
//     //         if (s != null)
//     //         {
//     //             savedInterfaces.Add(s);
//     //         }
//     //     }
//     //
//     //     Debug.Log("<color=green>Cache</color>");
//     // }
//     //
//     // private void Start()
//     // {
//     //     StartCoroutine(AutoSave());
//     // }
//     //
//     // private IEnumerator AutoSave()
//     // {
//     //     while (true)
//     //     {
//     //         yield return new WaitForSeconds(7f);
//     //
//     //         foreach (var obj in savedInterfaces)
//     //         {
//     //             obj?.Save();
//     //             Debug.Log("<color=magenta>Save</color>");
//     //         }
//     //
//     //         YandexGame.SaveProgress();
//     //     }
//     // }
// }