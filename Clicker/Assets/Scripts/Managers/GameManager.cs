using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private User _user;

    public User User => _user;

    public void SetUser(User user)
    {
        _user = user;
    }

}
