using GNClientLib;
using GNPacketLib;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyBehaviour : SceneBehaviour
{
    [Header("External References")]

    [SerializeField]
    private Button _matchButton;

    [SerializeField]
    private TMP_Text _matchButtonText;

    [Header("Properties")]

    [SerializeField]
    private GNP_MatchRes.RESULTS _recentMatchResult;

    private void Awake()
    {
        _matchButtonText.text = Consts.TEXT_MATCH_START;

        BeginPacketReceive();
    }

    public override void OnPacketReceived(GNPacket packet)
    {
        switch (packet)
        {
            case GNP_MatchRes p:
                OnMatchResult(p);
                break;
            case GNP_RoomCreate p:
                OnRoomCreate(p);
                break;
            default:
                Debug.LogError($"Can not process packet: {packet}");
                break;
        }
    }

    public void Match()
    {
        _matchButton.interactable = false;

        var request = GNP_Match.REQUESTS.NONE;
        if (_recentMatchResult == GNP_MatchRes.RESULTS.START)
            request = GNP_Match.REQUESTS.CANCEL;
        else
            request = GNP_Match.REQUESTS.START;

        SendPacket(new GNP_Match(request));
    }

    private void OnMatchResult(GNP_MatchRes p)
    {
        _recentMatchResult = p.Result;

        switch (_recentMatchResult)
        {
            case GNP_MatchRes.RESULTS.START:
                _matchButton.interactable = true;
                _matchButtonText.text = Consts.TEXT_MATCH_WAITING;

                Debug.Log("Match making is started.");
                break;
            case GNP_MatchRes.RESULTS.CANCEL:
                _matchButton.interactable = true;
                _matchButtonText.text = Consts.TEXT_MATCH_START;

                Debug.Log("Match making is canceled.");
                break;
            case GNP_MatchRes.RESULTS.SUCCESS:
                _matchButton.interactable = false;
                _matchButtonText.text = Consts.TEXT_MATCH_SUCCEED;

                Debug.Log("Match making is succeed.");
                break;
            default:
                Debug.LogError($"Can not process match case: {_recentMatchResult}");
                break;
        }
    }

    private void OnRoomCreate(GNP_RoomCreate p)
    {
        EndPacketReceive();

        SceneManager.LoadScene(Consts.SCENE_ROOM);
    }
}
