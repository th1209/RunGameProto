using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 障害物などの生成ポイント.
public class SpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _prefab = null;

    void Start()
    {
        Debug.Assert(_prefab != null);
        GameObject gameObject = (GameObject)Instantiate(_prefab, _prefab.transform.position, Quaternion.identity);
        gameObject.transform.SetParent(transform, false);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(0.0f, 0.0f, 1.0f, 0.25f);
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.DrawIcon(transform.position, _prefab.name, true);
    }
}
