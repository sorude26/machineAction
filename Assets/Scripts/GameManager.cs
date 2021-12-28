using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField]
    UnitPartsList _partsList;
    public UnitPartsList PartsList { get => _partsList; }
    [SerializeField]
    ColorData _colorData;
    [SerializeField]
    int[] _sParts;
    [SerializeField]
    GameDataManager _dataManager;
    [SerializeField]
    UnitBuildData _buildData = default;
    public UnitBuildData CurrentBuildData { get => _buildData; }
    public Color GetColor(int colorNum) => _colorData.GetColor(colorNum);
    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        UnitDataMaster.StartSet(_partsList);
    }

    void SetAllParts()
    {
        for (int i = 0; i < 6; i++)
        {
            var parts = (PartsType)i;
            for (int x = 0; x < UnitDataMaster.HavePartsDic[parts].Length; x++)
            {
                UnitDataMaster.HavePartsDic[parts][x]++;
            }
        }
    }
    void SetSParts()
    {
        UnitDataMaster.HavePartsDic[PartsType.Weapon][0]++;
        for (int i = 0; i < 6; i++)
        {
            var parts = (PartsType)i;
            UnitDataMaster.HavePartsDic[parts][_sParts[i]]++;
        }
        for (int i = 0; i < UnitDataMaster.MaxUintCount; i++)
        {
            //UnitDataMaster.SetData(i, new UnitBuildData(_sParts[1], _sParts[0], _sParts[2], _sParts[3], _sParts[4], _sParts[5], 0), 22);
        }
    }
    public void Save()
    {
        _dataManager.SaveData();
    }
    public void Load()
    {
        _dataManager.LoadData();
    }
}
