using GDGeek;
using System;
using System.Collections;
using UnityEngine;
using Mirror;

namespace MrPP.Myth
{
    public class Bridge : NetworkBehaviour
    {
        private static Bridge Instance_;
        public static Bridge Instance
        {
            get
            {
                return Instance_;
            }
        }

        private void Start()
        {

            if (isLocalPlayer)
            {
                Instance_ = this;
            }
        }

        private void broadcast(string handle, string evt, object data)
        {
           // Debug.LogError(evt);
           // Debug.LogError(data);
            BridgeBroadcast.Instance.broadcast(handle, evt, data);
        }
        [Command]
        private void CmdPostAsgardPose(string handle, string evt, MrPP.Myth.Yggdrasil.AsgardPose data)
        {
            broadcast(handle, evt, (object)data);
        }



        public void post(string handle, string evt, MrPP.Myth.Yggdrasil.AsgardPose data)
        {
            if (isClient)
            {
                  
                CmdPostAsgardPose(handle, evt, data);
            }
            else
            {
                broadcast(handle, evt, (object)data);
            }
        }

      
        [Command]
        private void CmdPost(string handle, string evt)
        {
            broadcast(handle, evt, (object)null);
        }

        public void post(string handle, string evt)
        {
            if (isClient)
            {

                CmdPost(handle, evt);
            }
            else
            {
                broadcast(handle, evt, (object)null);
            }
        }

        [Command]
        private void CmdPostInt(string handle, string evt, int data)
        {
            broadcast(handle, evt, (object)data);
        }

        public void post(string handle, string evt, int data)
        {
            if (isClient)
            {

                CmdPostInt(handle, evt, data);
            }
            else
            {
                broadcast(handle, evt, (object)data);
            }
        }



        [Command]
        private void CmdPostBool(string handle, string evt, bool data)
        {
            broadcast(handle, evt, (object)data);
        }

        public void post(string handle, string evt, bool data)
        {
            if (isClient)
            {

                CmdPostBool(handle, evt, data);
            }
            else
            {
                broadcast(handle, evt, (object)data);
            }
        }




        [Command]
        private void CmdPostString(string handle, string evt, string data)
        {
            broadcast(handle, evt, (object)data);
        }

        public void post(string handle, string evt, string data)
        {
            if (isClient)
            {

                CmdPostString(handle, evt, data);
            }
            else
            {
                broadcast(handle, evt, (object)data);
            }
        }


        [Command]
        private void CmdPostUint(string handle, string evt, uint data)
        {
            broadcast(handle, evt, (object)data);
        }
       
        public void post(string handle, string evt, uint data)
        {
            if (isClient)
            {

                CmdPostUint(handle, evt, data);
            }
            else
            {
                broadcast(handle, evt, (object)data);
            }
        }
    }
}