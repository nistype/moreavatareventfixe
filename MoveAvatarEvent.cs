using Plus.HabboHotel.Items;
using Plus.HabboHotel.Rooms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Plus.Communication.Packets.Incoming.Rooms.Engine
{
    internal class MoveAvatarEvent : IPacketEvent
    {
        public void Parse(HabboHotel.GameClients.GameClient Session, ClientPacket Packet)
        {
            if (Session == null || Session.GetHabbo() == null)
            {
                return;
            }

            if (!Session.GetHabbo().InRoom)
            {
                return;
            }

            Room Room = Session.GetHabbo().CurrentRoom;
            if (Room == null)
            {
                return;
            }

            RoomUser User = Room.GetRoomUserManager().GetRoomUserByHabbo(Session.GetHabbo().Id);

            if (User == null || !User.CanWalk)
            {
                return;
            }

            int MoveX = Packet.PopInt();
            int MoveY = Packet.PopInt();

            if (User.RidingHorse)
            {
                RoomUser Horse = Room.GetRoomUserManager().GetRoomUserByVirtualId(User.HorseID);
                if (Horse != null)
                {
                    Horse.MoveTo(MoveX, MoveY);
                }
            }

            if (User.isLying)
            {
                User.Z += 0.35;
                User.RemoveStatus("lay");
                User.isLying = false;
                User.UpdateNeeded = true;
            }

            if (User.isSitting)
            {
                User.Z += 0.35;
                User.RemoveStatus("sit");
                User.isSitting = false;
                User.UpdateNeeded = true;
            }

            User.MoveTo(MoveX, MoveY);
        }
    }
};

