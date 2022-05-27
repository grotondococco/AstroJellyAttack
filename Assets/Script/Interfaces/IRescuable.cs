using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IRescuable
{
    void ModifyCollidersForSave();
    void ResetOriginalColliders();
}
