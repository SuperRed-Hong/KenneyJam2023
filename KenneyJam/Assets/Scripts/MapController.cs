using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapController : MonoBehaviour
{
    [SerializeField]
    int mapHeight,mapWidth;
    [SerializeField]
    int startingRoomWidth;
    [SerializeField]
    int numberOfTurrets,numberOfMonsters;
    [SerializeField]
    Tilemap visibleTilemap,fogTilemap,backgroundTilemap;
    [System.Serializable]
    class BlockParm
    {
        public MapCellType type;
        public List<TileBase> tile;
    }
    [SerializeField]
    BlockParm[] blockParm;
    [SerializeField]
    TileBase fogTile,backgroundTile;
    [SerializeField]
    List<TileBase> mineralRockTile0, mineralRockTile1,mineralRockTile2;
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject turretPrefab;

    private MapCellType[,] visibleMap;
    private MapCellType[,] roomMap;
    private Dictionary<MapCellType,BlockParm> blockParmDic;
    private int originx,originy;
    private List<Vector2> roomCenterPos;
    private int rockTileId;

    void Start()
    {
        blockParmDic=new Dictionary<MapCellType, BlockParm>();
        foreach(var i in blockParm)
            blockParmDic.Add(i.type,i);
        roomCenterPos=new List<Vector2>();

        visibleMap=new MapCellType[mapWidth+startingRoomWidth,mapHeight];
        roomMap=new MapCellType[mapWidth+startingRoomWidth,mapHeight];

        originx=-mapWidth+2;
        originy=-mapHeight/2;

        Invoke("GenerateMap",1f);
    }

    public Vector2 GenerateMap()
    {
        roomCenterPos.Clear();
        originx+=mapWidth-2;
        MapGenerator.GenerateMap(mapHeight,mapWidth,startingRoomWidth,ref visibleMap,ref roomMap,ref roomCenterPos,numberOfTurrets,numberOfMonsters);
        CreateMap();
        for(int i=0;i<roomCenterPos.Count;i++)
        {
            roomCenterPos[i]+=new Vector2(originx,originy);
        }
        GenerateObjects();
        return roomCenterPos[0];
    }

    private void CreateMap()
    {
        rockTileId=Random.Range(0,blockParmDic[MapCellType.rock].tile.Count);
        for(int x=0;x<mapWidth+startingRoomWidth;x++)
        {
            for(int y=0;y<mapHeight;y++)
            {
                CreateCell(x+originx,y+originy,visibleMap[x,y]);
            }
        }
        for(int x=0-100;x<mapWidth+startingRoomWidth+100;x++)
        {
            for(int y=0-100;y<mapHeight+100;y++)
            {
                CreateFog(x+originx,y+originy);
            }
        }
    }

    public MapCellType GetBlockType(Vector3 worldPos)
    {
        Vector3Int tilePos=visibleTilemap.WorldToCell(worldPos);
        if(tilePos.x-originx<0 || tilePos.x-originx>=mapWidth+startingRoomWidth || tilePos.y-originy<0 || tilePos.y-originy>=mapHeight)
            return MapCellType.outOfRange;
        return visibleMap[tilePos.x-originx,tilePos.y-originy];
    }

    public MapCellType GetRoomType(Vector3 worldPos)
    {
        Vector3Int tilePos=visibleTilemap.WorldToCell(worldPos);
        if(tilePos.x-originx<0 || tilePos.x-originx>=mapWidth+startingRoomWidth || tilePos.y-originy<0 || tilePos.y-originy>=mapHeight)
            return MapCellType.outOfRange;
        return roomMap[tilePos.x-originx,tilePos.y-originy];
    }

    private void CreateCell(int x,int y,MapCellType type)
    {
        if(type==MapCellType.rock || type==MapCellType.mineral1 || type==MapCellType.mineral2 || type==MapCellType.mineral3)
            visibleTilemap.SetTile(new Vector3Int(x,y,0),blockParmDic[type].tile[rockTileId]);
        else
            visibleTilemap.SetTile(new Vector3Int(x,y,0),blockParmDic[type].tile[Random.Range(0,blockParmDic[type].tile.Count)]);
        backgroundTilemap.SetTile(new Vector3Int(x,y,0),backgroundTile);
    }

    public void DestroyCell(Vector3 worldPos)
    {
        Vector3Int tilePos=visibleTilemap.WorldToCell(worldPos);
        if(tilePos.x-originx<0 || tilePos.x-originx>=mapWidth+startingRoomWidth || tilePos.y-originy<0 || tilePos.y-originy>=mapHeight)
            return;
        visibleTilemap.SetTile(tilePos,blockParmDic[MapCellType.air].tile[0]);
        visibleMap[tilePos.x-originx,tilePos.y-originy]=MapCellType.air;
        CleanFog(tilePos.x,tilePos.y);
    }

    private void CreateFog(int x,int y)
    {
        fogTilemap.SetTile(new Vector3Int(x,y,0),fogTile);
    }

    private void CleanFog(int x,int y)
    {
        Queue<(int,int)> q=new Queue<(int, int)>();
        q.Enqueue((x,y));
        if(x-originx<0 || x-originx>=mapWidth+startingRoomWidth || y-originy<0 || y-originy>=mapHeight)
            return;
        fogTilemap.SetTile(new Vector3Int(x,y,0),null);
        while(q.Count!=0)
        {
            (x,y)=q.Dequeue();
            int[] dx={0,0,1,-1};
            int[] dy={1,-1,0,0};
            for(int i=0;i<4;i++)
            {
                if(x+dx[i]-originx<0 || x+dx[i]-originx>=mapWidth+startingRoomWidth || y+dy[i]-originy<0 || y+dy[i]-originy>=mapHeight)
                    continue;
                if(roomMap[x+dx[i]-originx,y+dy[i]-originy]!=MapCellType.notRoom && fogTilemap.GetTile(new Vector3Int(x+dx[i],y+dy[i],0))!=null)
                    q.Enqueue((x+dx[i],y+dy[i]));
                fogTilemap.SetTile(new Vector3Int(x+dx[i],y+dy[i],0),null);
            }
        }
    }

    public List<Vector2> GetNearRoomDirection(Vector2 now,float minRadius,float maxRadius)
    {
        List<Vector2> list=new List<Vector2>();
        foreach(var i in roomCenterPos)
        {
            if((i-now).magnitude<maxRadius && (i-now).magnitude>minRadius)
            {
                list.Add(i-now);
            }
        }
        return list;
    }

    public List<Vector2> GetNearRoomDirection(float x,float y,float minRadius,float maxRadius)
    {
        Vector2 now=new Vector2(x,y);
        return GetNearRoomDirection(now,minRadius,maxRadius);
    }

    private void GenerateObjects()
    {
        foreach(var i in roomCenterPos)
        {
            if(roomMap[visibleTilemap.WorldToCell(i).x-originx,visibleTilemap.WorldToCell(i).y-originy]==MapCellType.monsterRoom)
            {
                gameManager.CreateEnemy(i);
            }
            else if(roomMap[visibleTilemap.WorldToCell(i).x-originx,visibleTilemap.WorldToCell(i).y-originy]==MapCellType.turretRoom)
            {
                Transform.Instantiate(turretPrefab,i,turretPrefab.transform.rotation).GetComponent<TowerController>().level=Random.Range(1,4);
            }
        }
    }
}
