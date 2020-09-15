
using UnityEngine;
using System;

#if !UNITY_EDITOR && UNITY_WSA
using Windows.Networking;
using Windows.Networking.Connectivity;
#else

using System.Net.NetworkInformation;
using System.Net.Sockets;
#endif



namespace MrPP.Helper
{
    public class LocalInfo {


        internal static Tuple<int, int> Number2Mark(int number)
        {
            number %= 300;
            return new Tuple<int, int>(number % 3, number / 3);

        }
        internal static int Mark2Number(int mark3, int mark100)
        {
            return mark100 * 3 + mark3;
        }


#if !UNITY_EDITOR && UNITY_WSA
        public static string GetAddressIP()
        {
        
            string address = string.Empty;
            foreach (HostName hostName in NetworkInformation.GetHostNames())
            {
                if (hostName.DisplayName.Split(".".ToCharArray()).Length == 4)
                {
                    Debug.Log("Local IP " + hostName.DisplayName);
                    address =  hostName.DisplayName;
                }
            }
            return address;
        }
#else

        public static string GetAddressIP()
        {
            string address = string.Empty;
            NetworkInterface[] adapters = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < adapters.Length; i++)
            {
                if (adapters[i].Supports(NetworkInterfaceComponent.IPv4))
                {
                    UnicastIPAddressInformationCollection uniCast = adapters[i].GetIPProperties().UnicastAddresses;
                    if (uniCast.Count > 0)
                    {
                        for (int j = 0; j < uniCast.Count; j++)
                        {
                            if (uniCast[j].Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                address = uniCast[j].Address.ToString();
                            }
                        }
                    }
                }
            }

            return address;
        }
      
#endif
        public static string IP {

            get {
              
                return GetAddressIP();
            }
        }
       
        public static string ComputerName
        {

            get
            {
                string localComputerName = null;
                if (string.IsNullOrEmpty(localComputerName))
                {
#if !UNITY_EDITOR && UNITY_WSA
                    foreach (HostName hostName in NetworkInformation.GetHostNames())
                    {
                        if (hostName.Type == HostNameType.DomainName)
                        {

                            Debug.Log("My name is " + hostName.DisplayName);
                            localComputerName = hostName.DisplayName;
                        }
                    }
                    //if (string.IsNullOrEmpty(ComputerInfo.Instance.localComputerName_))
                    //{
                    //    localComputerName = "NotSureWhatMyNameIs";
                    //}
#else
                    localComputerName =  System.Environment.MachineName;
#endif
                }

                return localComputerName;

                

            }
        }


    }
}