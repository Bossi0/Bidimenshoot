namespace Photon.Pun
{
    using UnityEngine;
    using Photon.Realtime;
    using ExitGames.Client.Photon;


 
    internal static class CustomTypes
    {
        
        internal static void Register()
        {
            PhotonPeer.RegisterType(typeof(Player), (byte) 'P', SerializePhotonPlayer, DeserializePhotonPlayer);
        }


        #region Custom De/Serializer Methods

        public static readonly byte[] memPlayer = new byte[4];

        private static short SerializePhotonPlayer(StreamBuffer outStream, object customobject)
        {
            int ID = ((Player) customobject).ActorNumber;

            lock (memPlayer)
            {
                byte[] bytes = memPlayer;
                int off = 0;
                Protocol.Serialize(ID, bytes, ref off);
                outStream.Write(bytes, 0, 4);
                return 4;
            }
        }

        private static object DeserializePhotonPlayer(StreamBuffer inStream, short length)
        {
            if (length != 4)
            {
                return null;
            }

            int ID;
            lock (memPlayer)
            {
                inStream.Read(memPlayer, 0, length);
                int off = 0;
                Protocol.Deserialize(out ID, memPlayer, ref off);
            }

            if (PhotonNetwork.CurrentRoom != null)
            {
                Player player = PhotonNetwork.CurrentRoom.GetPlayer(ID);
                return player;
            }
            return null;
        }

        #endregion
    }
}