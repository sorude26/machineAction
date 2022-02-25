using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MapManager : MonoBehaviour
{
    public static MapManager Instance { get; private set; }
    [SerializeField] StageCreater _mapCreater;
    /// <summary> マップの全情報 </summary>
    public MapData[] MapDatas { get; private set; }
    /// <summary> 移動範囲 </summary>
    public List<MapData> MoveList { get; private set; }
    /// <summary> 攻撃範囲 </summary>
    public List<MapData> AttackList { get; private set; }
    /// <summary> ステージの最大X座標 </summary>
    [SerializeField] int _maxX = 15;
    /// <summary> ステージの最大Z座標 </summary>
    [SerializeField] int _maxZ = 15;
    [SerializeField] Vector2Int[] _sizeData = { new Vector2Int(10, 10), new Vector2Int(16, 16), new Vector2Int(20, 20) };
    /// <summary> 地形サイズ </summary>
    [SerializeField] int _mapScale = 10;
    /// <summary> ステージの最大X座標 </summary>
    public int MaxX { get => _maxX; }
    /// <summary> ステージの最大Z座標 </summary>
    public int MaxZ { get => _maxZ; }
    /// <summary> 地形サイズ </summary>
    public int MapScale { get => _mapScale; }
    public MapData this[int number]
    {
        get => MapDatas[number];
    }
    public MapData this[int x, int z]
    {
        get => MapDatas[GetPosition(x, z)];
    }
    private void Awake()
    {
        Instance = this;
        _maxX = _sizeData[0].x;
        _maxZ = _sizeData[0].y;
        MapDatas = _mapCreater.MapCreate(_maxX, _maxZ, this.transform, MapScale);
        MoveList = new List<MapData>();
        AttackList = new List<MapData>();
        _mapCreater.CityCreate(this);
    }
    /// <summary>
    /// ユニットの出現可能箇所の配列を返す
    /// </summary>
    /// <returns></returns>
    public MapData[] UnitSpownPoint()
    {
        return MapDatas.Where(p => p.MapType != MapType.NonAggressive).ToArray();
    }
    /// <summary>
    /// 2次元座標を1次元座標に変換する
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public int GetPosition(int x, int z)
    {
        return x + z * MaxX;
    }
    /// <summary>
    /// 指定座標の高度を返す
    /// </summary>
    /// <param name="x"></param>
    /// <param name="z"></param>
    /// <returns></returns>
    public float GetLevel(int x, int z)
    {
        return MapDatas[GetPosition(x, z)].Level;
    }
    /// <summary>
    /// 地形タイプごとの移動力補正を返す
    /// </summary>
    /// <param name="mapType">地形タイプ</param>
    /// <returns></returns>
    public int GetMoveCost(MapType mapType)
    {
        int point;
        switch (mapType)
        {
            case MapType.Normal:
                point = 1;
                break;
            case MapType.NonAggressive:
                point = 0;
                break;
            case MapType.Asphalt:
                point = 2;
                break;
            case MapType.Road:
                point = 2;
                break;
            case MapType.Wasteland:
                point = 3;
                break;
            case MapType.Forest:
                point = 4;
                break;
            default:
                point = 0;//０は移動不可
                break;
        }
        return point;
    }
    /// <summary>
    /// 指定箇所の十字方向を攻撃可能か調べる
    /// </summary>
    /// <param name="position"></param>
    /// <param name="attackRange"></param>
    /// <param name="startLevel"></param>
    /// <param name="verticalRang"></param>
    void SearchCross(MapData position, int attackRange, float startLevel, float verticalRang)
    {
        foreach (var map in NeighorMap(position))
        {
            SearchAttack(map, attackRange, startLevel, verticalRang);
        }
    }
    /// <summary>
    /// 指定箇所が攻撃可能であれば記録を行う
    /// </summary>
    /// <param name="position"></param>
    /// <param name="attackRange"></param>
    /// <param name="startLevel"></param>
    /// <param name="verticalRange"></param>    
    void SearchAttack(MapData position, int attackRange, float startLevel, float verticalRange)
    {
        if (position == null)//調査対象がマップ範囲内であるか確認
        {
            return;
        }
        if (GetMoveCost(position.MapType) == 0)//侵入可能か確認、０は侵入不可又は未設定
        {
            return;
        }
        if (attackRange <= position.AttackPoint)//確認済か確認
        {
            return;
        }
        if (position.Level - startLevel > verticalRange)//上方高低差確認
        {
            return;
        }
        if (startLevel - position.Level > verticalRange + verticalRange / 2)//下方高低差確認
        {
            return;
        }
        if (attackRange > 0)//攻撃可能箇所に足跡入力、再度検索
        {
            attackRange--;//攻撃範囲変動
            position.AttackPoint = attackRange;
            AttackList.Add(position);
            SearchCross(position, attackRange, startLevel, verticalRange);
        }
    }
    /// <summary>
    /// 実コスト最小のOpenマップデータを返す
    /// </summary>
    /// <returns></returns>
    MapData GetOpenMap()
    {
        MapData target = null;
        int scost = MaxX * MaxZ;
        foreach (var item in MoveList)
        {
            if (item.State != MapState.Open)
            {
                continue;
            }
            if (item.MapScore < scost)
            {
                scost = item.MapScore;
                target = item;
            }
            else if (item.MapScore == scost)
            {
                if (target.ZCost > item.ZCost)
                {
                    target = item;
                }
            }
        }
        return target;
    }
    /// <summary>
    /// 最短経路の目標地点に近い順に高得点を付ける
    /// </summary>
    /// <param name="map"></param>
    /// <returns></returns>
    bool RouteScoreSet(MapData map)
    {
        if (map == null)
        {
            return true;
        }
        if (map.State == MapState.Start)
        {
            return true;
        }
        map.MapScore = MaxX * MaxZ + map.ZCost;
        return RouteScoreSet(map.Parent);
    }

    /// <summary>
    /// 周囲四方向のマップデータ
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public MapData[] NeighorMap(MapData position)
    {
        if (position == null) { return null; }
        var mapData = new List<MapData>();
        if (position.PosZ > 0 && position.PosZ < MaxZ)
        {
            mapData.Add(MapDatas[position.PosID - MaxX]);
        }
        if (position.PosZ >= 0 && position.PosZ < MaxZ - 1)
        {
            mapData.Add(MapDatas[position.PosID + MaxX]);
        }
        if (position.PosX > 0 && position.PosX < MaxX)
        {
            mapData.Add(MapDatas[position.PosID - 1]);
        }
        if (position.PosX >= 0 && position.PosX < MaxX - 1)
        {
            mapData.Add(MapDatas[position.PosID + 1]);
        }
        return mapData.ToArray();
    }
    /// <summary>
    /// 指定領域のマップデータを返す
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="sizeX"></param>
    /// <param name="startZ"></param>
    /// <param name="sizeZ"></param>
    /// <returns></returns>
    public IEnumerable<MapData> GetArea(int startX, int sizeX, int startZ, int sizeZ)
    {
        int x = 0;
        int z = 0;
        while (x < sizeX && z < sizeZ)
        {
            if (startX + x < _maxX && startZ + z < _maxZ)
            {
                yield return this[startX + x, startZ + z];
            }
            x++;
            if (x >= sizeX)
            {
                x = 0;
                z++;
            }
        }
    }
    /// <summary>
    /// 指定領域外のマップデータを返す
    /// </summary>
    /// <param name="startX"></param>
    /// <param name="sizeX"></param>
    /// <param name="startZ"></param>
    /// <param name="sizeZ"></param>
    /// <returns></returns>
    public IEnumerable<MapData> GetOutArea(int startX, int sizeX, int startZ, int sizeZ)
    {
        int x = 0;
        int z = 0;
        while (x < MaxX && z < MaxZ)
        {
            if (x < startX || z < startZ || x >= startX + sizeX || z >= startZ + sizeZ)
            {
                yield return this[x, z];
            }
            x++;
            if (x >= MaxX)
            {
                x = 0;
                z++;
            }
        }
    }
}
