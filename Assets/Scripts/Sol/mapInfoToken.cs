using UnityEngine;
using System.Collections;
using Photon.Bolt;

public class MapInfoToken : IProtocolToken
{
    public byte[] mapInfos;
    short cnt;

    public MapInfoToken()
    {
        cnt = 0;
        mapInfos = new byte[10];
    }

    public MapInfoToken(short size)
    {
        cnt = 0;
        mapInfos = new byte[size];
    }

    public void AddTwoByte(ushort number)
    {
        byte front = (byte)(number >> 8);
        byte back = (byte)(number & 0x00FF);
        mapInfos[cnt++] = front;
        mapInfos[cnt++] = back;
    }

    public void Write(UdpKit.UdpPacket packet)
    {
        packet.WriteByteArray(mapInfos);
        //바이트 2개 붙이는거.
    }

    public void Read(UdpKit.UdpPacket packet)
    {
        mapInfos = packet.ReadByteArray(10);
    }
}
