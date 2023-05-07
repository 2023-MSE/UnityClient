using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SigninupResult
{
    SUCCESS = 0,
    NETWORK_ERROR = 1,
    INVALID_ID,
    INVALID_PASSWD,
    INVALID_NICKNAME,
    ID_DOUBLE_CHECK_SUCCESS,
    NICKNAME_DOUBLE_CHECK_SUCCESS
}
