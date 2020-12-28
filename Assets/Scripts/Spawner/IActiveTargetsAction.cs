using System.Collections.Generic;

public interface IActiveTargetsAction {
    public delegate void QueueAction(Queue<Monster> monsterQueue);
}