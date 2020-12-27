public interface IMonsterAction {
    public delegate void MonsterAction(); // MonsterActionArgs args

    public delegate void MonsterHealthAction(int health);
}