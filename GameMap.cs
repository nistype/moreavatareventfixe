// Replace only the public part bool SquareHasUsers (int X, int Y) with:

public bool SquareHasUsers(int X, int Y)
        {
            return _userMap.ContainsKey(new Point(X, Y));
        }
