using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tartarus
{
    public interface IInventorySubscriber
    {
        void RefreshInventory();
    }
}