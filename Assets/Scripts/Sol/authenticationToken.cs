using UnityEngine;
using System.Collections;
using Photon.Bolt;

public class authenticationToken : IProtocolToken
{
    public string password;
    public void Write(UdpKit.UdpPacket packet)
    {
        packet.WriteString(password);
        //바이트 2개 붙이는거.
    }

    public void Read(UdpKit.UdpPacket packet)
    {
        password = packet.ReadString();
    }
}
