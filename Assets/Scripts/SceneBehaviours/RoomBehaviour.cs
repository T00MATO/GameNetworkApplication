using GNClientLib;
using GNPacketLib;
using UnityEngine;

public class RoomBehaviour : NetworkBehaviour
{
    private void Awake()
    {
        BeginPacketReceive();
    }

    private void Start()
    {
        SendPacket(new GNP_UserJoin());
    }

    public override void OnPacketReceived(GNPacket packet)
    {
        switch (packet)
        {
            case GNP_UserJoinRes p:
                OnUserJoinResult(p);
                break;
            case GNP_UserExitRes p:
                OnUserExitResult(p);
                break;
            case GNP_RoomInfo p:
                OnRoomInfo(p);
                break;
            default:
                Debug.LogError($"Can not process packet: {packet}");
                break;
        }
    }

    private void OnUserJoinResult(GNP_UserJoinRes p)
    {
        if (p.Result != GNP_UserJoinRes.RESULTS.SUCCESS)
            Debug.LogWarning("Fail to join room.");

        Debug.Log("Room joined.");
    }

    private void OnUserExitResult(GNP_UserExitRes p)
    {
        Debug.Log("Room exited.");

        EndPacketReceive();
        
        ExtendedManager.LoadScene(SceneKey.LOBBY);
    }

    private void OnRoomInfo(GNP_RoomInfo p)
    {
        var yourName = string.Empty;
        var opponentName = string.Empty;
        for (var idx = 0; idx < GameSetting.MAX_USERS; idx++)
        {
            if (p.Uids[idx] == Oneself.Uid)
                yourName = p.Usernames[idx];
            else
                opponentName = p.Usernames[idx];
        }

        Debug.Log($"{yourName}(You) and {opponentName}(Opponent) are preparing for a match.");
    }
}
