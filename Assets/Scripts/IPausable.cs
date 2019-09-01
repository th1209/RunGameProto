using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ポーズ機能による停止・再開が可能であることを表すインタフェース.
public interface IPausable 
{
    void Pause();

    void Resume();
}
