[System.Serializable]
public class Action {
    public static int EMPTY = -1;
    public static int KEYDOWN_RIGHT = 0;
    public static int KEYDOWN_LEFT = 1;
    public static int KEYDOWN_UP = 2;
    public static int KEYUP_RIGHT = 3;
    public static int KEYUP_LEFT = 4;
    public static int KEYUP_UP = 5;

    public int frame;
    public int action;
    
    public Action(int frame, int action)
    {
        this.frame = frame;
        this.action = action;
    }

    public int GetFrame()
    {
        return frame;
    }

    public int GetAction()
    {
        return action;
    }
}
