using UnityEngine;
using System.Collections;
using Photon.Bolt;

public class authenticationToken : IProtocolToken
{
    public string password;
    public void Write(UdpKit.UdpPacket packet)
    {
        packet.WriteString(password);
        //����Ʈ 2�� ���̴°�.
    }

    public void Read(UdpKit.UdpPacket packet)
    {
        password = packet.ReadString();
    }
}
