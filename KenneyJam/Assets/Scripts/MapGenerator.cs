using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static public class MapGenerator
{
    const int maxTryGenOther=100;
    const int maxGenOther=5;
    const float rateOfMineralRoom=0.8f;
    const float rateOfMineral=0.5f;
    const int roomMinHeight=4,roomMaxHeight=7,roomMinWidth=4,roomMaxWidth=7;
    const int interval=2;

    static private bool firstLaunch=true;
    static private Vector2 lastEndingRoomPos;

    static public void GenerateMap(int mapHeight,int mapWidth,int startingRoomWidth,ref MapCellType[,] visibleMap,ref MapCellType[,] roomMap,ref List<Vector2> roomCenterPos,int numberOfTurrets,int numberOfMonsters)
    {
        //starting room
        CopyOrGenStaringRoom(mapWidth,mapHeight,startingRoomWidth,ref visibleMap,ref roomMap,ref roomCenterPos);

        //set all cells to rock except starting room
        for(int x=startingRoomWidth;x<mapWidth+startingRoomWidth;x++)
        {
            for(int y=0;y<mapHeight;y++)
            {
                visibleMap[x,y]=MapCellType.rock;
                roomMap[x,y]=MapCellType.notRoom;
            }
        }

        //set bedrock to indicate boundaries
        for(int x=0;x<mapWidth+startingRoomWidth;x++)
            visibleMap[x,0]=visibleMap[x,mapHeight-1]=MapCellType.bedrock;
        for(int y=0;y<mapHeight;y++)
            visibleMap[0,y]=visibleMap[mapWidth+startingRoomWidth-1,y]=MapCellType.bedrock;
        
        //stronghold room
        while(true)
        {
            int x=Random.Range(mapWidth+startingRoomWidth-1-startingRoomWidth,mapWidth+startingRoomWidth-1);
            int y=Random.Range(0,mapHeight);
            if(CheckIfCanGenRoom(x,y,ref roomMap,mapWidth+startingRoomWidth,mapHeight,interval,roomMaxWidth,roomMaxHeight))
            {
                lastEndingRoomPos=GenerateRoom(x,y,ref visibleMap,ref roomMap,roomMinHeight,roomMaxHeight,roomMinWidth,roomMaxWidth,MapCellType.strongholdRoom);
                roomCenterPos.Add(lastEndingRoomPos);
                lastEndingRoomPos.x-=mapWidth-2;
                break;
            }
        }

        //monsters and turrets rooms
        int cnt=numberOfMonsters+numberOfTurrets;
        List<(int,int)> list=new List<(int, int)>();
        while(cnt>0)
        {
            int x=Random.Range(startingRoomWidth+1,mapWidth+startingRoomWidth);
            int y=Random.Range(0,mapHeight);
            if(CheckIfCanGenRoom(x,y,ref roomMap,mapWidth+startingRoomWidth,mapHeight,interval,roomMaxWidth,roomMaxHeight))
            {
                roomCenterPos.Add(GenerateRoom(x,y,ref visibleMap,ref roomMap,roomMinHeight,roomMaxHeight,roomMinWidth,roomMaxWidth,MapCellType.emptyRoom));
                list.Add((x,y));
                cnt--;
            }
        }
        int minx=2147483647;
        foreach((int x,int y) in list)
        {
            minx=Mathf.Min(minx,x);
        }
        foreach((int x,int y) in list)
        {
            if(x==minx)
            {
                numberOfTurrets--;
                ReplaceRoom(x,y,roomMaxWidth,roomMaxHeight,ref roomMap,MapCellType.turretRoom);
                list.Remove((x,y));
                break;
            }
        }
        for(int i=0;i<list.Count;i++)
        {
            int randi=Random.Range(0,list.Count);
            (int,int) temp=list[i];
            list[i]=list[randi];
            list[randi]=temp;
        }
        foreach((int x,int y) in list)
        {
            if(numberOfTurrets>0)
            {
                numberOfTurrets--;
                ReplaceRoom(x,y,roomMaxWidth,roomMaxHeight,ref roomMap,MapCellType.turretRoom);
            }
            else
            {
                ReplaceRoom(x,y,roomMaxWidth,roomMaxHeight,ref roomMap,MapCellType.monsterRoom);
            }
        }


        //empty and mineral rooms
        cnt=maxTryGenOther;
        int genOther=0;
        while(cnt-->0 && genOther<maxGenOther)
        {
            int x=Random.Range(startingRoomWidth+1,mapWidth+startingRoomWidth);
            int y=Random.Range(0,mapHeight);
            if(CheckIfCanGenRoom(x,y,ref roomMap,mapWidth+startingRoomWidth,mapHeight,interval,roomMaxWidth,roomMaxHeight))
            {
                roomCenterPos.Add(GenerateRoom(x,y,ref visibleMap,ref roomMap,roomMinHeight,roomMaxHeight,roomMinWidth,roomMaxWidth,Random.value<rateOfMineralRoom?MapCellType.mineralRoom:MapCellType.emptyRoom));
                cnt--;
                genOther++;
            }
        }
    }

    static private Vector2 GenerateRoom(int x,int y,ref MapCellType[,] visibleMap,ref MapCellType[,] roomMap,int minHeight,int maxHeight,int minWidth,int maxWidth,MapCellType type)
    {
        int width=Random.Range(minWidth,maxWidth+1);
        int offestx=Random.Range(0,maxWidth-width+1);
        Vector2 sumPos=Vector2.zero;
        int cnt=0;
        for(int i=offestx;i<offestx+width;i++)
        {
            int height=Random.Range(minHeight,maxHeight+1);
            int offesty=Random.Range(0,maxHeight-height+1);
            for(int j=offesty;j<offesty+height;j++)
            {
                sumPos+=new Vector2(x+i,y+j);
                cnt++;
                visibleMap[x+i,y+j]=MapCellType.air;
                roomMap[x+i,y+j]=type;
                if(type==MapCellType.mineralRoom && Random.value<rateOfMineral)
                {
                    visibleMap[x+i,y+j]=MapCellType.mineral;
                }
            }
        }
        return sumPos/cnt;
    }

    static private bool CheckIfCanGenRoom(int x,int y,ref MapCellType[,] roomMap,int mapWidth,int mapHeight,int interval,int maxWidth,int maxHeight)
    {
        if(x+maxWidth+interval>=mapWidth-1 || y+maxHeight+interval>=mapHeight-1 || x-interval<0+1 ||y-interval<0+1)
            return false;
        for(int i=x-interval;i<x+maxWidth+interval;i++)
        {
            for(int j=y-interval;j<y+maxHeight+interval;j++)
            {
                if(roomMap[i,j]!=MapCellType.notRoom)
                    return false;
            }
        }
        return true;
    }

    static private void ReplaceRoom(int x,int y,int maxWidth,int maxHeight,ref MapCellType[,] roomMap,MapCellType type)
    {
        for(int i=x;i<x+maxWidth;i++)
        {
            for(int j=y;j<y+maxHeight;j++)
            {
                if(roomMap[i,j]==MapCellType.emptyRoom)
                    roomMap[i,j]=type;
            }
        }
    }

    static private void CopyOrGenStaringRoom(int mapWidth,int mapHeight,int startingRoomWidth,ref MapCellType[,] visibleMap,ref MapCellType[,] roomMap,ref List<Vector2> roomCenterPos)
    {
        if(firstLaunch)
        {
            firstLaunch=false;
            for(int x=0;x<mapWidth+startingRoomWidth;x++)
            {
                for(int y=0;y<mapHeight;y++)
                {
                    visibleMap[x,y]=MapCellType.rock;
                    roomMap[x,y]=MapCellType.notRoom;
                }
            }
            while(true)
            {
                int x=Random.Range(1,startingRoomWidth-roomMaxWidth+1+1);
                int y=Random.Range(0,mapHeight);
                if(CheckIfCanGenRoom(x,y,ref roomMap,mapWidth,mapHeight,interval,roomMaxWidth,roomMaxHeight))
                {
                    roomCenterPos.Add(GenerateRoom(x,y,ref visibleMap,ref roomMap,roomMinHeight,roomMaxHeight,roomMinWidth,roomMaxWidth,MapCellType.strongholdRoom));
                    break;
                }
            }
        }
        else
        {
            int newx=1;
            int newy=0;
            int originx=mapWidth-1;
            int originy=0;
            for(int i=0;i<startingRoomWidth;i++)
            {
                for(int j=0;j<mapHeight;j++)
                {
                    visibleMap[newx+i,newy+j]=visibleMap[originx+i,originy+j];
                    roomMap[newx+i,newy+j]=roomMap[originx+i,originy+j];
                }
            }
            roomCenterPos.Add(lastEndingRoomPos);
        }
    }
}
