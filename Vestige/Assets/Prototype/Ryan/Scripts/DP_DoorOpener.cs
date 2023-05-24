using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DP
{
    public class DP_DoorOpener : MonoBehaviour
    {
        public string Message = "Press F to open the door";
        public string FailMessage = "You don't have the key to open the door";
        public string SuccessMessage = "Press F to open the door";
        public Animator DoorAnim;
        public GameObject Door;

    }
}
