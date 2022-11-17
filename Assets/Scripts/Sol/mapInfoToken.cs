using UnityEngine;
using System.Collections;
using Photon.Bolt;

public class MapInfoToken : IProtocolToken
{
    public byte[] mapInfos;
    int cnt;

    public MapInfoToken()
    {
        cnt = 0;
        mapInfos = new byte[5000];
    }

    public void add(int x)
    {
        mapInfos[cnt] = (byte)x;
        ++cnt;
    }

    public void Write(UdpKit.UdpPacket packet)
    {
        packet.WriteByteArray(mapInfos);
        //바이트 2개 붙이는거.
    }

    public void Read(UdpKit.UdpPacket packet)
    {
        mapInfos = packet.ReadByteArray(5000);
    }
}
