namespace Obstacles {
    public interface IBaseObstacle {
        void setCorrectChoice(int choice);
        bool makeChoice(int choice);
    }
}