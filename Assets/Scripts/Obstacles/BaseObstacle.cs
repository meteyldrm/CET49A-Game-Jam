namespace Obstacles {
    public interface IBaseObstacle {
        void setInitialState(int state);
        void setStateAfterTime(int state, float time);
        void setCorrectChoice(int choice);
        bool makeChoice(int choice);
    }
}