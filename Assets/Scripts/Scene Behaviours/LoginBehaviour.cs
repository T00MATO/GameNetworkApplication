using GNClientLib;
using GNPacketLib;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginBehaviour : SceneBehaviour
{
    [Header("External References")]

    [SerializeField]
    private TMP_InputField _usernameField;

    [SerializeField]
    private Button _loginButton;

    private void Awake()
    {
        BeginPacketReceive();
    }

    private void Start()
    {
        ConnectToServer(Config.SERVER_IP, Config.SERVER_PORT);
    }

    public override void OnPacketReceived(GNPacket packet)
    {
        switch (packet)
        {
            case GNP_Connect p:
                OnConnected(p);
                break;
            default:
                Debug.LogError($"Can not process packet: {packet}");
                break;
        }
    }

    private void OnConnected(GNP_Connect p)
    {
        _usernameField.interactable = true;
        _loginButton.interactable = true;

        Debug.Log("Successfully connected to server.");
    }

    public void Login()
    {
        if (_usernameField.text == string.Empty)
            return;

        _usernameField.interactable = false;
        _loginButton.interactable = false;

        var packet = new GNP_Login(_usernameField.text);

        SendAndReceive<GNP_LoginRes>(packet, response =>
        {
            if (response.Result != GNP_LoginRes.RESULTS.SUCCESS)
            {
                Debug.LogWarning("Fail to login.");
                return;
            }
            
            Oneself.LoginSuccess(response.Uid, response.Username);

            EndPacketReceive();

            SceneManager.LoadScene(Consts.SCENE_LOBBY);

            Debug.Log("Successfully logined.");
        });
    }
}
