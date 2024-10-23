using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public partial class Game : MonoBehaviour
{
    [SerializeField] private int roomsAmountHorizontal;
    [SerializeField] private int roomsAmountVertical;
    [SerializeField] private Vector2 roomSize;
    [SerializeField] private Vector2 roomOffset;

    private PlayerMovement playerMovement;
    private Room[,] rooms;
    private Room currentRoom;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        BuildRooms();

        RegisterSpawners();

        currentRoom = GetRoomWithWorldPosition(playerMovement.transform.position);
        if(currentRoom != null)
        {
            currentRoom.Activate(true);
        }
    }

    private void BuildRooms()
    {
        rooms = new Room[roomsAmountVertical, roomsAmountHorizontal];

        for(int row = 0; row < rooms.GetLength(0); row++)
        {
            for(int column = 0; column < rooms.GetLength(1); column++)
            {
                Vector2Int identifier = new Vector2Int(column, row);

                Vector2 centerOfRoom = new Vector2(column * roomSize.x, row * roomSize.y);

                centerOfRoom += roomOffset;

                rooms[row, column] = new Room(identifier, centerOfRoom, roomSize);
            }
        }
    }

    public void ActivateRoom(Vector2 position)
    {
        Room activeRoom = GetRoomWithWorldPosition(position);

        if(activeRoom != null)
        {
            currentRoom = activeRoom;
            currentRoom.Activate(true);
        }
    }

    public void DeactivateCurrentRoom()
    {
        if(currentRoom != null)
        {
            currentRoom.Activate(false);
        }
    }

    private void RegisterSpawner(EnemySpawner spawner)
    {
        foreach(Room room in rooms)
        {
            room.RegisterIfInside(spawner);
        }
    }

    private Room GetRoomWithWorldPosition(Vector2 position)
    {
        Room room = null;

        foreach(Room r in rooms)
        {
            if (r.IsPositionInside(position))
            {
                room = r;
                break;
            }
        }

        return room;
    }

    private void RegisterSpawners()
    {
        EnemySpawner[] spawners = FindObjectsOfType<EnemySpawner>();

        foreach(EnemySpawner spawner in spawners)
        {
            RegisterSpawner(spawner);
        }
    }
    private void OnDrawGizmos()
    {
        if (rooms == null) return;

        foreach(Room room in rooms)
        {
            Gizmos.DrawWireCube(room.bounds.center, room.bounds.size);
            if (Application.isEditor)
            {
                Handles.Label(room.bounds.center, room.identifier.ToString());
            }
        }
    }

    public void TeleportPlayerToPortal(Portal portal)
    {

    }
    void Update()
    {
        
    }
}
