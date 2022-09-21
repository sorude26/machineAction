using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
/// <summary>
/// メッシュを生成する
/// </summary>
public class PartsMeshCreater : MonoBehaviour
{
    [Tooltip("メッシュを生成したいオブジェクト")]
    [SerializeField] MeshFilter m_meshFilter;
    [Tooltip("メッシュの名前")]
    [SerializeField] string m_meshName = "default";
    [SerializeField] string m_saveTarget = "Assets/Mesh/";
    [SerializeField] GameObject[] m_createObjects;
    private void Start()
    {
        CreateMeshs();
    }
    /// <summary>
    /// メッシュを生成する
    /// </summary>
   public void CreateMesh()
    {
        if (m_meshName == "")
        {
            Debug.Log("ファイル名を入力してください");
            return;
        }
        if (!m_meshFilter)
        {
            Debug.Log("対象がありません");
            return;
        }
        AssetDatabase.CreateAsset(m_meshFilter.mesh, "Assets/Mesh/"+ m_meshName +".asset");
        AssetDatabase.SaveAssets();
    }
    private void CreateMeshs()
    {
        if (m_createObjects.Length <= 0)
        {
            return;
        }
        foreach (var obj in m_createObjects)
        {
            MeshFilter meshFilter = obj.GetComponent<MeshFilter>();
            if (meshFilter == null)
            {
                continue;
            }
            AssetDatabase.CreateAsset(meshFilter.mesh, m_saveTarget + obj.name + ".asset");
            AssetDatabase.SaveAssets();
        }
    }
}
#endif