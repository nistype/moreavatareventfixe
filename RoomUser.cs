// Replace only the Void MoveTo part (int pX, int py, bool pOverride) with:

public void MoveTo(int pX, int pY, bool pOverride)
        {
            if (TeleportEnabled)
            {
                UnIdle();
                GetRoom().SendPacket(GetRoom().GetRoomItemHandler().UpdateUserOnRoller(this, new Point(pX, pY), 0, GetRoom().GetGameMap().SqAbsoluteHeight(GoalX, GoalY)));
                if (Status.ContainsKey("sit"))
                {
                    Z -= 0.35;
                }

                UpdateNeeded = true;
                return;
            }

            if ((!GetRoom().GetGameMap().IsValidMovement(pX, pY) && !AllowOverride) || Frozen)
            {
                return;
            }

            UnIdle();
            IsWalking = true;
            GoalX = pX;
            GoalY = pY;
            PathRecalcNeeded = true;
            FreezeInteracting = false;
        }
