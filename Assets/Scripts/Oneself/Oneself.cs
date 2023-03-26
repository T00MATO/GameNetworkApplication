public static class Oneself
{
    private static OneselfBehaviour _player => OneselfBehaviour.Instance;

    public static void LoginSuccess(ulong uid, string username)
    {
        _player.Uid = uid;
        _player.Username = username;
    }

    public static ulong Uid => _player.Uid;
    public static string Username => _player.Username;
}
